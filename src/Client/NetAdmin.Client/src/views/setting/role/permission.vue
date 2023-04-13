<template>
    <el-dialog
        v-model="visible"
        :width="500"
        destroy-on-close
        title="角色权限设置"
        @closed="$emit('closed')"
    >
        <el-tabs tab-position="top">
            <el-tab-pane label="菜单权限">
                <div class="treeMain">
                    <el-tree
                        ref="menu"
                        :data="menu.list"
                        :props="menu.props"
                        node-key="name"
                        show-checkbox
                    ></el-tree>
                </div>
            </el-tab-pane>
            <el-tab-pane label="数据权限">
                <el-form label-position="left" label-width="100px">
                    <el-form-item label="规则类型">
                        <el-select v-model="data.dataType" placeholder="请选择">
                            <el-option label="全部可见" value="1"></el-option>
                            <el-option label="本人可见" value="2"></el-option>
                            <el-option
                                label="所在部门可见"
                                value="3"
                            ></el-option>
                            <el-option
                                label="所在部门及子级可见"
                                value="4"
                            ></el-option>
                            <el-option
                                label="选择的部门可见"
                                value="5"
                            ></el-option>
                            <el-option label="自定义" value="6"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item
                        v-show="data.dataType === '5'"
                        label="选择部门"
                    >
                        <div class="treeMain" style="width: 100%">
                            <el-tree
                                ref="dept"
                                :data="data.list"
                                :props="data.props"
                                node-key="id"
                                show-checkbox
                            ></el-tree>
                        </div>
                    </el-form-item>
                    <el-form-item v-show="data.dataType === '6'" label="规则值">
                        <el-input
                            v-model="data.rule"
                            :rows="6"
                            clearable
                            placeholder="请输入自定义规则代码"
                            type="textarea"
                        ></el-input>
                    </el-form-item>
                </el-form>
            </el-tab-pane>
            <el-tab-pane label="控制台模块">
                <div class="treeMain">
                    <el-tree
                        ref="grid"
                        :data="grid.list"
                        :default-checked-keys="grid.checked"
                        :props="grid.props"
                        node-key="key"
                        show-checkbox
                    ></el-tree>
                </div>
            </el-tab-pane>
            <el-tab-pane label="控制台">
                <el-form label-position="left" label-width="100px">
                    <el-form-item label="控制台视图">
                        <el-select v-model="dashboard" placeholder="请选择">
                            <el-option
                                v-for="item in dashboardOptions"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            >
                                <span style="float: left">{{
                                    item.label
                                }}</span>
                                <span
                                    style="
                                        float: right;
                                        color: #8492a6;
                                        font-size: 12px;
                                    "
                                    >{{ item.views }}</span
                                >
                            </el-option>
                        </el-select>
                        <div class="el-form-item-msg">
                            用于控制角色登录后控制台的视图
                        </div>
                    </el-form-item>
                </el-form>
            </el-tab-pane>
        </el-tabs>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button :loading="isSaving" type="primary" @click="submit()"
                >保 存
            </el-button>
        </template>
    </el-dialog>
</template>

<script>
export default {
    emits: ["success", "closed"],
    data() {
        return {
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
            grid: {
                list: [],
                checked: [
                    "welcome",
                    "ver",
                    "time",
                    "progress",
                    "echarts",
                    "about",
                ],
                props: {
                    label: (data) => {
                        return data.title;
                    },
                    disabled: (data) => {
                        return data.isFixed;
                    },
                },
            },
            data: {
                dataType: "1",
                list: [],
                checked: [],
                props: {},
                rule: "",
            },
            dashboard: "0",
            dashboardOptions: [
                {
                    value: "0",
                    label: "数据统计",
                    views: "stats",
                },
                {
                    value: "1",
                    label: "工作台",
                    views: "work",
                },
            ],
        };
    },
    mounted() {
        this.getMenu();
        this.getDept();
        this.getGrid();
    },
    methods: {
        open() {
            this.visible = true;
        },
        submit() {
            this.isSaving = true;

            //选中的和半选的合并后传值接口
            const checkedKeys = this.$refs.menu
                .getCheckedKeys()
                .concat(this.$refs.menu.getHalfCheckedKeys());

            const checkedKeys_dept = this.$refs.dept
                .getCheckedKeys()
                .concat(this.$refs.dept.getHalfCheckedKeys());

            setTimeout(() => {
                this.isSaving = false;
                this.visible = false;
                this.$message.success("操作成功");
                this.$emit("success");
            }, 1000);
        },
        async getMenu() {
            const res = await this.$API.system.menu.list.get();
            this.menu.list = res.data;

            //获取接口返回的之前选中的和半选的合并，处理过滤掉有叶子节点的key
            this.menu.checked = [
                "system",
                "user",
                "user.add",
                "user.edit",
                "user.del",
                "directive.edit",
                "other",
                "directive",
            ];
            this.$nextTick().then(() => {
                let filterKeys = this.menu.checked.filter(
                    (key) => this.$refs.menu.getNode(key).isLeaf
                );
                this.$refs.menu.setCheckedKeys(filterKeys, true);
            });
        },
        async getDept() {
            const res = await this.$API.system.dept.list.get();
            this.data.list = res.data;
            this.data.checked = ["12", "2", "21", "22", "1"];
            this.$nextTick().then(() => {
                let filterKeys = this.data.checked.filter(
                    (key) => this.$refs.dept.getNode(key).isLeaf
                );
                this.$refs.dept.setCheckedKeys(filterKeys, true);
            });
        },
        getGrid() {
            this.grid.list = [
                {
                    key: "welcome",
                    title: "欢迎",
                    isFixed: true,
                },
                {
                    key: "ver",
                    title: "版本信息",
                    isFixed: true,
                },
                {
                    key: "time",
                    title: "时钟",
                },
                {
                    key: "progress",
                    title: "进度环",
                },
                {
                    key: "echarts",
                    title: "实时收入",
                },
                {
                    key: "about",
                    title: "关于项目",
                },
            ];
        },
    },
};
</script>

<style scoped>
.treeMain {
    height: 280px;
    overflow: auto;
    border: 1px solid #dcdfe6;
    margin-bottom: 10px;
}
</style>