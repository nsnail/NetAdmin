<!--
 * @Descripttion: 模拟触发错误
 * @version: 1.0
 * @Author: sakuya
 * @Date: 2021年6月25日08:53:13
 * @LastEditors:
 * @LastEditTime:
-->

<template>
	<el-main>
		<el-alert title="通过VUE开放的errorHandler可以很方便的捕捉到处理异常, SCUI收集后可上报日志收集系统, 相关代码:@/utils/errorHandler.js" type="success" style="margin-bottom:20px;"></el-alert>
		<el-row :gutter="15">
			<el-col :lg="8">
				<el-card shadow="never" header="ReferenceError">
					<el-button type="danger" @click="ReferenceError">模拟触发</el-button>
				</el-card>
			</el-col>
			<el-col :lg="8">
				<el-card shadow="never" header="TypeError">
					<el-button type="danger" @click="TypeError">模拟触发</el-button>
				</el-card>
			</el-col>
			<el-col :lg="8">
				<el-card shadow="never" header="RangeError">
					<el-button type="danger" @click="RangeError">模拟触发</el-button>
				</el-card>
			</el-col>
			<el-col :lg="8">
				<el-card shadow="never" header="API 401">
					<el-button type="danger" @click="api401">模拟触发</el-button>
				</el-card>
			</el-col>
			<el-col :lg="8">
				<el-card shadow="never" header="API 404">
					<el-button type="danger" @click="api404">模拟触发</el-button>
				</el-card>
			</el-col>
			<el-col :lg="8">
				<el-card shadow="never" header="API 500">
					<el-button type="danger" @click="api500">模拟触发</el-button>
				</el-card>
			</el-col>
			<el-col :lg="8">
				<el-card shadow="never" header="Router 404">
					<el-button type="danger" @click="router404">模拟触发</el-button>
				</el-card>
			</el-col>
		</el-row>
		<el-alert title=":) 尝试模拟SyntaxError语法错误时,发现VUE编译失败,所以这不作模拟了" type="info" style="margin-top:20px;"></el-alert>
	</el-main>
</template>

<script>
	/* eslint-disable */
	//为了演示异常，跳过eslint
	export default {
		name: 'codebug',
		data() {
			return {

			}
		},
		mounted() {

		},
		methods: {
			ReferenceError(){
				console.log(obj);
			},
			TypeError(){
				const obj = null
				console.log(obj.a);
			},
			RangeError(){
				const n = 1
				n.toFixed(101)
			},
			async api401(){
				var data = await this.$API.demo.status.get(401)
			},
			async api404(){
				var data = await this.$API.demo.status.get(404)
			},
			async api500(){
				var data = await this.$API.demo.status.get(500)
			},
			router404(){
				this.$router.push('/page/404')
			}
		}
	}
</script>

<style>
</style>