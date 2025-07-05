<template>
    <el-button
        :disabled="vue.table.selection.length === 0 || loading"
        :loading="loading"
        @click="bulkDel"
        icon="el-icon-delete"
        plain
        type="danger" />
</template>
<style scoped />
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
        async bulkDel() {
            this.loading = true
            let load
            try {
                await this.$confirm(this.$t('确定删除选中的 {count} 项吗？', { count: this.vue.table.selection.length }), this.$t('提示'), {
                    type: 'warning',
                })
                load = this.$loading()
                const res = await this.api.post({
                    items: this.vue.table.selection,
                })
                this.vue.$refs.table.refresh()
                this.$message.success(this.$t('删除 {count} 项', { count: res.data }))
            } catch {
                //
            }
            load?.close()
            this.loading = false
        },
    },
}
</script>