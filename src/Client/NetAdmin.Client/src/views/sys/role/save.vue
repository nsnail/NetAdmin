<template>
    <el-dialog
        v-model="visible"
        :title="titleMap[mode]"
        :width="800"
        destroy-on-close
        @closed="$emit('closed')"
    >
        <el-tabs tab-position="top">
            <el-tab-pane label="基本信息">
                <el-form
                    ref="dialogForm"
                    :disabled="mode === 'view'"
                    :model="form"
                    :rules="rules"
                    label-position="left"
                    label-width="100px"
                >
                    <el-form-item label="角色名称" prop="name">
                        <el-input v-model="form.name" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="排序" prop="sort">
                        <el-input-number
                            v-model="form.sort"
                            :min="0"
                            controls-position="right"
                            style="width: 100%"
                        ></el-input-number>
                    </el-form-item>
                    <el-form-item
                        label="无限权限"
                        prop="ignorePermissionControl"
                    >
                        <el-switch
                            v-model="form.ignorePermissionControl"
                        ></el-switch>
                    </el-form-item>
                    <el-form-item label="备注" prop="summary">
                        <el-input
                            v-model="form.summary"
                            clearable
                            type="textarea"
                        ></el-input>
                    </el-form-item>
                </el-form>
            </el-tab-pane>
            <el-tab-pane label="菜单权限">
                <div class="treeMain">
                    <el-tree
                        ref="menu"
                        :data="menu.list"
                        :props="menu.props"
                        default-expand-all
                        node-key="id"
                        show-checkbox
                    ></el-tree>
                </div>
            </el-tab-pane>
            <el-tab-pane label="接口权限">
                <div class="treeMain">
                    <el-tree
                        ref="api"
                        :data="api.list"
                        :props="api.props"
                        node-key="id"
                        show-checkbox
                    ></el-tree>
                </div>
            </el-tab-pane>
            <el-tab-pane label="数据范围">
                <el-form label-position="left" label-width="100px">
                    <el-form-item label="规则类型">
                        <el-select
                            v-model="form.dataScope"
                            :disabled="mode === 'view'"
                            placeholder="请选择"
                        >
                            <el-option
                                v-for="(item, i) in this.$CONFIG.ENUMS
                                    .dataScopes"
                                :key="i"
                                :label="item[1]"
                                :value="i"
                            ></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item
                        v-show="form.dataScope === 'specificDept'"
                        label="选择部门"
                    >
                        <div class="treeMain" style="width: 100%">
                            <el-tree
                                ref="dept"
                                :data="data.list"
                                :props="data.props"
                                default-expand-all
                                node-key="id"
                                show-checkbox
                            ></el-tree>
                        </div>
                    </el-form-item>
                </el-form>
            </el-tab-pane>
            <el-tab-pane label="控制台">
                <el-form label-position="left" label-width="100px">
                    <el-form-item label="控制台视图">
                        <el-select
                            v-model="form.displayDashboard"
                            :disabled="mode === 'view'"
                            placeholder="请选择"
                        >
                            <el-option
                                v-for="item in dashboardOptions"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            >
                            </el-option>
                        </el-select>
                        <div class="el-form-item-msg">
                            用于控制角色登录后控制台的视图
                        </div>
                    </el-form-item>
                </el-form>
            </el-tab-pane>
            <el-tab-pane v-if="mode === 'view'" label="Json">
                <json-viewer
                    :expand-depth="5"
                    :expanded="true"
                    :value="form"
                    boxed
                    copyable
                    sort
                ></json-viewer>
            </el-tab-pane>
        </el-tabs>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button
                v-if="mode !== 'view'"
                :loading="isSaving"
                type="primary"
                @click="submit()"
                >保 存
            </el-button>
        </template>
    </el-dialog>
</template>

<script>
import JsonViewer from "vue-json-viewer";

export default {
    components: {
        JsonViewer,
    },
    emits: ["success", "closed"],
    data() {
        return {
            mode: "add",
            titleMap: {
                add: "新增角色",
                edit: "编辑角色",
                view: "查看角色",
            },
            visible: false,
            isSaving: false,
            menu: {
                list: [],
                checked: [],
                props: {
                    label: (data) => {
                        return data.meta.title;
                    },
                },
            },
            api: {
                list: [],
                checked: [],
                props: {
                    label: (data) => data.summary,
                },
            },
            data: {
                list: [],
                checked: [],
                props: {
                    label: (data) => data.name,
                },
                rule: "",
            },
            dashboard: "0",
            dashboardOptions: [
                {
                    value: true,
                    label: "仪表板",
                },
                {
                    value: false,
                    label: "工作台",
                },
            ],
            //表单数据
            form: { displayDashboard: false, sort: 100, enabled: true },
            //验证规则
            rules: {
                sort: [
                    {
                        required: true,
                        message: "请输入排序",
                        trigger: "change",
                    },
                ],
                name: [{ required: true, message: "请输入角色名称" }],
            },
        };
    },
    mounted() {},
    methods: {
        async getMenu() {
            this.menu.checked = this.form.menuIds || [];
            const res = await this.$API.sys_menu.query.post();
            this.menu.list = res.data;
            //获取接口返回的之前选中的和半选的合并，处理过滤掉有叶子节点的key
            this.$nextTick().then(() => {
                let filterKeys = this.menu.checked.filter(
                    (key) => this.$refs.menu.getNode(key).isLeaf
                );
                this.$refs.menu.setCheckedKeys(filterKeys, true);
            });
        },
        async getApi() {
            //this.menu.checked = this.form.menuIds || [];
            const res = await this.$API.sys_api.query.post();
            this.api.list = res.data;
            this.api.checked = this.form.apiIds || [];
            //获取接口返回的之前选中的和半选的合并，处理过滤掉有叶子节点的key
            this.$nextTick().then(() => {
                let filterKeys = this.api.checked.filter(
                    (key) => this.$refs.api.getNode(key).isLeaf
                );
                this.$refs.api.setCheckedKeys(filterKeys, true);
            });
        },
        async getDept() {
            const res = await this.$API.sys_dept.query.post();
            this.data.list = res.data;
            this.data.checked = this.form.deptIds || [];
            this.$nextTick().then(() => {
                let filterKeys = this.data.checked.filter(
                    (key) => this.$refs.dept.getNode(key).isLeaf
                );
                this.$refs.dept.setCheckedKeys(filterKeys, true);
            });
        },
        //显示
        open(mode = "add") {
            this.mode = mode;
            this.visible = true;
            return this;
        },
        //表单提交方法
        submit() {
            this.$refs.dialogForm.validate(async (valid) => {
                if (valid) {
                    this.isSaving = true;
                    //选中的和半选的合并后传值接口
                    const checkedKeys = this.$refs.menu
                        .getCheckedKeys()
                        .concat(this.$refs.menu.getHalfCheckedKeys());
                    const checkedKeys_dept = this.$refs.dept
                        .getCheckedKeys()
                        .concat(this.$refs.dept.getHalfCheckedKeys());
                    const checkedKeys_api = this.$refs.api
                        .getCheckedKeys()
                        .concat(this.$refs.api.getHalfCheckedKeys());

                    try {
                        const method =
                            this.mode === "add"
                                ? this.$API.sys_role.create
                                : this.$API.sys_role.update;
                        this.form.deptIds = checkedKeys_dept;
                        this.form.menuIds = checkedKeys;
                        this.form.apiIds = checkedKeys_api;
                        const res = await method.post(this.form);
                        this.$emit("success", res.data, this.mode);
                        this.visible = false;
                        this.$message.success("操作成功");
                    } catch {}
                    this.isSaving = false;
                }
            });
        },
        //表单注入数据
        setData(data) {
            //可以和上面一样单个注入，也可以像下面一样直接合并进去
            Object.assign(this.form, data);

            return this;
        },
        loadTree() {
            this.getMenu();
            this.getDept();
            this.getApi();
        },
    },
};
</script>

<style></style>