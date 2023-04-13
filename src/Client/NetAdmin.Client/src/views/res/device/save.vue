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
                    <el-form-item label="设备编号" prop="id">
                        <el-input v-model="form.id" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="设备IP" prop="ip">
                        <el-input v-model="form.ip" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="平台" prop="useType">
                        <el-input v-model="form.useType" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="软件版本" prop="appVersion">
                        <el-input
                            v-model="form.appVersion"
                            clearable
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="已分配账号" prop="shareNumber">
                        <el-input
                            v-model="form.shareNumber"
                            clearable
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="已使用账号" prop="usedNum">
                        <el-input v-model="form.usedNum" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="设备坑位上限" prop="upLimitNum">
                        <el-input
                            v-model="form.upLimitNum"
                            clearable
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="启用" prop="enabled">
                        <el-switch v-model="form.enabled"></el-switch>
                    </el-form-item>
                    <el-form-item label="状态" prop="onlineStatus">
                        <el-input
                            v-model="form.onlineStatus"
                            clearable
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="最后心跳" prop="heartbeatTime">
                        <el-input v-model="form.heartTime" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="创建时间" prop="createdTime">
                        <el-input
                            v-model="form.createdTime"
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
                add: "新增设备",
                edit: "编辑设备",
                view: "查看设备",
            },
            visible: false,
            isSaving: false,
            //表单数据
            form: {
                id: 0,
                enabled: true,
            },
            //验证规则
            rules: {
                name: [{ required: true, message: "请输入岗位名称" }],
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
                        ? this.$API.sys_position.create
                        : this.$API.sys_position.update;
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
        },
    },
};
</script>

<style></style>