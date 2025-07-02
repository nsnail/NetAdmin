<template>
    <sc-table-select
        v-model="area"
        :params="form"
        :props="{ label: 'key', value: 'value' }"
        :query-api="$API.sys_dic.pagedQueryContent"
        clearable
        ref="area">
        <template #header>
            <el-form :model="form">
                <el-form-item>
                    <el-input v-model="form.keywords" :placeholder="$t('请输入地区或代码')" @input="onInput" clearable />
                </el-form-item>
            </el-form>
        </template>
        <el-table-column :label="$t('地区')" prop="key" width="400" />
        <el-table-column :label="$t('代码')" prop="value" />
    </sc-table-select>
</template>
<style scoped />
<script>
import { defineAsyncComponent } from 'vue'

const scTableSelect = defineAsyncComponent(() => import('@/components/sc-table-select'))
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
                    field: 'catalogId',
                    value: this.$GLOBAL.numbers.ID_DIC_CATALOG_GEO_AREA,
                    operator: 'eq',
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
    components: {
        scTableSelect,
    },
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