<template>
    <sc-dialog v-model="visible" :title="$t(`创建粉丝账号`)" @closed="$emit('closed')" destroy-on-close>
        <el-form :model="form" :rules="rules" label-position="right" label-width="12rem" ref="form">
            <el-form-item :label="$t('登录账号')" prop="userName">
                <el-input v-model="form.userName" :placeholder="$t('请输入登录账号')" clearable maxlength="16" />
            </el-form-item>
            <el-form-item :label="$t('登录密码')" prop="passwordText">
                <div class="flex w100p gap05">
                    <el-input v-model="form.passwordText" :placeholder="$t('请输入登录密码')" clearable maxlength="16" style="flex-grow: 1" />
                    <el-button @click="form.passwordText = '1234qwer'">{{ $t(`初始密码`) }}</el-button>
                </div>
                <sc-password-strength v-model="form.passwordText" />
                <div class="el-form-item-msg">{{ $t('请输入包含英文、数字的8位以上密码') }}</div>
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
import { defineAsyncComponent } from 'vue'
import naFormPassword from '@/config/na-form-password'
const scPasswordStrength = defineAsyncComponent(() => import('@/components/sc-password-strength'))
export default {
    components: { scPasswordStrength },
    data() {
        return {
            loading: true,
            visible: false,
            form: {
                profile: {
                    appConfig: '[]',
                },
            },
            rules: {
                roleId: [{ required: true, message: this.$t('请选择角色') }],
                passwordText2: naFormPassword.passwordText2(this, () => this.form.passwordText),
                passwordText: naFormPassword.passwordText(this),
                userName: [
                    { required: true, message: this.$t('请输入用户名') },
                    {
                        validator: async (rule, valueEquals, callback) => {
                            try {
                                const res = await this.$API.sys_user.checkUserNameAvailable.post({
                                    userName: valueEquals,
                                    id: this.form.id,
                                })
                                return res.data ? callback() : callback(new Error(this.$t('用户名已被使用')))
                            } catch (ex) {
                                return callback(new Error(ex.data.msg.userName[0].children))
                            }
                        },
                        trigger: 'blur',
                    },
                ],
            },
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            this.loading = true
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
            this.form.roleIds = [this.form.roleId]
            try {
                await this.$API.sys_userinvite.createFansAccount.post(this.form)
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