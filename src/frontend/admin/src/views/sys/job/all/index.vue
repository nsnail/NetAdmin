<template>
    <el-container>
        <el-header v-loading="total === '...'" style="height: auto; padding: 1rem 1rem 0 1rem; display: block">
            <el-row :gutter="15">
                <el-col :lg="12">
                    <el-card shadow="never">
                        <sc-statistic :value="total" group-separator title="总数"></sc-statistic>
                    </el-card>
                </el-col>
                <el-col :lg="12">
                    <el-card shadow="never">
                        <sc-statistic :value="running" group-separator title="运行"></sc-statistic>
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
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
                :label-width="10"
                @on-change="filterChange"
                ref="selectFilter"></sc-select-filter>
        </el-header>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'select',
                            field: ['dy', 'httpMethod'],
                            options: Object.entries(this.$GLOBAL.enums.httpMethods).map((x) => {
                                return { value: x[0], label: x[1][1] }
                            }),
                            placeholder: $t('请求方式'),
                            style: 'width:15rem',
                        },
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('作业编号 / 作业名称'),
                            style: 'width:20rem',
                        },
                    ]"
                    :vue="this"
                    @reset="onReset"
                    @search="onSearch"
                    dateFormat="YYYY-MM-DD HH:mm:ss"
                    dateType="datetimerange"
                    dateValueFormat="YYYY-MM-DD HH:mm:ss"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-button @click="(this.dialog.save = { mode: 'add' })" icon="el-icon-plus" type="primary"></el-button>
                <na-button-bulk-del :api="$API.sys_job.bulkDelete" :vue="this" />
                <el-dropdown v-show="this.selection.length > 0">
                    <el-button type="primary">
                        {{ $t('批量操作') }}
                        <el-icon>
                            <el-icon-arrow-down />
                        </el-icon>
                    </el-button>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item @click="setEnabled(true)">{{ $t('启用作业') }}</el-dropdown-item>
                            <el-dropdown-item @click="setEnabled(false)">{{ $t('禁用作业') }}</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :cell-style="
                    (row) => {
                        if (row.column.property === 'lastDuration') {
                            if (row.row.lastDuration > 1000) {
                                return { color: 'var(--el-color-danger)' }
                            }
                        }
                    }
                "
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
                    'lastDuration',
                ]"
                :default-sort="{ prop: 'lastExecTime', order: 'descending' }"
                :export-api="$API.sys_job.export"
                :on-command="this.getStatistics"
                :page-size="100"
                :params="query"
                :query-api="$API.sys_job.pagedQuery"
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
                <na-col-id :label="$t('作业编号')" prop="id" sortable="custom" width="170" />
                <el-table-column :label="$t('作业名称')" prop="jobName" show-overflow-tooltip sortable="custom" />
                <el-table-column :label="$t('执行计划')" align="right" prop="executionCron" sortable="custom" width="150">
                    <template #default="{ row }">
                        <p>{{ row.cronDescription }}</p>
                        <p>{{ row.executionCron }}</p>
                    </template>
                </el-table-column>
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
                    width="150" />
                <el-table-column :label="$t('上次执行')" align="center">
                    <el-table-column :label="$t('状态')" align="center" prop="lastStatusCode" sortable="custom" width="100">
                        <template #default="{ row }">
                            <na-indicator
                                :data="row"
                                :options="
                                    Object.entries(this.$GLOBAL.enums.httpStatusCodes).map((x) => {
                                        return { value: x[0], text: `${x[1][1]}`, type: x[1][2] }
                                    })
                                "
                                prop="lastStatusCode" />
                        </template>
                    </el-table-column>
                    <el-table-column :label="$t('时间')" align="right" prop="lastExecTime" sortable="custom" width="100">
                        <template #default="{ row }">
                            <span v-if="row.lastExecTime" v-time.tip="row.lastExecTime" :title="row.lastExecTime"></span>
                        </template>
                    </el-table-column>
                    <el-table-column
                        :formatter="(row) => (row.lastDuration ? `${$TOOL.groupSeparator(row.lastDuration.toFixed(0))} ms` : `-`)"
                        :label="$t('耗时')"
                        align="right"
                        prop="lastDuration"
                        sortable="custom"
                        width="100">
                    </el-table-column>
                </el-table-column>
                <el-table-column :label="$t('下次执行时间')" align="right" prop="nextExecTime" sortable="custom" width="170" />
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="100">
                    <template #default="{ row }">
                        <el-switch v-model="row.enabled" @change="changeSwitch($event, row)"></el-switch>
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
                                icon: 'el-icon-document-copy',
                                confirm: true,
                                title: '复制作业',
                                click: copyJob,
                            },
                            {
                                icon: 'el-icon-delete',
                                confirm: true,
                                title: $t('删除作业'),
                                click: rowDel,
                                type: 'danger',
                            },
                        )
                    "
                    :vue="this"
                    width="220" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="(dialog.save = null)"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'
import naIndicator from '@/components/naIndicator/index.vue'

const saveDialog = defineAsyncComponent(() => import('./save.vue'))
export default {
    components: {
        naIndicator,
        saveDialog,
    },
    computed: {
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    created() {},
    data() {
        return {
            total: '...',
            running: '...',
            dialog: {},
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
                keywords: this.keywords || this.$route.query.keywords,
            },
            selection: [],
            timer: null,
        }
    },
    inject: ['reload'],
    methods: {
        async getStatistics() {
            const runningFilter = JSON.parse(JSON.stringify(this.query))
            runningFilter.dynamicFilter.filters.push({
                field: 'status',
                operator: 'eq',
                value: 'running',
            })
            const res = await Promise.all([this.$API.sys_job.count.post(this.query), this.$API.sys_job.count.post(runningFilter)])
            this.total = res[0].data
            this.running = res[1].data
        },
        async copyJob(row) {
            let loading = this.$loading()
            try {
                const res = await this.$API.sys_job.create.post(
                    Object.assign({}, row, { id: 0, jobName: row.jobName + '-copy', enabled: false, nextTimeId: null, status: 'idle' }),
                )
                if (res.data) {
                    this.$message.success(this.$t('操作成功'))
                } else {
                    this.$message.error(this.$t('操作失败'))
                }
                this.$refs.table.refresh()
            } catch {}
            loading?.close()
        },
        async setEnabled(enabled) {
            let loading
            try {
                await this.$confirm(
                    this.$t('确定要 {operator} 选中的 {count} 项吗？', {
                        operator: enabled ? this.$t('启用') : this.$t('禁用'),
                        count: this.selection.length,
                    }),
                    this.$t('提示'),
                    {
                        type: 'warning',
                    },
                )
                loading = this.$loading()
                const res = await Promise.all(this.selection.map((x) => this.$API.sys_job.setEnabled.post(Object.assign(x, { enabled: enabled }))))
                this.$message.success(
                    this.$t('操作成功 {count}/{total} 项', {
                        count: res.map((x) => x.data ?? 0).reduce((a, b) => a + b, 0),
                        total: this.selection.length,
                    }),
                )
            } catch {
                //
            }
            this.$refs.table.refresh()
            loading?.close()
        },
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
                this.$message.success(this.$t('操作成功'))
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
                    message: `<div id="countdown">${this.$t('已发起执行请求，5 秒后弹出执行结果')}</div>`,
                    onClose: async () => {
                        clearInterval(this.timer)
                        this.dialog.save = { row, mode: 'view', tabId: 'record' }
                    },
                })

                this.timer = setInterval(() => {
                    const countdown = new RegExp('\\d+').exec(document.getElementById('countdown').innerText)[0]
                    const num = parseInt(countdown) - 1
                    document.getElementById('countdown').innerText = document
                        .getElementById('countdown')
                        .innerText.replace(countdown, `${num < 0 ? 0 : num}`)
                }, 1000)
            } catch {}
            this.$refs.table.refresh()
        },
        async rowDel(row) {
            try {
                const res = await this.$API.sys_job.delete.post({ id: row.id })
                this.$message.success(this.$t('删除 {count} 项', { count: res.data }))
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        //重置
        onReset() {
            Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))
            this.$refs.selectFilter.selected['enabled'] = [true]
        },
        //搜索
        async onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime.map((x) => x.replace(/ 00:00:00$/, '')),
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
            await this.getStatistics()
        },
    },
    async mounted() {
        if (this.keywords || this.$route.query.keywords) {
            this.$refs.search.form.root.keywords = this.keywords || this.$route.query.keywords
            this.$refs.search.keeps.push({
                field: 'keywords',
                value: this.keywords || this.$route.query.keywords,
                type: 'root',
            })
        }

        this.$refs.search.form.dy.enabled = true
        this.$refs.search.keeps.push({
            field: 'enabled',
            value: true,
            type: 'dy',
        })
        this.onReset()
        await this.getStatistics()
    },
    props: ['keywords'],
    watch: {},
}
</script>

<style scoped></style>