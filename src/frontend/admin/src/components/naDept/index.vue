<template>
    <el-cascader
        v-model="dept"
        :options="options"
        :placeholder="placeholder"
        :props="{ label: 'name', value: 'id', checkStrictly: true, expandTrigger: 'hover', emitPath: false, multiple: multiple }"
        clearable></el-cascader>
</template>
<style scoped></style>
<script>
export default {
    props: {
        modelValue: { type: Object },
        placeholder: { type: String },
        multiple: { type: Boolean, default: false },
    },
    data() {
        return {
            loaded: false,
            dept: null,
            options: [],
        }
    },
    watch: {
        dept(n) {
            this.$emit('update:modelValue', n)
        },

        modelValue: {
            immediate: true,
            deep: true,
            handler(n) {
                this.dept = n ?? null
            },
        },
    },
    mounted() {},
    async created() {
        const res = await this.$API.sys_dept.query.post()
        this.options = res.data
    },
    components: {},
    computed: {},
    methods: {},
}
</script>