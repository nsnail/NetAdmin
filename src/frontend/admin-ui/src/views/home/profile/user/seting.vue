<template>
	<el-card shadow="never" header="语言主题">
		<el-form ref="form" label-width="120px" style="margin-top:20px;">
			<el-form-item :label="$t('user.nightmode')">
				<el-switch v-model="config.dark" inline-prompt active-icon="el-icon-moon" inactive-icon="el-icon-sunny"></el-switch>
				<div class="el-form-item-msg">{{ $t('user.nightmode_msg') }}</div>
			</el-form-item>
			<el-form-item label="主题颜色">
				<el-color-picker v-model="config.colorPrimary" :predefine="colorList">></el-color-picker>
			</el-form-item>
			<el-form-item :label="$t('user.language')">
				<el-select v-model="config.lang">
					<el-option label="简体中文" value="zh-cn"></el-option>
					<el-option label="English" value="en"></el-option>
				</el-select>
				<div class="el-form-item-msg">{{ $t('user.language_msg') }}</div>
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
					lang: this.$TOOL.data.get('APP_LANG') || this.$CONFIG.LANG,
					dark: this.$TOOL.data.get('APP_DARK') || false,
					colorPrimary: this.$TOOL.data.get('APP_COLOR') || this.$CONFIG.COLOR || '#409EFF'
				}
			}
		},
		watch:{
			'config.dark'(val){
				if(val){
					document.documentElement.classList.add("dark")
					this.$TOOL.data.set("APP_DARK", val)
				}else{
					document.documentElement.classList.remove("dark")
					this.$TOOL.data.remove("APP_DARK")
				}
			},
			'config.lang'(val){
				this.$i18n.locale = val
				this.$TOOL.data.set("APP_LANG", val);
			},
			'config.colorPrimary'(val){
				document.documentElement.style.setProperty('--el-color-primary', val);
				for (let i = 1; i <= 9; i++) {
					document.documentElement.style.setProperty(`--el-color-primary-light-${i}`, colorTool.lighten(val,i/10));
				}
				for (let i = 1; i <= 9; i++) {
					document.documentElement.style.setProperty(`--el-color-primary-dark-${i}`, colorTool.darken(val,i/10));
				}
				this.$TOOL.data.set("APP_COLOR", val);
			}
		},
	}
</script>

<style>
</style>