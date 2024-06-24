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
                    ]"
                    :vue="this"
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
                :apiObj="$API.sys_job.recordPagedQuery"
                :context-menus="['id', 'duration', 'httpMethod', 'requestUrl', 'httpStatusCode', 'createdTime', 'jobId']"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :params="query"
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
                                        return { value: x[0], text: `${x[1][1]}`, type: x[1][2] }
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
                    width="150" />
                <el-table-column :label="$t('作业信息')" prop="jobId" show-overflow-tooltip sortable="custom" width="500">
                    <template #default="{ row }">
                        <p>
                            <el-link :href="`/sys/job?keywords=${row.jobId}`" target="_blank">
                                {{ row.job.jobName }}
                            </el-link>
                        </p>
                        <p>
                            {{ row.requestUrl }}
                        </p>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('响应体')" prop="responseBody" show-overflow-tooltip sortable="custom" />
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
    created() {
        if (this.keywords) {
            this.query.keywords = this.keywords
        }
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
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keepKeywords = this.keywords
        }
        if (this.statusCodes) {
            this.$refs.search.form.dy.httpStatusCode = this.statusCodes
            this.$refs.search.keepHttpStatusCode = this.statusCodes
        }
        this.$refs.search.form.dy.createdTime = this.$refs.search.keepCreatedTime = [
            `${this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd')} 00:00:00`,
            `${this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd')} 00:00:00`,
        ]
    },
    props: ['keywords', 'statusCodes'],
    watch: {},
}
</script>

<style scoped></style>