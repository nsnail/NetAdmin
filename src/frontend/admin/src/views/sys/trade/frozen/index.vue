<template>
    <na-table-page
        :buttons="[
            {
                title: $t(`解冻`),
                condition: (row) => {
                    return row.status === `frozen` && this.$GLOBAL.hasApiPermission(`api/sys/wallet.frozen/set.status.to.thawed`)
                },
                click: async (row) => {
                    try {
                        await this.$confirm(`确定解冻该记录？`, '提示', {
                            type: 'warning',
                            confirmButtonText: '是',
                            cancelButtonText: '否',
                        })
                    } catch {
                        return
                    }
                    await this.$API.sys_walletfrozen.setStatusToThawed.post({ id: row.id })
                    return true
                },
            },
        ]"
        :columns="{
            id: {
                label: $t(`日志编号`),
                is: `na-col-id`,
                extra: [`createdTime`],
                width: 170,
                show: [`list`, `view`],
                searchable: true,
            },
            ownerId: {
                rule: {
                    type: `number`,
                    required: true,
                },
                headerAlign: `center`,
                is: `na-col-user`,
                clickOpenDialog: this.$GLOBAL.hasApiPermission(`api/sys/user/get`),
                nestProp: `owner.userName`,
                nestProp2: `ownerId`,
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
                thousands: true,
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
                searchable: true,
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
        :operations="[`view`, `add`]"
        :summary="$t(`冻结记录`)"
        entity-name="sys_walletfrozen" />
</template>

<style scoped />