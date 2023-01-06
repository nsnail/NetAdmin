<template>
	<el-main>
		<el-card shadow="never" header="async-validator内置">
			<el-form ref="form" :model="form" :rules="rules" label-width="100px" status-icon>
				<el-form-item label="必填" prop="required">
					<el-input v-model="form.required"></el-input>
				</el-form-item>
				<el-form-item label="长度" prop="length">
					<el-input v-model="form.length" placeholder="长度需为3位"></el-input>
				</el-form-item>
				<el-form-item label="类型" prop="type">
					<el-input v-model="form.type" placeholder="类型可为string number boolean array date url email等"></el-input>
				</el-form-item>
				<el-form-item label="范围" prop="range">
					<el-input v-model="form.range" placeholder="请填写范围在3至5位"></el-input>
				</el-form-item>
				<el-form-item label="枚举" prop="enum">
					<el-input v-model="form.enum" placeholder="请填写admin，user，guest其一"></el-input>
				</el-form-item>
				<el-form-item label="自定义" prop="custom">
					<el-input v-model="form.custom" placeholder="请填写数字1"></el-input>
				</el-form-item>
				<el-form-item label="异步验证" prop="async">
					<el-input v-model="form.async" placeholder="请输入SCUI最新版本号,form开启status-icon后 可以查看右侧状态"></el-input>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" @click="submit">验证所有字段</el-button>
					<el-button @click="resetForm('form')">Reset</el-button>
				</el-form-item>
			</el-form>
		</el-card>
		<el-card shadow="never" header="自定义" style="margin-top: 15px;">
			<el-form ref="form2" :model="form2" :rules="rules2" label-width="100px" status-icon>
				<el-form-item label="手机号码" prop="phone">
					<el-input v-model="form2.phone"></el-input>
				</el-form-item>
				<el-form-item label="车牌号码" prop="cars">
					<el-input v-model="form2.cars"></el-input>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" @click="submit2">验证所有字段</el-button>
					<el-button @click="resetForm('form2')">Reset</el-button>
				</el-form-item>
			</el-form>
			<el-alert title="自定义验证可在 @/utils/verificate.js 中不断扩展业务需要的验证. 记得要 import 进来" type="success" />
		</el-card>
	</el-main>
</template>

<script>
	import { verifyPhone, verifyCars } from '@/utils/verificate'

	export default {
		name: 'verificate',
		data() {
			return {
				form: {

				},
				rules: {
					required: [
						{ required: true, message: '请填写' }
					],
					length: [
						{ required: true, len: 3, message: '长度需为3位' }
					],
					type: [
						{ required: true, type: 'email', message: '类型需为email' }
					],
					range: [
						{ required: true, min: 3, max: 5, message: '范围在3至5位' }
					],
					enum: [
						{ required: true, type: 'enum', enum: ['admin', 'user', 'guest'], message: '请填写admin，user，guest其一' }
					],
					custom: [
						{ required: true, validator: (rule, value)=>{return value === '1'}, message: '请填写数字1' , trigger:'blur'}
					],
					async: [
						{
							required: true,
							validator: (rule, value, callback)=>{
								this.$API.demo.ver.get({value: value}).then(res => {
									if(res.data != value){
										return callback(new Error('请输入SCUI最新版本号：'+res.data))
									}
									callback()
								})
							},
							trigger:'blur'
						}
					]
				},
				form2: {

				},
				rules2: {
					phone: [
						{ required: true, message: '请输入姓名' },
						{ validator: verifyPhone, trigger:'blur' }
					],
					cars: [
						{ required: true, validator: verifyCars, trigger:'blur' }
					]
				}
			}
		},
		mounted() {

		},
		methods: {
			submit(){
				this.$refs.form.validate(async (valid) => {
					if (valid) {
						this.$message.success("全部验证通过")
					}else{
						return false;
					}
				})
			},
			submit2(){
				this.$refs.form2.validate(async (valid) => {
					if (valid) {
						this.$message.success("全部验证通过")
					}else{
						return false;
					}
				})
			},
			resetForm(ref){
				this.$refs[ref].resetFields()
			}
		}
	}
</script>

<style>

</style>