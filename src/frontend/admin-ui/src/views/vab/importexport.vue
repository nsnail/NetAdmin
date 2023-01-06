<template>
	<el-main>
		<el-row :gutter="15">
			<el-col :lg="12">
				<el-card shadow="never" header="导入(使用mock,有50%几率导入失败)">
					<sc-file-import :apiObj="$API.common.importFile" templateUrl="http://www.scuiadmin/file.xlsx" @success="success"></sc-file-import>
					<sc-file-import :apiObj="$API.common.importFile" :data="{otherData:'demo'}" templateUrl="http://www.scuiadmin/file.xlsx" accept=".xls, .xlsx" :maxSize="30" tip="请上传小于或等于 30M 的 .xls, .xlsx 格式文件(自定义TIP)" @success="success">
						<template #default="{open}">
							<el-button type="primary" icon="sc-icon-upload" @click="open">导入(全配置)</el-button>
						</template>
						<template #uploader>
							<el-icon class="el-icon--upload"><sc-icon-file-excel /></el-icon>
							<div class="el-upload__text">
								将文件拖到此处或 <em>点击选择文件上传</em>
							</div>
						</template>
						<template #form="{formData}">
							<el-form-item label="覆盖已有数据">
								<el-switch v-model="formData.coverage" />
							</el-form-item>
							<el-form-item label="跳过错误数据">
								<el-switch v-model="formData.skipError" />
							</el-form-item>
						</template>
					</sc-file-import>
					<el-descriptions :column="1" border size="small" style="margin-top: 15px;">
						<el-descriptions-item label="apiObj" :width="200">Object 文件上传接口对象</el-descriptions-item>
						<el-descriptions-item label="data">Object 上传时附带的额外参数</el-descriptions-item>
						<el-descriptions-item label="accept">String 可选择文件类型，默认为".xls, .xlsx"</el-descriptions-item>
						<el-descriptions-item label="maxSize">Number 可选择文件大小，单位为M，默认为10</el-descriptions-item>
						<el-descriptions-item label="tip">String 上传框底下的提示语句，默认为"请上传小于或等于 {maxSize}M 的 {accept} 格式文件"</el-descriptions-item>
						<el-descriptions-item label="templateUrl">String 模板的下载URL</el-descriptions-item>
						<el-descriptions-item label="@success">事件 上传接口返回的事件，返回function(res, close)，执行close()将关闭窗口</el-descriptions-item>
						<el-descriptions-item label='#default="{open}"'>插糟 默认触发按钮插糟，返回open()打开窗口函数，可以绑定元素@click事件</el-descriptions-item>
						<el-descriptions-item label='#uploader'>插糟 自定义上传框插槽</el-descriptions-item>
						<el-descriptions-item label='#form="{formData}"'>插糟 自定义表单组件，插槽formData都将作为上传时附带的额外参数</el-descriptions-item>
					</el-descriptions>
				</el-card>
			</el-col>
			<el-col :lg="12">
				<el-card shadow="never" header="导出">
					<sc-file-export :apiObj="$API.common.exportFile"></sc-file-export>
					<sc-file-export :apiObj="$API.common.exportFile" fileName="人员列表(异步)" async>
						<template #default="{open}">
							<el-button type="primary" icon="sc-icon-download" @click="open">导出(异步)</el-button>
						</template>
					</sc-file-export>
					<sc-file-export :apiObj="$API.common.exportFile" blob fileName="人员列表" :data="{otherData:'demo'}" showData :column="column" :fileTypes="['xlsx','docx','pdf']">
						<template #default="{open}">
							<el-button type="primary" icon="sc-icon-download" @click="open">导出(blob文件流)</el-button>
						</template>
						<template #form="{formData}">
							<el-form-item label="导出条数">
								<el-select v-model="formData.limit" placeholder="Select">
									<el-option label="100条" value="100" />
									<el-option label="500条" value="500" />
									<el-option label="1000条" value="1000" />
									<el-option label="5000条" value="5000" />
									<el-option label="10000条" value="10000" />
								</el-select>
							</el-form-item>
						</template>
					</sc-file-export>
					<el-descriptions :column="1" border size="small" style="margin-top: 15px;">
						<el-descriptions-item label="apiObj" :width="200">Object 文件导出接口对象，通过apiObj.url请求文件</el-descriptions-item>
						<el-descriptions-item label="data">Object 上传时附带的额外参数(可为数据表格的过滤项)</el-descriptions-item>
						<el-descriptions-item label="showData">Boolean 是否显示附带的额外参数</el-descriptions-item>
						<el-descriptions-item label="async">Boolean 是否异步导出文件</el-descriptions-item>
						<el-descriptions-item label="fileName">String 下载文件名称，默认为当前时间戳</el-descriptions-item>
						<el-descriptions-item label="fileTypes">Array 可选择文件类型，默认为['xlsx']，组件将数组第一项当做已选项</el-descriptions-item>
						<el-descriptions-item label="column">Array 列配置，请求文件时将添加column为key的参数，值为prop逗号","分割的字符串</el-descriptions-item>
						<el-descriptions-item label="blob">Boolean 是否由游览器请求文件返回blob后提供下载</el-descriptions-item>
						<el-descriptions-item label="progress">Boolean blob开启后是否显示下载文件进度条，当服务器启用Gzip时，建议关闭，因为获取到的文件总数和下载总数不匹配。</el-descriptions-item>

						<el-descriptions-item label='#default="{open}"'>插糟 默认触发按钮插糟，返回open()打开窗口函数，可以绑定元素@click事件</el-descriptions-item>
						<el-descriptions-item label='#form="{formData}"'>插糟 自定义表单组件，插槽formData都将作为请求时附带的额外参数</el-descriptions-item>
					</el-descriptions>
				</el-card>
			</el-col>
		</el-row>
	</el-main>

	<el-dialog v-model="importErrDialogVisible" title="导入失败" :width="680" destroy-on-close @closed="()=>{importErrData={}}">
		<el-alert :title="`总条目数 ${importErrData.ok} ,其中有 ${importErrData.fail} 条格式不满足导入要求，请修改后再次操作。`" type="error" show-icon :closable="false"/>
		<div style="margin-top: 15px;">
			<el-table :data="importErrData.failList" border stripe max-height="270" style="width: 100%">
				<el-table-column prop="keyName" label="主键名" width="180" />
				<el-table-column prop="" label="状态" width="100" >
					<el-tag type="danger"><el-icon><el-icon-circle-close-filled/></el-icon> 失败</el-tag>
				</el-table-column>
				<el-table-column prop="reason" label="原因" />
			</el-table>
		</div>
		<template #footer>
			<el-button type="primary" @click="importErrDialogVisible=false">我知道了</el-button>
		</template>
	</el-dialog>

</template>

<script>
	import scFileImport from '@/components/scFileImport'
	import scFileExport from '@/components/scFileExport'

	export default {
		name: 'importexport',
		components: {
			scFileImport,
			scFileExport
		},
		data() {
			return {
				importErrDialogVisible: false,
				importErrData: {},
				column: [
					{
						label: "姓名",
						prop: "name"
					},
					{
						label: "性别",
						prop: "sex"
					},
					{
						label: "评分",
						prop: "num"
					},
					{
						label: "邮箱",
						prop: "email",
						hide: true
					},
					{
						label: "进度",
						prop: "progress"
					},
					{
						label: "注册时间",
						prop: "datetime"
					}
				]
			}
		},
		mounted() {

		},
		methods: {
			success(res, close){
				if(res.code==200){
					this.$alert("导入返回成功后，可后续操作，比如刷新表格等。执行回调函数close()可关闭上传窗口。", "导入成功", {
						type: "success",
						showClose: false,
						center: true
					})
					close()
				}else{
					//返回失败后的自定义操作，这里演示显示错误的条目
					this.importErrDialogVisible = true
					this.importErrData = res.data
				}

			}
		}
	}
</script>

<style>

</style>