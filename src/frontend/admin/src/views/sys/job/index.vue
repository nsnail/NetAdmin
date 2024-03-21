<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('作业编号 / 作业名称'),
                            style: 'width:20rem',
                        },
                        {
                            type: 'select',
                            field: ['dy', 'status'],
                            options: Object.entries(this.$GLOBAL.enums.jobStatues).map((x) => {
                                return { value: x[0], label: x[1][1] }
                            }),
                            placeholder: $t('作业状态'),
                            style: 'width:10rem',
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
                            type: 'select',
                            field: ['dy', 'enabled'],
                            options: [
                                { label: '启用', value: true },
                                { label: '禁用', value: false },
                            ],
                            placeholder: '状态',
                            style: 'width:10rem',
                        },
                    ]"
                    :vue="this"
                    @search="onSearch" />
            </div>
            <div class="right-panel">
                <na-button-add :vue="this" />
                <el-button :disabled="selection.length === 0" @click="batchDel" icon="el-icon-delete" plain type="danger"></el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                v-loading="loading"
                :apiObj="$API.sys_job.pagedQuery"
                :default-sort="{ prop: 'lastExecTime', order: 'descending' }"
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
                <el-table-column type="selection"></el-table-column>
                <el-table-column :label="$t('作业编号')" prop="id" width="150" />
                <el-table-column :label="$t('作业名称')" prop="jobName" />
                <na-col-indicator
                    :label="$t('作业状态')"
                    :options="[
                        { text: '空闲', type: 'success', value: 'idle' },
                        { text: '运行', type: 'warning', value: 'running' },
                    ]"
                    prop="status"
                    width="100"></na-col-indicator>
                <na-col-indicator
                    :label="$t('请求方式')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.httpMethods).map((x) => {
                            return { value: x[0], text: x[1][1] }
                        })
                    "
                    prop="httpMethod"
                    width="100" />
                <el-table-column :label="$t('上次执行时间')" prop="lastExecTime" sortable="custom" width="170" />
                <el-table-column :label="$t('上次执行状态')" prop="lastStatusCode" sortable="custom" width="150" />
                <el-table-column :label="$t('下次执行时间')" prop="nextExecTime" sortable="custom" width="170" />
                <el-table-column :label="$t('启用')" prop="enabled" sortable="custom" width="100">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" @change="changeSwitch($event, scope.row)"></el-switch>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('创建时间')" prop="createdTime" sortable="custom" width="170" />
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: $t('删除作业'),
                            click: rowDel,
                        })
                    "
                    :vue="this" />
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

export default {
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
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    mounted() {},
    created() {},
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

            if (typeof form.dy.status === 'string' && form.dy.status.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'status',
                    operator: 'eq',
                    value: form.dy.status,
                })
            }

            if (typeof form.dy.enabled === 'boolean') {
                this.query.dynamicFilter.filters.push({
                    field: 'enabled',
                    operator: 'eq',
                    value: form.dy.enabled,
                })
            }

            if (typeof form.dy.httpMethod === 'string' && form.dy.httpMethod.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'httpMethod',
                    operator: 'eq',
                    value: form.dy.httpMethod,
                })
            }

            this.$refs.table.upData()
        },
    },
}
</script>

<style scoped></style>