<template>
    <sc-dialog v-model="visible" :title="$t('余额转账')" @closed="$emit('closed')" append-to-body destroy-on-close>
        <div v-loading="loading">
            <el-form :model="form" :rules="rules" label-position="right" label-width="12rem" ref="dialogForm" style="height: 100%">
                <el-form-item>
                    <el-row :gutter="10" class="w100p" style="align-items: center">
                        <el-col :span="10">
                            <el-descriptions border column="1">
                                <el-descriptions-item :label="$t('用户名')">
                                    <b>{{ myWallet.owner.userName }}</b>
                                </el-descriptions-item>
                                <el-descriptions-item :label="$t('用户编号')">{{ myWallet.owner.id }}</el-descriptions-item>
                                <el-descriptions-item :label="$t('可用余额')"
                                    >&#165;{{ $TOOL.groupSeparator((myWallet.availableBalance / 100).toFixed(2)) }}</el-descriptions-item
                                >
                            </el-descriptions>
                        </el-col>
                        <el-col :span="4" style="text-align: center">
                            <el-icon size="large">
                                <sc-icon-transfer></sc-icon-transfer>
                            </el-icon>
                        </el-col>
                        <el-col :span="10">
                            <el-descriptions border column="1">
                                <el-descriptions-item :label="$t('用户名')">
                                    <b>{{ row.owner.userName }}</b>
                                </el-descriptions-item>
                                <el-descriptions-item :label="$t('用户编号')">{{ row.owner.id }}</el-descriptions-item>
                                <el-descriptions-item :label="$t('可用余额')"
                                    >&#165;{{ $TOOL.groupSeparator((row.availableBalance / 100).toFixed(2)) }}</el-descriptions-item
                                >
                            </el-descriptions>
                        </el-col>
                    </el-row>
                </el-form-item>
                <el-form-item :label="$t('交易方向')" prop="incomeOrExpense">
                    <el-select v-model="form.incomeOrExpense" clearable filterable>
                        <el-option value="从我的账户转给TA">从我的账户转给TA</el-option>
                        <el-option value="从TA的账户转给我">从TA的账户转给我</el-option>
                    </el-select>
                </el-form-item>
                <el-form-item :label="$t('交易金额')" prop="amount">
                    <el-input-number v-model="form.amount" :max="100000000" :min="0.01" :step="1" placeholder="元" precision="2" style="width: 15rem">
                        <template #prefix>
                            <span>&#165;</span>
                        </template>
                    </el-input-number>
                </el-form-item>
                <el-form-item :label="$t('备注')" prop="summary">
                    <el-input v-model="form.summary" rows="3" type="textarea" />
                </el-form-item>
            </el-form>
        </div>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            //表单数据
            form: {},
            row: {},
            myWallet: {},
            loading: true,
            //验证规则
            rules: {
                incomeOrExpense: [{ required: true, message: this.$t('请选择交易方向') }],
                amount: [{ required: true, message: this.$t('请输入交易金额') }],
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.loading = true
            this.row = data.row
            this.form.ownerId = data.row.id
            const res = await this.$API.sys_userwallet.get.post({ id: this.$GLOBAL.user.id })
            this.myWallet = res.data
            this.visible = true
            this.loading = false
            return this
        },

        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            try {
                await this.$confirm(`金额：￥${this.form.amount.toFixed(2)}，是否继续？`, this.form.incomeOrExpense, {
                    type: 'warning',
                    confirmButtonText: '是',
                    cancelButtonText: '否',
                })
                this.loading = true
                const res = await this.$API.sys_wallettrade[
                    this.form.incomeOrExpense === '从我的账户转给TA' ? 'transferToAnotherAccount' : 'transferFromAnotherAccount'
                ].post(Object.assign({}, this.form, { amount: Math.round(this.form.amount * 100) }))
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

<style scoped />