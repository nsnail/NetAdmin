<template>
    <scDialog v-model="visible" :title="$t('新建交易')" @closed="$emit('closed')" append-to-body destroy-on-close>
        <el-form :model="form" :rules="rules" label-position="right" label-width="12rem" ref="dialogForm" style="height: 100%">
            <el-form-item>
                <el-descriptions border column="1">
                    <el-descriptions-item :label="$t('用户名')">
                        <b>{{ row.owner.userName }}</b>
                    </el-descriptions-item>
                    <el-descriptions-item :label="$t('用户编号')">{{ row.owner.id }}</el-descriptions-item>
                    <el-descriptions-item :label="$t('可用余额')">{{ $TOOL.groupSeparator(row.availableBalance) }}</el-descriptions-item>
                </el-descriptions>
            </el-form-item>
            <el-form-item :label="$t('交易类型')" prop="tradeType">
                <el-select v-model="form.tradeType" clearable filterable>
                    <el-option v-for="(item, i) in $GLOBAL.enums.tradeTypes" :key="i" :label="item[1]" :value="i" />
                </el-select>
            </el-form-item>
            <el-form-item :label="$t('交易金额')" prop="amount">
                <el-input-number v-model="form.amount" :max="999999999" :min="-999999999" precision="0" style="width: 15rem"></el-input-number>
            </el-form-item>
            <el-form-item :label="$t('备注')" prop="summary">
                <el-input v-model="form.summary" rows="3" type="textarea" />
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </scDialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'

export default {
    components: {},
    data() {
        return {
            //表单数据
            form: {},
            row: {},
            loading: true,

            //验证规则
            rules: {
                tradeType: [{ required: true, message: this.$t('请选择交易类型') }],
                amount: [{ required: true, message: this.$t('请输入交易金额') }],
            },
            tabId: '0',
            titleMap: {
                add: this.$t('新增用户'),
                edit: this.$t('编辑用户'),
                view: this.$t('查看用户'),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.row = data.row
            this.form.ownerId = data.row.id
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
            this.loading = true
            try {
                const res = await this.$API.sys_wallettrade.create.post(this.form)
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

<style scoped></style>