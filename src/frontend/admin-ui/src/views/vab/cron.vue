<template>
	<el-main>
		<el-alert title="SCUI独创, 区别于市面上的规则生成器, 对不是很理解Cron的操作员也可以选择设置的快捷录入" type="success" style="margin-bottom:20px;"></el-alert>
		<el-row :gutter="15">
			<el-col :lg="12">
				<el-card shadow="never" header="默认">
					<sc-cron v-model="v1" placeholder="请输入Cron定时规则" clearable></sc-cron>
				</el-card>
			</el-col>
			<el-col :lg="12">
				<el-card shadow="never" header="快捷扩展">
					<sc-cron v-model="v2" placeholder="请输入Cron定时规则" clearable :shortcuts="shortcuts"></sc-cron>
				</el-card>
			</el-col>
		</el-row>
	</el-main>
</template>

<script>
	import scCron from '@/components/scCron'

	export default {
		name: 'cron',
		components: {
			scCron
		},
		data() {
			return {
				v1: "",
				v2: "",
				shortcuts: [
					{
						text: "每天8点和12点 (自定义追加)",
						value: "0 0 8,12 * * ?"
					}
				]
			}
		},
		mounted() {

		},
		methods: {

		}
	}
</script>