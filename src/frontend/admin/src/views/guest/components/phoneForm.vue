<template>
    <el-form ref="loginForm" :model="form" :rules="rules" label-width="0" size="large" @keyup.enter="login">
        <na-form-phone v-model="form" :vue="this" code-field="code" form-name="loginForm" phone-field="destDevice"></na-form-phone>
        <el-form-item>
            <el-button :loading="loading" class="w100p" round type="primary" @click="login">{{ $t('登录') }}</el-button>
        </el-form-item>
        <div class="login-reg">
            {{ $t('还没有账号?') }}
            <router-link to="/guest/register">{{ $t('创建新账号') }}</router-link>
        </div>
    </el-form>
</template>
<script>
import naFormPhone from '@/components/naFormPhone/index.vue'
import phoneConfig from '@/config/naFormPhone'

export default {
    components: {
        naFormPhone,
    },
    data() {
        return {
            loading: false,
            form: {
                destDevice: '',
                code: '',
            },
            rules: {
                destDevice: phoneConfig.mobile(this),
                code: phoneConfig.code(this),
            },
        }
    },
    mounted() {},
    methods: {
        async login() {
            if (!(await this.$refs.loginForm.validate().catch(() => {}))) {
                return false
            }
            this.loading = true
            try {
                await this.$API.sys_user.loginBySms.post(this.form)
            } catch {
                this.loading = false
                return false
            }

            window.location.href = '/'
        },
    },
}
</script>
<style scoped></style>