<template>
    <common-page title="注册新账号">
        <el-steps :active="stepActive" finish-status="success" simple>
            <el-step title="填写账号" />
            <el-step title="验证手机" />
            <el-step title="完成注册" />
        </el-steps>
        <el-form
            v-if="stepActive === 0"
            ref="stepForm_0"
            :label-width="120"
            :model="form"
            :rules="rules"
            size="large"
        >
            <el-form-item label="登录账号" prop="userName">
                <el-input
                    v-model="form.userName"
                    clearable
                    maxlength="16"
                    placeholder="请输入登录账号"
                ></el-input>
                <div class="el-form-item-msg">
                    登录账号将作为登录时的唯一凭证
                </div>
            </el-form-item>
            <el-form-item label="登录密码" prop="passwordText">
                <el-input
                    v-model="form.passwordText"
                    clearable
                    maxlength="16"
                    placeholder="请输入登录密码"
                    show-password
                    type="password"
                ></el-input>
                <sc-password-strength
                    v-model="form.passwordText"
                ></sc-password-strength>
                <div class="el-form-item-msg">
                    请输入包含英文、数字的8位以上密码
                </div>
            </el-form-item>
            <el-form-item label="确认密码" prop="passwordText2">
                <el-input
                    v-model="form.passwordText2"
                    clearable
                    maxlength="16"
                    placeholder="请再一次输入登录密码"
                    show-password
                    type="password"
                ></el-input>
            </el-form-item>
            <el-form-item label="" prop="agree">
                <el-checkbox v-model="form.agree" label=""
                    >已阅读并同意
                </el-checkbox>
                <span class="link" @click="showAgree = true"
                    >《平台服务协议》</span
                >
            </el-form-item>
        </el-form>
        <el-form
            v-if="stepActive === 1"
            ref="stepForm_1"
            :label-width="120"
            :model="form"
            :rules="rules"
            size="large"
        >
            <el-form-item label="手机号码" prop="verifySmsCodeReq.destMobile">
                <el-input
                    v-model="form.verifySmsCodeReq.destMobile"
                    clearable
                    el-icon-iphone
                    maxlength="11"
                    oninput="value=value.replace(/[^\d]/g,'')"
                    placeholder="请输入手机号码"
                >
                    <template #prepend>+86</template>
                </el-input>
            </el-form-item>
            <el-form-item label="短信验证码" prop="verifySmsCodeReq.code">
                <div class="yzm">
                    <el-input
                        v-model="form.verifySmsCodeReq.code"
                        clearable
                        maxlength="4"
                        oninput="value=value.replace(/[^\d]/g,'')"
                        placeholder="请输入短信验证码"
                    ></el-input>
                    <el-button :disabled="disabled" @click="getYzm"
                        >获取验证码<span v-if="disabled"> ({{ time }})</span>
                    </el-button>
                </div>
            </el-form-item>
        </el-form>
        <div v-if="stepActive === 2">
            <el-result
                icon="success"
                sub-title="可以使用登录账号以及手机号登录系统"
                title="注册成功"
            >
                <template #extra>
                    <el-button type="primary" @click="goLogin"
                        >前去登录
                    </el-button>
                </template>
            </el-result>
        </div>
        <el-form size="large" style="text-align: center">
            <el-button v-if="stepActive > 0 && stepActive < 2" @click="pre"
                >上一步
            </el-button>
            <el-button v-if="stepActive < 1" type="primary" @click="next"
                >下一步
            </el-button>
            <el-button
                v-if="stepActive === 1"
                v-loading="isSubmit"
                type="primary"
                @click="save"
                >提 交
            </el-button>
        </el-form>
        <el-dialog
            v-model="showAgree"
            :width="800"
            destroy-on-close
            title="平台服务协议"
        >
            平台服务协议
            <template #footer>
                <el-button @click="showAgree = false">取消</el-button>
                <el-button
                    type="primary"
                    @click="
                        showAgree = false;
                        form.agree = true;
                    "
                    >我已阅读并同意
                </el-button>
            </template>
        </el-dialog>
        <Verify
            ref="verify"
            :imgSize="{ width: '310px', height: '155px' }"
            captchaType="blockPuzzle"
            mode="pop"
            @success="captchaSuccess"
        ></Verify>
    </common-page>
</template>
<script>
import scPasswordStrength from "@/components/scPasswordStrength";
import commonPage from "./components/commonPage";
import Verify from "@/views/login/components/verifition/Verify.vue";

export default {
    components: {
        Verify,
        commonPage,
        scPasswordStrength,
    },
    data() {
        return {
            isSubmit: false,
            time: 0,
            disabled: false,
            stepActive: 0,
            showAgree: false,
            form: {
                passwordText: "",
                passwordText2: "",
                agree: false,
                userName: "",
                verifySmsCodeReq: {},
            },
            rules: {
                verifySmsCodeReq: {
                    destMobile: [
                        {
                            required: true,
                            message:
                                this.$CONFIG.LOC_STRINGS
                                    .mobile_phone_number_that_can_be_used_normally,
                            pattern: this.$CONFIG.STRINGS.RGX_MOBILE,
                        },
                        {
                            validator: async (rule, valueEquals, callback) => {
                                try {
                                    const res =
                                        await this.$API.sys_user.checkMobileAvailable.post(
                                            {
                                                mobile: valueEquals,
                                            }
                                        );
                                    if (res.data) {
                                        return callback();
                                    }
                                } catch {}
                                callback(new Error("手机号已被使用"));
                            },
                            trigger: "blur",
                        },
                    ],
                    code: [
                        {
                            required: true,
                            message: "请输入4位数字验证码",
                        },
                    ],
                },
                userName: [
                    { required: true, message: this.$t("login.userError") },
                    {
                        validator: async (rule, valueEquals, callback) => {
                            try {
                                const res =
                                    await this.$API.sys_user.checkUserNameAvailable.post(
                                        { userName: valueEquals }
                                    );
                                return res.data
                                    ? callback()
                                    : callback(new Error("用户名已被使用"));
                            } catch {}
                        },
                        trigger: "blur",
                    },
                ],
                passwordText: [
                    {
                        required: true,
                        message:
                            this.$CONFIG.LOC_STRINGS
                                .number_letter_combination_of_more_than_8_digits,
                        pattern: this.$CONFIG.STRINGS.RGX_PASSWORD,
                    },
                    {
                        validator: (rule, value, callback) => {
                            if (this.form.passwordText2 !== "") {
                                this.$refs.stepForm_0.validateField(
                                    "passwordText2"
                                );
                            }
                            callback();
                        },
                    },
                ],
                passwordText2: [
                    { required: true, message: "请再次输入密码" },
                    {
                        validator: (rule, value, callback) => {
                            if (value !== this.form.passwordText) {
                                callback(new Error("两次输入密码不一致"));
                            } else {
                                callback();
                            }
                        },
                    },
                ],
                agree: [
                    {
                        validator: (rule, value, callback) => {
                            if (!value) {
                                callback(new Error("请阅读并同意协议"));
                            } else {
                                callback();
                            }
                        },
                    },
                ],
            },
        };
    },
    mounted() {},
    methods: {
        async getYzm() {
            const formName = `stepForm_${this.stepActive}`;
            this.$refs[formName].validateField(
                ["verifySmsCodeReq.destMobile"],
                (valid) => {
                    if (valid) {
                        this.$refs.verify.show();
                    }
                }
            );
        },
        async captchaSuccess(obj) {
            this.disabled = true;
            try {
                await this.$API.sys_sms.sendSmsCode.post({
                    destMobile: this.form.verifySmsCodeReq.destMobile,
                    type: "register",
                    verifyCaptchaReq: obj,
                });
                this.$message.success("发送成功");
                this.time = 60;
                const t = setInterval(() => {
                    this.time -= 1;
                    if (this.time < 1) {
                        clearInterval(t);
                        this.disabled = false;
                        this.time = 0;
                    }
                }, 1000);
            } catch {
                this.disabled = false;
            }
        },
        pre() {
            this.stepActive -= 1;
        },
        next() {
            const formName = `stepForm_${this.stepActive}`;
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.stepActive += 1;
                } else {
                    return false;
                }
            });
        },
        async save() {
            const formName = `stepForm_${this.stepActive}`;
            await this.$refs[formName].validate(async (valid) => {
                if (valid) {
                    this.isSubmit = true;
                    try {
                        await this.$API.sys_user.register.post(this.form);
                        this.stepActive += 1;
                    } catch {}
                    this.isSubmit = false;
                } else {
                    return false;
                }
            });
        },
        goLogin() {
            this.$router.push({
                path: "/login",
            });
        },
    },
};
</script>
<style scoped></style>