<template>
    <el-cascader
        v-bind="$attrs"
        v-model="data"
        :options="options"
        :placeholder="placeholder"
        :props="{
            value: 'id',
            label: 'name',
            emitPath: false,
            checkStrictly: true,
            expandTrigger: 'hover',
        }"
        clearable></el-cascader>
</template>
<style scoped></style>
<script>
export default {
    props: {
        modelValue: { type: Object },
        placeholder: { type: String },
    },
    data() {
        return {
            loaded: false,
            data: null,
            options: [],
        }
    },
    watch: {
        data(n) {
            this.$emit('update:modelValue', n)
        },

        modelValue: {
            immediate: true,
            deep: true,
            handler(n) {
                this.data = n ?? null
            },
        },
    },
    mounted() {},
    async created() {
        const res = await this.$API.sys_dic.queryCatalog.post()
        this.options = res.data
    },
    components: {},
    computed: {},
    methods: {},
}
</script>