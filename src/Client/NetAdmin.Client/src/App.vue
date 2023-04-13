<template>
    <el-config-provider :button="config.button" :locale="locale" :size="config.size" :zIndex="config.zIndex">
        <router-view v-if="config.constantLoaded"></router-view>
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
                constantLoaded: false,
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
        const [strings, locStrings, enums, numbers] = await Promise.all([this.$API.sys_constant.getChars.post(),
            this.$API.sys_constant.getLocalizedStrings.post(),
            this.$API.sys_constant.getEnums.post(),
            this.$API.sys_constant.getNumbers.post()]);
        this.$CONFIG.STRINGS = strings.data;
        this.$CONFIG.LOC_STRINGS = locStrings.data;
        this.$CONFIG.ENUMS = enums.data;
        this.$CONFIG.NUMBERS = numbers.data;
        this.config.constantLoaded = true;
        //设置主题颜色
        const app_color = this.$TOOL.data.get('APP_COLOR') || this.$CONFIG.COLOR
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