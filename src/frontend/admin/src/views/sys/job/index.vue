<template>
    <el-container>
        <el-header style="height: auto; padding: 0 1rem">
            <sc-select-filter
                :data="[
                    {
                        title: $t('作业状态'),
                        key: 'status',
                        options: [
                            { label: $t('全部'), value: '' },
                            ...Object.entries(this.$GLOBAL.enums.jobStatues).map((x) => {
                                return { value: x[0], label: x[1][1] }
                            }),
                        ],
                    },
                    {
                        title: $t('启用状态'),
                        key: 'enabled',
                        options: [
                            { label: $t('全部'), value: '' },
                            { label: $t('启用'), value: true },
                            { label: $t('禁用'), value: false },
                        ],
                    },
                ]"
                :label-width="6"
                @on-change="filterChange"
                ref="selectFilter"></sc-select-filter>
        </el-header>
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
                            field: ['dy', 'httpMethod'],
                            options: Object.entries(this.$GLOBAL.enums.httpMethods).map((x) => {
                                return { value: x[0], label: x[1][1] }
                            }),
                            placeholder: $t('请求方式'),
                            style: 'width:10rem',
                        },
                    ]"
                    :vue="this"
                    @reset="Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))"
                    @search="onSearch"
                    ref="search" />
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
                :context-menus="[
                    'id',
                    'jobName',
                    'executionCron',
                    'status',
                    'httpMethod',
                    'lastExecTime',
                    'lastStatusCode',
                    'nextExecTime',
                    'enabled',
                    'createdTime',
                ]"
                :default-sort="{ prop: 'lastExecTime', order: 'descending' }"
                :page-size="100"
                :params="query"
                :vue="this"
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
                <el-table-column type="selection" />
                <el-table-column :label="$t('作业编号')" prop="id" sortable="custom" width="150" />
                <el-table-column :label="$t('作业名称')" prop="jobName" show-overflow-tooltip sortable="custom" />
                <el-table-column :label="$t('执行计划')" align="center" prop="executionCron" sortable="custom" width="150" />
                <na-col-indicator
                    :label="$t('作业状态')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.jobStatues).map((x) => {
                            return { value: x[0], text: x[1][1], type: x[1][2] }
                        })
                    "
                    align="center"
                    prop="status"
                    sortable="custom"
                    width="100" />
                <na-col-indicator
                    :label="$t('请求方式')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.httpMethods).map((x) => {
                            return { value: x[0], text: x[1][1], type: x[1][2] }
                        })
                    "
                    align="center"
                    prop="httpMethod"
                    sortable="custom"
                    width="100" />
                <el-table-column :label="$t('上次执行')" align="center">
                    <el-table-column :label="$t('状态')" align="center" prop="lastExecTime" sortable="custom" width="150">
                        <template #default="scope">
                            <sc-status-indicator :type="scope.row.lastStatusCode === 'ok' ? 'success' : 'danger'" />
                            {{
                                this.$GLOBAL.enums.httpStatusCodes[scope.row.lastStatusCode]
                                    ? this.$GLOBAL.enums.httpStatusCodes[scope.row.lastStatusCode][1]
                                    : scope.row.lastStatusCode
                            }}
                        </template>
                    </el-table-column>
                    <el-table-column :label="$t('时间')" align="right" prop="lastExecTime" sortable="custom" width="100">
                        <template #default="scope">
                            <span v-if="scope.row.lastExecTime" v-time.tip="scope.row.lastExecTime"></span>
                        </template>
                    </el-table-column>
                    <el-table-column
                        :formatter="(row) => (row.lastDuration ? `${tool.groupSeparator(row.lastDuration.toFixed(0))} ms` : `-`)"
                        :label="$t('耗时')"
                        align="right"
                        prop="lastDuration"
                        sortable="custom"
                        width="100">
                    </el-table-column>
                </el-table-column>

                <el-table-column :label="$t('下次执行时间')" align="right" prop="nextExecTime" sortable="custom" width="170" />
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="80">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" @change="changeSwitch($event, scope.row)"></el-switch>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom" width="100">
                    <template #default="scope">
                        <span v-if="scope.row.createdTime" v-time.tip="scope.row.createdTime"></span>
                    </template>
                </el-table-column>
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat(
                            {
                                icon: 'el-icon-video-play',
                                click: execute,
                            },
                            {
                                icon: 'el-icon-delete',
                                confirm: true,
                                type: 'danger',
                                title: $t('删除作业'),
                                click: rowDel,
                            },
                        )
                    "
                    :vue="this"
                    width="180" />
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
import ScSelectFilter from '@/components/scSelectFilter/index.vue'
import tool from '@/utils/tool'

export default {
    components: {
        ScSelectFilter,
        saveDialog,
    },
    inject: ['reload'],
    data() {
        return {
            timer: null,
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [
                        {
                            field: 'enabled',
                            operator: 'eq',
                            value: true,
                        },
                    ],
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
        this.$refs.search.form.dy.enabled = true
    },
    created() {},
    methods: {
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
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
        async execute(row) {
            try {
                await this.$API.sys_job.execute.post({ id: row.id })
                this.$notify.success({
                    dangerouslyUseHTMLString: true,
                    message: `<div id="countdown">已发起执行请求，5 秒后弹出执行结果</div>`,
                    onClose: async () => {
                        clearInterval(this.timer)
                        this.loading = true
                        this.dialog.save = true
                        await this.$nextTick()
                        await this.$refs.saveDialog.open('view', row, 1)
                        this.loading = false
                    },
                })

                this.timer = setInterval(() => {
                    const countdown = new RegExp('\\d+').exec(document.getElementById('countdown').innerText)[0]
                    document.getElementById('countdown').innerText = document
                        .getElementById('countdown')
                        .innerText.replace(countdown, `${parseInt(countdown) - 1}`)
                }, 1000)
            } catch {}
            this.$refs.table.refresh()
        },
        //删除
        async rowDel(row) {
            try {
                const res = await this.$API.sys_job.delete.post({ id: row.id })
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            this.$refs.table.refresh()
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
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            loading?.close()
            this.$refs.table.refresh()
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