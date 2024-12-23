<template>
    <div class="login_bg">
        <div class="login_main">
            <div class="login_config">
                <el-button :icon="config.dark ? 'el-icon-sunny' : 'el-icon-moon'" @click="configDark" circle type="info"></el-button>
                <el-dropdown @command="configLang" placement="bottom-end" trigger="click">
                    <el-button circle>
                        <svg
                            aria-hidden="true"
                            height="1em"
                            preserveAspectRatio="xMidYMid meet"
                            role="img"
                            viewBox="0 0 512 512"
                            width="1em"
                            xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M478.33 433.6l-90-218a22 22 0 0 0-40.67 0l-90 218a22 22 0 1 0 40.67 16.79L316.66 406h102.67l18.33 44.39A22 22 0 0 0 458 464a22 22 0 0 0 20.32-30.4zM334.83 362L368 281.65L401.17 362z"
                                fill="currentColor" />
                            <path
                                d="M267.84 342.92a22 22 0 0 0-4.89-30.7c-.2-.15-15-11.13-36.49-34.73c39.65-53.68 62.11-114.75 71.27-143.49H330a22 22 0 0 0 0-44H214V70a22 22 0 0 0-44 0v20H54a22 22 0 0 0 0 44h197.25c-9.52 26.95-27.05 69.5-53.79 108.36c-31.41-41.68-43.08-68.65-43.17-68.87a22 22 0 0 0-40.58 17c.58 1.38 14.55 34.23 52.86 83.93c.92 1.19 1.83 2.35 2.74 3.51c-39.24 44.35-77.74 71.86-93.85 80.74a22 22 0 1 0 21.07 38.63c2.16-1.18 48.6-26.89 101.63-85.59c22.52 24.08 38 35.44 38.93 36.1a22 22 0 0 0 30.75-4.9z"
                                fill="currentColor" />
                        </svg>
                    </el-button>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item v-for="item in lang" :class="{ selected: config.lang === item.value }" :command="item" :key="item.value"
                                >{{ item.name }}
                            </el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
            <div class="login-form">
                <div class="login-header">
                    <div class="logo">
                        <img :alt="$CONFIG.APP_NAME" class="logo-fill-color" src="@/assets/img/logo.svg" />
                        <label>{{ $CONFIG.APP_NAME }}</label>
                    </div>
                </div>
                <el-tabs>
                    <el-tab-pane :label="$t('账号登录')" lazy>
                        <password-form></password-form>
                    </el-tab-pane>
                    <el-tab-pane :label="$t('手机号登录')" lazy>
                        <phone-form></phone-form>
                    </el-tab-pane>
                </el-tabs>
            </div>
        </div>
    </div>
</template>

<script>
import passwordForm from './components/passwordForm'
import phoneForm from './components/phoneForm'
import $CONFIG from '@/config'

export default {
    computed: {
        $CONFIG() {
            return $CONFIG
        },
    },
    components: {
        passwordForm,
        phoneForm,
    },
    data() {
        return {
            config: {
                lang: this.$TOOL.data.get('APP_SET_LANG') || this.$CONFIG.APP_SET_LANG,
                dark: this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK,
            },
            lang: [
                {
                    name: '简体中文',
                    value: 'zh-cn',
                },
                {
                    name: 'English',
                    value: 'en',
                },
            ],
        }
    },
    watch: {
        'config.dark'(val) {
            if (val) {
                document.documentElement.classList.add('dark')
                this.$TOOL.data.set('APP_SET_DARK', val)
            } else {
                document.documentElement.classList.remove('dark')
                this.$TOOL.data.remove('APP_SET_DARK')
            }
        },
        'config.lang'(val) {
            this.$i18n.locale = val
            this.$TOOL.data.set('APP_SET_LANG', val)
        },
    },
    created: function () {
        this.$TOOL.cookie.remove('TOKEN')
        this.$TOOL.data.remove('USER_INFO')
        this.$TOOL.data.remove('MENU')
        this.$TOOL.data.remove('PERMISSIONS')
        this.$TOOL.data.remove('DASHBOARD_GRID')
        this.$TOOL.data.remove('APP_SET_HOME_GRID')
        this.$store.commit('clearViewTags')
        this.$store.commit('clearKeepLive')
        this.$store.commit('clearIframeList')
    },
    methods: {
        configDark() {
            this.config.dark = !this.config.dark
        },
        configLang(command) {
            this.config.lang = command.value
        },
    },
}
</script>

<style scoped>
.login_bg {
    width: 100%;
    height: 100%;
    background: var(--el-color-white);
    display: flex;
}

.login_main {
    flex: 1;
    overflow: auto;
    display: flex;
}

.login-form {
    width: 30rem;
    margin: auto;
}

.login-header {
    margin-bottom: 3rem;
}

.login-header .logo {
    display: flex;
    align-items: center;
}

.login-header .logo img {
    width: 3rem;
    height: 3rem;
    vertical-align: bottom;
    margin-right: 1rem;
}

.login-header .logo label {
    font-size: 2rem;
    font-weight: bold;
}

.login-form:deep(.login-forgot) {
    text-align: right;
}

.login-form:deep(.login-forgot) a {
    color: var(--el-color-primary);
}

.login-form:deep(.login-forgot) a:hover {
    color: var(--el-color-primary-light-3);
}

.login-form:deep(.login-reg) {
    font-size: 1.1rem;
    color: var(--el-text-color-primary);
}

.login-form:deep(.login-reg) a {
    color: var(--el-color-primary);
}

.login-form:deep(.login-reg) a:hover {
    color: var(--el-color-primary-light-3);
}

.login_config {
    position: absolute;
    top: 1.5rem;
    right: 1.5rem;
}

@media (max-width: 77rem) {
    .login_main {
        display: block;
    }

    .login_main .login_config {
        position: static;
        padding: 1.5rem 1.5rem 0 1.5rem;
        text-align: right;
    }

    .login-form {
        width: 100%;
        padding: 1.5rem 3rem;
    }
}
</style>