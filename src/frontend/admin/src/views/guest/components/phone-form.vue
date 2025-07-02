<template>
    <el-form :model="form" :rules="rules" @keyup.enter="login" label-width="0" ref="loginForm" size="large">
        <na-form-phone
            v-model="form"
            :phone-place-holder="$t('手机号码')"
            :vue="this"
            code-field="code"
            form-name="loginForm"
            phone-field="destDevice" />
        <el-form-item>
            <el-button :loading="loading" @click="login" class="w100p" round type="primary">{{ $t('登录') }}</el-button>
        </el-form-item>
        <div class="login-reg">
            {{ $t('还没有账号？') }}
            <router-link to="/guest/register">{{ $t('创建新账号') }}</router-link>
        </div>
    </el-form>
</template>
<script>
import { defineAsyncComponent } from 'vue'
const naFormPhone = defineAsyncComponent(() => import('@/components/na-form-phone'))
import phoneConfig from '@/config/na-form-phone'

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
<style scoped />