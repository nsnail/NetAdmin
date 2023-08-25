<template>
    <el-container>
        <el-header>
            <div class="left-panel"></div>
            <div class="right-panel">
                <el-button :loading="loading" icon="sc-icon-sync" type="primary" @click="sync">同步接口</el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                ref="table"
                :apiObj="$API.sys_api.query"
                :summary-method="(x) => ['接口总数', countTotalRows(x.data)]"
                default-expand-all
                hidePagination
                row-key="id"
                show-summary
                stripe>
                <el-table-column label="接口路径" prop="id"></el-table-column>
                <el-table-column label="接口名称" prop="name"></el-table-column>
                <el-table-column label="请求方式" prop="method"></el-table-column>
                <el-table-column label="接口描述" prop="summary"></el-table-column>
            </sc-table>
        </el-main>
    </el-container>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            loading: false,
        }
    },
    methods: {
        countTotalRows(data) {
            let count = 0

            function countNodes(node) {
                if (node.method) count++
                if (node.children?.length > 0) {
                    for (const child of node.children) {
                        countNodes(child)
                    }
                }
            }

            for (const item of data) {
                countNodes(item)
            }

            return count
        },
        //同步
        async sync() {
            this.loading = true
            try {
                await this.$API.sys_api.sync.post()
                this.$refs.table.refresh()
                this.$message.success('同步成功')
            } catch {
                //
            }
            this.loading = false
        },
    },
}
</script>

<style></style>