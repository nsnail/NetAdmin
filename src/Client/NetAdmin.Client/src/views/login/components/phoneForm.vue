<template>
    <el-form
        ref="loginForm"
        :model="form"
        :rules="rules"
        label-width="0"
        size="large"
        @keyup.enter="login"
    >
        <el-form-item prop="destMobile">
            <el-input
                v-model="form.destMobile"
                :placeholder="$t('login.mobilePlaceholder')"
                clearable
                maxlength="11"
                oninput="value=value.replace(/[^\d]/g,'')"
                prefix-icon="el-icon-iphone"
            >
                <template #prepend>+86</template>
            </el-input>
        </el-form-item>
        <el-form-item prop="code" style="margin-bottom: 35px">
            <div class="login-msg-yzm">
                <el-input
                    v-model="form.code"
                    :placeholder="$t('login.smsPlaceholder')"
                    clearable
                    maxlength="4"
                    oninput="value=value.replace(/[^\d]/g,'')"
                    prefix-icon="el-icon-message"
                ></el-input>
                <el-button :disabled="disabled" @click="getYzm"
                    >{{ this.$t("login.smsGet")
                    }}<span v-if="disabled"> ({{ time }})</span></el-button
                >
            </div>
        </el-form-item>
        <el-form-item>
            <el-button
                :loading="islogin"
                round
                style="width: 100%"
                type="primary"
                @click="login"
                >{{ $t("login.signIn") }}
            </el-button>
        </el-form-item>
        <div class="login-reg">
            {{ $t("login.noAccount") }}
            <router-link to="/user_register"
                >{{ $t("login.createAccount") }}
            </router-link>
        </div>
    </el-form>
    <Verify
        ref="verify"
        :imgSize="{ width: '310px', height: '155px' }"
        captchaType="blockPuzzle"
        mode="pop"
        @success="captchaSuccess"
    ></Verify>
</template>
<script>
import Verify from "@/views/login/components/verifition/Verify.vue";

export default {
    components: {
        Verify,
    },
    data() {
        return {
            form: {
                destMobile: "",
                code: "",
            },
            rules: {
                destMobile: [
                    {
                        required: true,
                        message:
                            this.$CONFIG.LOC_STRINGS
                                .mobile_phone_number_that_can_be_used_normally,
                        pattern: this.$CONFIG.STRINGS.RGX_MOBILE,
                    },
                ],
                code: [{ required: true, message: this.$t("login.smsError") }],
            },
            disabled: false,
            time: 0,
            islogin: false,
        };
    },
    mounted() {},
    methods: {
        async captchaSuccess(obj) {
            this.disabled = true;
            try {
                await this.$API.sys_sms.sendSmsCode.post({
                    destMobile: this.form.destMobile,
                    type: "login",
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
                this.loading = false;
            }
        },
        async getYzm() {
            const validate = await this.$refs.loginForm
                .validateField("destMobile")
                .catch(() => {});
            if (!validate) {
                return false;
            }
            this.$refs.verify.show();
        },
        async login() {
            const validate = await this.$refs.loginForm
                .validate()
                .catch(() => {});
            if (!validate) {
                return false;
            }
            this.islogin = true;
            try {
                await this.$API.sys_user.smsLogin.post(this.form);
            } catch {
                this.islogin = false;
                return false;
            }
            //获取用户信息
            try {
                const userInfo = await this.$API.sys_user.userInfo.post();
                this.$TOOL.data.set("USER_INFO", userInfo.data);
            } catch {
                this.islogin = false;
                return false;
            }
            //获取菜单
            try {
                const menu = await this.$API.sys_menu.userMenus.post();
                if (menu.data.length === 0) {
                    await this.$alert(
                        "当前用户无任何菜单权限，请联系系统管理员",
                        "无权限访问",
                        {
                            type: "error",
                            center: true,
                        }
                    );
                    this.islogin = false;
                    return false;
                }
                this.$TOOL.data.set("MENU", menu.data);
            } catch {
                this.islogin = false;
                return false;
            }
            // this.$TOOL.data.set("PERMISSIONS", menu.data.permissions)
            this.$TOOL.data.set("DASHBOARD_GRID", [
                "welcome",
                "ver",
                "time",
                "progress",
                "echarts",
                "about",
            ]);
            this.$router.replace({
                path: "/",
            });
            this.$message.success("Login Success 登录成功");
            this.islogin = false;
        },
    },
};
</script>
<style></style>