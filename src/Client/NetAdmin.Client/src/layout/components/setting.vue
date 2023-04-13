<template>
    <el-form ref="form" label-position="left" label-width="120px" style="padding:0 20px;">
        <el-divider></el-divider>
        <el-form-item :label="$t('user.nightmode')">
            <el-switch v-model="dark"></el-switch>
        </el-form-item>
        <el-form-item :label="$t('user.language')">
            <el-select v-model="lang">
                <el-option label="简体中文" value="zh-cn"></el-option>
                <el-option label="English" value="en"></el-option>
            </el-select>
        </el-form-item>
        <el-divider></el-divider>
        <el-form-item label="主题颜色">
            <el-color-picker v-model="colorPrimary" :predefine="colorList">></el-color-picker>
        </el-form-item>
        <el-divider></el-divider>
        <el-form-item label="框架布局">
            <el-select v-model="layout" placeholder="请选择">
                <el-option label="默认" value="default"></el-option>
                <el-option label="通栏" value="header"></el-option>
                <el-option label="经典" value="menu"></el-option>
                <el-option label="功能坞" value="dock"></el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="折叠菜单">
            <el-switch v-model="menuIsCollapse"></el-switch>
        </el-form-item>
        <el-form-item label="标签栏">
            <el-switch v-model="layoutTags"></el-switch>
        </el-form-item>
        <el-divider></el-divider>
    </el-form>
</template>

<script>
import colorTool from '@/utils/color'

export default {
    data() {
        return {
            layout: this.$store.state.global.layout,
            menuIsCollapse: this.$store.state.global.menuIsCollapse,
            layoutTags: this.$store.state.global.layoutTags,
            lang: this.$TOOL.data.get('APP_LANG') || this.$CONFIG.LANG,
            dark: this.$TOOL.data.get('APP_DARK') || false,
            colorList: ['#06c755', '#009688', '#536dfe', '#ff5c93', '#c62f2f', '#fd726d'],
            colorPrimary: this.$TOOL.data.get('APP_COLOR') || this.$CONFIG.COLOR || '#06c755'
        }
    },
    watch: {
        layout(val) {
            this.$store.commit("SET_layout", val)
            this.$TOOL.data.set('LAYOUT', val)
        },
        menuIsCollapse(val) {
            this.$store.commit("TOGGLE_menuIsCollapse")
            this.$TOOL.data.set('MENU_IS_COLLAPSE', val)
        },
        layoutTags(val) {
            this.$store.commit("TOGGLE_layoutTags")
            this.$TOOL.data.set('LAYOUT_TAGS', val)
        },
        dark(val) {
            if (val) {
                document.documentElement.classList.add("dark")
                this.$TOOL.data.set("APP_DARK", val)
            } else {
                document.documentElement.classList.remove("dark")
                this.$TOOL.data.remove("APP_DARK")
            }
        },
        lang(val) {
            this.$i18n.locale = val
            this.$TOOL.data.set("APP_LANG", val);
        },
        colorPrimary(val) {
            if (!val) {
                val = '#06c755'
                this.colorPrimary = '#06c755'
            }
            document.documentElement.style.setProperty('--el-color-primary', val);
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(`--el-color-primary-light-${i}`, colorTool.lighten(val, i / 10));
            }
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(`--el-color-primary-dark-${i}`, colorTool.darken(val, i / 10));
            }
            this.$TOOL.data.set("APP_COLOR", val);
        }
    }
}
</script>

<style>
</style>