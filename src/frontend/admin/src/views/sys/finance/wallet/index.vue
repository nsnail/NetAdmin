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
            ownerId: {
                headerAlign: `center`,
                is: `na-col-user`,
                showUserDialog: this.$GLOBAL.hasApiPermission(`api/sys/user/get`),
                nestProp: `owner`,
                width: 170,
                label: $t(`归属用户`),
                show: [`list`, `view`],
                searchable: `eq`,
            },
            'owner.roles': {
                sortable: false,
                is: `na-col-tags`,
                field: `name`,
                label: $t(`归属角色`),
                show: [`list`],
                width: 300,
            },
            availableBalance: {
                label: '可用余额',
                formatter: (row) => $TOOL.groupSeparator(row.availableBalance),
                show: [`list`, `view`],
                align: `right`,
                width: 120,
            },
            frozenBalance: {
                label: '冻结余额',
                formatter: (row) => $TOOL.groupSeparator(row.frozenBalance),
                show: [`list`, `view`],
                align: `right`,
                width: 120,
            },
            totalIncome: {
                label: '总收入',
                formatter: (row) => $TOOL.groupSeparator(row.totalIncome),
                show: [`list`, `view`],
                align: `right`,
                width: 120,
            },
            totalExpenditure: {
                label: '总支出',
                formatter: (row) => $TOOL.groupSeparator(row.totalExpenditure),
                show: [`list`, `view`],
                align: `right`,
                width: 120,
            },
            createdTime: {
                label: $t(`创建时间`),
                show: [`view`],
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
        :dialog-full-screen="['view']"
        :operations="[`view`]"
        :row-buttons="[
            {
                label: `新建交易`,
                click: (row) => {
                    this.dialog.trade = { row }
                },
            },
        ]"
        :summary="$t(`钱包管理`)"
        :tabs="tabs"
        entity-name="sys_userwallet"
        ref="tablePage" />

    <trade-dialog
        v-if="dialog.trade"
        @closed="dialog.trade = null"
        @mounted="$refs.tradeDialog.open(dialog.trade)"
        @success="(data, mode) => $refs.tablePage.$refs.table.refresh()"
        ref="tradeDialog" />
</template>

<script>
import { defineAsyncComponent } from 'vue'

const tradeDialog = defineAsyncComponent(() => import('@/views/sys/finance/wallet/trade'))

export default {
    components: {
        tradeDialog,
    },
    computed: {},
    created() {},
    data() {
        return {
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
                ],
            },
        }
    },
    inject: [`reload`],
    methods: {},
    mounted() {},
    props: [`keywords`],
    watch: {},
}
</script>

<style scoped />