<template>
    <el-form ref="loginForm" :model="form" :rules="rules" label-width="0" size="large" @keyup.enter="login">
        <el-form-item prop="account">
            <el-input v-model="form.account" :placeholder="$t('login.userPlaceholder')" clearable prefix-icon="el-icon-user"></el-input>
        </el-form-item>
        <el-form-item prop="password">
            <el-input v-model="form.password" :placeholder="$t('login.PWPlaceholder')" clearable prefix-icon="el-icon-lock" show-password></el-input>
        </el-form-item>
        <el-form-item style="margin-bottom: 10px">
            <el-col :span="12">
                <el-checkbox v-model="autoLogin" :label="$t('login.rememberMe')"></el-checkbox>
            </el-col>
            <el-col :span="12" class="login-forgot">
                <router-link to="/anonymous/reset-password">{{ $t('login.forgetPassword') }}？</router-link>
            </el-col>
        </el-form-item>
        <el-form-item>
            <el-button :loading="isLoading" round style="width: 100%" type="primary" @click="login">{{ $t('login.signIn') }}</el-button>
        </el-form-item>
        <div class="login-reg">
            {{ $t('login.noAccount') }}
            <router-link to="/anonymous/register">{{ $t('login.createAccount') }}</router-link>
        </div>
    </el-form>
</template>

<script>
export default {
    data() {
        return {
            autoLogin: false,
            form: {
                account: 'root',
                password: '1234qwer',
            },
            rules: {
                account: [{ required: true, message: this.$t('login.userError'), trigger: 'blur' }],
                password: [{ required: true, message: this.$t('login.PWError'), trigger: 'blur' }],
            },
            isLoading: false,
        }
    },
    watch: {
        autoLogin(v) {
            this.$TOOL.data.set('AUTO_LOGIN', v)
        },
    },
    mounted() {
        this.autoLogin = this.$TOOL.data.get('AUTO_LOGIN')
    },
    methods: {
        async login() {
            const validate = await this.$refs.loginForm.validate().catch(() => {})
            if (!validate) {
                return false
            }

            this.isLoading = true
            //获取token
            try {
                await this.$API.sys_user.loginByPwd.post(this.form)
            } catch {
                this.isLoading = false
                return false
            }
            window.location.href = '/'
        },
    },
}
</script>

<style></style>