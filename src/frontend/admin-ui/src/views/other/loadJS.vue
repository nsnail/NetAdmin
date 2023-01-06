<template>
	<el-container>
		<el-aside style="padding:15px;width: 400px;">
			<el-card shadow="never" header="异步加载JS">
				<div style="line-height: 1.5;font-size: 13px;">
					<p>演示了使用 @/utils/load 加载百度地图的JSAPI和它的GL库BMapGLLib</p>
					<p>当然也可以像传统网页一样加载任何JS和CSS，甚至可以是JQ。</p>
					<br/>
					<el-alert title="这是一项试验工具, 具有不稳定性" type="warning" show-icon :closable="false"/>
				</div>
			</el-card>
		</el-aside>
		<el-main class="nopadding">
			<div style="width: 100%;height: 100%;" id="container"></div>
		</el-main>
	</el-container>
</template>

<script>
	import { loadJS } from '@/utils/load'

	export default {
		name: 'loadJS',
		data() {
			return {

			}
		},
		mounted() {
			this.init()
		},
		methods: {
			async init(){
				var ak = "vxSbZuydZ42ktZCvXvy5xCai28OEVqUq"
				//loadJS (src, keyName, callbackName)
				//src 			必填，需要加载的URL路径
				//keyName 		必填，有2个作用，作为唯一KEY防止N次插入DOM；作为JS返回对象的key名，类似百度地图的BMapGL，如果没有则返回null
				//callbackName 	非必填，如果远程JS有callback，填写callback方法名称。
				//loadJS返回Promise，如果全局对象eslint发出警告 需要//eslint-disable-next-line，暂时关闭警告行
				//																							╭───这两个字符串要一致───╮
				var BMapGL = await loadJS(`//api.map.baidu.com/api?type=webgl&v=1.0&ak=${ak}&callback=BMapGLinit`, "BMapGL", "BMapGLinit")
				//像BMapGLLib就没有callback，无需第3个参数
				//var BMapGLLib = await loadJS("//api.map.baidu.com/library/LuShu/gl/src/LuShu_min.js", "BMapGLLib")


				var map = new BMapGL.Map('container')
				map.centerAndZoom(new BMapGL.Point(116.297611, 40.047363), 20);
				map.enableScrollWheelZoom()
				map.setTilt(55)
				map.setDisplayOptions({
					poiText: false,
					poiIcon: false
				})
			}
		}
	}
</script>

<style>
</style>