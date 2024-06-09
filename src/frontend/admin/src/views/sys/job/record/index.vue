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
                    dateFormat="YYYY-MM-DD HH:mm:ss"
                    dateType="datetimerange"
                    dateValueFormat="YYYY-MM-DD HH:mm:ss"
                    ref="search" />
            </div>
            <div class="right-panel"></div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                v-loading="loading"
                :apiObj="$API.sys_job.recordPagedQuery"
                :context-menus="['id', 'duration', 'httpMethod', 'requestUrl', 'httpStatusCode', 'createdTime']"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :params="query"
                :vue="this"
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
                    align="center"
                    prop="httpMethod"
                    sortable="custom"
                    width="100" />
                <el-table-column :label="$t('响应状态码')" align="center" prop="httpStatusCode" sortable="custom" width="200">
                    <template #default="scope">
                        <sc-status-indicator :type="scope.row.httpStatusCode === 'ok' ? 'success' : 'danger'" />
                        {{
                            this.$GLOBAL.enums.httpStatusCodes[scope.row.httpStatusCode]
                                ? this.$GLOBAL.enums.httpStatusCodes[scope.row.httpStatusCode][1]
                                : scope.row.httpStatusCode
                        }}
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
    inject: ['reload'],
    data() {
        return {
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [
                        {
                            field: 'createdTime',
                            operator: 'dateRange',
                            value: [tool.dateFormat(new Date(), 'yyyy-MM-dd'), tool.dateFormat(new Date(), 'yyyy-MM-dd')],
                        },
                    ],
                },
                filter: {},
            },
            dialog: {
                save: false,
            },
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
        this.$refs.search.form.dy.createdTime = this.$refs.search.keepCreatedTime = [
            `${tool.dateFormat(new Date(), 'yyyy-MM-dd')} 00:00:00`,
            `${tool.dateFormat(new Date(), 'yyyy-MM-dd')} 00:00:00`,
        ]
    },
    created() {
        if (this.keywords) {
            this.query.keywords = this.keywords
        }
    },
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
}
</script>

<style scoped></style>