<template>
    <el-form
        ref="loginForm"
        :model="form"
        :rules="rules"
        label-width="0"
        size="large"
        @keyup.enter="login"
    >
        <el-form-item prop="account">
            <el-input
                v-model="form.account"
                :placeholder="$t('login.userPlaceholder')"
                clearable
                maxlength="16"
                prefix-icon="el-icon-user"
            >
            </el-input>
        </el-form-item>
        <el-form-item prop="password">
            <el-input
                v-model="form.password"
                :placeholder="$t('login.PWPlaceholder')"
                clearable
                maxlength="16"
                prefix-icon="el-icon-lock"
                show-password
            ></el-input>
        </el-form-item>
        <el-form-item style="margin-bottom: 10px">
            <el-col :span="12">
                <el-checkbox
                    v-model="form.autologin"
                    :label="$t('login.rememberMe')"
                ></el-checkbox>
            </el-col>
            <el-col :span="12" class="login-forgot">
                <router-link to="/reset_password"
                    >{{ $t("login.forgetPassword") }}？
                </router-link>
            </el-col>
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
</template>

<script>
export default {
    data() {
        return {
            form: {
                account: "",
                password: "",
                autologin: false,
            },
            rules: {
                account: [
                    {
                        required: true,
                        message: this.$t("login.userError"),
                        trigger: "blur",
                    },
                ],
                password: [
                    {
                        required: true,
                        message: this.$t("login.PWError"),
                        trigger: "blur",
                    },
                ],
            },
            islogin: false,
        };
    },
    watch: {},
    mounted() {},
    methods: {
        async login() {
            const validate = await this.$refs.loginForm
                .validate()
                .catch(() => {});
            if (!validate) {
                return false;
            }
            this.islogin = true;
            //获取token
            try {
                await this.$API.sys_user.pwdLogin.post(this.form);
            } catch {
                this.islogin = false;
                return false;
            }
            //获取用户信息
            try {
                const userInfo = await this.$API.sys_user.userInfo.post();
                this.$TOOL.data.set("USER_INFO", userInfo.data);
                let permissions = [];
                let ignorePermissionControl = false;
                userInfo.data.roles.forEach((x) => {
                    permissions = permissions.concat(x.apiIds);
                    if (x.ignorePermissionControl) {
                        ignorePermissionControl = true;
                    }
                });
                if (ignorePermissionControl) permissions = ["*/*/*"];
                this.$TOOL.data.set("PERMISSIONS", permissions);
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