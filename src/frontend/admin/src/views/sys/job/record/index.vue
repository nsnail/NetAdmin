<template>
    <el-container>
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
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :export-api="$API.sys_job.exportRecord"
                :params="query"
                :query-api="$API.sys_job.pagedQueryRecord"
                :vue="this"
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <na-col-id :label="$t('唯一编码')" prop="id" sortable="custom" width="170" />
                <el-table-column :label="$t('响应状态码')" prop="httpStatusCode" sortable="custom" width="200">
                    <template #default="{ row }">
                        <p>
                            <na-indicator
                                :data="row"
                                :options="
                                    Object.entries(this.$GLOBAL.enums.httpMethods).map((x) => {
                                        return { value: x[0], text: `${x[1][1].toString().toUpperCase()}`, type: x[1][2] }
                                    })
                                "
                                prop="httpMethod" />
                        </p>
                        <p>
                            <na-indicator
                                :data="row"
                                :options="
                                    Object.entries(this.$GLOBAL.enums.httpStatusCodes).map((x) => {
                                        return { value: x[0], text: `${x[1][1]}`, type: x[1][2] }
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
                <el-table-column :label="$t('作业信息')" min-width="150" prop="jobId" show-overflow-tooltip sortable="custom">
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
                <na-col-operation :buttons="[naColOperation.buttons[0]]" :vue="this" width="100" />
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
import naIndicator from '@/components/naIndicator/index.vue'

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
        jobClick(job) {
            this.dialog.job = { mode: 'view', row: { id: job.id } }
        },
        onReset() {
            if (this.jobId) {
                this.$refs.search.selectInputKey = 'jobId'
            }
        },
        //搜索
        onSearch(form) {
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

            if (typeof form.dy.jobId === 'string' && form.dy.jobId.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'jobId',
                    operator: 'eq',
                    value: form.dy.jobId,
                })
            }

            if (typeof form.dy.jobId === 'number' && form.dy.jobId !== 0) {
                this.query.dynamicFilter.filters.push({
                    field: 'jobId',
                    operator: 'eq',
                    value: form.dy.jobId,
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
    mounted() {
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