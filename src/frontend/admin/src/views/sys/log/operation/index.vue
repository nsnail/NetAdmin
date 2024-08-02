<template>
    <el-container>
        <el-header style="height: auto; padding: 0 1rem">
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
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('日志编号 / 用户编号 / 客户端IP'),
                            style: 'width:25rem',
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
                    <el-table-column :label="$t('路径')" prop="apiPathCrc32" show-overflow-tooltip sortable="custom">
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
                                size="small"></el-avatar>
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
                            click: rowClick,
                        },
                    ]"
                    :vue="this"
                    width="70" />
            </sc-table>
        </el-main>
    </el-container>

    <na-info v-if="dialog.info" ref="info"></na-info>
    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import http from '@/utils/request'
const saveDialog = defineAsyncComponent(() => import('@/views/sys/user/save.vue'))
import naInfo from '@/components/naInfo/index.vue'

export default {
    components: {
        naInfo,
        saveDialog,
    },
    computed: {},
    created() {
        if (this.ownerId) {
            this.query.dynamicFilter.filters.push({ field: 'ownerId', operator: 'eq', value: this.ownerId })
        }
    },
    data() {
        return {
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
            this.apis = []
            const ownerIds = data.data.rows?.filter((x) => x.ownerId).map((x) => x.ownerId)
            const apiCrcs = data.data.rows?.map((x) => x.apiPathCrc32)
            const ips = data.data.rows?.map((x) => x.createdClientIp) ?? []
            const res = await Promise.all([
                ownerIds && ownerIds.length > 0
                    ? this.$API.sys_user.query.post({
                          dynamicFilter: {
                              field: 'id',
                              operator: 'any',
                              value: ownerIds,
                          },
                      })
                    : new Promise((x) => x({ data: [] })),

                apiCrcs && apiCrcs.length > 0
                    ? this.$API.sys_api.query.post({
                          dynamicFilter: {
                              field: 'pathCrc32',
                              operator: 'any',
                              value: apiCrcs,
                          },
                      })
                    : new Promise((x) => x({ data: [] })),

                ips && ips.length > 0 ? http.get(`https://ip.tools92.top/?ip=${ips.join('&ip=')}`) : new Promise((x) => x({ data: [] })),
            ])
            this.owners = res[0].data
            this.apis = res[1].data
            this.ips = res[2]
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
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
            if (Array.isArray(form.dy.httpStatusCode) && form.dy.httpStatusCode.length !== 0) {
                this.query.dynamicFilter.filters.push({
                    logic: 'or',
                    filters: form.dy.httpStatusCode.map((x) => {
                        return { field: 'httpStatusCode', operator: 'range', value: x }
                    }),
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
            this.$refs.table.upData()
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
            )
        },
    },
    mounted() {
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
    props: ['keywords', 'ownerId'],
    watch: {},
}
</script>

<style scoped></style>