<template>
    <na-table-page
        :columns="{
            id: {
                label: $t(`冻结编号`),
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
            amount: {
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
                enum: `walletFrozenStatues`,
                label: $t(`状态`),
                countBy: true,
                align: `center`,
                width: 100,
                show: [`list`, `view`],
            },
            reason: {
                rule: {
                    required: true,
                },
                countBy: true,
                is: `na-col-indicator`,
                enum: `walletFrozenReasons`,
                label: $t(`冻结原因`),
                align: `center`,
                width: 100,
                show: [`list`, `view`, `add`],
            },
            modifiedTime: {
                label: $t(`更新时间`),
                show: [`list`, `view`],
                width: 170,
            },
            summary: {
                label: $t(`备注`),
                show: [`list`, `view`, `add`],
                searchable: `eq`,
                operator: `contains`,
            },
            createdTime: {
                label: $t(`创建时间`),
                show: [`view`],
            },
            version: {
                label: $t(`数据版本`),
                show: [`view`],
            },
        }"
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
        :summary="$t(`冻结记录`)"
        entity-name="sys_walletfrozen" />
</template>
<script>
export default {
    created() {
        if (this.row) {
            this.dyFilters.push({
                field: 'id',
                operator: 'eq',
                value: this.row.frozenId.toString(),
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