<template>
	<el-dialog :title="titleMap[mode]" v-model="visible" :width="500" destroy-on-close @closed="$emit('closed')">
		<el-tabs tab-position="top">
			<el-tab-pane label="基本信息">
				<el-form :model="form" :rules="rules" :disabled="mode=='show'" ref="dialogForm" label-width="100px"
						 label-position="left">
					<el-form-item label="角色名称" prop="label">
						<el-input v-model="form.label" clearable></el-input>
					</el-form-item>
					<el-form-item label="排序" prop="sort">
						<el-input-number v-model="form.sort" controls-position="right" :min="0"
										 style="width: 100%;"></el-input-number>
					</el-form-item>
					<el-form-item label="是否有效" prop="enabled">
						<el-switch v-model="form.enabled"></el-switch>
					</el-form-item>
					<el-form-item label="无限权限" prop="ignorePermissionControl">
						<el-switch v-model="form.ignorePermissionControl"></el-switch>
					</el-form-item>
					<el-form-item label="备注" prop="remark">
						<el-input v-model="form.remark" clearable type="textarea"></el-input>
					</el-form-item>
				</el-form>
			</el-tab-pane>
			<el-tab-pane label="菜单权限">
				<div class="treeMain">
					<el-tree ref="menu" node-key="id" :data="menu.list" :props="menu.props" show-checkbox
							 default-expand-all></el-tree>
				</div>
			</el-tab-pane>
			<el-tab-pane label="数据权限">
				<el-form label-width="100px" label-position="left">
					<el-form-item label="规则类型">
						<el-select v-model="form.dataScope" placeholder="请选择">
							<el-option v-for="(item,i) in this.$CONFIG.ENUMS.dataScopes" :key="i" :label="item.desc"
									   :value="item.value"
							></el-option>
						</el-select>
					</el-form-item>
					<el-form-item label="选择部门" v-show="form.dataScope=='specificDept'">
						<div class="treeMain" style="width: 100%;">
							<el-tree ref="dept" node-key="id" :data="data.list" :props="data.props" show-checkbox
							default-expand-all ></el-tree>
						</div>
					</el-form-item>
				</el-form>
			</el-tab-pane>
			<el-tab-pane label="控制台">
				<el-form label-width="100px" label-position="left">
					<el-form-item label="控制台视图">
						<el-select v-model="form.dashboard" placeholder="请选择">
							<el-option v-for="item in dashboardOptions" :key="item.value" :label="item.label" :value="item.value">
								<span style="float: left">{{ item.label }}</span>
								<span style="float: right; color: #8492a6; font-size: 12px">{{ item.views }}</span>
							</el-option>
						</el-select>
						<div class="el-form-item-msg">用于控制角色登录后控制台的视图</div>
					</el-form-item>
				</el-form>
			</el-tab-pane>
		</el-tabs>
		<template #footer>
			<el-button @click="visible=false">取 消</el-button>
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
			menu: {
				list: [],
				checked: [],
				props: {
					label: (data)=>{
						return data.meta.title
					}
				}
			},
			data: {
				list: [],
				checked: [],
				props: {
				},
				rule: ""
			},
			dashboard: "0",
			dashboardOptions: [
				{
					value: '0',
					label: '数据统计',
					views: 'stats'

				},
				{
					value: '1',
					label: '工作台',
					views: 'work'
				},
			],
			//表单数据
			form: {
				id: 0,
				label: "",
				sort: 0,
				enabled: true,
				ignorePermissionControl: false,
				remark: "",
				dataScope:'all'
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
		async getMenu(){
			console.error(this.form.menuIds)
			this.menu.checked = this.form.menuIds || [];
			const res = await this.$API.sys_menu.query.post()
			this.menu.list = res.data
			//获取接口返回的之前选中的和半选的合并，处理过滤掉有叶子节点的key
			this.$nextTick(() => {
				let filterKeys = this.menu.checked.filter(key => this.$refs.menu.getNode(key).isLeaf)
				this.$refs.menu.setCheckedKeys(filterKeys, true)
			})
		},
		async getDept(){
			var res = await this.$API.sys_dept.query.post();
			this.data.list = res.data
			this.data.checked = this.form.deptIds || [];
			this.$nextTick(() => {
				let filterKeys = this.data.checked.filter(key => this.$refs.dept.getNode(key).isLeaf)
				this.$refs.dept.setCheckedKeys(filterKeys, true)
			})
		},
		//显示
		open(mode = 'add') {
			this.mode = mode;
			this.visible = true;
			return this
		},
		//表单提交方法
		submit() {
			this.$refs.dialogForm.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					//选中的和半选的合并后传值接口
					const checkedKeys = this.$refs.menu.getCheckedKeys().concat(this.$refs.menu.getHalfCheckedKeys());
					const checkedKeys_dept = this.$refs.dept.getCheckedKeys().concat(this.$refs.dept.getHalfCheckedKeys());

					try {
						const method = (this.mode == 'add' ? this.$API.sys_role.create
							: this.$API.sys_role.update)
						this.form.deptIds = checkedKeys_dept;
						this.form.menuIds = checkedKeys;
						const res = await method.post(this.form);
						this.$emit('success', res.data, this.mode)
						this.visible = false;
						this.$message.success("操作成功")

					} catch {
					}
					this.isSaveing = false;
				}
			})
		},
		//表单注入数据
		setData(data) {
			this.form.id = data.id
			this.form.label = data.label
			this.form.sort = data.sort
			this.form.enabled = data.enabled
			this.form.ignorePermissionControl = data.ignorePermissionControl
			this.form.remark = data.remark
			this.form.version = data.version
			this.form.deptIds = data.deptIds
			this.form.menuIds = data.menuIds
			this.form.dataScope = data.dataScope
			//可以和上面一样单个注入，也可以像下面一样直接合并进去
			//Object.assign(this.form, data)

			return this
		},
		loadTree(){
			this.getMenu()
			this.getDept()
		}
	}
}
</script>

<style>
</style>