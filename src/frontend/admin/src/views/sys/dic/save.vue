<template>
    <el-dialog
        v-model="visible"
        :title="titleMap[mode]"
        :width="400"
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
                    <el-form-item label="所属字典" prop="catalogId">
                        <el-cascader
                            v-model="form.catalogId"
                            :options="dic"
                            :props="dicProps"
                            :show-all-levels="false"
                            clearable
                        ></el-cascader>
                    </el-form-item>
                    <el-form-item label="项名称" prop="key">
                        <el-input v-model="form.key" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="键值" prop="value">
                        <el-input v-model="form.value" clearable></el-input>
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
                view: "查看字典",
                add: "新增字典",
                edit: "编辑字典",
            },
            visible: false,
            isSaving: false,
            form: {
                version: 0,
                id: 0,
                catalogId: 0,
                key: "",
                value: "",
                enabled: true,
            },
            rules: {
                catalogId: [{ required: true, message: "请选择所属字典" }],
                key: [{ required: true, message: "请输入项名称" }],
                value: [{ required: true, message: "请输入键值" }],
            },
            dic: [],
            dicProps: {
                value: "id",
                label: "name",
                emitPath: false,
                checkStrictly: true,
            },
        };
    },
    mounted() {
        if (this.params) {
            this.form.catalogId = this.params.id;
        }
        this.getDic();
    },
    methods: {
        //显示
        open(mode = "add") {
            this.mode = mode;
            this.visible = true;
            return this;
        },
        //获取字典列表
        async getDic() {
            const res = await this.$API.sys_dic.queryCatalog.post();
            this.dic = res.data;
        },
        //表单提交方法
        submit() {
            this.$refs.dialogForm.validate(async (valid) => {
                if (valid) {
                    this.isSaving = true;
                    try {
                        const method =
                            this.mode === "add"
                                ? this.$API.sys_dic.createContent
                                : this.$API.sys_dic.updateContent;
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
            Object.assign(this.form, data);
        },
    },
};
</script>

<style></style>