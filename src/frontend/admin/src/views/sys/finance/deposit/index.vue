<template>
    <na-table-page
        :columns="{
            id: {
                label: $t(`订单编号`),
                is: `na-col-id`,
                extra: [`createdTime`],
                width: 160,
                show: [`list`, `view`],
                searchable: true,
            },
            'owner.userName': {
                show: [],
            },
            'owner.id': {
                extra: ['owner.userName'],
                headerAlign: `center`,
                is: `na-col-user`,
                userObjPath: `owner`,
                width: 160,
                label: $t(`归属用户`),
                show: [`list`, `view`],
            },
            'owner.invite.owner.userName': {
                show: [],
            },
            'owner.invite.owner.id': {
                extra: ['owner.invite.owner.userName'],
                headerAlign: `center`,
                is: `na-col-user`,
                userObjPath: `owner.invite.owner`,
                width: 170,
                label: $t(`上级`),
                condition: () => this.$GLOBAL.user.isAdmin || this.$GLOBAL.user.isChannel,
                show: [`list`, `view`],
            },
            'owner.invite.channel.userName': {
                show: [],
            },
            'owner.invite.channel.id': {
                extra: ['owner.invite.channel.userName'],
                headerAlign: `center`,
                is: `na-col-user`,
                userObjPath: `owner.invite.channel`,
                width: 170,
                label: $t(`渠道`),
                condition: () => this.$GLOBAL.user.isAdmin || this.$GLOBAL.user.isChannel,
                show: [`list`, `view`],
            },
            depositOrderStatus: {
                label: `订单状态`,
                is: `na-col-indicator`,
                enum: { name: `depositOrderStatues` },
                width: 100,
                align: `center`,
                show: [`view`, `list`],
                selectFilter: { countBy: true },
            },
            depositPoint: {
                label: $t(`充值金额`),
                width: 200,
                formatter: (row) => `&#165;${$TOOL.groupSeparator((row.depositPoint / 100).toFixed(2))}`,
                align: `right`,
                show: [`list`, `view`],
                sum: {
                    label: $t(`充值金额（RMB）`),
                    currency: {
                        flag: '&#165;',
                        multiple: 100,
                    },
                },
            },
            toPointRate: {
                label: `汇率`,
                formatter: (row) => `${$TOOL.groupSeparator((row.toPointRate / 100).toFixed(2))}`,
                width: 100,
                align: `right`,
                show: [`view`, `list`],
            },
            actualPayAmount: {
                label: `支付金额`,
                formatter: (row) => `&dollar;${$TOOL.groupSeparator((row.actualPayAmount / 1000).toFixed(3))}`,
                width: 200,
                align: `right`,
                sum: {
                    label: $t(`支付金额（U）`),
                    currency: {
                        flag: '&dollar;',
                        multiple: 1000,
                        decimal: 3,
                    },
                },
                show: [`view`, `list`],
            },
            paymentMode: {
                label: `支付方式`,
                is: `na-col-indicator`,
                enum: { name: `paymentModes` },
                width: 100,
                align: `center`,
                show: [`view`, `list`],
            },
            paidAccount: {
                label: $t(`付款账号`),
                show: [`view`],
            },
            receiptAccount: {
                label: $t(`收款账号`),
                show: [`view`],
            },
            paidTime: {
                label: $t(`支付时间`),
                show: [`view`],
            },
            paymentFinger: {
                label: $t(`交易指纹`),
                show: [`view`],
            },
            createdTime: {
                label: $t(`创建时间`),
                show: [`view`],
                operator: `dateRange`,
            },
            modifiedTime: {
                label: $t(`更新时间`),
                show: [`view`],
            },
            version: {
                label: $t(`数据版本`),
                show: [`view`],
            },
        }"
        :custom-search-controls="[
            {
                type: 'remote-select',
                field: ['dy', 'owner.id'],
                api: $API.sys_userinvite.query,
                params: { filter: { isPlainQuery: true } },
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/user.invite/query`),
                config: { props: { label: 'user.userName', value: 'user.id' } },
                placeholder: $t('归属用户'),
                style: 'width:10rem',
            },
            {
                type: 'remote-select',
                field: ['dy', 'owner.invite.owner.id'],
                api: $API.sys_userinvite.query,
                params: { filter: { isPlainQuery: true } },
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/user.invite/query`),
                config: { props: { label: 'user.userName', value: 'user.id' } },
                placeholder: $t('上级'),
                style: 'width:10rem',
            },
            {
                type: 'remote-select',
                field: ['dy', 'owner.invite.channel.id'],
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/user/query`),
                api: $API.sys_user.query,
                params: { filter: { roleId: 698928437833740 } },
                config: { props: { label: 'userName', value: 'id' } },
                placeholder: $t('渠道'),
                style: 'width:10rem',
            },
        ]"
        :dy-filters="dyFilters"
        :operations="[`view`]"
        :right-buttons="rightButtons"
        :row-buttons="[
            {
                label: `进入付款`,
                condition: (row) => row.depositOrderStatus === 'waitingForPayment',
                click: async (row) => {
                    this.dialog.save = { row, mode: 'pay' }
                },
            },
            {
                label: `取消订单`,
                condition: (row) => row.depositOrderStatus === 'waitingForPayment' || row.depositOrderStatus === 'paymentConfirming',
                click: async (row) => {
                    try {
                        await this.$confirm(`是否确定取消充值订单 ${row.id} ？`, '提示', {
                            type: 'warning',
                            confirmButtonText: '是',
                            cancelButtonText: '否',
                        })
                    } catch {
                        return
                    }
                    const loading = this.$loading()
                    if ((await this.$API.sys_depositorder.delete.post({ id: row.id })).data === 1) {
                        this.$message.success(`已取消充值订单`)
                    } else {
                        this.$message.error(`此订单不可取消`)
                    }
                    this.$refs.tablePage.$refs.table.refresh()
                    loading.close()
                },
            },
        ]"
        :show-search-mine="$GLOBAL.user.isAdmin || $GLOBAL.user.isChannel || $GLOBAL.user.isSeller"
        :show-selection="false"
        :summary="$t(`充值订单`)"
        entity-name="sys_depositorder"
        ref="tablePage"
        total-count-label="订单总数" />

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.tablePage.$refs.table, data, mode)"
        ref="saveDialog" />

    <save-step-dialog
        v-if="dialog.saveStep"
        @closed="dialog.saveStep = null"
        @mounted="
            () => {
                this.$refs.saveStepDialog.open(dialog.saveStep)
            }
        "
        @success="(data, mode) => table.handleUpdate($refs.tablePage.$refs.table, data, mode)"
        ref="saveStepDialog" />
</template>
<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
const saveStepDialog = defineAsyncComponent(() => import('./save-step'))
const saveDialog = defineAsyncComponent(() => import('./save'))
export default {
    computed: {
        table() {
            return table
        },
    },
    components: {
        saveStepDialog,
        saveDialog,
    },
    async created() {
        if (this.ownerId) {
            this.dyFilters.push({
                field: `owner.id`,
                operator: `eq`,
                value: this.ownerId,
            })
        } else if (this.$route.query.id) {
            this.dyFilters.push({
                field: `id`,
                operator: `eq`,
                value: this.$route.query.id,
            })
        } else if (this.$GLOBAL.user.isAdmin) {
            this.dyFilters.push({
                field: 'createdTime',
                operator: 'dateRange',
                value: [this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd 00:00:00'), this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd 00:00:00')],
            })
        }

        if ((await this.$API.sys_userinvite.getSelfDepositAllowed.post({})).data) {
            this.rightButtons.push({
                label: '创建充值订单',
                click: this.addOrderPrev,
                props: {
                    type: 'primary',
                    icon: 'el-icon-plus',
                },
            })
        }
    },
    methods: {
        async addOrderPrev() {
            let existNoPay = await this.$API.sys_depositorder.pagedQuery.post({
                dynamicFilter: {
                    filters: [
                        {
                            field: `owner.id`,
                            operator: `eq`,
                            value: this.$GLOBAL.user.id,
                        },
                        {
                            field: `depositOrderStatus`,
                            operator: `any`,
                            value: [1, 2],
                        },
                    ],
                },
            })
            if (existNoPay.data.total > 0) {
                this.$message.error(`请先完成或取消正在进行的订单`)
                return null
            }
            this.dialog.saveStep = { mode: 'add' }
        },
    },
    data() {
        return {
            dyFilters: [],
            dialog: {},
            rightButtons: [],
        }
    },
    async activated() {
        if (this.$route.query.action === 'create') {
            await this.addOrderPrev()
        }
    },
    props: ['ownerId'],
}
</script>