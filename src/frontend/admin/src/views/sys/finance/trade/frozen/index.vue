<template>
    <na-table-page
        :columns="{
            id: {
                label: $t(`冻结编号`),
                is: `na-col-id`,
                extra: [`createdTime`],
                width: 170,
                show: [`list`, `view`],
                searchable: true,
            },
            'owner.userName': {
                show: [],
            },
            'owner.id': {
                rule: {
                    type: `number`,
                    required: true,
                },
                extra: ['owner.userName'],
                headerAlign: `center`,
                is: `na-col-user`,
                userObjPath: `owner`,
                width: 170,
                label: $t(`归属用户`),
                show: [`list`, `view`, `add`],
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
            amount: {
                currency: {
                    flag: '&#165;',
                },
                sum: {
                    label: $t(`冻结金额`),
                    currency: {
                        flag: '&#165;',
                    },
                },
                label: $t(`冻结金额`),
                align: `right`,
                width: 100,
                show: [`list`, `view`, `add`],
                rule: {
                    required: true,
                    validator: (rule, value, callback) => {
                        if (/^[1-9]\d*$/.test(value)) callback()
                        else callback(new Error())
                    },
                },
            },
            status: {
                is: `na-col-indicator`,
                enum: { name: `walletFrozenStatues` },
                label: $t(`状态`),
                selectFilter: { countBy: true },
                align: `center`,
                width: 100,
                show: [`list`, `view`],
            },
            reason: {
                rule: {
                    required: true,
                },
                selectFilter: { countBy: true },
                is: `na-col-indicator`,
                enum: { name: `walletFrozenReasons` },
                label: $t(`冻结原因`),
                align: `center`,
                width: 100,
                show: [`list`, `view`, `add`],
            },
            summary: {
                label: $t(`备注`),
                show: [`list`, `view`, `add`],
                showOverflowTooltip: true,
                minWidth: 200,
                searchable: true,
                operator: `contains`,
            },
            createdTime: {
                label: $t(`创建时间`),
                show: [`view`],
                operator: `dateRange`,
            },
            modifiedTime: {
                label: $t(`更新时间`),
                show: [`list`, `view`],
                width: 170,
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
        :operations="[`view`, `add`]"
        :row-buttons="[
            {
                label: $t(`解冻`),
                condition: (row) => {
                    return row.status === `frozen` && this.$GLOBAL.hasApiPermission(`api/sys/wallet.frozen/set.status.to.thawed`)
                },
                click: async (row) => {
                    try {
                        await this.$confirm(this.$t(`确定解冻该记录？`), this.$t(`提示`), {
                            type: 'warning',
                            confirmButtonText: this.$t(`是`),
                            cancelButtonText: this.$t(`否`),
                        })
                    } catch {
                        return
                    }
                    await this.$API.sys_walletfrozen.setStatusToThawed.post({ id: row.id })
                    return true
                },
            },
        ]"
        :show-search-mine="$GLOBAL.user.isAdmin || $GLOBAL.user.isChannel || $GLOBAL.user.isSeller"
        :summary="$t(`冻结记录`)"
        entity-name="sys_walletfrozen"
        total-count-label="冻结记录" />
</template>
<script>
export default {
    created() {
        const id = this.row?.frozenId || this.$route.query.id
        if (id) {
            this.dyFilters.push({
                field: 'id',
                operator: 'eq',
                value: id.toString(),
            })
        } else {
            const ownerId = this.ownerId || (this.row ?? {})['owner.id']
            if (ownerId) {
                this.dyFilters.push({
                    field: `owner.id`,
                    operator: `eq`,
                    value: ownerId,
                })
            } else if (this.$GLOBAL.user.isAdmin) {
                this.dyFilters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: [this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd 00:00:00'), this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd 00:00:00')],
                })
            }
        }
    },
    data() {
        return { dyFilters: [] }
    },
    props: {
        row: { type: Object },
        ownerId: { type: Number },
    },
}
</script>
<style scoped />