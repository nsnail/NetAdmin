<template>
	<router-view></router-view>
	<el-main>
		<el-row :gutter="15">
			<el-col :xl="6" :lg="6" :md="8" :sm="12" :xs="24" v-for="item in list" :key="item.title">
				<el-card shadow="hover" :body-style="{ padding: '0px' }">
					<div class="code-item">
						<div class="img" :style="{background: item.color}">
							<el-icon :style="`background-image: -webkit-linear-gradient(top left, #fff, ${item.color} 100px)`"><component :is="item.icon" /></el-icon>
						</div>
						<div class="title">
							<h2>{{item.title}}</h2>
							<p><el-input v-model="item.url" placeholder="代码文件夹路径"></el-input></p>
							<p><el-button @click="click(item.url)">生成</el-button></p>
						</div>
					</div>
				</el-card>
			</el-col>
		</el-row>
	</el-main>
</template>

<script>
	export default {
		name: 'autocode',
		data() {
			return {
				list: [
					{
						title: "生成前端代码",
						des: "配置型生成经典的增删改查列表",
						icon: "sc-icon-js",
						color: "blue",
						ver: "开发中",
						url: "d:\\Work\\SVN\\Tao\\NetAdmin\\src\\frontend\\admin-ui\\src\\api\\"
					}
				]
			}
		},
		methods: {
			async click(url){
				try{
					 await this.$API.sys_endpoint.generateJsCode.post(null, {params:{diskPath:url}})
					this.$message.success( '生成完毕' )
				}catch{

				}
			}
		}
	}
</script>

<style scoped>
	.el-card {margin-bottom: 15px;}
	.code-item {cursor: pointer;}
	.code-item .img {width: 100%;height: 150px;background: #09f;display:flex;align-items: center;justify-content: center;}
	.code-item .img i {font-size: 100px;color: #fff;background-image: -webkit-linear-gradient(top left, #fff, #09f 100px);-webkit-background-clip: text;-webkit-text-fill-color: transparent;}
	.code-item .title {padding:15px;}
	.code-item .title h2 {font-size: 16px;}
	.code-item .title h4 {font-size: 12px;color: #999;font-weight: normal;margin-top: 5px;}
	.code-item .title p {margin-top: 15px;}
</style>