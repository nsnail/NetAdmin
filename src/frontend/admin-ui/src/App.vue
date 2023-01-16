<template>
    <el-config-provider :locale="locale" :size="config.size" :zIndex="config.zIndex" :button="config.button">
        <router-view v-if="config.stringsLoaded &&  config.enumsLoaded"></router-view>
    </el-config-provider>
</template>

<script>
import colorTool from '@/utils/color'
import tool from "@/utils/tool";

export default {
    name: 'App',
    data() {
        return {
            config: {
                stringsLoaded: false,
                enumsLoaded: false,
                size: "default",
                zIndex: 2000,
                button: {
                    autoInsertSpace: false
                }
            }
        }
    },
    computed: {
        locale() {
            return this.$i18n.messages[this.$i18n.locale].el
        },
    },
    async created() {
        const [strings, locStrings, enums] = await Promise.all([this.$API.sys_constant.getStrings.post(),
            this.$API.sys_constant.getLocalizedStrings.post(),
            this.$API.sys_constant.getEnums.post()]);
        this.$CONFIG.STRINGS = strings.data;
        this.$CONFIG.LOC_STRINGS = locStrings.data;
        this.config.stringsLoaded = true;
        this.$CONFIG.ENUMS = enums.data;
        this.config.enumsLoaded = true;
        //设置主题颜色
        const app_color = this.$CONFIG.COLOR || this.$TOOL.data.get('APP_COLOR')
        if (app_color) {
            document.documentElement.style.setProperty('--el-color-primary', app_color);
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(`--el-color-primary-light-${i}`, colorTool.lighten(app_color, i / 10));
            }
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(`--el-color-primary-dark-${i}`, colorTool.darken(app_color, i / 10));
            }
        }

        let token = tool.cookie.get("TOKEN");
        if (token) {
            this.$API.sys_menu.userMenus.post().then(res => {
                    this.$TOOL.data.set("MENU", res.data)
                }
            )
        }
    }
}
</script>

<style lang="scss">
@import '@/style/style.scss';
</style>