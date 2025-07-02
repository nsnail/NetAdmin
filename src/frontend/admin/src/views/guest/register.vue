<template>
    <common-page :title="$t('注册新账号')">
        <el-steps :active="stepActive" finish-status="success" simple>
            <el-step :title="$t('填写账号')" />
            <el-step v-if="config.registerInviteRequired" :title="$t('验证手机')" />
            <el-step :title="$t('注册成功')" />
        </el-steps>
        <el-form v-if="stepActive === 0" :model="form" :rules="rules" @keyup.enter="next" label-width="15rem" ref="stepForm_0" size="large">
            <el-form-item :label="$t('登录账号')" prop="userName">
                <el-input v-model="form.userName" :placeholder="$t('请输入登录账号')" clearable maxlength="16" />
            </el-form-item>
            <el-form-item :label="$t('登录密码')" prop="passwordText">
                <el-input v-model="form.passwordText" :placeholder="$t('请输入登录密码')" clearable maxlength="16" show-password type="password" />
                <sc-password-strength v-model="form.passwordText" />
                <div class="el-form-item-msg">{{ $t('请输入包含英文、数字的8位以上密码') }}</div>
            </el-form-item>
            <el-form-item :label="$t('确认密码')" prop="passwordText2">
                <el-input
                    v-model="form.passwordText2"
                    :placeholder="$t('请再一次输入登录密码')"
                    clearable
                    maxlength="16"
                    show-password
                    type="password" />
            </el-form-item>
            <el-form-item :label="$t('邀请码')" prop="inviter">
                <el-input v-model="form.inviter" :placeholder="$t('请输入邀请码')" clearable maxlength="6" />
            </el-form-item>
            <el-form-item label="" prop="agree">
                <el-checkbox v-model="form.agree" label="">{{ $t('我已阅读并同意') }}</el-checkbox>
                <span @click="showAgree = true" class="link">《{{ $t('平台服务协议') }}》</span>
            </el-form-item>
        </el-form>
        <el-form v-if="stepActive === 1" :model="form" :rules="rules" ref="stepForm_1" size="large">
            <na-form-phone
                v-model="form.verifySmsCodeReq"
                :code-field="['verifySmsCodeReq.code', 'code']"
                :phone-field="['verifySmsCodeReq.destDevice', 'destDevice']"
                :phone-place-holder="$t('手机号码')"
                :vue="this"
                form-name="stepForm_1" />
        </el-form>
        <div v-if="stepActive >= 2">
            <el-result :sub-title="$t('可以使用登录账号以及手机号登录系统')" :title="$t('注册成功')" icon="success">
                <template #extra>
                    <el-button @click="goLogin" size="large" type="primary">{{ $t('前去登录') }}</el-button>
                </template>
            </el-result>
        </div>
        <el-form size="large" style="text-align: center">
            <el-button v-if="stepActive > 0 && stepActive < 2" @click="pre" size="large">{{ $t('上一步') }}</el-button>
            <el-button v-if="stepActive < (this.config.registerInviteRequired ? 1 : 0)" @click="next" size="large" type="primary">{{
                $t('下一步')
            }}</el-button>
            <el-button
                v-if="stepActive === (this.config.registerInviteRequired ? 1 : 0)"
                :loading="loading"
                @click="save"
                size="large"
                type="primary"
                >{{ $t('提交') }}</el-button
            >
        </el-form>
        <el-dialog v-model="showAgree" :title="$t('平台服务协议')" destroy-on-close>
            <template #footer>
                <el-button @click="showAgree = false">{{ $t('取消') }}</el-button>
                <el-button
                    @click="
                        () => {
                            showAgree = false
                            form.agree = true
                        }
                    "
                    type="primary">
                    {{ $t('我已阅读并同意') }}
                </el-button>
            </template>
        </el-dialog>
    </common-page>
</template>
<script>
import commonPage from './components/common-page'
import naFormPassword from '@/config/na-form-password'
import naFormPhone from '@/components/na-form-phone'
import phoneConfig from '@/config/na-form-phone'
import scPasswordStrength from '@/components/sc-password-strength'

export default {
    components: {
        commonPage,
        naFormPhone,
        scPasswordStrength,
    },
    data() {
        return {
            config: {},
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
                                callback(new Error(this.$t('请阅读并同意协议')))
                            } else {
                                callback()
                            }
                        },
                    },
                ],
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
                inviter: [
                    {
                        validator: async (rule, valueEquals, callback) => {
                            if (!valueEquals) return callback()
                            try {
                                const res = await this.$API.sys_user.checkInviterAvailable.post({
                                    code: valueEquals,
                                })
                                return res.data ? callback() : callback(new Error(this.$t('邀请码不正确')))
                            } catch (ex) {
                                return callback(new Error(ex.data.msg.code[0].children))
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
    async created() {
        const res = await this.$API.sys_config.getRegisterConfig.post({})
        Object.assign(this.config, res.data)
        if (this.config.registerInviteRequired) {
            this.rules.inviter.push({
                required: true,
                message: this.$t('请输入邀请码'),
                trigger: 'blur',
            })
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
                const postData = Object.assign({}, this.form)
                if (!postData.verifySmsCodeReq.destDevice) {
                    postData.verifySmsCodeReq.destDevice = '13838381438'
                    postData.verifySmsCodeReq.deviceType = 1
                    postData.verifySmsCodeReq.code = '1234'
                }
                await this.$API.sys_user.register.post(postData)
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
<style scoped />