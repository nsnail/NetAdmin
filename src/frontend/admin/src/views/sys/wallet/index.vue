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
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'select-input',
                            field: [
                                'dy',
                                [
                                    { label: $t('用户编号'), key: 'id' },
                                    { label: $t('用户名'), key: 'owner.userName' },
                                    { label: $t('电子邮箱'), key: 'owner.email' },
                                    { label: $t('手机号'), key: 'owner.mobile' },
                                ],
                            ],
                            placeholder: $t('匹配内容'),
                            style: 'width:25rem',
                            selectStyle: 'width:8rem',
                        },
                        {
                            type: 'remote-select',
                            field: ['filter', 'deptId'],
                            api: $API.sys_dept.query,
                            config: { props: { label: 'name', value: 'id' } },
                            placeholder: $t('归属部门'),
                            style: 'width:15rem',
                            condition: () => $GLOBAL.hasApiPermission('api/sys/dept/query'),
                        },
                        {
                            type: 'remote-select',
                            field: ['filter', 'roleId'],
                            api: $API.sys_role.query,
                            config: { props: { label: 'name', value: 'id' } },
                            placeholder: $t('归属角色'),
                            style: 'width:15rem',
                            condition: () => $GLOBAL.hasApiPermission('api/sys/dept/query'),
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
                :context-extra="{ id: ['createdTime'], ownerId: ['owner.userName'] }"
                :context-menus="[
                    'id',
                    'ownerId',
                    'owner.userName',
                    'createdTime',
                    'availableBalance',
                    'frozenBalance',
                    'totalIncome',
                    'totalExpenditure',
                    'modifiedTime',
                ]"
                :context-opers="['view']"
                :default-sort="{ prop: 'id', order: 'descending' }"
                :export-api="$API.sys_userwallet.export"
                :params="query"
                :query-api="$API.sys_userwallet.pagedQuery"
                :vue="this"
                @data-change="getStatistics"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <na-col-id :label="$t('钱包编号')" prop="id" sortable="custom" width="170" />
                <na-col-user
                    :clickOpenDialog="$GLOBAL.hasApiPermission('api/sys/user/get')"
                    :label="$t('归属用户')"
                    nestProp="owner.userName"
                    nestProp2="ownerId"
                    prop="ownerId"
                    sortable="custom"
                    width="170" />
                <el-table-column
                    :formatter="(row) => $TOOL.groupSeparator(row.availableBalance)"
                    :label="$t('可用余额')"
                    align="right"
                    prop="availableBalance"
                    sortable="custom" />
                <el-table-column
                    :formatter="(row) => $TOOL.groupSeparator(row.frozenBalance)"
                    :label="$t('冻结余额')"
                    align="right"
                    prop="frozenBalance"
                    sortable="custom" />
                <el-table-column
                    :formatter="(row) => $TOOL.groupSeparator(row.totalIncome)"
                    :label="$t('总收入')"
                    align="right"
                    prop="totalIncome"
                    sortable="custom" />
                <el-table-column
                    :formatter="(row) => $TOOL.groupSeparator(row.totalExpenditure)"
                    :label="$t('总支出')"
                    align="right"
                    prop="totalExpenditure"
                    sortable="custom" />
                <el-table-column v-tim :label="$t('最后交易时间')" align="right" prop="modifiedTime" sortable="custom" width="150">
                    <template #default="{ row }">
                        <span v-if="row.modifiedTime" v-time.tip="row.modifiedTime" :title="row.modifiedTime"></span>
                    </template>
                </el-table-column>
                <na-col-operation
                    :buttons="[
                        naColOperation.buttons[0],
                        {
                            icon: 'el-icon-plus',
                            title: $t('新建交易'),
                            click: async (row, vue) => {
                                vue.dialog.trade = { row }
                            },
                            condition: () => {
                                return $GLOBAL.hasApiPermission('api/sys/wallet.trade/create')
                            },
                        },
                    ]"
                    :vue="this"
                    width="120" />
            </sc-table>
        </el-main>
    </el-container>

    <trade-dialog
        v-if="dialog.trade"
        @closed="dialog.trade = null"
        @mounted="$refs.tradeDialog.open(dialog.trade)"
        @success="(data, mode) => $refs.table.refresh()"
        ref="tradeDialog" />

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => $refs.table.refresh()"
        ref="saveDialog" />
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/na-col-operation'
const tradeDialog = defineAsyncComponent(() => import('./trade'))
const saveDialog = defineAsyncComponent(() => import('./save'))
const naColUser = defineAsyncComponent(() => import('@/components/na-col-user'))
export default {
    components: {
        tradeDialog,
        saveDialog,
        naColUser,
    },
    computed: {
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    async created() {
        if (this.roleId) {
            this.query.filter.roleId = this.roleId
        }
        if (this.deptId) {
            this.query.filter.deptId = this.deptId
        }
        if (this.id) {
            this.query.dynamicFilter.filters.push({ field: 'id', operator: 'eq', value: this.id })
        }
    },
    data() {
        return {
            statistics: {
                total: '...',
            },
            dialog: {},
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
        },
        //重置
        onReset() {
            if (this.id) {
                this.$refs.search.selectInputKey = 'id'
            }
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
            if (typeof form.dy['owner.userName'] === 'string' && form.dy['owner.userName'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'owner.userName',
                    operator: 'eq',
                    value: form.dy['owner.userName'],
                })
            }
            if (typeof form.dy['owner.email'] === 'string' && form.dy['owner.email'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'owner.email',
                    operator: 'eq',
                    value: form.dy['owner.email'],
                })
            }
            if (typeof form.dy['owner.mobile'] === 'string' && form.dy['owner.mobile'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'owner.mobile',
                    operator: 'eq',
                    value: form.dy['owner.mobile'],
                })
            }
            if (typeof form.dy['id'] === 'string' && form.dy['id'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'id',
                    operator: 'eq',
                    value: form.dy['id'],
                })
            }
            await this.$refs.table.upData()
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

        if (this.id) {
            this.$refs.search.selectInputKey = 'id'
            this.$refs.search.form.dy.id = this.id
            this.$refs.search.keeps.push({
                field: 'id',
                value: this.id,
                type: 'dy',
            })
        }

        if (this.roleId) {
            this.$refs.search.form.filter.roleId = this.roleId
            this.$refs.search.keeps.push({
                field: 'roleId',
                value: this.roleId,
                type: 'filter',
            })
        }
        if (this.deptId) {
            this.$refs.search.form.filter.deptId = this.deptId
            this.$refs.search.keeps.push({
                field: 'deptId',
                value: this.deptId,
                type: 'filter',
            })
        }

        this.onReset()
    },
    props: ['keywords', 'roleId', 'deptId', 'id'],
    watch: {},
}
</script>

<style scoped />