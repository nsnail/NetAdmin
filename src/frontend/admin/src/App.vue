<template>
    <el-config-provider :button="config.button" :locale="locale" :size="config.size" :zIndex="config.zIndex">
        <router-view v-if="isRouterAlive"></router-view>
        <na-version-updater />
    </el-config-provider>
</template>

<script>
import colorTool from '@/utils/color'
import naVersionUpdater from '@/components/naVersionUpdater/index.vue'
import UseTabs from '@/utils/useTabs'

export default {
    name: 'App',
    components: { naVersionUpdater },
    provide() {
        return {
            reload: this.reload,
        }
    },
    data() {
        return {
            isRouterAlive: true,
            config: {
                size: 'default',
                zIndex: 2000,
                button: {
                    autoInsertSpace: false,
                },
            },
        }
    },
    computed: {
        locale() {
            return this.$i18n.messages[this.$i18n.locale].el
        },
    },
    methods: {
        reload() {
            this.isRouterAlive = false
            this.$nextTick(function () {
                this.isRouterAlive = true
            })
        },
    },
    async created() {
        await this.$TOOL.data.downloadConfig()

        //设置深色模式
        if (this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK) {
            document.documentElement.classList.add('dark')
        } else {
            document.documentElement.classList.remove('dark')
        }

        //设置主题颜色
        const app_color = this.$TOOL.data.get('APP_SET_COLOR') || this.$CONFIG.APP_SET_COLOR
        if (app_color) {
            document.documentElement.style.setProperty('--el-color-primary', app_color)
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(`--el-color-primary-light-${i}`, colorTool.lighten(app_color, i / 10))
            }
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(`--el-color-primary-dark-${i}`, colorTool.darken(app_color, i / 10))
            }
        }

        //设置布局
        const layout = this.$TOOL.data.get('APP_SET_LAYOUT') || this.$CONFIG.APP_SET_LAYOUT
        if (layout) {
            this.$store.commit('SET_layout', layout)
        }

        //菜单是否折叠
        const menuIsCollapse = this.$TOOL.data.get('APP_SET_MENU_IS_COLLAPSE') || this.$CONFIG.APP_SET_MENU_IS_COLLAPSE
        if (menuIsCollapse !== this.$store.state.global.menuIsCollapse) {
            this.$store.commit('TOGGLE_menuIsCollapse')
        }

        //是否开启多标签
        const layoutTags = this.$TOOL.data.get('APP_SET_MULTI_TAGS') || this.$CONFIG.APP_SET_MULTI_TAGS
        if (layoutTags !== this.$store.state.global.layoutTags) {
            this.$store.commit('TOGGLE_layoutTags')
        }

        //是否开启手风琴菜单
        const menuUniqueOpened = this.$TOOL.data.get('APP_SET_MENU_UNIQUE_OPENED') || this.$CONFIG.APP_SET_MENU_UNIQUE_OPENED
        if (menuUniqueOpened !== this.$CONFIG.APP_SET_MENU_UNIQUE_OPENED) {
            this.$CONFIG.APP_SET_MENU_UNIQUE_OPENED = menuUniqueOpened
        }

        // 设置语言
        this.$i18n.locale = this.$TOOL.data.get('APP_SET_LANG') || this.$CONFIG.APP_SET_LANG

        document.onkeydown = (e) => {
            //ctrl + enter 触发主按钮点击事件
            if (e.ctrlKey && e.keyCode === 13) {
                document
                    .getElementsByClassName('el-dialog__footer')[0]
                    ?.getElementsByClassName('el-button--primary')[0]
                    ?.dispatchEvent(
                        new MouseEvent('click', {
                            view: window,
                            bubbles: true,
                            cancelable: false,
                        }),
                    )
            } else if (!e.altKey && !e.ctrlKey && !e.shiftKey) {
                for (const el of document.getElementsByClassName('sc-contextmenu__menu')[0]?.getElementsByTagName('li') ?? []) {
                    if (el.getElementsByClassName('sc-contextmenu__suffix')[0]?.innerText === String.fromCharCode(e.keyCode)) {
                        el.dispatchEvent(
                            new MouseEvent('click', {
                                view: window,
                                bubbles: true,
                                cancelable: false,
                            }),
                        )
                        break
                    }
                }
            } else if (e.altKey) {
                if (e.keyCode === 81) {
                    if (e.ctrlKey) {
                        UseTabs.closeOther()
                    } else {
                        UseTabs.close()
                    }
                } else if (e.keyCode === 65) {
                    document.getElementsByClassName('userbar-btn-search')[0]?.dispatchEvent(
                        new MouseEvent('click', {
                            view: window,
                            bubbles: true,
                            cancelable: false,
                        }),
                    )
                }
            }
        }
    },
}
</script>

<style lang="scss">
@use '@/style/style.scss' as *;
</style>