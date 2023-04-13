<template>
    <common-page title="重置密码">
        <el-steps :active="stepActive" finish-status="success" simple>
            <el-step title="填写新密码" />
            <el-step title="完成重置" />
        </el-steps>
        <el-form
            v-if="stepActive === 0"
            ref="form"
            :label-width="120"
            :model="form"
            :rules="rules"
            size="large"
        >
            <el-form-item label="手机号码" prop="verifySmsCodeReq.destMobile">
                <el-input
                    v-model="form.verifySmsCodeReq.destMobile"
                    clearable
                    maxlength="11"
                    oninput="value=value.replace(/[^\d]/g,'')"
                    placeholder="请输入手机号码"
                ></el-input>
            </el-form-item>
            <el-form-item label="短信验证码" prop="verifySmsCodeReq.code">
                <div class="yzm">
                    <el-input
                        v-model="form.verifySmsCodeReq.code"
                        clearable
                        maxlength="4"
                        oninput="value=value.replace(/[^\d]/g,'')"
                        placeholder="请输入4位短信验证码"
                    ></el-input>
                    <el-button :disabled="disabled" @click="getYzm"
                        >获取验证码<span v-if="disabled"> ({{ time }})</span>
                    </el-button>
                </div>
            </el-form-item>
            <el-form-item label="新密码" prop="passwordText">
                <el-input
                    v-model="form.passwordText"
                    clearable
                    maxlength="16"
                    placeholder="请输入新密码"
                    show-password
                ></el-input>
                <div class="el-form-item-msg">
                    请输入包含英文、数字的8位以上密码
                </div>
            </el-form-item>
            <el-form-item label="确认新密码" prop="passwordText2">
                <el-input
                    v-model="form.passwordText2"
                    clearable
                    maxlength="16"
                    placeholder="请再一次输入新密码"
                    show-password
                ></el-input>
            </el-form-item>
            <el-form-item>
                <el-button v-loading="isSubmit" type="primary" @click="save"
                    >提交
                </el-button>
            </el-form-item>
        </el-form>
        <el-result
            v-if="stepActive === 1"
            icon="success"
            sub-title="请牢记自己的新密码,返回登录后使用新密码登录"
            title="密码重置成功"
        >
            <template #extra>
                <el-button type="primary" @click="backLogin"
                    >返回登录
                </el-button>
            </template>
        </el-result>
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
import commonPage from "./components/commonPage";
import Verify from "@/views/login/components/verifition/Verify.vue";

export default {
    components: {
        Verify,
        commonPage,
    },
    data() {
        return {
            isSubmit: false,
            stepActive: 0,
            form: {
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
                    ],
                    code: [{ required: true, message: "请输入短信验证码" }],
                },
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
                                this.$refs.form.validateField("passwordText2");
                            }
                            callback();
                        },
                    },
                ],
                passwordText2: [
                    { required: true, message: "请再次输入新的密码" },
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
            },
            disabled: false,
            time: 0,
        };
    },
    mounted() {},
    methods: {
        async captchaSuccess(obj) {
            this.disabled = true;
            try {
                await this.$API.sys_sms.sendSmsCode.post({
                    destMobile: this.form.verifySmsCodeReq.destMobile,
                    type: "resetPassword",
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
        async getYzm() {
            const validate = await this.$refs.form
                .validateField("verifySmsCodeReq.destMobile")
                .catch(() => {});
            if (!validate) {
                return false;
            }
            this.$refs.verify.show();
        },
        async save() {
            const validate = await this.$refs.form.validate().catch(() => {});
            if (!validate) {
                return false;
            }
            this.isSubmit = true;
            try {
                await this.$API.sys_user.resetPassword.post(this.form);
                this.stepActive = 1;
            } catch {}
            this.isSubmit = false;
        },
        backLogin() {
            this.$router.push({
                path: "/login",
            });
        },
    },
};
</script>
<style scoped></style>