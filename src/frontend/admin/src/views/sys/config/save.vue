<template>
    <el-dialog v-model="visible" :title="titleMap[mode]" :width="500" destroy-on-close @closed="$emit('closed')">
        <el-tabs tab-position="top">
            <el-tab-pane label="基本信息">
                <el-form ref="dialogForm" :disabled="mode==='view'" :model="form" :rules="rules" label-width="100px">
                    <el-collapse>
                        <el-collapse-item name="1" title="用户注册设置">
                            <div style="margin:10px">

                                <el-form-item label="默认角色" prop="userRegisterRoleId">
                                    <el-select v-model="form.userRegisterRoleId" clearable filterable
                                               style="width: 100%">
                                        <el-option v-for="item in roles" :key="item.id" :label="item.name"
                                                   :value="item.id"/>
                                    </el-select>
                                </el-form-item>
                                <el-form-item label="默认部门" prop="userRegisterDeptId">
                                    <el-cascader v-model="form.userRegisterDeptId" :options="depts" :props="deptsProps"
                                                 clearable
                                                 style="width: 100%;"></el-cascader>
                                </el-form-item>
                                <el-form-item label="默认岗位" prop="userRegisterPosId">
                                    <el-select v-model="form.userRegisterPosId" clearable filterable
                                               style="width: 100%">
                                        <el-option v-for="item in positions" :key="item.id" :label="item.name"
                                                   :value="item.id"/>
                                    </el-select>
                                </el-form-item>

                                <el-form-item label="开启人工审核" prop="boolean">
                                    <el-switch v-model="form.userRegisterConfirm"></el-switch>
                                </el-form-item>
                            </div>
                        </el-collapse-item>
                        <el-collapse-item name="2" title="其他设置">
                        </el-collapse-item>
                    </el-collapse>

                    <el-form-item label="启用" prop="boolean">
                        <el-switch v-model="form.enabled"></el-switch>
                    </el-form-item>


                </el-form>
            </el-tab-pane>
            <el-tab-pane v-if="mode==='view'" label="Json">
                <json-viewer
                    :expand-depth="5"
                    :expanded="true"
                    :value="form"
                    boxed
                    copyable
                    sort
                ></json-viewer>
            </el-tab-pane>
        </el-tabs>

        <template #footer>
            <el-button @click="visible=false">取 消</el-button>
            <el-button v-if="mode!=='view'" :loading="isSaving" type="primary" @click="submit()">保 存</el-button>
        </template>
    </el-dialog>
</template>

<script>
import JsonViewer from 'vue-json-viewer'

export default {
    components: {
        JsonViewer
    },
    emits: ['success', 'closed'],
    data() {
        return {
            deptsProps: {
                label: "name",
                emitPath: false,
                value: "id",
                checkStrictly: true
            }, roles: [],
            positions: [],
            depts: [],
            mode: "add",
            titleMap: {
                add: '新增配置',
                edit: '编辑配置',
                view: '查看配置'
            },
            visible: false,
            isSaving: false,
            //表单数据
            form: {
                id: 0,
                enabled: true
            },
            //验证规则
            rules: {
                userRegisterDeptId: [
                    {required: true, message: '请选择默认部门'}
                ],
                userRegisterPosId: [
                    {required: true, message: '请选择默认岗位'}
                ],
                userRegisterRoleId: [
                    {required: true, message: '请选择默认角色'}
                ],

            },
            //所需数据选项
            groups: [],
            groupsProps: {
                value: "id",
                emitPath: false,
                checkStrictly: true
            }
        }
    },
    mounted() {

        this.getRoles()
        this.getPositions()
        this.getDept()
    },
    methods: {
        //显示
        open(mode = 'add') {
            this.mode = mode;
            this.visible = true;
            return this
        },
        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {
            });
            if (!valid) {
                return false
            }

            this.isSaving = true;
            try {
                const method = (this.mode === 'add' ? this.$API.sys_config.create
                    : this.$API.sys_config.update)
                const res = await method.post(this.form);
                this.$emit('success', res.data, this.mode)
                this.visible = false;
                this.$message.success("操作成功")
            } catch {
            }
            this.isSaving = false;

        },
        //加载树数据
        async getRoles() {
            const res = await this.$API.sys_role.query.post();
            this.roles = res.data;
        },
        async getPositions() {
            const res = await this.$API.sys_position.query.post();
            this.positions = res.data;
        },
        async getDept() {
            const res = await this.$API.sys_dept.query.post();
            this.depts = res.data;
        },
        //表单注入数据
        setData(data) {
            // this.form.id = data.id
            // this.form.enabled = data.enabled
            //可以和上面一样单个注入，也可以像下面一样直接合并进去
            Object.assign(this.form, data)
        }
    }
}
</script>

<style>
</style>