<template>
    <el-cascader
        v-model="dept"
        :options="options"
        :placeholder="placeholder"
        :props="{ label: 'name', value: 'id', checkStrictly: true, expandTrigger: 'hover', emitPath: false }"
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
        this.options = (await this.$API.sys_dept.query.post()).data
    },
    components: {},
    computed: {},
    methods: {},
}
</script>