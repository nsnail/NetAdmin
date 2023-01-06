<template>
	<el-container>
		<el-aside width="200px">
			<el-container>
				<el-header>
					<el-input placeholder="输入关键字进行过滤" v-model="groupFilterText" clearable></el-input>
				</el-header>
				<el-main class="nopadding">
					<el-tree ref="group" class="menu" node-key="id" :data="groupData" :current-node-key="''" :highlight-current="true" :expand-on-click-node="false" :filter-node-method="groupFilterNode" @node-click="groupClick"></el-tree>
				</el-main>
			</el-container>
		</el-aside>
		<el-container>
			<el-header>
				<div class="left-panel">
					<el-button type="primary" icon="el-icon-plus"></el-button>
					<el-button type="danger" plain icon="el-icon-delete"></el-button>
				</div>
				<div class="right-panel">
					<div class="right-panel-search">
						<el-input v-model="search.keyword" placeholder="关键词" clearable></el-input>
						<el-button type="primary" icon="el-icon-search" @click="upsearch"></el-button>
					</div>
				</div>
			</el-header>
			<el-main class="nopadding">
				<scTable ref="table" :apiObj="list.apiObj" row-key="id" stripe>
					<el-table-column type="selection" width="50"></el-table-column>
					<el-table-column label="姓名" prop="name" width="150"></el-table-column>
					<el-table-column label="性别" prop="sex" width="150"></el-table-column>
					<el-table-column label="邮箱" prop="email" width="250"></el-table-column>
					<el-table-column label="评分" prop="num" width="150"></el-table-column>
					<el-table-column label="注册时间" prop="datetime" width="150" sortable></el-table-column>
					<el-table-column label="操作" fixed="right" align="right" width="160">
						<template #default>
							<el-button-group>
								<el-button text type="primary" size="small">查看</el-button>
								<el-button text type="primary" size="small">编辑</el-button>
								<el-button text type="primary" size="small">删除</el-button>
							</el-button-group>
						</template>
					</el-table-column>
				</scTable>
			</el-main>
		</el-container>
	</el-container>
</template>

<script>
	export default {
		name: 'listTree',
		data() {
			return {
				groupFilterText: "",
				groupData: [
					{
						id: "",
						label: "所有"
					},
					{
						id: "1",
						label: "华东总部",
						children: [
							{
								id: "11",
								label: "售前客服部"
							},
							{
								id: "12",
								label: "技术研发部"
							},
							{
								id: "13",
								label: "行政人事部"
							}
						]
					},
					{
						id: "2",
						label: "华难总部",
						children: [
							{
								id: "21",
								label: "售前客服部"
							},
							{
								id: "22",
								label: "技术研发部"
							}
						]
					}
				],
				list: {
					apiObj: this.$API.demo.list
				},
				search: {
					keyword: ""
				}
			}
		},
		watch: {
			groupFilterText(val) {
				this.$refs.group.filter(val);
			}
		},
		methods: {
			//树过滤
			groupFilterNode(value, data){
				if (!value) return true;
				return data.label.indexOf(value) !== -1;
			},
			//树点击事件
			groupClick(data){
				var params = {
					groupId: data.id
				}
				this.$refs.table.reload(params)
			},
			//搜索
			upsearch(){
				this.$refs.table.upData(this.search)
			},
		}
	}
</script>

<style>
</style>