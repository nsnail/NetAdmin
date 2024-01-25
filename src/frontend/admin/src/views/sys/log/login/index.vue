<template>
    <el-container>
        <el-main class="nopadding">
            <el-container>
                <el-header class="headerPublic">
                    <div class="left-panel">
                        <na-search
                            ref="search"
                            :controls="[
                                {
                                    type: 'select',
                                    field: ['dy', 'httpStatusCode'],
                                    options: [
                                        { label: '成功', value: '200,299' },
                                        { label: '失败', value: '300,999' },
                                    ],
                                    placeholder: '登录结果',
                                    style: 'width:15rem',
                                },
                                {
                                    type: 'input',
                                    field: ['dy', 'extraData'],
                                    placeholder: '登录名',
                                    style: 'width:15rem',
                                },
                                {
                                    type: 'input',
                                    field: ['dy', 'createdClientIp'],
                                    placeholder: 'IP地址',
                                    style: 'width:15rem',
                                },
                            ]"
                            :vue="this"
                            @search="onSearch" />
                    </div>
                    <div class="right-panel"></div>
                </el-header>
                <el-main class="nopadding">
                    <sc-table
                        ref="table"
                        :apiObj="$API.sys_log.pagedQuery"
                        :default-sort="{ prop: 'createdTime', order: 'descending' }"
                        :params="query"
                        remoteFilter
                        remoteSort
                        stripe
                        @row-click="rowClick">
                        <el-table-column :label="$t('日志编号')" prop="id" sortable="custom"></el-table-column>
                        <el-table-column :label="$t('日志时间')" prop="createdTime" sortable="custom"></el-table-column>
                        <el-table-column :label="$t('结果')" prop="httpStatusCode" sortable="custom">
                            <template #default="scope">
                                <sc-status-indicator :type="scope.row.httpStatusCode === 200 ? 'success' : 'danger'" />
                                {{ scope.row.httpStatusCode === 200 ? '成功' : '失败' }}
                            </template>
                        </el-table-column>
                        <el-table-column :label="$t('登录名')" prop="extraData" sortable="custom"></el-table-column>
                        <el-table-column :label="$t('IP地址')" prop="createdClientIp" sortable="custom"></el-table-column>
                        <el-table-column :label="$t('操作系统')" prop="os" sortable="custom"></el-table-column>
                        <el-table-column
                            :label="$t('用户代理')"
                            min-width="200"
                            prop="createdUserAgent"
                            show-overflow-tooltip
                            sortable="custom"></el-table-column>
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
    mounted() {
        this.$refs.search.form.dy.apiId = 'api/sys/user/login.by.pwd'
    },
    created() {
        this.query.dynamicFilter.filters.push({
            field: 'apiId',
            operator: 'eq',
            value: 'api/sys/user/login.by.pwd',
        })
    },
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
            if (form.dy.httpStatusCode) {
                this.query.dynamicFilter.filters.push({
                    field: 'httpStatusCode',
                    operator: 'range',
                    value: form.dy.httpStatusCode,
                })
            }
            if (form.dy.apiId) {
                this.query.dynamicFilter.filters.push({
                    field: 'apiId',
                    operator: 'contains',
                    value: form.dy.apiId,
                })
            }
            if (form.dy.extraData) {
                this.query.dynamicFilter.filters.push({
                    field: 'extraData',
                    operator: 'contains',
                    value: form.dy.extraData,
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