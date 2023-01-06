<template>
	<el-main>
		<el-card shadow="never" header="文件示例">
			<sc-upload-file v-model="fileurl" :limit="3" :data="{otherData:'demo'}" tip="最多上传3个文件,单个文件不要超过10M,请上传xlsx/docx格式文件">
				<el-button type="primary" icon="el-icon-upload">上传附件</el-button>
			</sc-upload-file>
		</el-card>

		<el-card shadow="never" header="文件示例(值为对象数组,适合保存原始文件名)">
			<sc-upload-file v-model="fileurlArr" :limit="3" tip="最多上传3个文件,单个文件不要超过10M,请上传xlsx/docx格式文件">
				<el-button type="primary" icon="el-icon-upload">上传附件</el-button>
			</sc-upload-file>
		</el-card>

		<el-card shadow="never" header="图片卡片示例(已开启拖拽排序)">
			<sc-upload-multiple v-model="fileurl2" draggable :limit="3" tip="最多上传3个文件,单个文件不要超过10M,请上传图像格式文件"></sc-upload-multiple>
		</el-card>

		<el-card shadow="never" header="单图像示例">
			<el-space wrap :size="8">
				<sc-upload v-model="fileurl3"></sc-upload>
				<sc-upload v-model="fileurl4" title="自定义标题" icon="el-icon-picture"></sc-upload>
				<sc-upload v-model="fileurl5" :apiObj="uploadApi" accept="image/jpg,image/png" :on-success="success" :width="220">
					<div class="custom-empty">
						<el-icon><el-icon-upload /></el-icon>
						<p>自定义插槽</p>
					</div>
				</sc-upload>
				<sc-upload v-model="fileurl6" round icon="el-icon-avatar" title="开启圆形"></sc-upload>
				<sc-upload v-model="fileurl7" title="开启剪裁" :cropper="true" :compress="1" :aspectRatio="1/1"></sc-upload>
			</el-space>
		</el-card>



		<el-card shadow="never" header="在验证表单中使用">
			<el-form ref="ruleForm" :model="form" :rules="rules" label-width="100px">
				<el-form-item label="身份证" required>
					<el-space wrap :size="8">
						<el-form-item prop="file1">
							<sc-upload v-model="form.file1" title="人像面"></sc-upload>
						</el-form-item>
						<el-form-item prop="file2">
							<sc-upload v-model="form.file2" title="国徽面"></sc-upload>
						</el-form-item>
					</el-space>
				</el-form-item>
				<el-form-item label="其他凭证" prop="file3">
					<sc-upload-multiple v-model="form.file3"></sc-upload-multiple>
				</el-form-item>
				<el-form-item label="附件" prop="file4">
					<sc-upload-file v-model="form.file4" :limit="1" drag>
						<el-icon class="el-icon--upload"><el-icon-upload-filled /></el-icon>
						    <div class="el-upload__text">
						      Drop file here or <em>click to upload</em>
						    </div>
					</sc-upload-file>
				</el-form-item>
				<el-form-item label="日期" prop="date">
					<el-date-picker type="date" placeholder="选择日期" v-model="form.date"></el-date-picker>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" @click="submitForm">保存</el-button>
				    <el-button @click="resetForm">重置</el-button>
				</el-form-item>
			</el-form>
		</el-card>

	</el-main>
</template>

<script>
	export default {
		name: 'upload',
		data() {
			return {
				uploadApi: this.$API.common.upload,
				fileurlArr: [
					{
						name: '销售合同模板.xlsx',
						url: 'http://www.scuiadmin.com/files/220000198611262243.xlsx'
					},
					{
						name: '企业员工联系方式.xlsx',
						url: 'http://www.scuiadmin.com/files/350000201004261875.xlsx',
					}
				],
				fileurl: "http://www.scuiadmin.com/files/220000198611262243.xlsx,http://www.scuiadmin.com/files/350000201004261875.xlsx",
				fileurl2: "img/auth_banner.jpg,img/avatar3.gif",
				fileurl3: "img/auth_banner.jpg",
				fileurl4: "",
				fileurl5: "",
				fileurl6: "",
				fileurl7: "",
				form: {
					file1: "",
					file2: "",
					file3: "",
					file4: "",
					date: ""
				},
				rules: {
					file1: [
						{required: true, message: '请上传', trigger: 'change'}
					],
					file2: [
						{required: true, message: '请上传', trigger: 'change'}
					],
					file3: [
						{required: true, message: '请上传', trigger: 'change'}
					],
					file4: [
						{required: true, message: '请上传附件', trigger: 'change'}
					],
					date: [
						{required: true, message: '请选择日期', trigger: 'change'}
					]
				}
			}
		},
		methods: {
			success(response){
				this.$alert(`success函数钩子，可用于类似OCR返回信息，return false后阻止后续执行，回调参数打开控制台查看`, {
					title: "提示",
					type: "success"
				})
				console.log(response);
				return false;
			},
			submitForm(){
				this.$refs.ruleForm.validate((valid) => {
					if (valid) {
						alert('请看控制台输出');
						console.log(this.form);
					}else{
						return false;
					}
				})
			},
			resetForm(){
				this.$refs.ruleForm.resetFields();
			}
		}
	}
</script>

<style scoped>
	.el-card+.el-card {margin-top: 15px;}
	.imglist .el-col+.el-col {margin-left: 8px;}

	.custom-empty {width: 100%;height: 100%;display: flex;flex-direction: column;align-items: center;justify-content: center;background: #8c939d;border-radius:5px;}
	.custom-empty i {font-size: 30px;color: #fff;}
	.custom-empty p {font-size: 12px;font-weight: normal;color: #fff;margin-top: 10px;}
</style>