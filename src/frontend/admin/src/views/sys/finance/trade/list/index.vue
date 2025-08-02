<template>
    <na-table-page
        :columns="{
            id: {
                label: $t(`交易编号`),
                is: `na-col-id`,
                extra: [`createdTime`],
                width: 170,
                show: [`list`, `view`],
                searchable: `eq`,
            },
            ownerId: {
                rule: {
                    type: `number`,
                    required: true,
                },
                headerAlign: `center`,
                is: `na-col-user`,
                showUserDialog: this.$GLOBAL.hasApiPermission(`api/sys/user/get`),
                nestProp: `owner`,
                width: 170,
                label: $t(`归属用户`),
                show: [`list`, `view`],
                detail: {
                    is: `sc-select`,
                    props: {
                        queryApi: $API.sys_user.query,
                        clearable: true,
                        filterable: true,
                        config: {
                            props: {
                                label: `userName`,
                                value: `id`,
                            },
                        },
                    },
                },
            },
            tradeDirection: {
                rule: {
                    required: true,
                },
                countBy: true,
                is: `na-col-indicator`,
                enum: { name: `tradeDirections` },
                label: $t(`交易方向`),
                align: `center`,
                width: 100,
                show: [`list`, `view`],
            },
            tradeType: {
                rule: {
                    required: true,
                },
                countBy: true,
                is: `na-col-indicator`,
                enum: { name: `tradeTypes` },
                label: $t(`交易类型`),
                align: `center`,
                width: 150,
                show: [`list`, `view`],
            },
            businessOrderNumber: {
                label: $t(`业务订单号`),
                align: `right`,
                width: 170,
                show: [`list`, `view`],
                searchable: `eq`,
            },
            balanceBefore: {
                label: $t(`交易前余额`),
                formatter: (row) => $TOOL.groupSeparator(row.balanceBefore),
                align: `right`,
                width: 120,
                show: [`list`, `view`],
            },
            amount: {
                label: $t(`发生金额`),
                formatter: (row) => $TOOL.groupSeparator(row.amount),
                align: `right`,
                width: 120,
                show: [`list`, `view`],
            },
            balanceAfter: {
                sortable: false,
                noFilterable: true,
                label: $t(`交易后余额`),
                formatter: (row) => $TOOL.groupSeparator(row.balanceBefore + row.amount),
                align: `right`,
                width: 120,
                show: [`list`],
            },
            summary: {
                showOverflowTooltip: true,
                label: $t(`备注`),
                show: [`list`, `view`],
                searchable: `eq`,
                operator: `contains`,
            },
            createdTime: {
                label: $t(`创建时间`),
                show: [`view`],
                operator: `dateRange`,
            },
            createdUserId: {
                headerAlign: `center`,
                is: `na-col-user`,
                showUserDialog: this.$GLOBAL.hasApiPermission(`api/sys/user/get`),
                nestProp: `createdUserName`,
                width: 170,
                label: $t(`操作人`),
                show: [`list`, `view`],
                detail: {
                    is: `sc-select`,
                    props: {
                        queryApi: $API.sys_user.query,
                        clearable: true,
                        filterable: true,
                        config: {
                            props: {
                                label: `userName`,
                                value: `id`,
                            },
                        },
                    },
                },
            },
        }"
        :dy-filters="dyFilters"
        :operations="[`view`]"
        :summary="$t(`冻结记录`)"
        entity-name="sys_wallettrade" />
</template>

<script>
export default {
    created() {
        if (this.row) {
            this.dyFilters.push({
                field: 'businessOrderNumber',
                operator: 'eq',
                value: this.row.id.toString(),
            })
        }
    },
    data() {
        return { dyFilters: [] }
    },
    props: {
        row: { type: Object },
    },
}
</script>
<style scoped />