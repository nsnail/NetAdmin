<template>
    <sc-dialog v-model="visible" :title="titleMap[mode]" :width="400" destroy-on-close @closed="$emit('closed')">
        <div v-loading="loading">
            <el-tabs tab-position="top">
                <el-tab-pane label="基本信息">
                    <el-form ref="dialogForm" :disabled="mode === 'view'" :model="form" :rules="rules" label-width="100px">
                        <el-form-item label="所属字典" prop="catalogId">
                            <na-dic-catalog v-model="form.catalogId" />
                        </el-form-item>
                        <el-form-item label="项名" prop="key">
                            <el-input v-model="form.key" clearable></el-input>
                        </el-form-item>
                        <el-form-item label="项值" prop="value">
                            <el-input v-model="form.value" clearable></el-input>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" label="原始数据">
                    <json-viewer
                        :expand-depth="5"
                        :expanded="true"
                        :theme="this.$TOOL.data.get('APP_DARK') ? 'dark' : 'light'"
                        :value="form"
                        copyable
                        sort></json-viewer>
                </el-tab-pane>
            </el-tabs>
        </div>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button v-if="mode !== 'view'" :loading="loading" type="primary" @click="submit">保 存</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    components: {},
    emits: ['success', 'closed'],
    data() {
        return {
            mode: 'add',
            titleMap: {
                view: '查看字典',
                add: '新增字典',
                edit: '编辑字典',
            },
            visible: false,
            loading: false,
            form: {},
            rules: {
                catalogId: [{ required: true, message: '请选择所属字典' }],
                key: [{ required: true, message: '请输入项名' }],
                value: [{ required: true, message: '请输入项值' }],
            },
        }
    },
    mounted() {},
    methods: {
        //显示
        async open(mode = 'add', data) {
            this.visible = true
            this.loading = true
            this.mode = mode
            if (data) {
                Object.assign(this.form, (await this.$API.sys_dic.getContent.post({ id: data.id })).data)
            }
            this.loading = false
            return this
        },
        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }

            this.loading = true
            try {
                const method = this.mode === 'add' ? this.$API.sys_dic.createContent : this.$API.sys_dic.updateContent
                const res = await method.post(this.form)
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success('操作成功')
            } catch {
                //
            }
            this.loading = false
        },
    },
}
</script>

<style scoped></style>