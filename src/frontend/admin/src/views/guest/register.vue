<template>
    <common-page :title="$t('注册新账号')">
        <el-steps :active="stepActive" finish-status="success" simple>
            <el-step :title="$t('填写账号')" />
            <el-step :title="$t('验证手机')" />
            <el-step :title="$t('注册成功')" />
        </el-steps>
        <el-form v-if="stepActive === 0" ref="stepForm_0" :model="form" :rules="rules" label-width="120" size="large" @keyup.enter="next">
            <el-form-item :label="$t('登录账号')" prop="userName">
                <el-input v-model="form.userName" clearable maxlength="16" :placeholder="$t('请输入登录账号')"></el-input>
            </el-form-item>
            <el-form-item :label="$t('登录密码')" prop="passwordText">
                <el-input
                    v-model="form.passwordText"
                    clearable
                    maxlength="16"
                    :placeholder="$t('请输入登录密码')"
                    show-password
                    type="password"></el-input>
                <sc-password-strength v-model="form.passwordText"></sc-password-strength>
                <div class="el-form-item-msg">请输入包含英文、数字的8位以上密码</div>
            </el-form-item>
            <el-form-item :label="$t('确认密码')" prop="passwordText2">
                <el-input
                    v-model="form.passwordText2"
                    clearable
                    maxlength="16"
                    :placeholder="$t('请再一次输入登录密码')"
                    show-password
                    type="password"></el-input>
            </el-form-item>
            <el-form-item label="" prop="agree">
                <el-checkbox v-model="form.agree" label="">已阅读并同意</el-checkbox>
                <span class="link" @click="showAgree = true">《平台服务协议》</span>
            </el-form-item>
        </el-form>
        <el-form v-if="stepActive === 1" ref="stepForm_1" :model="form" :rules="rules" size="large">
            <na-form-phone
                v-model="form.verifySmsCodeReq"
                :code-field="['verifySmsCodeReq.code', 'code']"
                :phone-field="['verifySmsCodeReq.destDevice', 'destDevice']"
                :vue="this"
                form-name="stepForm_1"></na-form-phone>
        </el-form>
        <div v-if="stepActive >= 2">
            <el-result icon="success" sub-title="$t('可以使用登录账号以及手机号登录系统')" :title="$t('注册成功')">
                <template #extra>
                    <el-button size="large" type="primary" @click="goLogin">前去登录</el-button>
                </template>
            </el-result>
        </div>
        <el-form size="large" style="text-align: center">
            <el-button v-if="stepActive > 0 && stepActive < 2" size="large" @click="pre">上一步</el-button>
            <el-button v-if="stepActive < 1" size="large" type="primary" @click="next">下一步</el-button>
            <el-button v-if="stepActive === 1" :loading="loading" size="large" type="primary" @click="save">提 交</el-button>
        </el-form>
        <el-dialog v-model="showAgree" destroy-on-close :title="$t('平台服务协议')">
            平台服务协议
            <template #footer>
                <el-button @click="showAgree = false">取消</el-button>
                <el-button
                    type="primary"
                    @click="
                        () => {
                            showAgree = false
                            form.agree = true
                        }
                    "
                    >我已阅读并同意
                </el-button>
            </template>
        </el-dialog>
    </common-page>
</template>
<script>
import commonPage from './components/commonPage'
import naFormPassword from '@/config/naFormPassword'
import naFormPhone from '@/components/naFormPhone/index.vue'
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
            showAgree: false,
            form: {
                agree: false,
                passwordText2: '',
                passwordText: '',
                userName: '',
                verifySmsCodeReq: {},
            },
            rules: {
                agree: [
                    {
                        validator: (rule, value, callback) => {
                            if (!value) {
                                callback(new Error('请阅读并同意协议'))
                            } else {
                                callback()
                            }
                        },
                    },
                ],
                passwordText2: naFormPassword.passwordText2(() => this.form.passwordText),
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
                                return res.data ? callback() : callback(new Error('用户名已被使用'))
                            } catch (ex) {
                                return callback(new Error(ex.data.msg.userName[0].children))
                            }
                        },
                        trigger: 'blur',
                    },
                ],
                verifySmsCodeReq: {
                    destDevice: [phoneConfig.mobile(this), phoneConfig.mobileNoUsed(this, () => this.form.id)],
                    code: phoneConfig.code(this),
                },
            },
        }
    },
    async mounted() {},
    methods: {
        pre() {
            this.stepActive -= 1
        },
        async next() {
            if (!(await this.$refs[`stepForm_${this.stepActive}`].validate().catch(() => {}))) {
                return false
            }
            this.stepActive += 1
        },
        async save() {
            if (!(await this.$refs[`stepForm_${this.stepActive}`].validate().catch(() => {}))) {
                return false
            }

            this.loading = true
            try {
                await this.$API.sys_user.register.post(this.form)
                this.stepActive += 2
            } catch {
                //
            }
            this.loading = false
        },
        goLogin() {
            this.$router.push({
                path: '/guest/login',
            })
        },
    },
}
</script>
<style scoped></style>