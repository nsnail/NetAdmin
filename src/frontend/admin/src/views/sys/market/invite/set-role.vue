<template>
    <sc-dialog v-model="visible" :title="$t(`更改角色`)" @closed="$emit('closed')" destroy-on-close>
        <el-form :model="form" :rules="rules" label-position="right" label-width="12rem" ref="form">
            <el-form-item :label="$t(`用户`)">
                <el-input v-model="form.user.userName" disabled></el-input>
            </el-form-item>
            <el-form-item :label="$t('选择角色')" prop="roleId">
                <sc-select
                    v-model="form.roleId"
                    :config="{
                        props: {
                            label: `key`,
                            value: `value`,
                        },
                    }"
                    :query-api="$API.sys_userinvite.queryRolesAllowApply"
                    class="w100p"
                    clearable
                    filterable />
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">取消</el-button>
            <el-button :disabled="loading" :loading="loading" @click="submit" type="primary">保存</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            loading: true,
            visible: false,
            form: {},
            rules: {
                roleId: [{ required: true, message: this.$t('请选择角色') }],
            },
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            this.loading = true
            Object.assign(this.form, data.data)
            this.loading = false
            return this
        },

        //表单提交方法
        async submit() {
            const validate = await this.$refs.form.validate().catch(() => {})
            if (!validate) {
                return false
            }
            this.loading = true
            try {
                await this.$API.sys_userinvite.setFansRole.post(this.form)
                this.$emit('success')
                this.visible = false
            } catch {}
            this.loading = false
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped />