<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close>
        <div v-loading="loading">
            <el-tabs v-model="tabId" @tab-change="tabChange" tab-position="top">
                <el-tab-pane :label="$t('基本信息')" name="basic">
                    <el-form :disabled="mode === 'view' || mode === 'pay'" :model="form" :rules="rules" label-width="15rem" ref="dialogForm">
                        <el-form-item v-if="mode !== 'add'" :label="$t('订单编号')" prop="id">
                            <el-input v-model="form.id" clearable />
                        </el-form-item>
                        <el-form-item v-if="mode !== 'add'" :label="$t('订单状态')" prop="depositOrderStatus">
                            <el-select v-model="form.depositOrderStatus" filterable>
                                <el-option v-for="(item, i) in $GLOBAL.enums.depositOrderStatues" :key="i" :label="item[1]" :value="i" />
                            </el-select>
                        </el-form-item>
                        <el-form-item :label="$t('充值点数')" prop="depositPoint">
                            <el-input-number
                                :max="999999999"
                                :min="100"
                                :model-value="form.depositPoint"
                                :step="100"
                                @input="
                                    (e) => {
                                        if (e < 0) e = 0
                                        if (e % 10 === 0) {
                                            e += Math.floor(Math.random() * 9) + 1
                                        }
                                        if (e < 100) {
                                            e += 100
                                        }

                                        this.form.depositPoint = e
                                    }
                                "
                                clearable
                                style="width: 15rem" />
                        </el-form-item>
                        <el-form-item :label="$t('支付方式')" prop="paymentMode">
                            <el-select v-model="form.paymentMode" filterable>
                                <el-option v-for="(item, i) in $GLOBAL.enums.paymentModes" :key="i" :label="item[1]" :value="i" />
                            </el-select>
                        </el-form-item>
                        <el-form-item :label="$t('货币兑点数比率')" prop="toPointRate">
                            <el-input :model-value="form.toPointRate ? `1:${form.toPointRate}` : ''" disabled />
                        </el-form-item>
                        <el-form-item :label="$t('支付货币金额')" prop="actualPayAmount">
                            <el-input :model-value="form.actualPayAmount ? form.actualPayAmount / 1000 : ''" disabled />
                        </el-form-item>
                        <el-form-item v-if="mode === 'pay'" :label="$t('收款账号')" prop="receiptAccount">
                            <el-input v-model="form.receiptAccount" clearable />
                        </el-form-item>
                        <el-form-item v-if="mode === 'pay'" :label="$t('收款二维码')">
                            <div v-html="form.receiptAccountQrCode"></div>
                        </el-form-item>
                        <el-form-item v-if="mode !== 'add'" :label="$t('创建时间')" prop="createdTime">
                            <el-input v-model="form.createdTime" clearable />
                        </el-form-item>
                    </el-form>
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
            <el-button v-if="mode === 'add'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('提交订单') }}</el-button>
            <el-button
                v-if="mode === 'pay'"
                :disabled="loading"
                :loading="loading"
                @click="
                    async () => {
                        this.loading = true
                        try {
                            const res = await this.$API.sys_depositorder.payConfirm.post(this.form)
                            this.$emit('success', res.data, this.mode)
                            this.visible = false
                            this.$message.success(this.$t('操作成功'))
                        } catch {}
                        this.loading = false
                    }
                "
                type="danger"
                >{{ $t('确认已支付') }}</el-button
            >
        </template>
    </sc-dialog>
</template>

<script>
import qrCode from 'qrcode-svg'

export default {
    computed: {},
    components: {},

    watch: {
        'form.paymentMode': {
            immediate: true,
            handler(n, o) {
                if (n === 'alipay' || n === 'weChat') {
                    this.form.toPointRate = this.config?.cnyToPointRate
                } else if (n === 'usdt') {
                    this.form.toPointRate = this.config?.usdToPointRate
                } else {
                    delete this.form.toPointRate
                }
                if (this.form.toPointRate && this.form.depositPoint) {
                    this.form.actualPayAmount = (this.form.depositPoint / this.form.toPointRate).toFixed(3) * 1000
                } else {
                    delete this.form.actualPayAmount
                }
            },
        },
        'form.depositPoint': {
            immediate: true,
            handler(n, o) {
                if (this.form.toPointRate) {
                    this.form.actualPayAmount = (n / this.form.toPointRate).toFixed(3) * 1000
                }
            },
        },
    },
    data() {
        return {
            config: null,
            //表单数据
            form: {},
            loading: true,
            mode: 'add',
            //验证规则
            rules: {
                depositPoint: [{ required: true, message: this.$t('请输入充值点数') }],
                paymentMode: [{ required: true, message: this.$t('请选择支付方式') }],
            },
            tabId: 'basic',
            titleMap: {
                add: this.$t('新建充值订单'),
                view: this.$t('查看充值订单'),
                pay: this.$t('支付充值订单'),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            this.config = (await this.$API.sys_depositorder.getDepositConfig.post({})).data
            if (data.mode === 'add') {
                this.loading = false
                return this
            }
            this.loading = true
            this.mode = data.mode
            if (data.row?.id) {
                const res = await this.$API.sys_depositorder.get.post({ id: data.row.id })
                if (res.data) {
                    Object.assign(this.form, res.data)
                    this.form.receiptAccountQrCode = new qrCode(this.form.receiptAccount).svg()
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
            try {
                const res = await this.$API.sys_depositorder.create.post(this.form)
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