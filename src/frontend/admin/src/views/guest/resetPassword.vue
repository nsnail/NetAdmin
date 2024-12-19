<template>
    <common-page :title="$t('重置密码')">
        <el-steps :active="stepActive" finish-status="success" simple>
            <el-step :title="$t('填写新密码')" />
            <el-step :title="$t('完成重置')" />
        </el-steps>
        <el-form v-if="stepActive === 0" :model="form" :rules="rules" @keyup.enter="save" label-width="15rem" ref="form" size="large">
            <na-form-phone
                v-model="form.verifySmsCodeReq"
                :code-field="['verifySmsCodeReq.code', 'code']"
                :code-label="$t('短信验证码')"
                :phone-field="['verifySmsCodeReq.destDevice', 'destDevice']"
                :phone-label="$t('手机号码')"
                :phone-place-holder="$t('手机号码')"
                :vue="this"
                form-name="form"></na-form-phone>
            <el-form-item :label="$t('新密码')" prop="passwordText">
                <el-input v-model="form.passwordText" :placeholder="$t('请输入新密码')" show-password></el-input>
                <sc-password-strength v-model="form.passwordText"></sc-password-strength>
                <div class="el-form-item-msg">{{ $t('请输入包含英文、数字的8位以上密码') }}</div>
            </el-form-item>
            <el-form-item :label="$t('确认新密码')" prop="passwordText2">
                <el-input v-model="form.passwordText2" :placeholder="$t('请再一次输入新密码')" show-password></el-input>
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" @click="save" type="primary">{{ $t('提交') }}</el-button>
            </el-form-item>
        </el-form>
        <el-result v-if="stepActive >= 1" :sub-title="$t('请牢记自己的新密码，返回登录后使用新密码登录')" :title="$t('密码重置成功')" icon="success">
            <template #extra>
                <el-button @click="backLogin" size="large" type="primary">{{ $t('返回登录') }}</el-button>
            </template>
        </el-result>
    </common-page>
</template>

<script>
import commonPage from './components/commonPage'
import naFormPassword from '@/config/naFormPassword'
import naFormPhone from '@/components/naFormPhone'
import phoneConfig from '@/config/naFormPhone'
import scPasswordStrength from '@/components/scPasswordStrength'

export default {
    components: {
        commonPage,
        naFormPhone,
        scPasswordStrength,
    },
    data() {
        return {
            loading: false,
            stepActive: 0,
            form: {
                passwordText2: '',
                passwordText: '',
                verifySmsCodeReq: {},
            },
            rules: {
                verifySmsCodeReq: {
                    destDevice: phoneConfig.mobile(this),
                    code: phoneConfig.code(this),
                },
                passwordText2: naFormPassword.passwordText2(() => this.form.passwordText),
                passwordText: naFormPassword.passwordText(this),
            },
        }
    },
    mounted() {},
    methods: {
        async save() {
            if (!(await this.$refs.form.validate().catch(() => {}))) {
                return false
            }
            this.loading = true
            try {
                await this.$API.sys_user.resetPassword.post(this.form)
                this.stepActive = 2
            } catch {
                //
            }
            this.loading = false
        },
        backLogin() {
            this.$router.push({
                path: '/guest/login',
            })
        },
    },
}
</script>

<style scoped></style>