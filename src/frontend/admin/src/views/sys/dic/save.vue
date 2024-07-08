<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" :width="800" @closed="$emit('closed')" destroy-on-close>
        <el-form v-loading="loading" :model="form" :rules="rules" label-width="10rem" ref="dialogForm">
            <el-form-item :label="$t('字典名称')" prop="name">
                <el-input v-model="form.name" :placeholder="$t('字典名称')" clearable></el-input>
            </el-form-item>
            <el-form-item :label="$t('字典编码')" prop="code">
                <el-input v-model="form.code" :placeholder="$t('字典编码')" clearable></el-input>
            </el-form-item>
            <el-form-item :label="$t('父路径')" prop="parentId">
                <na-dic-catalog v-model="form.parentId" class="w100p" />
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    data() {
        return {
            //表单数据
            form: {},
            loading: false,
            mode: 'add',
            //验证规则
            rules: {
                code: [{ required: true, message: '请输入编码' }],
                name: [{ required: true, message: '请输入字典名称' }],
            },
            titleMap: {
                add: this.$t('新增字典'),
                edit: this.$t('编辑字典'),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            this.loading = true
            this.mode = data.mode
            this.form.parentId = data.data?.catalogId
            this.form.code = data.data?.code
            if (data.row?.id) {
                const res = await this.$API.sys_dic.getCatalog.post({ id: data.row.id })
                Object.assign(this.form, res.data)
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
            const method = this.mode === 'add' ? this.$API.sys_dic.createCatalog : this.$API.sys_dic.editCatalog
            try {
                const res = await method.post(this.form)
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success(this.$t('操作成功'))
            } catch {}
            this.loading = false
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped></style>