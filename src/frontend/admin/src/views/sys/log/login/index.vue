<template>
    <el-container>
        <el-header v-loading="statistics.total === '...'" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <sc-statistic :value="statistics.total" group-separator title="总数"></sc-statistic>
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
        <el-header class="el-header-select-filter">
            <sc-select-filter
                v-if="showFilter"
                :data="[
                    {
                        title: $t('登录结果'),
                        key: 'loginResult',
                        options: [
                            { label: $t('全部'), value: '' },
                            { label: $t('成功'), value: true },
                            { label: $t('失败'), value: false },
                        ],
                    },
                    {
                        title: $t('错误码'),
                        key: 'errorCode',
                        options: [
                            { label: $t('全部'), value: '' },
                            ...(this.statistics.errorCode?.map((x) => {
                                return {
                                    value: x.key.errorCode,
                                    label: x.key.errorCode,
                                    badge: x.value,
                                }
                            }) ?? []),
                        ],
                    },
                    {
                        title: $t('登录用户名'),
                        key: 'loginUserName',
                        options: [
                            { label: $t('全部'), value: '' },
                            ...(this.statistics.loginUserName?.map((x) => {
                                return {
                                    value: x.key.loginUserName,
                                    label: x.key.loginUserName,
                                    badge: x.value,
                                }
                            }) ?? []),
                        ],
                    },
                    {
                        title: $t('客户端IP地址'),
                        key: 'createdClientIp',
                        options: [
                            { label: $t('全部'), value: '' },
                            ...(this.statistics.createdClientIp?.map((x) => {
                                return {
                                    value: x.key.createdClientIp,
                                    label: x.key.createdClientIp,
                                    badge: x.value,
                                }
                            }) ?? []),
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
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('日志编号 / 登录名 / 客户端IP'),
                            style: 'width:25rem',
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
                :context-menus="['id', 'httpStatusCode', 'loginUserName', 'createdClientIp', 'createdUserAgent', 'createdTime']"
                :context-opers="['view']"
                :default-sort="{ prop: 'id', order: 'descending' }"
                :export-api="$API.sys_loginlog.export"
                :params="query"
                :query-api="$API.sys_loginlog.pagedQuery"
                :vue="this"
                @data-change="dataChange"
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <na-col-id label="日志编号" prop="id" sortable="custom" width="170" />
                <el-table-column :label="$t('结果')" align="center" prop="httpStatusCode" sortable="custom" width="100">
                    <template #default="{ row }">
                        <sc-status-indicator :type="row.httpStatusCode === 200 ? 'success' : 'danger'" />
                        {{ row.httpStatusCode === 200 ? '成功' : '失败' }}
                    </template>
                </el-table-column>
                <el-table-column :label="$t('登录名')" prop="loginUserName" sortable="custom" width="150" />
                <el-table-column :label="$t('客户端IP')" prop="createdClientIp" show-overflow-tooltip sortable="custom" width="200">
                    <template #default="{ row }">
                        <template v-if="ips && ips.length > 0 && row.createdClientIp">
                            <p>{{ row.createdClientIp }}</p>
                            <p>{{ this.ips.filter((x) => x.ip === row.createdClientIp)[0]?.region ?? '...' }}</p>
                        </template>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('操作系统')" align="center" prop="os" width="150" />
                <el-table-column :label="$t('用户代理')" min-width="150" prop="createdUserAgent" show-overflow-tooltip sortable="custom" />
                <na-col-operation
                    :buttons="[
                        {
                            icon: 'el-icon-view',
                            title: '查看',
                            click: rowClick,
                        },
                    ]"
                    :vue="this"
                    width="70" />
            </sc-table>
        </el-main>
    </el-container>

    <na-info v-if="dialog.info" ref="info"></na-info>
</template>

<script>
import naInfo from '@/components/naInfo/index.vue'
import http from '@/utils/request'

export default {
    components: {
        naInfo,
    },
    computed: {},
    created() {},
    data() {
        return {
            statistics: {
                total: '...',
            },
            dialog: {
                info: false,
            },
            ips: [],
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
                keywords: this.keywords,
            },
            selection: [],
        }
    },
    inject: ['reload'],
    methods: {
        async getStatistics() {
            this.statistics.total = this.$refs.table?.total
            const res = await Promise.all([
                this.$API.sys_loginlog.countBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['ErrorCode'],
                }),
                this.$API.sys_loginlog.countBy.post({
                    dynamicFilter: {
                        filters: [
                            ...this.query.dynamicFilter.filters,
                            {
                                field: 'LoginUserName',
                                operator: 'notEqual',
                            },
                        ],
                    },
                    requiredFields: ['LoginUserName'],
                }),
                this.$API.sys_loginlog.countBy.post({
                    dynamicFilter: {
                        filters: [
                            ...this.query.dynamicFilter.filters,
                            {
                                field: 'CreatedClientIp',
                                operator: 'notEqual',
                            },
                        ],
                    },
                    requiredFields: ['CreatedClientIp'],
                }),
            ])
            this.statistics.errorCode = res[0].data
            this.statistics.loginUserName = res[1].data
            this.statistics.createdClientIp = res[2].data
        },
        async dataChange(data) {
            this.apis = []
            const ips = data.data.rows?.map((x) => x.createdClientIp) ?? []
            const res = await Promise.all([
                ips && ips.length > 0 ? http.get(`https://ip.tools92.top/?ip=${ips.join('&ip=')}`) : new Promise((x) => x({ data: [] })),
            ])
            this.ips = res[0]
            await this.getStatistics()
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
        onReset() {
            if (!this.showFilter) return
            Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))
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
            if (typeof form.dy.loginResult === 'boolean') {
                this.query.dynamicFilter.filters.push(
                    form.dy.loginResult
                        ? {
                              field: 'httpStatusCode',
                              operator: 'range',
                              value: '200,299',
                          }
                        : {
                              field: 'httpStatusCode',
                              operator: 'range',
                              value: '300,999',
                          },
                )
            }

            if (typeof form.dy.errorCode === 'string' && form.dy.errorCode.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'errorCode',
                    operator: 'eq',
                    value: form.dy.errorCode,
                })
            }

            if (typeof form.dy.loginUserName === 'string' && form.dy.loginUserName.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'loginUserName',
                    operator: 'eq',
                    value: form.dy.loginUserName,
                })
            }

            if (typeof form.dy.createdClientIp === 'string' && form.dy.createdClientIp.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'createdClientIp',
                    operator: 'eq',
                    value: form.dy.createdClientIp,
                })
            }

            await this.$refs.table.upData()
        },

        async rowClick(row) {
            this.dialog.info = true
            await this.$nextTick()
            await this.$refs.info.open(
                () => this.$t('日志详情：{id}', { id: row.id }),
                () =>
                    this.$API.sys_loginlog.get.post({
                        id: row.id,
                    }),
            )
        },
    },
    async mounted() {
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keeps.push({
                field: 'keywords',
                value: this.keywords,
                type: 'root',
            })
        }
    },
    props: { keywords: { type: String }, showFilter: { type: Boolean, default: true } },
    watch: {},
}
</script>

<style scoped></style>