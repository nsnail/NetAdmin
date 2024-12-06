<template>
    <el-container>
        <el-header>
            <div class="left-panel"></div>
            <div class="right-panel">
                <el-button :loading="loading" @click="sync" icon="sc-icon-sync" type="primary">{{ $t('同步接口') }}</el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :export-api="$API.sys_api.export"
                :query-api="$API.sys_api.query"
                :summary-method="(x) => ['接口总数', countTotalRows(x.data)]"
                default-expand-all
                hidePagination
                ref="table"
                row-key="id"
                show-summary
                stripe>
                <el-table-column :label="$t('接口路径')" min-width="400" prop="id" />
                <el-table-column :label="$t('接口名称')" min-width="200" prop="name" />
                <na-col-indicator
                    :label="$t('请求方式')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.httpMethods).map((x) => {
                            return { value: x[0].toUpperCase(), text: x[1][1] }
                        })
                    "
                    align="center"
                    prop="method"
                    sortable="custom"
                    width="150" />
                <el-table-column :label="$t('接口描述')" min-width="200" prop="summary" />
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
                this.$message.success('同步成功')
            } catch {
                //
            }
            this.$refs.table.refresh()
            this.loading = false
        },
    },
}
</script>

<style></style>