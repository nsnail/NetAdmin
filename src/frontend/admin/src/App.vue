<template>
    <el-config-provider :button="config.button" :locale="locale" :size="config.size" :zIndex="config.zIndex">
        <router-view></router-view>
    </el-config-provider>
</template>

<script>
import colorTool from '@/utils/color'

export default {
    name: 'App',
    data() {
        return {
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
    async created() {
        //设置主题颜色
        const app_color = this.$TOOL.data.get('APP_COLOR') ?? this.$CONFIG.COLOR
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
        const layout = this.$TOOL.data.get('LAYOUT') ?? this.$CONFIG.LAYOUT
        if (layout) {
            this.$store.commit('SET_layout', layout)
        }

        //菜单是否折叠
        const menuIsCollapse = this.$TOOL.data.get('MENU_IS_COLLAPSE') ?? this.$CONFIG.MENU_IS_COLLAPSE
        if (menuIsCollapse !== this.$store.state.global.menuIsCollapse) {
            this.$store.commit('TOGGLE_menuIsCollapse')
        }

        //是否开启多标签
        const layoutTags = this.$TOOL.data.get('LAYOUT_TAGS') ?? this.$CONFIG.LAYOUT_TAGS
        if (layoutTags !== this.$store.state.global.layoutTags) {
            this.$store.commit('TOGGLE_layoutTags')
        }

        //是否开启手风琴菜单
        const menuUniqueOpened = this.$TOOL.data.get('MENU_UNIQUE_OPENED') ?? this.$CONFIG.MENU_UNIQUE_OPENED
        if (menuUniqueOpened !== this.$CONFIG.MENU_UNIQUE_OPENED) {
            this.$CONFIG.MENU_UNIQUE_OPENED = menuUniqueOpened
        }
    },
}
</script>

<style lang="scss">
@import '@/style/style.scss';
</style>