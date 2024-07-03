<template>
    <sc-table-select
        v-model="area"
        :params="form"
        :props="{ label: 'key', value: 'value' }"
        :query-api="$API.sys_dic.pagedQueryContent"
        :table-width="60"
        clearable
        ref="area">
        <template #header>
            <el-form :model="form">
                <el-form-item>
                    <el-input v-model="form.keywords" :placeholder="$t('请输入地区或代码')" @input="onInput" clearable></el-input>
                </el-form-item>
            </el-form>
        </template>
        <el-table-column :label="$t('地区')" prop="key" width="400" />
        <el-table-column :label="$t('代码')" prop="value" />
    </sc-table-select>
</template>
<style scoped></style>
<script>
export default {
    props: {
        modelValue: { type: Object },
    },
    data() {
        return {
            area: {},
            form: {
                dynamicFilter: {
                    filters: [],
                    logic: 'or',
                },
            },
        }
    },
    watch: {
        area(n) {
            this.$emit('update:modelValue', n)
        },

        modelValue: {
            immediate: true,
            deep: true,
            handler(n) {
                this.area = n ?? {}
            },
        },
    },
    mounted() {},
    created() {},
    components: {},
    computed: {},
    methods: {
        onInput() {
            this.form.dynamicFilter.filters = []
            if (this.form.keywords) {
                this.form.dynamicFilter.filters.push({
                    field: 'key',
                    operator: 'contains',
                    value: this.form.keywords,
                })
                this.form.dynamicFilter.filters.push({
                    field: 'value',
                    operator: 'contains',
                    value: this.form.keywords,
                })
            }

            this.$refs.area.reload()
        },
    },
}
</script>