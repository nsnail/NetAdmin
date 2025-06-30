<template>
    <scDialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close>
        <div v-loading="loading">
            <el-tabs v-if="!loading" tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form :disabled="mode === 'view'" :model="form" :rules="rules" label-width="15rem" ref="dialogForm">
                        <el-collapse>
                            <el-collapse-item :title="$t('用户注册设置')" name="1">
                                <div style="margin: 1rem">
                                    <el-form-item :label="$t('默认角色')" prop="userRegisterRoleId">
                                        <scSelect
                                            v-model="form.userRegisterRoleId"
                                            :config="{ props: { label: 'name', value: 'id' } }"
                                            :export-api="$API.sys_role.export"
                                            :query-api="$API.sys_role.query"
                                            clearable
                                            filterable
                                            style="width: 15rem" />
                                    </el-form-item>
                                    <el-form-item :label="$t('默认部门')" prop="userRegisterDeptId">
                                        <naDept v-model="form.userRegisterDeptId" style="width: 15rem"></naDept>
                                    </el-form-item>
                                    <el-form-item :label="$t('开启人工审核')" prop="userRegisterConfirm">
                                        <el-switch v-model="form.userRegisterConfirm"></el-switch>
                                    </el-form-item>
                                    <el-form-item :label="$t('邀请注册')" prop="registerInviteRequired">
                                        <el-switch v-model="form.registerInviteRequired"></el-switch>
                                    </el-form-item>
                                    <el-form-item :label="$t('手机注册')" prop="registerMobileRequired">
                                        <el-switch v-model="form.registerMobileRequired"></el-switch>
                                    </el-form-item>
                                </div>
                            </el-collapse-item>
                            <el-collapse-item :title="$t('财务配置')" name="2">
                                <div style="margin: 1rem">
                                    <el-form-item :label="$t('人民币兑点数比率')" prop="cnyToPointRate">
                                        <el-input-number v-model="form.cnyToPointRate" :max="999999999" :min="1"></el-input-number>
                                    </el-form-item>
                                    <el-form-item :label="$t('美元兑点数比率')" prop="usdToPointRate">
                                        <el-input-number v-model="form.usdToPointRate" :max="999999999" :min="1"></el-input-number>
                                    </el-form-item>
                                    <el-form-item :label="$t('USDT 收款地址')" prop="trc20ReceiptAddress">
                                        <el-input v-model="form.trc20ReceiptAddress" maxlength="34" placeholder="placeholder"></el-input>
                                    </el-form-item>
                                </div>
                            </el-collapse-item>
                        </el-collapse>

                        <el-form-item :label="$t('启用')" prop="enabled">
                            <el-switch v-model="form.enabled"></el-switch>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('原始数据')">
                    <JsonViewer
                        :expand-depth="5"
                        :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                        :value="form"
                        copyable
                        expanded
                        sort></JsonViewer>
                </el-tab-pane>
            </el-tabs>
        </div>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </scDialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const naDept = defineAsyncComponent(() => import('@/components/naDept'))
const scSelect = defineAsyncComponent(() => import('@/components/scSelect'))
export default {
    components: { naDept, scSelect },
    data() {
        return {
            //表单数据
            form: {
                enabled: true,
            },
            loading: true,
            mode: 'add',
            //验证规则
            rules: {
                userRegisterDeptId: [{ required: true, message: '请选择默认部门' }],
                userRegisterRoleId: [{ required: true, message: '请选择默认角色' }],
            },
            titleMap: {
                add: this.$t('新建配置'),
                edit: this.$t('编辑配置'),
                view: this.$t('查看配置'),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            this.loading = true
            this.mode = data.mode
            if (data.row?.id) {
                const res = await this.$API.sys_config.get.post({ id: data.row.id })
                Object.assign(this.form, res.data)
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

            const method = this.mode === 'add' ? this.$API.sys_config.create : this.$API.sys_config.edit
            try {
                const res = await method.post(this.form)
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success(this.$t('操作成功'))
            } catch {}
            this.loading = false
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped>
.el-collapse {
    border-top: none;
}
</style>