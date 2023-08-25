<template>
    <el-card :header="$t('主题样式')" class="mt-4" shadow="never">
        <el-form class="mt-4" label-width="10rem">
            <el-form-item :label="$t('黑夜模式')">
                <el-switch v-model="config.dark" active-icon="el-icon-moon" inactive-icon="el-icon-sunny" inline-prompt />
            </el-form-item>
            <el-form-item :label="$t('主题颜色')">
                <el-color-picker v-model="config.colorPrimary" :predefine="colorList" />
            </el-form-item>
            <el-form-item :label="$t('框架布局')">
                <el-select v-model="config.layout">
                    <el-option :label="$t('默认')" value="default"></el-option>
                    <el-option :label="$t('通栏')" value="header"></el-option>
                    <el-option :label="$t('经典')" value="menu"></el-option>
                    <el-option :label="$t('功能坞')" value="dock"></el-option>
                </el-select>
            </el-form-item>
            <el-form-item :label="$t('折叠菜单')">
                <el-switch v-model="config.menuIsCollapse"></el-switch>
            </el-form-item>
            <el-form-item :label="$t('手风琴菜单')">
                <el-switch v-model="config.menuUniqueOpened"></el-switch>
            </el-form-item>
            <el-form-item :label="$t('标签栏')">
                <el-switch v-model="config.layoutTags"></el-switch>
            </el-form-item>
        </el-form>
    </el-card>
    <el-card :header="$t('个人设置')" class="mt-4" shadow="never">
        <el-form class="mt-4" label-width="10rem">
            <el-form-item :label="$t('界面语言')">
                <el-select v-model="config.lang">
                    <el-option :label="$t('简体中文')" value="zh-cn" />
                    <el-option :label="$t('English')" value="en" />
                </el-select>
            </el-form-item>
            <el-form-item :label="$t('自动登出')">
                <el-select v-model="config.autoExit">
                    <el-option :label="$t('从不')" :value="0" />
                    <el-option :label="$t('1分钟')" :value="1" />
                    <el-option :label="$t('5分钟')" :value="5" />
                    <el-option :label="$t('10分钟')" :value="10" />
                    <el-option :label="$t('15分钟')" :value="15" />
                    <el-option :label="$t('20分钟')" :value="20" />
                    <el-option :label="$t('25分钟')" :value="25" />
                    <el-option :label="$t('30分钟')" :value="30" />
                    <el-option :label="$t('35分钟')" :value="35" />
                    <el-option :label="$t('40分钟')" :value="40" />
                    <el-option :label="$t('45分钟')" :value="45" />
                    <el-option :label="$t('50分钟')" :value="50" />
                    <el-option :label="$t('55分钟')" :value="55" />
                    <el-option :label="$t('60分钟')" :value="60" />
                </el-select>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script>
import colorTool from '@/utils/color'

export default {
    data() {
        return {
            colorList: ['#409EFF', '#009688', '#536dfe', '#ff5c93', '#c62f2f', '#fd726d'],
            config: {
                layout: this.$TOOL.data.get('LAYOUT') ?? this.$CONFIG.LAYOUT,
                menuIsCollapse: this.$TOOL.data.get('MENU_IS_COLLAPSE') ?? this.$CONFIG.MENU_IS_COLLAPSE,
                menuUniqueOpened: this.$TOOL.data.get('MENU_UNIQUE_OPENED') ?? this.$CONFIG.MENU_UNIQUE_OPENED,
                layoutTags: this.$TOOL.data.get('LAYOUT_TAGS') ?? this.$CONFIG.LAYOUT_TAGS,
                lang: this.$TOOL.data.get('APP_LANG') ?? this.$CONFIG.LANG,
                dark: this.$TOOL.data.get('APP_DARK') ?? false,
                colorPrimary: this.$TOOL.data.get('APP_COLOR') ?? this.$CONFIG.COLOR ?? '#409EFF',
                autoExit: this.$TOOL.data.get('AUTO_EXIT') ?? 0,
            },
        }
    },
    watch: {
        'config.dark'(val) {
            if (val) {
                document.documentElement.classList.add('dark')
                this.$TOOL.data.set('APP_DARK', val)
            } else {
                document.documentElement.classList.remove('dark')
                this.$TOOL.data.remove('APP_DARK')
            }
        },
        'config.layout'(val) {
            if (val) {
                this.$TOOL.data.set('LAYOUT', val)
                this.$store.commit('SET_layout', val)
            } else {
                this.$TOOL.data.remove('LAYOUT')
            }
        },
        'config.menuIsCollapse'(val) {
            if (typeof val === 'boolean') {
                this.$TOOL.data.set('MENU_IS_COLLAPSE', val)
                this.$store.commit('TOGGLE_menuIsCollapse')
            } else {
                this.$TOOL.data.remove('MENU_IS_COLLAPSE')
            }
        },
        'config.layoutTags'(val) {
            if (typeof val === 'boolean') {
                this.$TOOL.data.set('LAYOUT_TAGS', val)
                this.$store.commit('TOGGLE_layoutTags')
            } else {
                this.$TOOL.data.remove('LAYOUT_TAGS')
            }
        },
        'config.menuUniqueOpened'(val) {
            if (typeof val === 'boolean') {
                this.$TOOL.data.set('MENU_UNIQUE_OPENED', val)
            } else {
                this.$TOOL.data.remove('MENU_UNIQUE_OPENED')
            }
        },
        'config.lang'(val) {
            this.$i18n.locale = val
            this.$TOOL.data.set('APP_LANG', val)
        },
        'config.colorPrimary'(val) {
            if (!val) {
                val = '#409EFF'
                this.config.colorPrimary = '#409EFF'
            }
            document.documentElement.style.setProperty('--el-color-primary', val)
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(`--el-color-primary-light-${i}`, colorTool.lighten(val, i / 10))
            }
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(`--el-color-primary-dark-${i}`, colorTool.darken(val, i / 10))
            }
            this.$TOOL.data.set('APP_COLOR', val)
        },
        'config.autoExit'(val) {
            if (val === 0) {
                this.$TOOL.data.remove('AUTO_EXIT')
            } else {
                this.$TOOL.data.set('AUTO_EXIT', val)
            }
        },
    },
}
</script>

<style scoped></style>