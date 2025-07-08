<template>
    <na-table-page
        :columns="{
            id: {
                label: $t(`唯一编码`),
                is: `na-col-id`,
                extra: [`createdTime`],
                width: 170,
                show: [`list`, `view`],
                searchable: `eq`,
            },
            name: {
                label: $t(`名字`),
                width: 150,
                show: [`list`, `view`, `add`, `edit`],
                rule: {
                    required: true,
                },
                searchable: `eq`,
            },
            gender: {
                label: $t(`性别`),
                is: `na-col-indicator`,
                enum: `genders`,
                width: 100,
                align: `center`,
                countBy: true,
                show: [`list`, `view`, `add`, `edit`],
            },
            sort: {
                label: $t(`排序`),
                align: `right`,
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
                searchable: `contains`,
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
        :operations="[`add`, `del`, `edit`]"
        :search-controls="[
            {
                type: `input`,
                field: [`root`, `keywords`],
                placeholder: $t(`消息编号 / 消息主题 / 消息内容`),
                style: `width:25rem`,
            },
        ]"
        :select-filters="[
            {
                title: $t(`是否启用`),
                key: `Enabled`,
                enumName: `Enabled`,
                isBoolean: [
                    { label: $t(`启用`), value: true },
                    { label: $t(`禁用`), value: false },
                ],
            },
            {
                title: $t(`性别`),
                key: `Gender`,
                enumName: `genders`,
            },
        ]"
        :summary="$t(`代码模板`)"
        entity-name="sys_codetemplate" />
</template>

<script>
export default {
    components: {},
    computed: {},
    created() {},
    data() {},
    inject: [`reload`],
    methods: {},
    mounted() {},
    props: [`keywords`],
    watch: {},
}
</script>

<style scoped />