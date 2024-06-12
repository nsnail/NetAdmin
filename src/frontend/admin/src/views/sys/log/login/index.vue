<template>
    <el-container>
        <el-header style="height: auto; padding: 0 1rem">
            <sc-select-filter
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
                            placeholder: '日志编号 / 登录名 / 客户端IP',
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
                v-loading="loading"
                :apiObj="$API.sys_log.pagedQuery"
                :context-menus="['id', 'httpStatusCode', 'createdClientIp', 'createdUserAgent', 'createdTime']"
                :context-opers="['view']"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :params="query"
                :vue="this"
                @row-click="rowClick"
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <el-table-column :label="$t('日志编号')" prop="id" sortable="custom" width="150" />
                <el-table-column :label="$t('创建时间')" prop="createdTime" sortable="custom" width="170" />
                <el-table-column :label="$t('结果')" align="center" prop="httpStatusCode" sortable="custom" width="80">
                    <template #default="scope">
                        <sc-status-indicator :type="scope.row.httpStatusCode === 200 ? 'success' : 'danger'" />
                        {{ scope.row.httpStatusCode === 200 ? '成功' : '失败' }}
                    </template>
                </el-table-column>
                <el-table-column :label="$t('登录名')" prop="loginName" width="150" />
                <el-table-column :label="$t('客户端IP')" prop="createdClientIp" show-overflow-tooltip sortable="custom" width="200">
                    <template #default="scope">
                        <na-ip :ip="scope.row.createdClientIp"></na-ip>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('操作系统')" align="center" prop="os" width="150" />
                <el-table-column :label="$t('用户代理')" prop="createdUserAgent" show-overflow-tooltip sortable="custom" />
            </sc-table>
        </el-main>
    </el-container>

    <na-info v-if="dialog.info" ref="info"></na-info>
</template>

<script>
import naInfo from '@/components/naInfo/index.vue'
import tool from '@/utils/tool'
import ScTable from '@/components/scTable/index.vue'

export default {
    components: {
        ScTable,
        naInfo,
    },
    watch: {},
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
                info: false,
            },
        }
    },
    async mounted() {
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keepKeywords = this.keywords
        }
        this.$refs.search.form.dy.apiId = 'api/sys/user/login.by.pwd'
        this.$refs.search.form.dy.createdTime = this.$refs.search.keepCreatedTime = [
            `${tool.dateFormat(new Date(), 'yyyy-MM-dd')} 00:00:00`,
            `${tool.dateFormat(new Date(), 'yyyy-MM-dd')} 00:00:00`,
        ]
    },
    created() {
        if (this.keywords) {
            this.query.keywords = this.keywords
        }
        this.query.dynamicFilter.filters.push({
            field: 'apiId',
            operator: 'eq',
            value: 'api/sys/user/login.by.pwd',
        })
    },
    methods: {
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
        //搜索
        onSearch(form) {
            if (this.query.dynamicFilter.filters.findIndex((x) => x.field === 'apiId') < 0) {
                this.query.dynamicFilter.filters.push({
                    field: 'apiId',
                    operator: 'eq',
                    value: 'api/sys/user/login.by.pwd',
                })
            }
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
            const res = await this.$API.sys_log.query.post({
                filter: { id: row.id },
            })
            this.$refs.info.open(tool.sortProperties(res.data[0]), `日志详情：${row.id}`)
        },
    },
}
</script>

<style scoped></style>