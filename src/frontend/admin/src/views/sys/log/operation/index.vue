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
                            field: ['dy', 'apiId'],
                            api: $API.sys_api.query,
                            props: { label: 'summary', value: 'id', checkStrictly: true, expandTrigger: 'hover', emitPath: false },
                            placeholder: $t('请求服务'),
                            style: 'width:20rem',
                        },
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('日志编号 / 用户 / 客户端IP'),
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
                :apiObj="$API.sys_log.pagedQuery"
                :context-menus="['id', 'httpStatusCode', 'apiId', 'userId', 'method', 'duration', 'createdClientIp', 'createdTime']"
                :context-opers="[]"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :params="query"
                :vue="this"
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <el-table-column :label="$t('日志编号')" prop="id" sortable="custom" width="150" />
                <el-table-column :label="$t('创建时间')" prop="createdTime" sortable="custom" width="170" />
                <el-table-column :label="$t('响应码')" align="center" prop="httpStatusCode" sortable="custom" width="150">
                    <template #default="{ row }">
                        <sc-status-indicator :type="row.httpStatusCode >= 200 && row.httpStatusCode < 300 ? 'success' : 'danger'" />
                        {{ row.httpStatusCode }}
                    </template>
                </el-table-column>
                <el-table-column :label="$t('请求服务')" align="center">
                    <el-table-column :label="$t('路径')" prop="apiId" show-overflow-tooltip sortable="custom">
                        <template #default="scope">
                            <p>{{ scope.row.apiId }}</p>
                            <p>{{ scope.row.apiSummary }}</p>
                        </template>
                    </el-table-column>
                    <el-table-column :label="$t('方法')" align="center" prop="method" sortable="custom" width="100">
                        <template #default="scope">
                            <sc-status-indicator
                                :style="`background: #${Math.abs(this.$TOOL.crypto.hashCode(scope.row.method)).toString(16).substring(0, 6)}`" />
                            {{ scope.row.method }}
                        </template>
                    </el-table-column>
                    <el-table-column
                        :formatter="(row) => `${tool.groupSeparator((row.duration / 1000).toFixed(0))} ms`"
                        :label="$t('耗时')"
                        align="right"
                        prop="duration"
                        sortable="custom"
                        width="90">
                    </el-table-column>
                </el-table-column>
                <na-col-user
                    v-auth="'sys/log/operation/user'"
                    :label="$t('用户')"
                    header-align="center"
                    nestProp="user.userName"
                    nestProp2="user.id"
                    prop="userId"
                    sortable="custom"
                    width="170"></na-col-user>
                <el-table-column :label="$t('客户端IP')" prop="createdClientIp" show-overflow-tooltip sortable="custom" width="200">
                    <template #default="scope">
                        <na-ip :ip="scope.row.createdClientIp"></na-ip>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('操作系统')" align="center" prop="os" width="150" />
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
import tool from '@/utils/tool'
import ScTable from '@/components/scTable/index.vue'
import ScStatusIndicator from '@/components/scMini/scStatusIndicator.vue'

export default {
    computed: {
        tool() {
            return tool
        },
    },
    components: {
        ScStatusIndicator,
        ScTable,
        naInfo,
    },
    watch: {},
    inject: ['reload'],
    data() {
        return {
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
            if (typeof form.dy.apiId === 'string' && form.dy.apiId.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'apiId',
                    operator: 'eq',
                    value: form.dy.apiId,
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
            const res = await this.$API.sys_log.query.post({
                filter: { id: row.id },
            })
            this.$refs.info.open(tool.sortProperties(res.data[0]), this.$t('日志详情：{id}', { id: row.id }))
        },
    },
}
</script>

<style scoped></style>