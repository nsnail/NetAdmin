<template>
	<el-container>
		<el-header>
			<div class="left-panel">
				<el-button type="primary" icon="el-icon-plus"></el-button>
				<el-button type="danger" plain icon="el-icon-delete"></el-button>
			</div>
			<div class="right-panel">
				<el-alert title="请在右下角第二个按钮体验自定义列" type="warning" :closable="false"/>
			</div>
		</el-header>
		<el-main class="nopadding">
			<scTable ref="table" tableName="listCustomColumn" :apiObj="list.apiObj" :column="list.column" row-key="id" stripe>
				<el-table-column type="selection" width="50"></el-table-column>
				<template #progress="scope">
					<el-progress :percentage="scope.row.progress" />
				</template>
			</scTable>
		</el-main>
	</el-container>
</template>

<script>
	export default {
		name: 'tableCustomColumn',
		data() {
			return {
				list: {
					apiObj: this.$API.demo.list,
					column: [
						{
							label: "姓名",
							prop: "name",
							width: "150"
						},
						{
							label: "性别",
							prop: "sex",
							width: "150",
							filters: [{text: '男', value: '男'}, {text: '女', value: '女'}]
						},
						{
							label: "邮箱",
							prop: "email",
							width: "250",
							hide: true
						},
						{
							label: "评分",
							prop: "num",
							width: "150",
							sortable: true
						},
						{
							label: "进度",
							prop: "progress",
							width: "250",
							sortable: true
						},
						{
							label: "注册时间",
							prop: "datetime",
							width: "150",
							sortable: true
						}
					]
				}
			}
		}
	}
</script>

<style>
</style>