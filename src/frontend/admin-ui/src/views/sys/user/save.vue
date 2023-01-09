<template>
	<el-dialog :title="titleMap[mode]" v-model="visible" :width="500" destroy-on-close @closed="$emit('closed')">
		<el-form :model="form" :rules="rules" :disabled="mode=='show'" ref="dialogForm" label-width="100px" label-position="left">
			<el-form-item label="头像" prop="avatar">
				<sc-upload v-model="form.avatar" title="上传头像"></sc-upload>
			</el-form-item>
			<el-form-item label="登录账号" prop="userName">
				<el-input v-model="form.userName" placeholder="用于登录系统" clearable></el-input>
			</el-form-item>
			<el-form-item label="手机号" prop="mobile">
				<el-input v-model="form.mobile" placeholder="请输入手机号" clearable></el-input>
			</el-form-item>
			<template v-if="mode=='add'">
				<el-form-item label="登录密码" prop="passwordText">
					<el-input type="password" v-model="form.passwordText" clearable show-password></el-input>
				</el-form-item>
				<el-form-item label="确认密码" prop="passwordText2">
					<el-input type="password" v-model="form.passwordText2" clearable show-password></el-input>
				</el-form-item>
			</template>

			<el-form-item label="所属部门" prop="deptId">
				<el-cascader v-model="form.deptId" :options="depts" :props="deptsProps" clearable
							 style="width: 100%;"></el-cascader>
			</el-form-item>
			<el-form-item label="所属角色" prop="roleIds">
				<el-select v-model="form.roleIds" multiple filterable style="width: 100%">
					<el-option v-for="item in roles" :key="item.id" :label="item.label" :value="item.id"/>
				</el-select>
			</el-form-item>

			<template v-if="mode!='add'">
				<el-form-item label="启用" prop="enabled">
					<el-switch v-model="form.enabled"></el-switch>
				</el-form-item>
			</template>
		</el-form>
		<template #footer>
			<el-button @click="visible=false" >取 消</el-button>
			<el-button v-if="mode!='show'" type="primary" :loading="isSaveing" @click="submit()">保 存</el-button>
		</template>
	</el-dialog>
</template>

<script>

	export default {
		emits: ['success', 'closed'],
		data() {
			return {
				mode: "add",
				titleMap: {
					add: '新增用户',
					edit: '编辑用户',
					show: '查看'
				},
				visible: false,
				isSaveing: false,
				//表单数据
				form: {
					mobile: null,
					id:"",
					userName: "",
					avatar: null,
					name: "",
					deptId: "",
					roleIds: []
				},
				//验证规则
				rules: {
					userName: [
						{required: true, message: this.$CONFIG.LOC_STRINGS.MORE_THAN_4_DIGITS_ALPHANUMERIC_UNDERLINE,
							pattern:this.$CONFIG.STRINGS.RGX_USERNAME}
					],
					mobile: [
						{ message: this.$CONFIG.LOC_STRINGS.MOBILE_PHONE_NUMBER_THAT_CAN_BE_USED_NORMALLY,
							pattern:this.$CONFIG.STRINGS.RGX_MOBILE}
					],
					passwordText: [
						{required: true,
							message: this.$CONFIG.LOC_STRINGS.NUMBER_LETTER_COMBINATION_OF_MORE_THAN_8_DIGITS,pattern:this.$CONFIG.STRINGS.RGX_PASSWORD},
						{validator: (rule, value, callback) => {
							if (this.form.passwordText2 !== '') {
								this.$refs.dialogForm.validateField('passwordText2');
							}
							callback();
						}}
					],
					passwordText2: [
						{required: true, message: '请再次输入密码'},
						{validator: (rule, value, callback) => {
							if (value !== this.form.passwordText) {
								callback(new Error('两次输入密码不一致!'));
							}else{
								callback();
							}
						}}
					],
					deptId: [
						{required: true, message: '请选择所属部门'}
					],
					roleIds: [
						{required: true, message: '请选择所属角色', trigger: 'change'}
					]
				},
				//所需数据选项
				roles: [],
				depts: [],
				deptsProps: {
					emitPath:false,
					value: "id",
					checkStrictly: true
				}
			}
		},
		mounted() {
			this.getRoles()
			this.getDept()
		},
		methods: {
			//显示
			open(mode='add'){
				this.mode = mode;
				this.visible = true;
				return this
			},
			//加载树数据
			async getRoles(){
				var res = await this.$API.sys_role.query.post();
				this.roles = res.data;
			},
			async getDept(){
				var res = await this.$API.sys_dept.query.post();
				this.depts = res.data;
			},
			//表单提交方法
			submit(){
				this.$refs.dialogForm.validate(async (valid) => {
					if (valid) {
						this.isSaveing = true;
						try{
							if (!this.form.mobile)this.form.mobile=null;
							const method =  ( this.mode == 'add' ?   this.$API.sys_user.create
								:  this.$API.sys_user.update)
							const res = await method.post(this.form);
							this.$emit('success', res.data, this.mode)
							this.visible = false;
							this.$message.success("操作成功")
						}catch {
						}
						this.isSaveing = false;
					}else{
						return false;
					}
				})
			},
			//表单注入数据
			setData(data){
				this.form.enabled = data.enabled;
				this.form.id = data.id
				this.form.mobile = data.mobile
				this.form.userName = data.userName
				this.form.avatar = data.avatar
				this.form.name = data.name
				this.form.roleIds = data.roles.map(x=>x.id)
				this.form.deptId = data.dept.id
				this.form.version = data.version

				//可以和上面一样单个注入，也可以像下面一样直接合并进去
				//Object.assign(this.form, data)
			}
		}
	}
</script>

<style>
</style>