<template>
    <el-form :model="form" :rules="rules" @keyup.enter="login" label-width="0" ref="loginForm" size="large">
        <el-form-item prop="account">
            <el-input v-model="form.account" :placeholder="$t('用户名 / 手机 / 邮箱')" clearable prefix-icon="el-icon-user"></el-input>
        </el-form-item>
        <el-form-item prop="password">
            <el-input v-model="form.password" :placeholder="$t('请输入密码')" clearable prefix-icon="el-icon-lock" show-password></el-input>
        </el-form-item>
        <el-form-item style="margin-bottom: 1rem">
            <el-col :span="12">
                <el-checkbox v-model="autoLogin" :label="$t('24小时免登录')"></el-checkbox>
            </el-col>
            <el-col :span="12" class="login-forgot">
                <router-link to="/guest/reset-password">{{ $t('忘记密码？') }}</router-link>
            </el-col>
        </el-form-item>
        <el-form-item>
            <el-button :loading="isLoading" @click="login" round style="width: 100%" type="primary"
                >{{ starred ? $t('登录') : $t('Star 后可登录') }}
            </el-button>
        </el-form-item>
        <div class="login-reg">
            {{ $t('还没有账号？') }}
            <router-link to="/guest/register">{{ $t('创建新账号') }}</router-link>
        </div>
    </el-form>
</template>

<script>
export default {
    data() {
        return {
            starred: false,
            autoLogin: false,
            form: {
                account: 'root',
                password: '1234qwer',
            },
            rules: {
                account: [{ required: true, message: this.$t('请输入用户名'), trigger: 'blur' }],
                password: [{ required: true, message: this.$t('请输入密码'), trigger: 'blur' }],
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
        this.autoLogin = this.$TOOL.data.get('AUTO_LOGIN') || false
    },
    methods: {
        async login() {
            if (!this.starred) {
                window.open('https://github.com/nsnail/NetAdmin')
                this.starred = true
                return
            }

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
            const redirect = this.$TOOL.data.get('LOGIN_REDIRECT') ?? '/'
            await this.$TOOL.data.remove('LOGIN_REDIRECT')
            window.location.href = redirect
        },
    },
}
</script>

<style></style>