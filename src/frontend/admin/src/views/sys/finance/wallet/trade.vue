<template>
    <sc-dialog v-model="visible" :title="$t(`新建交易`)" @closed="$emit(`closed`)" append-to-body destroy-on-close>
        <div v-loading="loading">
            <el-form :model="form" :rules="rules" label-position="right" label-width="12rem" ref="dialogForm" style="height: 100%">
                <el-form-item>
                    <el-descriptions border column="1">
                        <el-descriptions-item :label="$t(`用户编号`)">{{ row.owner.id }}</el-descriptions-item>
                        <el-descriptions-item :label="$t(`用户名`)">
                            <b>{{ row.owner.userName }}</b>
                        </el-descriptions-item>
                        <el-descriptions-item :label="$t(`可用余额`)"
                            >&#165;{{ $TOOL.groupSeparator((row.availableBalance / 100).toFixed(2)) }}</el-descriptions-item
                        >
                    </el-descriptions>
                </el-form-item>
                <el-form-item :label="$t(`交易类型`)" prop="tradeType">
                    <el-select v-model="form.tradeType" clearable filterable>
                        <el-option v-for="(item, i) in $GLOBAL.enums.adminTradeTypes" :key="i" :label="item[1]" :value="i" />
                    </el-select>
                </el-form-item>
                <el-form-item :label="$t(`交易金额`)" prop="amount">
                    <el-input-number
                        v-model="form.amount"
                        :max="100000000"
                        :min="0.01"
                        :precision="2"
                        :step="1"
                        placeholder="元"
                        style="width: 15rem">
                        <template #prefix>
                            <span>&#165;</span>
                        </template>
                    </el-input-number>
                </el-form-item>
                <el-form-item :label="$t(`备注`)" prop="summary">
                    <el-input v-model="form.summary" rows="3" type="textarea" />
                </el-form-item>
            </el-form>
        </div>
        <template #footer>
            <el-button @click="visible = false">{{ $t(`取消`) }}</el-button>
            <el-button v-if="mode !== `view`" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t(`保存`) }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    watch: {
        'form.count'(n) {
            this.form.amount = ((n * this.lowPrice) / 1000).toFixed(3)
        },
    },
    components: {},
    data() {
        return {
            //表单数据
            form: {},
            row: {},
            lowPrice: 0,
            loading: true,

            //验证规则
            rules: {
                tradeType: [{ required: true, message: this.$t(`请选择交易类型`) }],
                amount: [{ required: true, message: this.$t(`请输入交易金额`) }],
            },
            tabId: `0`,
            visible: false,
        }
    },
    emits: [`success`, `closed`, `mounted`],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            this.row = data.row
            this.form.ownerId = data.row.id
            const res = await this.$API.adm_usermessageprice.getUserPriceList.post({ ownerId: data.row.id })
            this.lowPrice = res.data.sort((x, y) => x.unitPrice - y.unitPrice)[0].unitPrice
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
                const res = await this.$API.sys_wallettrade.create.post(Object.assign({}, this.form, { amount: Math.round(this.form.amount * 100) }))
                this.$emit(`success`, res.data, this.mode)
                this.visible = false
                this.$message.success(this.$t(`操作成功`))
            } catch {}
            this.loading = false
        },
    },
    mounted() {
        this.$emit(`mounted`)
    },
}
</script>

<style scoped />