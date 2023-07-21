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
                    <el-form-item label="岗位名称" prop="name">
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
                    <el-form-item label="备注" prop="summary">
                        <el-input
                            v-model="form.summary"
                            clearable
                            type="textarea"
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
                add: "新增岗位",
                edit: "编辑岗位",
                view: "查看岗位",
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