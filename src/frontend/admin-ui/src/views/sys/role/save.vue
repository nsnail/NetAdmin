<template>
	<el-dialog :title="titleMap[mode]" v-model="visible" :width="500" destroy-on-close @closed="$emit('closed')">
		<el-form :model="form" :rules="rules" :disabled="mode=='show'" ref="dialogForm" label-width="100px" label-position="left">
			<el-form-item label="角色名称" prop="label">
				<el-input v-model="form.label" clearable></el-input>
			</el-form-item>
			<el-form-item label="排序" prop="sort">
				<el-input-number v-model="form.sort" controls-position="right" :min="0"
								 style="width: 100%;"></el-input-number>
			</el-form-item>
			<el-form-item label="是否有效" prop="enabled">
				<el-switch v-model="form.enabled" ></el-switch>
			</el-form-item>
			<el-form-item label="无限权限" prop="ignorePermissionControl">
				<el-switch v-model="form.ignorePermissionControl"></el-switch>
			</el-form-item>
			<el-form-item label="备注" prop="remark">
				<el-input v-model="form.remark" clearable type="textarea"></el-input>
			</el-form-item>
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
					add: '新增',
					edit: '编辑',
					show: '查看'
				},
				visible: false,
				isSaveing: false,
				//表单数据
				form: {
					id:"",
					label: "",
					sort: 0,
					enabled: true,
					ignorePermissionControl:false,
					remark: ""
				},
				//验证规则
				rules: {
					sort: [
						{required: true, message: '请输入排序', trigger: 'change'}
					],
					label: [
						{required: true, message: '请输入角色名称'}
					]
				}
			}
		},
		mounted() {

		},
		methods: {
			//显示
			open(mode='add'){
				this.mode = mode;
				this.visible = true;
				return this
			},
			//表单提交方法
			submit(){
				this.$refs.dialogForm.validate(async (valid) => {
					if (valid) {
						this.isSaveing = true;
						try{
							const method =  ( this.mode == 'add' ?   this.$API.sys_role.create
								:  this.$API.sys_role.update)
							const res = await method.post(this.form);
							this.$emit('success', res.data, this.mode)
							this.visible = false;
							this.$message.success("操作成功")

						}catch {
						}
						this.isSaveing = false;
					}
				})
			},
			//表单注入数据
			setData(data){
				this.form.id = data.id
				this.form.label = data.label
				this.form.sort = data.sort
				this.form.enabled = data.enabled
				this.form.ignorePermissionControl = data.ignorePermissionControl
				this.form.remark = data.remark
				this.form.version = data.version

				//可以和上面一样单个注入，也可以像下面一样直接合并进去
				//Object.assign(this.form, data)
			}
		}
	}
</script>

<style>
</style>