<template>
    <na-table-page
        :columns="{
            id: {
                label: $t(`钱包编号`),
                is: `na-col-id`,
                extra: [`createdTime`],
                width: 170,
                show: [`list`, `view`],
            },
            'owner.userName': {
                show: [],
            },
            'owner.id': {
                extra: ['owner.userName'],
                headerAlign: `center`,
                is: `na-col-user`,
                userObjPath: `owner`,
                width: 170,
                label: $t(`归属用户`),
                show: [`list`, `view`],
            },
            'owner.roles': {
                sortable: false,
                is: `na-col-tags`,
                field: `name`,
                label: $t(`归属角色`),
                condition: () => this.$GLOBAL.user.isAdmin || this.$GLOBAL.user.isChannel,
                show: [`list`],
                width: 150,
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
            availableBalance: {
                label: '可用余额',
                currency: {
                    flag: '&#165;',
                },
                formatter: (row) => $TOOL.groupSeparator(row.availableBalance),
                show: [`list`, `view`],
                align: `right`,
                width: 125,
                sum: {
                    label: $t(`可用余额`),
                    currency: {
                        flag: '&#165;',
                    },
                    span: 1,
                },
            },
            frozenBalance: {
                label: '冻结余额',
                currency: {
                    flag: '&#165;',
                },
                formatter: (row) => $TOOL.groupSeparator(row.frozenBalance),
                show: [`list`, `view`],
                align: `right`,
                width: 125,
                sum: {
                    label: $t(`冻结余额`),
                    currency: {
                        flag: '&#165;',
                    },
                    span: 1,
                },
            },
            totalIncome: {
                label: '总收入',
                currency: {
                    flag: '&#165;',
                },
                formatter: (row) => $TOOL.groupSeparator(row.totalIncome),
                show: [`list`, `view`],
                align: `right`,
                width: 125,
                sum: {
                    label: $t(`总收入`),
                    currency: {
                        flag: '&#165;',
                    },
                    span: 1,
                },
            },
            totalExpenditure: {
                label: '总支出',
                currency: {
                    flag: '&#165;',
                },
                formatter: (row) => $TOOL.groupSeparator(row.totalExpenditure),
                show: [`list`, `view`],
                align: `right`,
                width: 125,
                sum: {
                    label: $t(`总支出`),
                    currency: {
                        flag: '&#165;',
                    },
                    span: 1,
                },
            },
            createdTime: {
                label: $t(`创建时间`),
                show: [`view`],
                operator: `dateRange`,
            },
            modifiedTime: {
                relativeTime: true,
                align: `right`,
                label: $t(`最后交易时间`),
                show: [`list`, `view`],
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
                style: 'width:15rem',
            },
            {
                type: 'remote-select',
                field: ['filter', 'roleId'],
                api: $API.sys_role.query,
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/role/query`),
                config: { props: { label: 'name', value: 'id' } },
                placeholder: $t('归属角色'),
                style: 'width:15rem',
            },
            {
                type: 'remote-select',
                field: ['dy', 'owner.invite.owner.id'],
                api: $API.sys_userinvite.query,
                params: { filter: { isPlainQuery: true } },
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/user.invite/query`),
                config: { props: { label: 'user.userName', value: 'user.id' } },
                placeholder: $t('上级'),
                style: 'width:15rem',
            },
            {
                type: 'remote-select',
                field: ['dy', 'owner.invite.channel.id'],
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/user/query`),
                api: $API.sys_user.query,
                params: { filter: { roleId: 698928437833740 } },
                config: { props: { label: 'userName', value: 'id' } },
                placeholder: $t('渠道'),
                style: 'width:15rem',
            },
        ]"
        :dialog-full-screen="['view']"
        :dy-filters="dyFilters"
        :operations="[`view`]"
        :right-buttons="rightButtons"
        :row-buttons="[
            {
                label: `新建交易`,
                condition: () => this.$GLOBAL.hasApiPermission(this.$API.sys_wallettrade.create.url.replace(`${this.$CONFIG.API_URL}/`, ``)),
                click: (row) => {
                    this.dialog.trade = { row }
                },
            },
            {
                label: `余额转账`,
                condition: () =>
                    this.$GLOBAL.hasApiPermission(this.$API.sys_wallettrade.transferToAnotherAccount.url.replace(`${this.$CONFIG.API_URL}/`, ``)) ||
                    this.$GLOBAL.hasApiPermission(this.$API.sys_wallettrade.transferFromAnotherAccount.url.replace(`${this.$CONFIG.API_URL}/`, ``)),
                click: (row) => {
                    this.dialog.transfer = { row }
                },
            },
        ]"
        :show-search-mine="$GLOBAL.user.isAdmin || $GLOBAL.user.isChannel || $GLOBAL.user.isSeller"
        :summary="$t(`钱包`)"
        :tabs="tabs"
        entity-name="sys_userwallet"
        ref="tablePage"
        total-count-label="钱包总数" />

    <trade-dialog
        v-if="dialog.trade"
        @closed="dialog.trade = null"
        @mounted="$refs.tradeDialog.open(dialog.trade)"
        @success="(data, mode) => $refs.tablePage.$refs.table.refresh()"
        ref="tradeDialog" />

    <transfer-dialog
        v-if="dialog.transfer"
        @closed="dialog.transfer = null"
        @mounted="$refs.transferDialog.open(dialog.transfer)"
        @success="(data, mode) => $refs.tablePage.$refs.table.refresh()"
        ref="transferDialog" />
</template>

<script>
import { defineAsyncComponent } from 'vue'
const tradeDialog = defineAsyncComponent(() => import('./trade'))
const transferDialog = defineAsyncComponent(() => import('./transfer'))

export default {
    components: {
        tradeDialog,
        transferDialog,
    },
    computed: {},
    created() {
        if (this.$GLOBAL.hasApiPermission(this.$API.sys_userwallet.export?.url.replace(`${this.$CONFIG.API_URL}/`, ``))) {
            this.rightButtons.push({
                label: '导出',
                props: {
                    type: 'success',
                    icon: `el-icon-download`,
                    plain: true,
                },
                click: async () => {
                    await this.$refs.tablePage.$refs.table.exportData()
                },
            })
        }

        if (this.id) {
            this.dyFilters.push({
                field: `id`,
                operator: `eq`,
                value: this.id,
            })
        }
    },
    data() {
        return {
            rightButtons: [],
            dyFilters: [],
            dialog: {},
            tabs: {
                view: [
                    {
                        label: `交易流水`,
                        name: `trade`,
                        component: defineAsyncComponent(() => import('@/views/sys/finance/trade/list')),
                        props: {
                            ref: `trade`,
                        },
                    },
                    {
                        label: `冻结记录`,
                        name: `frozen`,
                        component: defineAsyncComponent(() => import('@/views/sys/finance/trade/frozen')),
                        props: {
                            ref: `frozen`,
                        },
                    },
                ],
            },
        }
    },
    inject: [`reload`],
    methods: {},
    mounted() {},
    props: [`keywords`, `id`],
    watch: {},
}
</script>

<style scoped />