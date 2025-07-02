<template>
    <sc-dialog v-model="visible" :title="$t('高级筛选')" destroy-on-close>
        <el-form :model="form" :rules="rules" label-width="10rem" ref="form">
            <el-form-item :label="$t('字段名')" prop="field">
                <el-input v-model="form.field" :placeholder="$t('字段名')" clearable />
            </el-form-item>
            <el-form-item :label="$t('操作符')" prop="operator">
                <el-input v-model="form.operator" :placeholder="$t('操作符')" clearable />
            </el-form-item>
            <el-form-item :label="$t('字段值')" prop="value">
                <el-input v-model="form.value" :placeholder="$t('一行一个')" :rows="5" clearable type="textarea" />
            </el-form-item>
        </el-form>

        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button @click="submit" type="primary">{{ $t('确定') }}</el-button>
        </template>
    </sc-dialog>
</template>
<script>
export default {
    data() {
        return {
            form: {},
            visible: false,
            callback: () => {},
            rules: {
                field: [{ required: true, message: this.$t('请输入字段名'), trigger: 'blur' }],
                operator: [{ required: true, message: this.$t('请输入操作符'), trigger: 'blur' }],
            },
        }
    },
    methods: {
        open(form, callback) {
            Object.assign(this.form, form)
            this.visible = true
            this.callback = callback
        },
        async submit() {
            const valid = await this.$refs.form.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.callback(this.form)
            this.visible = false
        },
    },
}
</script>
<style scoped />