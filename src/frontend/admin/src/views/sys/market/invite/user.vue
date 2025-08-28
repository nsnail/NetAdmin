<template>
    <sc-dialog
        v-model="visible"
        :title="`查看用户 ${form.userName}(${form.id})`"
        @closed="$emit('closed')"
        append-to-body
        destroy-on-close
        full-screen>
        <el-form :model="form" :rules="rules" label-position="right" label-width="12rem" ref="dialogForm" style="height: 100%">
            <el-tabs v-model="tabId" tab-position="top">
                <el-tab-pane :label="$t('充值订单')" name="deposit">
                    <deposit v-if="tabId === 'deposit'" :owner-id="form.id.toString()" />
                </el-tab-pane>
                <el-tab-pane :label="$t('钱包信息')" name="wallet">
                    <wallet v-if="tabId === 'wallet'" :id="form.id.toString()" />
                </el-tab-pane>
                <el-tab-pane :label="$t('交易流水')" name="trade">
                    <trade v-if="tabId === 'trade'" :owner-id="form.id.toString()" />
                </el-tab-pane>
                <el-tab-pane :label="$t('冻结记录')" name="frozen">
                    <frozen v-if="tabId === 'frozen'" :owner-id="form.id.toString()" />
                </el-tab-pane>
                <el-tab-pane v-if="this.$GLOBAL.hasApiPermission(`api/sys/user.invite/query`)" :label="$t('粉丝裂变')" name="invite">
                    <invite v-if="tabId === 'invite'" :owner-id="form.id.toString()" />
                </el-tab-pane>
            </el-tabs>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'

const trade = defineAsyncComponent(() => import('@/views/sys/finance/trade/list/index'))
const frozen = defineAsyncComponent(() => import('@/views/sys/finance/trade/frozen/index'))
const wallet = defineAsyncComponent(() => import('@/views/sys/finance/wallet'))
const deposit = defineAsyncComponent(() => import('@/views/sys/finance/deposit'))
const invite = defineAsyncComponent(() => import('@/views/sys/market/invite'))
export default {
    components: { trade, frozen, wallet, messageOrder, deposit, filterTask, userPrice, invite },
    data() {
        return {
            //表单数据
            form: {},
            loading: true,
            //验证规则
            rules: {},
            tabId: 'userPrice',
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open({ data }) {
            this.visible = true
            this.loading = true
            this.form = data.user
            this.tabId = data.tabId
            this.loading = false
            return this
        },

        //表单提交方法
        async submit() {},
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped></style>