<template>
    <el-container>
        <el-header v-loading="statistics.total === '...'" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :span="12">
                    <el-card shadow="never">
                        <sc-statistic :value="statistics.total" group-separator title="总数"></sc-statistic>
                    </el-card>
                </el-col>
                <el-col :span="12">
                    <el-card shadow="never">
                        <sc-statistic :value="statistics.rate" suffix="%" title="成功率"></sc-statistic>
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
        <el-header class="el-header-select-filter">
            <sc-select-filter
                :data="[
                    {
                        title: $t('响应状态码'),
                        key: 'httpStatusCode',
                        options: [
                            { label: $t('全部'), value: '' },
                            ...(this.statistics.httpStatusCode?.map((x) => {
                                return {
                                    value: x.key.httpStatusCode,
                                    label: x.key.httpStatusCode,
                                    badge: x.value,
                                }
                            }) ?? []),
                        ],
                    },
                ]"
                :label-width="9"
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
                            placeholder: $t('状态码'),
                            style: 'width:20rem',
                        },
                        {
                            type: 'select-input',
                            field: [
                                'dy',
                                [
                                    { label: '唯一编码', key: 'id' },
                                    { label: '作业编号', key: 'jobId' },
                                ],
                            ],
                            placeholder: '匹配内容',
                            style: 'width:25rem',
                            selectStyle: 'width:8rem',
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
            <div class="right-panel"></div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :cell-style="
                    (row) => {
                        if (row.column.property === 'duration') {
                            if (row.row.duration > 1000) {
                                return { color: 'var(--el-color-danger)' }
                            }
                        }
                    }
                "
                :context-menus="['id', 'duration', 'httpMethod', 'requestUrl', 'httpStatusCode', 'createdTime', 'jobId', 'responseBody']"
                :default-sort="{ prop: 'id', order: 'descending' }"
                :export-api="$API.sys_job.exportRecord"
                :params="query"
                :query-api="$API.sys_job.pagedQueryRecord"
                :vue="this"
                @data-change="getStatistics"
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <naColId :label="$t('唯一编码')" prop="id" sortable="custom" width="170" />
                <el-table-column :label="$t('响应状态码')" prop="httpStatusCode" sortable="custom" width="200">
                    <template #default="{ row }">
                        <p>
                            <na-indicator
                                :data="row"
                                :options="
                                    Object.entries(this.$GLOBAL.enums.httpMethods).map((x) => {
                                        return { value: x[0], text: `${x[1][1].toString().toUpperCase()}`, type: x[1][2], pulse: x[1][3] === 'true' }
                                    })
                                "
                                prop="httpMethod" />
                        </p>
                        <p>
                            <na-indicator
                                :data="row"
                                :options="
                                    Object.entries(this.$GLOBAL.enums.httpStatusCodes).map((x) => {
                                        return { value: x[0], text: `${x[1][1]}`, type: x[1][2], pulse: x[1][3] === 'true' }
                                    })
                                "
                                prop="httpStatusCode" />
                        </p>
                    </template>
                </el-table-column>
                <el-table-column
                    :formatter="(row) => `${$TOOL.groupSeparator(row.duration.toFixed(0))} ms`"
                    :label="$t('执行耗时')"
                    align="right"
                    prop="duration"
                    sortable="custom"
                    width="100" />
                <el-table-column :label="$t('作业信息')" min-width="200" prop="jobId" show-overflow-tooltip sortable="custom">
                    <template #default="{ row }">
                        <p>
                            <el-link @click="jobClick(row.job)">
                                {{ row.job.jobName }}
                            </el-link>
                        </p>
                        <p>
                            {{ row.requestUrl }}
                        </p>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('响应体')" min-width="300" prop="responseBody" show-overflow-tooltip sortable="custom" />
                <naColOperation :buttons="[naColOperation.buttons[0]]" :vue="this" width="50" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>

    <job-dialog
        v-if="dialog.job"
        @closed="dialog.job = null"
        @mounted="$refs.jobDialog.open(dialog.job)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="jobDialog"></job-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'
import naIndicator from '@/components/naIndicator'

const saveDialog = defineAsyncComponent(() => import('./save.vue'))
const jobDialog = defineAsyncComponent(() => import('@/views/sys/job/all/save.vue'))
export default {
    components: {
        naIndicator,
        saveDialog,
        jobDialog,
    },
    computed: {
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    created() {
        if (this.statusCodes) {
            this.query.dynamicFilter.filters.push({
                logic: 'or',
                filters: this.statusCodes.map((x) => {
                    return {
                        field: 'httpStatusCode',
                        operator: 'range',
                        value: x,
                    }
                }),
            })
        }
        if (this.jobId) {
            this.query.dynamicFilter.filters.push({
                field: 'jobId',
                operator: 'eq',
                value: this.jobId,
            })
        }
    },
    data() {
        return {
            statistics: {
                total: '...',
                rate: '...',
            },
            dialog: {},
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [
                        {
                            field: 'createdTime',
                            operator: 'dateRange',
                            value: [this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd'), this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd')],
                        },
                    ],
                },
                filter: {},
            },
        }
    },
    inject: ['reload'],
    methods: {
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
        async getStatistics() {
            this.statistics.total = this.$refs.table?.total
            const res = await Promise.all([
                this.$API.sys_job.recordCountBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['HttpStatusCode'],
                }),
            ])

            this.statistics.httpStatusCode = res[0].data
            try {
                let rate =
                    ((res[0].data
                        ?.filter((x) => x.key.httpStatusCode.indexOf('2') === 0)
                        ?.map((x) => x.value)
                        ?.reduce((a, b) => a + b) ?? 0) /
                        this.statistics.total) *
                    100
                if (rate > 100) {
                    rate = 100
                }
                this.statistics.rate = rate.toFixed(2)
            } catch {
                this.statistics.rate = 0
            }
        },
        jobClick(job) {
            this.dialog.job = { mode: 'view', row: { id: job.id } }
        },
        onReset() {
            if (this.jobId) {
                this.$refs.search.selectInputKey = 'jobId'
            }
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

            if (typeof form.dy.id === 'string' && form.dy.id.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'id',
                    operator: 'eq',
                    value: form.dy.id,
                })
            }

            if (typeof form.dy.jobId === 'number' && form.dy.jobId !== 0) {
                this.query.dynamicFilter.filters.push({
                    field: 'jobId',
                    operator: 'eq',
                    value: form.dy.jobId,
                })
            }

            if (typeof form.dy.httpStatusCode === 'string' && form.dy.httpStatusCode.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'httpStatusCode',
                    operator: 'eq',
                    value: form.dy.httpStatusCode,
                })
            }

            if (typeof form.dy.httpMethod === 'string' && form.dy.httpMethod.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'httpMethod',
                    operator: 'eq',
                    value: form.dy.httpMethod,
                })
            }
            await this.$refs.table.upData()
        },
    },
    async mounted() {
        if (this.jobId) {
            this.$refs.search.selectInputKey = 'jobId'
            this.$refs.search.form.dy.jobId = this.jobId
            this.$refs.search.keeps.push({
                field: 'jobId',
                value: this.jobId,
                type: 'dy',
            })
        }
        if (this.statusCodes) {
            this.$refs.search.form.dy.httpStatusCode = this.statusCodes
            this.$refs.search.keeps.push({
                field: 'httpStatusCode',
                value: this.statusCodes,
                type: 'dy',
            })
        }
        this.$refs.search.form.dy.createdTime = [
            `${this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd')} 00:00:00`,
            `${this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd')} 00:00:00`,
        ]
        this.$refs.search.keeps.push({
            field: 'createdTime',
            value: this.$refs.search.form.dy.createdTime,
            type: 'dy',
        })
    },
    props: ['statusCodes', 'jobId'],
    watch: {},
}
</script>

<style scoped></style>