<template>
    <scDialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="handleDialogClose" destroy-on-close>
        <div v-loading="loading">
            <el-steps :active="stepActive" finish-status="success" process-status="finish" simple>
                <el-step :title="$t('确认金额')" />
                <el-step :title="$t('扫码支付')" />
                <el-step :title="$t('充值成功')" />
            </el-steps>
            <el-form
                v-if="stepActive === 0"
                :model="form"
                :rules="rules"
                @keyup.enter="next"
                class="center-form"
                justify="center"
                ref="stepForm_0"
                size="large">
                <el-form-item :label="$t('充值金额（USDT）：')" prop="userName" style="margin-top: 50px; margin-left: -50px">
                    <el-input-number
                        :max="100000000"
                        :min="20"
                        :model-value="this.PayAmount ? this.PayAmount : ''"
                        :precision="0"
                        :step="10"
                        @input="
                            (e) => {
                                if (e < 20) {
                                    e = 20
                                }
                                this.PayAmount = e
                            }
                        "
                        clearable
                        style="width: 30rem"></el-input-number>
                </el-form-item>
                <el-form-item v-if="this.form.depositPoint">
                    <p style="margin-left: -110px; margin-top: -30px">
                        到账金额：<span class="highlight-placeholder">&#165;{{ (this.form.depositPoint / 100).toFixed(2) }}</span>
                    </p>
                </el-form-item>

                <el-form-item :label="$t('当前汇率 ：')" prop="toPointRate" style="margin-left: -120px; margin-bottom: 30px">
                    <el-input :model-value="form.toPointRate ? `${form.toPointRate / 100}` : ''" disabled />
                </el-form-item>
            </el-form>
            <el-form v-if="stepActive === 1">
                <el-form :model="form" :rules="rules" justify="center" label-width="12rem" ref="stepForm_1" size="large" style="margin: 30px auto">
                    <el-form-item v-if="!this.isLoading" style="margin-top: 20px; margin-left: 200px">
                        <template #default>
                            <div>
                                <p style="margin-bottom: -8px; margin-top: -20px">请在10分钟之内扫码支付，避免支付超时!</p>
                                <div v-html="form.receiptAccountQrCode"></div>
                                <p style="margin-left: 60px; margin-top: -30px">
                                    充值金额(U)：<span style="color: red; font-weight: bold; font-size: 18px">{{ form.actualPayAmount / 1000 }}</span>
                                </p>
                                <p style="font-size: 11px; margin-top: -10px">
                                    注：为确保转账成功，会随机增加 <span style="color: red">0.001~0.1</span> 充值金额
                                </p>
                            </div>
                        </template>
                    </el-form-item>
                    <el-form-item v-if="this.isLoading" justify="center" style="margin-top: 50px; margin-left: 200px">
                        <template #default>
                            <div>
                                <div class="spinner"></div>
                                <p style="font-size: 18px; margin-top: 20px; margin-left: 80px">充值核验中...</p>
                                <p style="font-size: 11px; color: #17abe3; margin-left: 40px">等待时间过长可关闭后在列表中查询结果</p>
                            </div>
                        </template>
                    </el-form-item>
                    <el-form-item :label="$t('到账金额')" prop="depositPoint" style="margin-top: -10px; margin-left: 150px">
                        <el-input-number
                            :max="100000000"
                            :min="100"
                            :model-value="form.depositPoint / 100"
                            :step="100"
                            clearable
                            disabled
                            style="width: 30rem">
                            <template #prefix>
                                <span>&#165;</span>
                            </template>
                        </el-input-number>
                    </el-form-item>
                    <el-form-item :label="$t('当前汇率')" prop="toPointRate" style="margin-left: 150px">
                        <el-input :model-value="form.toPointRate ? `${form.toPointRate / 100}` : ''" disabled style="width: 30rem" />
                    </el-form-item>
                    <el-form-item :label="$t('收款地址')" prop="receiptAccount" style="margin-left: 150px">
                        <el-input v-model="form.receiptAccount" clearable disabled style="width: 30rem" />
                        <el-button @click="copyToClipboard(form.receiptAccount)" type="text">复制</el-button>
                    </el-form-item>
                    <el-form-item :label="$t('创建时间')" prop="createdTime" style="margin-left: 150px">
                        <el-input v-model="form.createdTime" clearable disabled style="width: 30rem" />
                    </el-form-item>
                    <el-form-item v-if="!this.isLoading">
                        <p style="font-size: 14px; margin-left: 150px; margin-top: -20px; color: var(--el-color-warning)">
                            ⚠️转账金额必须与红字完全一致（不含手续费）
                        </p>
                    </el-form-item>
                </el-form>
            </el-form>
            <div v-if="stepActive >= 2">
                <el-result :title="$t('充值成功')" icon="success">
                    <template #extra></template>
                </el-result>
            </div>
            <el-form size="large" style="text-align: center">
                <el-button v-if="stepActive > 0 && stepActive < 2 && !isLoading" @click="pre" size="large" type="primary"
                    >{{ $t('上一步') }}
                </el-button>
                <el-button v-if="stepActive < 1" @click="next" size="large" type="primary">{{ $t('下一步') }}</el-button>
                <el-button v-if="stepActive === 1" :loading="this.isLoading" @click="save" size="large" type="danger"
                    >{{ $t('确认已支付') }}
                </el-button>
            </el-form>
        </div>
    </scDialog>
</template>

<script>
import QRCode from 'qrcode-svg'

export default {
    computed: {},
    components: {},
    watch: {
        PayAmount: {
            immediate: true,
            handler(n, o) {
                if (this.PayAmount) {
                    this.form.depositPoint = Math.floor(n * this.form.toPointRate)
                }
            },
        },
    },
    data() {
        return {
            config: null,
            //表单数据
            form: {
                paymentMode: 'usdt',
            },
            loading: true,
            isLoading: false,
            stepActive: 0,
            mode: 'add',
            isEdit: false,
            PayAmount: 20,
            //验证规则
            rules: {
                actualPayAmount: [{ required: true, message: this.$t('请输入充值金额') }],
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
            this.form.toPointRate = this.config?.usdToPointRate
            if (data.mode === 'add') {
                this.form.depositPoint = this.PayAmount * this.form.toPointRate
                this.loading = false
                return this
            }
            if (data.mode === 'pay') {
                this.stepActive = 1
            }
            this.loading = true
            if (data.row?.id) {
                const res = await this.$API.sys_depositorder.get.post({ id: data.row.id })
                if (res.data) {
                    Object.assign(this.form, res.data)
                    this.form.receiptAccountQrCode = new QRCode(this.form.receiptAccount).svg()
                    this.PayAmount = this.form.actualPayAmount / 1000
                    this.loading = false
                    return this
                }
            }
            this.$message.error(`未找到该数据`)
            return this
        },
        pre() {
            this.isEdit = true
            this.stepActive -= 1
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
        async next() {
            if (!(await this.$refs[`stepForm_${this.stepActive}`].validate().catch(() => {}))) {
                return false
            }
            this.loading = true
            try {
                if (this.form.toPointRate && this.form.depositPoint && this.PayAmount) {
                    this.PayAmount += (Math.floor(Math.random() * 9) + 1) / 100
                }
                this.form.actualPayAmount = this.PayAmount * 1000
                this.form.depositPoint = Math.floor(this.PayAmount * this.form.toPointRate)
                if (this.isEdit) {
                    this.form.receiptAccountQrCode = null
                }
                const res = this.isEdit
                    ? await this.$API.sys_depositorder.edit.post(this.form)
                    : await this.$API.sys_depositorder.create.post(this.form)
                if (res.data) {
                    this.$emit('success', res.data, this.mode)
                    Object.assign(this.form, res.data)
                    this.form.receiptAccountQrCode = new QRCode(this.form.receiptAccount).svg()
                    this.stepActive += 1
                } else {
                    this.visible = false
                }
            } catch {}
            this.loading = false
        },
        async save() {
            try {
                this.loading = true
                const res = await this.$API.sys_depositorder.payConfirm.post(this.form)
                if (res.data) {
                    this.loading = false
                    this.$emit('success', res.data, this.mode)
                    Object.assign(this.form, res.data)
                    this.isLoading = true
                    this.startPolling()
                } else {
                    this.loading = false
                    this.$message.error('确认支付失败，请在列表中确认')
                    this.visible = false
                }
            } catch {
                this.loading = falses
                this.visible = false
            }
        },
        copyToClipboard(text) {
            navigator.clipboard
                .writeText(text)
                .then(() => {
                    this.$message.success('已复制到剪贴板')
                })
                .catch((err) => {
                    this.$message.error('复制失败，请手动复制')
                })
        },
        startPolling() {
            this.intervalId = setInterval(async () => {
                try {
                    // 调用 API 获取状态
                    const res = await this.$API.sys_depositorder.get.post(this.form)
                    if (res.data && res.data.depositOrderStatus === 'succeeded') {
                        this.stopPolling()
                        this.stepActive += 1
                    }
                } catch (error) {
                    this.stopPolling()
                }
            }, 5 * 1000) // 每5秒执行一次
        },
        stopPolling() {
            clearInterval(this.intervalId)
        },
        handleDialogClose() {
            this.$emit('closed')
            this.stopPolling()
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped>
.center-form {
    display: flex;
    flex-direction: column; /* 让表单项垂直排列 */
    align-items: center; /* 水平居中 */
    gap: 10px; /* 表单项之间的间距（可选） */
}

.highlight-placeholder {
    color: #ed2e2e;
    font-weight: bold;
    cursor: pointer;
}

.spinner {
    width: 100px;
    height: 100px;
    border: 14px solid #f3f3f3;
    border-top: 14px solid #409eff; /* 颜色可改 */
    border-radius: 50%;
    animation: spin 1s linear infinite;
    margin: 20px auto;
}

@keyframes spin {
    0% {
        transform: rotate(0deg);
    }
    100% {
        transform: rotate(360deg);
    }
}
</style>