<template>
    <el-container>
        <el-main class="nopadding">
            <el-container>
                <el-header class="headerPublic">
                    <div class="left-panel">
                        <na-search
                            :controls="[
                                {
                                    type: 'input',
                                    field: ['filter', 'id'],
                                    placeholder: '日志编号',
                                    style: 'width:12rem',
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
                                    style: 'width:15rem',
                                },
                                {
                                    type: 'cascader',
                                    field: ['dy', 'apiId'],
                                    api: $API.sys_api.query,
                                    props: { label: 'summary', value: 'id', checkStrictly: true, expandTrigger: 'hover', emitPath: false },
                                    placeholder: '请求服务',
                                    style: 'width:20rem',
                                },
                                {
                                    type: 'input',
                                    field: ['dy', 'createdUserName'],
                                    placeholder: '用户名',
                                    style: 'width:12rem',
                                },
                                {
                                    type: 'input',
                                    field: ['dy', 'createdClientIp'],
                                    placeholder: '客户端IP',
                                    style: 'width:12rem',
                                },
                            ]"
                            :vue="this"
                            @search="onSearch" />
                    </div>
                    <div class="right-panel"></div>
                </el-header>
                <el-main class="nopadding">
                    <sc-table
                        :apiObj="$API.sys_log.pagedQuery"
                        :context-menus="[
                            'id',
                            'httpStatusCode',
                            'apiId',
                            'createdUserName',
                            'method',
                            'duration',
                            'createdClientIp',
                            'os',
                            'createdTime',
                        ]"
                        :context-opers="[]"
                        :default-sort="{ prop: 'createdTime', order: 'descending' }"
                        :params="query"
                        :vue="this"
                        @row-click="rowClick"
                        ref="table"
                        remoteFilter
                        remoteSort
                        row-key="id"
                        stripe>
                        <el-table-column :label="$t('日志编号')" align="center" prop="id" sortable="custom" width="150"></el-table-column>
                        <el-table-column :label="$t('响应码')" align="center" prop="httpStatusCode" sortable="custom" width="100">
                            <template #default="scope">
                                <div class="indicator">
                                    <sc-status-indicator
                                        :style="`background: #${Math.abs(this.$TOOL.crypto.hashCode(scope.row.httpStatusCode)).toString(16).substring(0, 6)}`" />
                                    <span> {{ scope.row.httpStatusCode }}</span>
                                </div>
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
                                    <div class="indicator">
                                        <sc-status-indicator
                                            :style="`background: #${Math.abs(this.$TOOL.crypto.hashCode(scope.row.method)).toString(16).substring(0, 6)}`" />
                                        <span> {{ scope.row.method }}</span>
                                    </div>
                                </template>
                            </el-table-column>
                            <el-table-column
                                :formatter="(row) => `${tool.groupSeparator((row.duration / 1000).toFixed(0))} ms`"
                                :label="$t('耗时')"
                                align="right"
                                prop="duration"
                                sortable="custom"
                                width="120">
                            </el-table-column>
                        </el-table-column>
                        <el-table-column :label="$t('用户名')" prop="createdUserName" sortable="custom" width="150">
                            <template #default="scope">
                                {{ scope.row.apiId === 'api/sys/user/pwd.login' ? scope.row.extraData : scope.row.createdUserName }}
                            </template>
                        </el-table-column>
                        <el-table-column :label="$t('客户端IP')" prop="createdClientIp" show-overflow-tooltip sortable="custom" width="200">
                            <template #default="scope">
                                <na-ip :ip="scope.row.createdClientIp"></na-ip>
                            </template>
                        </el-table-column>
                        <el-table-column :label="$t('操作系统')" align="center" prop="os" width="150"></el-table-column>
                        <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom" width="170"></el-table-column>
                    </sc-table>
                </el-main>
            </el-container>
        </el-main>
    </el-container>

    <na-info v-if="dialog.info" ref="info"></na-info>
</template>

<script>
import naInfo from '@/components/naInfo/index.vue'
import tool from '@/utils/tool'
import ScTable from '@/components/scTable/index.vue'

export default {
    inject: ['reload'],
    computed: {
        tool() {
            return tool
        },
    },
    components: {
        ScTable,
        naInfo,
    },
    watch: {},
    data() {
        return {
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
            },
            dialog: {
                info: false,
            },
        }
    },
    mounted() {},
    methods: {
        //搜索
        onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime,
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
            if (form.dy.apiId) {
                this.query.dynamicFilter.filters.push({
                    field: 'apiId',
                    operator: 'contains',
                    value: form.dy.apiId,
                })
            }
            if (form.dy.createdUserName) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdUserName',
                    operator: 'contains',
                    value: form.dy.createdUserName,
                })
            }
            if (form.dy.createdClientIp) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdClientIp',
                    operator: 'contains',
                    value: form.dy.createdClientIp,
                })
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