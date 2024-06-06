<template>
    <sc-dialog v-model="visible" :title="titleMap[mode]" :width="400" @closed="$emit('closed')" destroy-on-close>
        <el-form v-loading="loading" :model="form" :rules="rules" label-width="10rem" ref="dialogForm">
            <el-form-item :label="$t('字典名称')" prop="name">
                <el-input v-model="form.name" :placeholder="$t('字典显示名称')" clearable></el-input>
            </el-form-item>
            <el-form-item :label="$t('编码')" prop="code">
                <el-input v-model="form.code" :placeholder="$t('字典编码')" clearable></el-input>
            </el-form-item>
            <el-form-item :label="$t('父路径')" prop="parentId">
                <na-dic-catalog v-model="form.parentId" />
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button :loading="loading" @click="submit" type="primary">保 存</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    emits: ['success', 'closed'],
    data() {
        return {
            mode: 'add',
            titleMap: {
                add: '新增字典',
                edit: '编辑字典',
            },
            visible: false,
            loading: false,
            form: {},
            rules: {
                code: [{ required: true, message: '请输入编码' }],
                name: [{ required: true, message: '请输入字典名称' }],
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
                Object.assign(this.form, (await this.$API.sys_dic.getCatalog.post({ id: data.id })).data)
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
                const method = this.mode === 'add' ? this.$API.sys_dic.createCatalog : this.$API.sys_dic.editCatalog
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

<style></style>