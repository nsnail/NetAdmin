<template>
    <el-container>
        <el-header style="height: auto; padding: 0 1rem">
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
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
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
                <el-table-column :label="$t('用户代理')" prop="createdUserAgent" show-overflow-tooltip sortable="custom" />
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
        async dataChange(data) {
            this.apis = []
            const ips = data.data.rows?.map((x) => x.createdClientIp) ?? []
            const res = await Promise.all([
                ips && ips.length > 0 ? http.get(`https://ip.tools92.top/?ip=${ips.join('&ip=')}`) : new Promise((x) => x({ data: [] })),
            ])
            this.ips = res[0]
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
        onSearch(form) {
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
            this.$refs.table.upData()
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
    mounted() {
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