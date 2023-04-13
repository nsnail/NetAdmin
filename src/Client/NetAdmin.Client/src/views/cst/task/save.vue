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
                    <el-form-item label="任务编号" prop="id">
                        <el-input v-model="form.id" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="机器人编号" prop="robotId">
                        <el-input v-model="form.robotId" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="应用编号" prop="appId">
                        <el-input v-model="form.appId" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="任务类型" prop="cmd">
                        <el-input v-model="form.cmd" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="任务状态" prop="taskStatus">
                        <el-input
                            v-model="form.taskStatus"
                            clearable
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="业务状态" prop="bizStatus">
                        <el-input v-model="form.bizStatus" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="启动时间" prop="startedTime">
                        <el-input
                            v-model="form.startedTime"
                            clearable
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="完成时间" prop="finishedTime">
                        <el-input
                            v-model="form.finishedTime"
                            clearable
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="完成用时" prop="finishedDuration">
                        <el-input
                            v-model="form.finishedDuration"
                            clearable
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="回调用时" prop="callbackedDuration">
                        <el-input
                            v-model="form.callbackedDuration"
                            clearable
                        ></el-input>
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
                add: "新增任务",
                edit: "编辑任务",
                view: "查看任务",
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