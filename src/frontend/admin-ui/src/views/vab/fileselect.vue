<template>
	<el-main>
		<el-row :gutter="15">
			<el-col :lg="18">
				<el-card shadow="never">
					<el-alert title="资源库选择器即将弃用,将不会维护更新,与1.7版本之后将移除此组件" type="error" style="margin-bottom:20px;"></el-alert>
					<sc-file-select v-model="file" :multiple="multiple" :hideUpload="hideUpload" :max="99" @submit="submit">
						<template #do>
							<el-button>自定义插槽</el-button>
						</template>
					</sc-file-select>
				</el-card>
			</el-col>
			<el-col :lg="6">
				<el-card shadow="never" header="参数和方法">
					<el-button type="primary" @click="multiple = !multiple">切换multiple</el-button>
					<el-button type="primary" @click="hideUpload = !hideUpload">切换hideUpload</el-button>
				</el-card>
			</el-col>
		</el-row>
	</el-main>
</template>

<script>
	import scFileSelect from '@/components/scFileSelect'

	export default {
		name: 'fileselect',
		components: {
			scFileSelect
		},
		data() {
			return {
				file: '',
				multiple: false,
				hideUpload: false,
				upload: '',
				upload2: ''
			}
		},
		mounted() {

		},
		methods: {
			submit(value){
				console.log(value)
				this.$message("返回值请查看F12控制台console.log()")
			}
		}
	}
</script>

<style scoped>

</style>