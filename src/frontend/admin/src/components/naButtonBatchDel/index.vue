<template>
    <el-button :disabled="vue.selection.length === 0" :loading="loading" icon="el-icon-delete" plain type="danger" @click="batchDel"></el-button>
</template>
<style scoped></style>
<script>
export default {
    emits: [],
    props: { vue: { type: Object }, data: { type: Object }, api: { type: Object } },
    data() {
        return {
            loading: false,
        }
    },
    mounted() {},
    created() {},
    components: {},
    computed: {},
    methods: {
        //批量删除
        async batchDel() {
            this.loading = true
            try {
                await this.$confirm(`确定删除选中的 ${this.vue.selection.length} 项吗？`, '提示', {
                    type: 'warning',
                })
                const res = await this.api.post({
                    items: this.vue.selection,
                })
                this.vue.$refs.table.refresh()
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            this.loading = false
        },
    },
}
</script>