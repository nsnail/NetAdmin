<template>
	<el-main class="sc-page">
		<el-page-header content="定宽页面" />
		<el-alert class="page-msg" description="<el-main>添加定宽样式 .sc-page  即可定宽, 固定1200px宽度, 适用于一些无法自适应的页面, 类似报表等需要固定宽度的页面. 其实定宽页面更受C端用户接受和习惯" type="warning" show-icon :closable="false" />
		<el-card shadow="never" header="所有主机">
			<div class="page-buttons">
				<el-button type="primary" style="width: 180px;">创建云主机</el-button>
				<el-button type="primary" text>管理秘钥</el-button>
				<el-button type="primary" text>黑名单</el-button>
			</div>
			<scTable ref="table" :data="data" height="auto" paginationLayout="total, prev, pager, next" hideDo>
				<el-table-column label="主机名称" prop="name" min-width="240"></el-table-column>
				<el-table-column label="外网ip" prop="ip" width="200"></el-table-column>
				<el-table-column label="系统盘" prop="c" width="150"></el-table-column>
				<el-table-column label="数据盘" prop="d" width="150"></el-table-column>
				<el-table-column label="业务状态" prop="zt" width="150">
					<el-tag type="success">运行中</el-tag>
				</el-table-column>
				<el-table-column label="操作" width="70">
					<el-dropdown>
						<el-button icon="el-icon-more" size="small"></el-button>
						<template #dropdown>
							<el-dropdown-menu>
								<el-dropdown-item>配额</el-dropdown-item>
								<el-dropdown-item divided>重启</el-dropdown-item>
								<el-dropdown-item >停机</el-dropdown-item>
								<el-dropdown-item divided>释放主机</el-dropdown-item>
							</el-dropdown-menu>
						</template>
					</el-dropdown>
				</el-table-column>
			</scTable>
		</el-card>
	</el-main>
</template>

<script>
	export default {
		name: 'widthlist',
		data(){
			return {
				data: [
					{
						name: "WIN-server-202256324",
						ip: "211.187.11.18",
						c: "80G",
						d: "2048G",
						out: "1"
					},
					{
						name: "WIN-server-20210312081254",
						ip: "211.187.41.56",
						c: "80G",
						d: "2048G",
						out: "1"
					}
				]
			}
		}
	}
</script>

<style scoped>
	.page-msg {margin: 15px 0;}
	.page-buttons {margin-bottom: 20px;}
</style>