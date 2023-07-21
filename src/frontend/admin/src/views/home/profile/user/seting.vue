<template>
    <el-card header="语言主题" shadow="never">
        <el-form ref="form" label-width="120px" style="margin-top: 20px">
            <el-form-item :label="$t('user.nightmode')">
                <el-switch
                    v-model="config.dark"
                    active-icon="el-icon-moon"
                    inactive-icon="el-icon-sunny"
                    inline-prompt
                ></el-switch>
                <div class="el-form-item-msg">
                    {{ $t("user.nightmode_msg") }}
                </div>
            </el-form-item>
            <el-form-item label="主题颜色">
                <el-color-picker
                    v-model="config.colorPrimary"
                    :predefine="colorList"
                    >></el-color-picker
                >
            </el-form-item>
            <el-form-item :label="$t('user.language')">
                <el-select v-model="config.lang">
                    <el-option label="简体中文" value="zh-cn"></el-option>
                    <el-option label="English" value="en"></el-option>
                </el-select>
                <div class="el-form-item-msg">
                    {{ $t("user.language_msg") }}
                </div>
            </el-form-item>
        </el-form>
    </el-card>
    <el-card header="个人设置" shadow="never" style="margin-top: 20px">
        <el-form ref="form" label-width="120px" style="margin-top: 20px">
            <el-form-item label="自动登出">
                <el-select v-model="config.autoExit">
                    <el-option :value="0" label="从不"></el-option>
                    <el-option :value="1" label="1分钟"></el-option>
                    <el-option :value="5" label="5分钟"></el-option>
                    <el-option :value="10" label="10分钟"></el-option>
                    <el-option :value="15" label="15分钟"></el-option>
                    <el-option :value="20" label="20分钟"></el-option>
                    <el-option :value="25" label="25分钟"></el-option>
                    <el-option :value="30" label="30分钟"></el-option>
                    <el-option :value="35" label="35分钟"></el-option>
                    <el-option :value="40" label="40分钟"></el-option>
                    <el-option :value="45" label="45分钟"></el-option>
                    <el-option :value="50" label="50分钟"></el-option>
                    <el-option :value="55" label="55分钟"></el-option>
                    <el-option :value="60" label="60分钟"></el-option>
                </el-select>
                <div class="el-form-item-msg">
                    自动登出设置将在下次登录时生效
                </div>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script>
import colorTool from "@/utils/color";

export default {
    data() {
        return {
            colorList: [
                "#06c755",
                "#009688",
                "#536dfe",
                "#ff5c93",
                "#c62f2f",
                "#fd726d",
            ],
            config: {
                lang: this.$TOOL.data.get("APP_LANG") || this.$CONFIG.LANG,
                dark: this.$TOOL.data.get("APP_DARK") || false,
                colorPrimary:
                    this.$TOOL.data.get("APP_COLOR") ||
                    this.$CONFIG.COLOR ||
                    "#06c755",
                autoExit: this.$TOOL.data.get("AUTO_EXIT") || 0,
            },
        };
    },
    watch: {
        "config.dark"(val) {
            if (val) {
                document.documentElement.classList.add("dark");
                this.$TOOL.data.set("APP_DARK", val);
            } else {
                document.documentElement.classList.remove("dark");
                this.$TOOL.data.remove("APP_DARK");
            }
        },
        "config.lang"(val) {
            this.$i18n.locale = val;
            this.$TOOL.data.set("APP_LANG", val);
        },
        "config.colorPrimary"(val) {
            if (!val) {
                val = "#06c755";
                this.config.colorPrimary = "#06c755";
            }
            document.documentElement.style.setProperty(
                "--el-color-primary",
                val
            );
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(
                    `--el-color-primary-light-${i}`,
                    colorTool.lighten(val, i / 10)
                );
            }
            for (let i = 1; i <= 9; i++) {
                document.documentElement.style.setProperty(
                    `--el-color-primary-dark-${i}`,
                    colorTool.darken(val, i / 10)
                );
            }
            this.$TOOL.data.set("APP_COLOR", val);
        },
        "config.autoExit"(val) {
            if (val === 0) {
                this.$TOOL.data.remove("AUTO_EXIT");
            } else {
                this.$TOOL.data.set("AUTO_EXIT", val);
            }
        },
    },
};
</script>

<style></style>