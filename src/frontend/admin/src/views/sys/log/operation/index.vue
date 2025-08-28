<template>
    <el-container>
        <el-header v-loading="statistics.total === '...'" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <sc-statistic :title="$t('总数')" :value="statistics.total" group-separator />
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
        <el-header class="el-header-select-filter">
            <sc-select-filter
                :data="[
                    {
                        title: $t('操作结果'),
                        key: 'operationResult',
                        options: [
                            { label: $t('全部'), value: '' },
                            { label: $t('成功'), value: true },
                            { label: $t('失败'), value: false },
                        ],
                    },
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
                    {
                        title: $t('请求服务'),
                        key: 'apiPathCrc32',
                        options: [
                            { label: $t('全部'), value: '' },
                            ...(this.statistics.apiPathCrc32?.map((x) => {
                                let api = this.apis?.find((y) => y.pathCrc32.toString() === x.key.apiPathCrc32)
                                return {
                                    value: x.key.apiPathCrc32,
                                    label: api?.summary,
                                    title: api?.id,
                                    badge: x.value,
                                }
                            }) ?? []),
                        ],
                    },
                ]"
                :label-width="10"
                @on-change="filterChange"
                ref="selectFilter" />
        </el-header>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'remote-select',
                            field: ['dy', 'ownerId'],
                            api: $API.sys_user.query,
                            config: { props: { label: 'userName', value: 'id' } },
                            placeholder: '用户',
                            style: 'width:15rem',
                            condition: () => $GLOBAL.hasApiPermission('api/sys/user/query'),
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
                            style: 'width:15rem',
                        },
                        {
                            type: 'cascader',
                            field: ['dy', 'apiPathCrc32'],
                            api: $API.sys_api.query,
                            props: { label: 'summary', value: 'pathCrc32', checkStrictly: true, expandTrigger: 'hover', emitPath: false },
                            placeholder: $t('请求服务'),
                            style: 'width:20rem',
                        },
                        {
                            type: 'select-input',
                            field: [
                                'dy',
                                [
                                    { label: '日志编号', key: 'id' },
                                    { label: '用户编号', key: 'ownerId' },
                                    { label: '客户端IP', key: 'createdClientIp' },
                                ],
                            ],
                            placeholder: '检索内容',
                            style: 'width:25rem',
                            selectStyle: 'width:8rem',
                        },
                    ]"
                    :vue="this"
                    @reset="Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))"
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
                :context-extra="{ id: ['createdTime'] }"
                :context-menus="['id', 'httpStatusCode', 'apiPathCrc32', 'ownerId', 'httpMethod', 'duration', 'createdClientIp', 'createdTime']"
                :context-opers="[]"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :export-api="$API.sys_requestlog.export"
                :params="query"
                :query-api="$API.sys_requestlog.pagedQuery"
                :vue="this"
                @data-change="dataChange"
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <na-col-id label="日志编号" prop="id" sortable="custom" width="170" />
                <el-table-column :label="$t('响应码')" align="center" prop="httpStatusCode" sortable="custom" width="150">
                    <template #default="{ row }">
                        <sc-status-indicator :type="row.httpStatusCode >= 200 && row.httpStatusCode < 300 ? 'success' : 'danger'" />
                        {{ row.httpStatusCode }}
                    </template>
                </el-table-column>
                <el-table-column :label="$t('请求服务')" align="center">
                    <el-table-column :label="$t('路径')" min-width="300" prop="apiPathCrc32" show-overflow-tooltip sortable="custom">
                        <template #default="{ row }">
                            <p>
                                {{ apis?.find((x) => x.pathCrc32 === row.apiPathCrc32)?.id }}
                            </p>
                            <p>
                                {{ apis?.find((x) => x.pathCrc32 === row.apiPathCrc32)?.summary }}
                            </p>
                        </template>
                    </el-table-column>
                    <el-table-column :label="$t('方法')" align="center" prop="httpMethod" sortable="custom" width="100">
                        <template #default="{ row }">
                            <sc-status-indicator
                                :style="`background: #${Math.abs(this.$TOOL.crypto.hashCode(row.httpMethod)).toString(16).substring(0, 6)}`" />
                            {{ row.httpMethod.toUpperCase() }}
                        </template>
                    </el-table-column>
                    <el-table-column
                        :formatter="(row) => `${$TOOL.groupSeparator(row.duration.toFixed(0))} ms`"
                        :label="$t('耗时')"
                        align="right"
                        prop="duration"
                        sortable="custom"
                        width="90">
                    </el-table-column>
                </el-table-column>
                <el-table-column
                    v-auth="'sys/log/operation/user'"
                    :label="$t('用户')"
                    header-align="center"
                    prop="ownerId"
                    sortable="custom"
                    width="170">
                    <template #default="{ row }">
                        <div v-if="row.ownerId" :style="{ display: 'flex' }" @click="userClick(row.ownerId)" class="el-table-column-avatar">
                            <el-avatar
                                v-if="owners?.find((x) => x.id === row.ownerId)"
                                :src="
                                    owners?.find((x) => x.id === row.ownerId)?.avatar ??
                                    $CONFIG.DEFAULT_AVATAR(owners?.find((x) => x.id === row.ownerId)?.userName)
                                "
                                size="small" />
                            <div>
                                <p>{{ owners?.find((x) => x.id === row.ownerId)?.userName }}</p>
                                <p>{{ owners?.find((x) => x.id === row.ownerId)?.id }}</p>
                            </div>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('客户端IP')" prop="createdClientIp" show-overflow-tooltip sortable="custom" width="200">
                    <template #default="{ row }">
                        <template v-if="ips && ips.length > 0 && row.createdClientIp">
                            <p>{{ row.createdClientIp }}</p>
                            <p>{{ ips.filter((x) => x.ip === row.createdClientIp)[0]?.region ?? '...' }}</p>
                        </template>
                    </template>
                </el-table-column>
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

    <na-info v-if="dialog.info" ref="info" />
    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog" />
</template>

<script>
import { defineAsyncComponent } from 'vue'
import http from '@/utils/request'
const saveDialog = defineAsyncComponent(() => import('@/views/sys/power/user/save'))

export default {
    components: {
        saveDialog,
    },
    computed: {},
    async created() {
        if (this.ownerId) {
            this.query.dynamicFilter.filters.push({ field: 'ownerId', operator: 'eq', value: this.ownerId })
        }
        if (this.excludeApiPathCrc32) {
            this.query.dynamicFilter.filters.push({ field: 'apiPathCrc32', operator: 'notEqual', value: this.excludeApiPathCrc32 })
        }
        const res = await this.$API.sys_api.plainQuery.post({ count: 1000 })
        this.apis = res.data
    },
    data() {
        return {
            statistics: {
                total: '...',
            },
            dialog: {
                info: false,
            },
            owners: [],
            apis: [],
            ips: [],
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [
                        {
                            field: 'createdTime',
                            operator: 'dateRange',
                            value: [
                                this.$TOOL.dateFormat(new Date(new Date() - 3600 * 1000), 'yyyy-MM-dd hh:mm:ss'),
                                this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd hh:mm:ss'),
                            ],
                        },
                    ],
                },
                filter: {},
                keywords: this.keywords,
            },
            selection: [],
        }
    },
    inject: ['reload'],
    methods: {
        userClick(id) {
            this.dialog.save = { mode: 'view', row: { id } }
        },
        async dataChange(data) {
            this.owners = []
            const ownerIds = data.data.rows?.filter((x) => x.ownerId).map((x) => x.ownerId)
            const apiCrcs = data.data.rows?.map((x) => x.apiPathCrc32)
            const ips = data.data.rows?.map((x) => x.createdClientIp) ?? []
            const res = await Promise.all([
                ownerIds && ownerIds.length > 0 && this.$GLOBAL.permissions.some((x) => x === '*/*/*' || x === 'sys/log/operation/user')
                    ? this.$API.sys_user.query.post({
                          dynamicFilter: {
                              field: 'id',
                              operator: 'any',
                              value: ownerIds,
                          },
                      })
                    : new Promise((x) => x({ data: [] })),
                ips && ips.length > 0 ? http.get(`https://ip.tools92.top/?ip=${ips.join('&ip=')}`) : new Promise((x) => x({ data: [] })),
            ])
            this.owners = res[0].data
            this.ips = res[1]
            await this.getStatistics()
        },
        async getStatistics() {
            this.statistics.total = this.$refs.table?.total

            const res = await Promise.all([
                this.$API.sys_requestlog.countBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['HttpStatusCode'],
                }),
                this.$API.sys_requestlog.countBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['ApiPathCrc32'],
                }),
            ])
            this.statistics.httpStatusCode = res[0].data
            this.statistics.apiPathCrc32 = res[1].data
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
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
            if (Array.isArray(form.dy.httpStatusCode) && form.dy.httpStatusCode.length !== 0) {
                this.query.dynamicFilter.filters.push({
                    logic: 'or',
                    filters: form.dy.httpStatusCode.map((x) => {
                        return { field: 'httpStatusCode', operator: 'range', value: x }
                    }),
                })
            }
            if (typeof form.dy['excludeApiPathCrc32'] === 'number' && form.dy['excludeApiPathCrc32'] !== 0) {
                this.query.dynamicFilter.filters.push({
                    field: 'apiPathCrc32',
                    operator: 'notEqual',
                    value: form.dy['excludeApiPathCrc32'],
                })
            }
            if (typeof form.dy['apiPathCrc32'] === 'number' && form.dy['apiPathCrc32'] !== 0) {
                this.query.dynamicFilter.filters.push({
                    field: 'apiPathCrc32',
                    operator: 'eq',
                    value: form.dy['apiPathCrc32'],
                })
            }

            if (typeof form.dy.ownerId === 'number' && form.dy.ownerId !== 0) {
                this.query.dynamicFilter.filters.push({
                    field: 'ownerId',
                    operator: 'eq',
                    value: form.dy.ownerId,
                })
            }

            if (typeof form.dy.ownerId === 'string' && form.dy.ownerId.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'ownerId',
                    operator: 'eq',
                    value: form.dy.ownerId,
                })
            }

            if (typeof form.dy.httpStatusCode === 'string' && form.dy.httpStatusCode.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'httpStatusCode',
                    operator: 'eq',
                    value: form.dy.httpStatusCode,
                })
            }
            if (typeof form.dy.apiPathCrc32 === 'string' && form.dy.apiPathCrc32.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'apiPathCrc32',
                    operator: 'eq',
                    value: form.dy.apiPathCrc32,
                })
            }
            if (typeof form.dy.id === 'string' && form.dy.id.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'id',
                    operator: 'eq',
                    value: form.dy.id,
                })
            }

            if (typeof form.dy.createdClientIp === 'string' && form.dy.createdClientIp.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'createdClientIp',
                    operator: 'eq',
                    value: form.dy.createdClientIp,
                })
            }

            if (typeof form.dy.operationResult === 'boolean') {
                this.query.dynamicFilter.filters.push(
                    form.dy.operationResult
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
            await this.$refs.table.upData()
        },

        async rowClick(row) {
            this.dialog.info = true
            await this.$nextTick()
            await this.$refs.info.open(
                () => this.$t('日志详情：{id}', { id: row.id }),
                () =>
                    this.$API.sys_requestlog.get.post({
                        id: row.id,
                        createdTime: row.createdTime,
                    }),
                {
                    query: {
                        bool: {
                            must: [
                                {
                                    range: {
                                        '@timestamp': {
                                            gt: new Date(row.createdTime).getTime() - 1000 * 60 * 60,
                                            lt: new Date(row.createdTime).getTime() + 1000 * 60 * 60,
                                        },
                                    },
                                },
                                {
                                    match_phrase: {
                                        log_message: row.traceId,
                                    },
                                },
                            ],
                        },
                    },
                    size: 1000,
                    sort: [
                        {
                            '@timestamp': 'desc',
                        },
                    ],
                },
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

        if (this.ownerId) {
            this.$refs.search.keeps.push({
                field: 'ownerId',
                value: this.ownerId,
                type: 'dy',
            })
            this.$refs.search.form.dy.ownerId = this.ownerId
        }

        if (this.excludeApiPathCrc32) {
            this.$refs.search.keeps.push({
                field: 'excludeApiPathCrc32',
                value: this.excludeApiPathCrc32,
                type: 'dy',
            })
            this.$refs.search.form.dy.excludeApiPathCrc32 = this.excludeApiPathCrc32
        }

        this.$refs.search.form.dy.createdTime = [
            this.$TOOL.dateFormat(new Date(new Date() - 3600 * 1000), 'yyyy-MM-dd hh:mm:ss'),
            this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd hh:mm:ss'),
        ]
        this.$refs.search.keeps.push({
            field: 'createdTime',
            value: this.$refs.search.form.dy.createdTime,
            type: 'dy',
        })
    },
    props: ['keywords', 'ownerId', 'excludeApiPathCrc32'],
    watch: {},
}
</script>

<style scoped />