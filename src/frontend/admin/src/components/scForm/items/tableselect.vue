<template>
    <sc-table-select
        v-model="value"
        :apiObj="apiObj"
        :multiple="item.options.multiple"
        :props="item.options.props"
        :table-width="60"
        style="width: 100%">
        <el-table-column v-for="(_item, _index) in item.options.column" :key="_index" :label="_item.label" :prop="_item.prop" :width="_item.width" />
    </sc-table-select>
</template>

<script>
export default {
    name: 'uploadRender',
    props: {
        modelValue: [String, Number, Boolean, Date, Object, Array],
        item: {
            type: Object,
            default: () => {},
        },
    },
    data() {
        return {
            value: this.modelValue,
            apiObj: this.getApiObj(),
        }
    },
    watch: {
        value(val) {
            this.$emit('update:modelValue', val)
        },
    },
    mounted() {},
    methods: {
        getApiObj() {
            return eval(`this.` + this.item.options.apiObj)
        },
    },
}
</script>

<style></style>