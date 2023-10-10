<template>
    <sc-dialog v-model="visible" :title="titleMap[mode]" destroy-on-close @closed="$emit('closed')">
        <div v-loading="loading">
            <el-tabs v-if="!loading" tab-position="top">
                <el-tab-pane label="基本信息">
                    <el-form ref="dialogForm" :disabled="mode === 'view'" :model="form" :rules="rules" label-width="100px">
                        <el-collapse>
                            <el-collapse-item name="1" title="用户注册设置">
                                <div style="margin: 10px">
                                    <el-form-item label="默认角色" prop="userRegisterRoleId">
                                        <sc-select
                                            v-model="form.userRegisterRoleId"
                                            :apiObj="$API.sys_role.query"
                                            :config="{ props: { label: 'name', value: 'id' } }"
                                            clearable
                                            filterable />
                                    </el-form-item>
                                    <el-form-item label="默认部门" prop="userRegisterDeptId">
                                        <na-dept v-model="form.userRegisterDeptId"></na-dept>
                                    </el-form-item>
                                    <el-form-item label="开启人工审核" prop="userRegisterConfirm">
                                        <el-switch v-model="form.userRegisterConfirm"></el-switch>
                                    </el-form-item>
                                </div>
                            </el-collapse-item>
                            <el-collapse-item name="2" title="其他设置"></el-collapse-item>
                        </el-collapse>

                        <el-form-item label="启用" prop="enabled">
                            <el-switch v-model="form.enabled"></el-switch>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" label="原始数据">
                    <json-viewer
                        :expand-depth="5"
                        :expanded="true"
                        :theme="this.$TOOL.data.get('APP_DARK') ? 'dark' : 'light'"
                        :value="form"
                        copyable
                        sort></json-viewer>
                </el-tab-pane>
            </el-tabs>
        </div>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button v-if="mode !== 'view'" :loading="loading" type="primary" @click="submit">保 存</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    components: {},
    emits: ['success', 'closed'],
    data() {
        return {
            mode: 'add',
            titleMap: {
                add: '新增配置',
                edit: '编辑配置',
                view: '查看配置',
            },
            visible: false,
            loading: false,
            //表单数据
            form: {
                enabled: true,
            },
            //验证规则
            rules: {
                userRegisterDeptId: [{ required: true, message: '请选择默认部门' }],
                userRegisterRoleId: [{ required: true, message: '请选择默认角色' }],
            },
        }
    },
    mounted() {},
    methods: {
        //显示
        async open(mode = 'add', data) {
            this.visible = true
            this.loading = true
            this.mode = mode
            if (data) {
                Object.assign(this.form, (await this.$API.sys_config.get.post({ id: data.id })).data)
            }
            this.loading = false
            return this
        },

        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }

            this.loading = true
            try {
                const method = this.mode === 'add' ? this.$API.sys_config.create : this.$API.sys_config.update
                const res = await method.post(this.form)
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success('操作成功')
            } catch {
                ///
            }
            this.loading = false
        },
    },
}
</script>

<style></style>