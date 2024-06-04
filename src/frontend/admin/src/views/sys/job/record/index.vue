<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('作业编号 / 执行编号'),
                            style: 'width:20rem',
                        },
                        {
                            type: 'select',
                            field: ['dy', 'httpMethod'],
                            options: Object.entries(this.$GLOBAL.enums.httpMethods).map((x) => {
                                return { value: x[0], label: x[1][1] }
                            }),
                            placeholder: $t('请求方式'),
                            style: 'width:10rem',
                        },
                        {
                            multiple: true,
                            type: 'select',
                            field: ['dy', 'httpStatusCode'],
                            options: [
                                { label: '20x', value: '200,299' },
                                { label: '30x', value: '300,399' },
                                { label: '40x', value: '400,499' },
                                { label: '50x', value: '500,599' },
                                { label: '90x', value: '900,999' },
                            ],
                            placeholder: '状态码',
                            style: 'width:20rem',
                        },
                    ]"
                    :vue="this"
                    @search="onSearch"
                    ref="search" />
            </div>
            <div class="right-panel"></div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                v-loading="loading"
                :apiObj="$API.sys_job.recordPagedQuery"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :params="query"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <el-table-column :label="$t('唯一编码')" prop="id" sortable="custom" width="150" />
                <el-table-column
                    :formatter="(row) => `${tool.groupSeparator(row.duration.toFixed(0))} ms`"
                    :label="$t('执行耗时')"
                    align="right"
                    prop="duration"
                    sortable="custom"
                    width="150" />
                <na-col-indicator
                    :label="$t('请求方式')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.httpMethods).map((x) => {
                            return { value: x[0], text: x[1][1] }
                        })
                    "
                    prop="httpMethod"
                    width="100" />
                <el-table-column :label="$t('响应状态码')" align="center" prop="httpStatusCode" sortable="custom" width="200">
                    <template #default="scope">
                        <div class="indicator">
                            <sc-status-indicator :type="scope.row.httpStatusCode === 'ok' ? 'success' : 'danger'" />
                            <span>{{
                                this.$GLOBAL.enums.httpStatusCodes[scope.row.httpStatusCode]
                                    ? this.$GLOBAL.enums.httpStatusCodes[scope.row.httpStatusCode][1]
                                    : scope.row.httpStatusCode
                            }}</span>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('请求的网络地址')" prop="requestUrl" sortable="custom" />
                <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom" width="170" />
                <na-col-operation :buttons="[naColOperation.buttons[0]]" :vue="this" width="100" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = false"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>
</template>

<script>
import saveDialog from './save'
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'
import tool from '@/utils/tool'

export default {
    props: ['keywords'],
    components: {
        saveDialog,
    },
    data() {
        return {
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
            },
            dialog: {
                save: false,
            },
            selection: [],
        }
    },
    watch: {},
    computed: {
        tool() {
            return tool
        },
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    mounted() {
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keepKeywords = this.keywords
        }
    },
    created() {
        if (this.keywords) {
            this.query.keywords = this.keywords
        }
    },
    methods: {
        //表格内开关事件
        async changeSwitch(event, row) {
            try {
                await this.$API.sys_job.setEnabled.post(row)
                this.$message.success(`操作成功`)
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        //删除
        async rowDel(row) {
            try {
                const res = await this.$API.sys_job.delete.post({ id: row.id })
                this.$refs.table.refresh()
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
        },
        //批量删除
        async batchDel() {
            let loading
            try {
                await this.$confirm(`确定删除选中的 ${this.selection.length} 项吗？`, '提示', {
                    type: 'warning',
                })
                loading = this.$loading()
                const res = await this.$API.sys_job.bulkDelete.post({
                    items: this.selection,
                })
                this.$refs.table.refresh()
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            loading?.close()
        },

        //搜索
        onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime,
                })
            }

            if (typeof form.dy.httpMethod === 'string' && form.dy.httpMethod.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'httpMethod',
                    operator: 'eq',
                    value: form.dy.httpMethod,
                })
            }

            if (Array.isArray(form.dy.httpStatusCode) && form.dy.httpStatusCode.length !== 0) {
                const filters = []
                for (const code of form.dy.httpStatusCode) {
                    filters.push({
                        field: 'httpStatusCode',
                        operator: 'range',
                        value: code,
                    })
                }
                this.query.dynamicFilter.filters.push({
                    logic: 'or',
                    filters: filters,
                })
            }

            this.$refs.table.upData()
        },
    },
}
</script>

<style scoped></style>