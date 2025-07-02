<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close full-screen>
        <div v-loading="loading">
            <el-tabs v-model="tabId" @tab-change="tabChange" tab-position="top">
                <el-tab-pane :label="$t('基本信息')" name="basic">
                    <el-form :disabled="mode === 'view'" :model="form" :rules="rules" label-width="15rem" ref="dialogForm">
                        <el-form-item :label="$t('唯一编码')" prop="id">
                            <el-input v-model="form.id" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('总余额')" prop="totalBalance">
                            <el-input v-model="form.totalBalance" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('可用余额')" prop="availableBalance">
                            <el-input v-model="form.availableBalance" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('冻结余额')" prop="frozenBalance">
                            <el-input v-model="form.frozenBalance" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('总收入')" prop="totalIncome">
                            <el-input v-model="form.totalIncome" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('总支出')" prop="totalExpenditure">
                            <el-input v-model="form.totalExpenditure" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('归属部门编号')" prop="ownerDeptId">
                            <el-input v-model="form.ownerDeptId" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('归属用户编号')" prop="ownerId">
                            <el-input v-model="form.ownerId" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('创建时间')" prop="createdTime">
                            <el-input v-model="form.createdTime" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('修改时间')" prop="modifiedTime">
                            <el-input v-model="form.modifiedTime" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('数据版本')" prop="version">
                            <el-input v-model="form.version" :disabled="mode === 'edit'" clearable />
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('交易流水')" name="trade">
                    <trade v-if="tabId === 'trade'" :ownerId="form.ownerId.toString()" />
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('原始数据')">
                    <json-viewer
                        :expand-depth="5"
                        :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                        :value="form"
                        copyable
                        expanded
                        sort />
                </el-tab-pane>
            </el-tabs>
        </div>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const trade = defineAsyncComponent(() => import('@/views/sys/trade'))
export default {
    components: { trade },
    data() {
        return {
            //表单数据
            form: {},
            loading: true,
            mode: 'add',
            //验证规则
            rules: {},
            tabId: 'basic',
            titleMap: {
                add: this.$t('新建钱包'),
                edit: this.$t('编辑钱包'),
                view: this.$t('查看钱包'),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            if (data.mode === 'add') {
                this.loading = false
                return this
            }
            this.loading = true
            this.mode = data.mode
            if (data.row?.id) {
                const res = await this.$API.sys_userwallet.get.post({ id: data.row.id })
                if (res.data) {
                    Object.assign(this.form, res.data)
                    this.loading = false
                    return this
                }
            }
            this.$message.error(`未找到该数据`)
            return this
        },
        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true
            //
            this.loading = false
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped />