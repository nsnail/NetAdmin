<template>
    <na-table-page
        :columns="{
            id: {
                label: $t(`交易编号`),
                is: `na-col-id`,
                extra: [`createdTime`],
                width: 170,
                show: [`list`, `view`],
                searchable: true,
            },
            'owner.id': {
                headerAlign: `center`,
                is: `na-col-user`,
                userObjPath: `owner`,
                width: 170,
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
                condition: () => this.$GLOBAL.user.isAdmin || this.$GLOBAL.user.isChannel,
                width: 170,
                label: $t(`上级`),
                show: [`list`],
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
                show: [`list`],
            },
            tradeDirection: {
                rule: {
                    required: true,
                },
                selectFilter: { countBy: true },
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
                selectFilter: { countBy: true },
                is: `na-col-indicator`,
                enum: { name: `tradeTypes` },
                label: $t(`交易类型`),
                align: `center`,
                width: 150,
                show: [`list`, `view`],
            },
            businessOrderNumber: {
                is: businessOrderNumber,
                label: $t(`业务订单号`),
                align: `right`,
                width: 170,
                show: [`list`, `view`],
                searchable: true,
            },
            balanceBefore: {
                label: $t(`交易前余额`),
                currency: {
                    flag: '&#165;',
                },
                formatter: (row) => $TOOL.groupSeparator(row.balanceBefore),
                align: `right`,
                width: 125,
                show: [`list`, `view`],
            },
            amount: {
                label: $t(`交易金额`),
                currency: {
                    flag: '&#165;',
                },
                formatter: (row) => $TOOL.groupSeparator(row.amount),
                align: `right`,
                sum: {
                    label: $t(`累计交易金额`),
                    currency: {
                        flag: '&#165;',
                    },
                },
                width: 125,
                show: [`list`, `view`],
            },
            balanceAfter: {
                sortable: false,
                showInContextMenu: false,
                label: $t(`交易后余额`),
                formatter: (row) => '&#165;' + $TOOL.groupSeparator(((row.balanceBefore + row.amount) / 100).toFixed(2)),
                align: `right`,
                width: 125,
                show: [`list`],
            },
            summary: {
                showOverflowTooltip: true,
                label: $t(`备注`),
                show: [`list`, `view`],
                searchable: true,
                operator: `contains`,
            },
            createdUserName: {
                show: [],
            },
            createdUserId: {
                extra: ['createdUserName'],
                headerAlign: `center`,
                is: `na-col-user`,
                userNameFieldName: `createdUserName`,
                userIdFieldName: `createdUserId`,
                userObjPath: ``,
                width: 170,
                label: $t(`操作人`),
                show: [`list`, `view`],
            },
            createdTime: {
                label: $t(`创建时间`),
                show: [`view`],
                operator: `dateRange`,
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
        :show-search-mine="$GLOBAL.user.isAdmin || $GLOBAL.user.isChannel || $GLOBAL.user.isSeller"
        :summary="$t(`交易记录`)"
        entity-name="sys_wallettrade"
        ref="tablePage"
        total-count-label="交易笔数" />
</template>

<script>
import { defineAsyncComponent } from 'vue'
export default {
    created() {
        if (this.$GLOBAL.hasApiPermission(this.$API.sys_wallettrade.export?.url.replace(`${this.$CONFIG.API_URL}/`, ``))) {
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

        if (this.$GLOBAL.user.roles[0].id === 371729946431493) {
            delete this.$GLOBAL.enums.tradeTypes.commission
        }

        if (this.ownerId) {
            this.dyFilters.push({
                field: `owner.id`,
                operator: `eq`,
                value: this.ownerId,
            })
        } else if (this.$route.query['business-order-number']) {
            this.dyFilters.push({
                field: `businessOrderNumber`,
                operator: `eq`,
                value: this.$route.query['business-order-number'],
            })
        } else if (this.$GLOBAL.user.isAdmin) {
            this.dyFilters.push({
                field: 'createdTime',
                operator: 'dateRange',
                value: [this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd 00:00:00'), this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd 00:00:00')],
            })
        }

        if (this.row) {
            if (this.row.availableBalance >= 0) {
                this.dyFilters.push({
                    field: 'owner.id',
                    operator: 'eq',
                    value: this.row.id.toString(),
                })
            } else {
                this.dyFilters.push({
                    field: 'businessOrderNumber',
                    operator: 'eq',
                    value: this.row.id.toString(),
                })
            }
        }
    },
    data() {
        return {
            businessOrderNumber: defineAsyncComponent(() => import('./components/business-order-number')),
            rightButtons: [],
            dyFilters: [],
        }
    },
    props: {
        row: { type: Object },
        ownerId: { type: Number },
    },
}
</script>
<style scoped />