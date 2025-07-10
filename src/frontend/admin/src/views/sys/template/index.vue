<template>
    <na-table-page
        :columns="{
            id: {
                label: $t(`唯一编码`),
                is: `na-col-id`,
                extra: [`createdTime`],
                width: 170,
                show: [`list`, `view`],
                searchable: true,
            },
            name: {
                label: $t(`名称`),
                rule: {
                    required: true,
                },
                show: [`list`, `view`, `add`, `edit`],
                searchable: true,
                operator: `contains`,
            },
            gender: {
                label: $t(`性别`),
                is: `na-col-indicator`,
                enum: `genders`,
                width: 200,
                align: `center`,
                rule: {
                    required: true,
                },
                countBy: true,
                show: [`list`, `view`, `add`, `edit`],
            },
            sort: {
                label: $t(`排序`),
                align: `right`,
                thousands: true,
                width: 100,
                show: [`list`, `view`, `add`, `edit`],
                rule: {
                    required: true,
                    validator: (rule, value, callback) => {
                        if (/^-?\d+$/.test(value)) callback()
                        else callback(new Error())
                    },
                },
            },
            summary: {
                label: $t(`备注`),
                show: [`list`, `view`, `add`, `edit`],
                searchable: true,
                operator: `contains`,
            },
            enabled: {
                label: $t(`启用`),
                width: 100,
                align: `center`,
                countBy: true,
                show: [`list`, `view`],
                isBoolean: true,
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
        :operations="operations"
        :summary="$t(`页面模板`)"
        entity-name="sys_codetemplate" />
</template>
<script>
export default {
    created() {
        if (this.$GLOBAL.hasApiPermission(`api/sys/code.template/get`)) {
            this.operations.push(`view`)
        }
        if (this.$GLOBAL.hasApiPermission(`api/sys/code.template/edit`)) {
            this.operations.push(`edit`)
        }
        if (this.$GLOBAL.hasApiPermission(`api/sys/code.template/create`)) {
            this.operations.push(`add`)
        }
        if (this.$GLOBAL.hasApiPermission(`api/sys/code.template/delete`)) {
            this.operations.push(`del`)
        }
    },
    data() {
        return {
            operations: [],
        }
    },
}
</script>
<style scoped />