<template>
    <el-dialog
        v-model="visible"
        :title="titleMap[mode]"
        :width="500"
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
                    label-width="100px"
                >
                    <el-form-item
                        v-if="mode === 'edit'"
                        label="应用编号"
                        prop="id"
                    >
                        <el-input v-model="form.id" disabled></el-input>
                    </el-form-item>
                    <el-form-item
                        v-if="mode === 'edit'"
                        label="应用密钥"
                        prop="secret"
                    >
                        <el-input v-model="form.secret" disabled></el-input>
                    </el-form-item>
                    <el-form-item
                        v-if="mode !== 'reg'"
                        label="所属用户"
                        prop="userApp.user.id"
                    >
                        <sc-table-select
                            v-model="form.userApp.user"
                            :apiObj="$API.sys_user.pagedQuery"
                            :disabled="mode === 'edit'"
                            :props="props"
                            :table-width="600"
                            @change="change"
                        >
                            <template #header="{ form, submit }">
                                <el-form :inline="true" :model="form">
                                    <el-form-item>
                                        <el-input
                                            v-model="form.keywords"
                                            clearable
                                        ></el-input>
                                    </el-form-item>
                                    <el-form-item>
                                        <el-button
                                            type="primary"
                                            @click="submit"
                                            >查询
                                        </el-button>
                                    </el-form-item>
                                </el-form>
                            </template>
                            <el-table-column
                                label="用户编号"
                                prop="id"
                            ></el-table-column>
                            <el-table-column
                                label="用户名"
                                prop="userName"
                            ></el-table-column>
                            <el-table-column
                                label="手机号"
                                prop="mobile"
                            ></el-table-column>
                        </sc-table-select>
                    </el-form-item>
                    <el-form-item label="应用名称" prop="summary">
                        <el-input v-model="form.summary" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="回调地址" prop="callbackUrl">
                        <el-input
                            v-model="form.callbackUrl"
                            clearable
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="推送地址" prop="pushUrl">
                        <el-input v-model="form.pushUrl" clearable></el-input>
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
                add: "新增应用",
                edit: "编辑应用",
                view: "查看应用",
                reg: "注册应用",
            },
            visible: false,
            isSaving: false,
            //表单数据
            form: {
                id: 0,
                enabled: false,
                userApp: { user: {} },
            },
            //selectedUser: {},
            props: {
                label: "userName",
                value: "id",
                keyword: "keywords",
            },
            //验证规则
            rules: {
                summary: [{ required: true, message: "请输入应用名称" }],
                callbackUrl: [{ required: true, message: "请输入回调地址" }],
                pushUrl: [{ required: true, message: "请输入推送地址" }],
                userApp: {
                    user: {
                        id: [
                            {
                                required: true,
                                type: "integer",
                                min: 1,
                                message: "请选择所属用户",
                                trigger: "blur",
                            },
                        ],
                    },
                },
            },
            //所需数据选项
            groups: [],
            groupsProps: {
                value: "id",
                emitPath: false,
                checkStrictly: true,
            },
        };
    },
    mounted() {},
    methods: {
        //显示
        open(mode = "add") {
            this.mode = mode;
            this.visible = true;
            return this;
        },
        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm
                .validate()
                .catch(() => {});
            if (!valid) {
                return false;
            }

            this.isSaving = true;
            try {
                const method =
                    this.mode === "add"
                        ? this.$API.cst_app.create
                        : this.mode === "reg"
                        ? this.$API.cst_app.registerNewApp
                        : this.$API.cst_app.update;
                this.form.userId = this.form.userApp.user.id;
                const res = await method.post(this.form);
                this.$emit("success", res.data, this.mode);
                this.visible = false;
                this.$message.success("操作成功");
            } catch {}
            this.isSaving = false;
        },
        //表单注入数据
        setData(data) {
            // this.form.id = data.id
            // this.form.enabled = data.enabled
            //可以和上面一样单个注入，也可以像下面一样直接合并进去
            Object.assign(this.form, data);
            //this.selectedUser = {id: this.form.userApp.user.id, userName: this.form.userApp.user.userName}
        },
        change() {
            //this.form.userApp.user.id = this.selectedUser.id
        },
    },
};
</script>

<style></style>