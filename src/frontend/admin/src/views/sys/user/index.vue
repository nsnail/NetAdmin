<template>
    <el-container>
        <el-header v-loading="statistics.total === '...'" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <scStatistic :title="$t('总数')" :value="statistics.total" group-separator></scStatistic>
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
        <el-header class="el-header-select-filter">
            <scSelectFilter
                :data="[
                    {
                        title: $t('启用状态'),
                        key: 'enabled',
                        options: [
                            { label: $t('全部'), value: '' },
                            { label: $t('启用'), value: true, badge: statistics.enabled?.find((x) => x.key.enabled === 'True')?.value },
                            { label: $t('禁用'), value: false, badge: statistics.enabled?.find((x) => x.key.enabled === 'False')?.value },
                        ],
                    },
                ]"
                :label-width="10"
                @on-change="filterChange"
                ref="selectFilter"></scSelectFilter>
        </el-header>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('用户编号 / 用户名 / 手机号 / 邮箱 / 备注'),
                            style: 'width:25rem',
                        },
                        {
                            type: 'remote-select',
                            field: ['filter', 'roleId'],
                            api: $API.sys_role.query,
                            config: { props: { label: 'name', value: 'id' } },
                            placeholder: $t('所属角色'),
                            style: 'width:15rem',
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
            <div class="right-panel">
                <el-button @click="this.dialog.save = { mode: 'add' }" icon="el-icon-plus" type="primary"></el-button>
                <el-dropdown v-show="this.selection.length > 0">
                    <el-button type="primary">
                        {{ $t('批量操作') }}
                        <el-icon>
                            <el-icon-arrow-down />
                        </el-icon>
                    </el-button>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item @click="setEnabled(true)">{{ $t('启用用户') }}</el-dropdown-item>
                            <el-dropdown-item @click="setEnabled(false)">{{ $t('禁用用户') }}</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-header>
        <el-main class="nopadding">
            <el-row class="h100p">
                <el-col :lg="4">
                    <el-tree
                        v-loading="!deptTree"
                        :data="deptTree"
                        :default-expand-all="true"
                        :expand-on-click-node="false"
                        :props="{
                            label: (data) => {
                                return data.name
                            },
                        }"
                        @node-click="deptClick"
                        highlight-current
                        node-key="id"
                        ref="tree">
                    </el-tree>
                </el-col>
                <el-col :lg="20">
                    <scTable
                        :context-menus="['id', 'userName', 'mobile', 'email', 'enabled', 'createdTime', 'lastLoginTime']"
                        :context-opers="['view', 'edit']"
                        :default-sort="{ prop: 'id', order: 'descending' }"
                        :export-api="$API.sys_user.export"
                        :params="query"
                        :query-api="$API.sys_user.pagedQuery"
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
                        <el-table-column type="selection" width="50" />
                        <naColId :label="$t('用户编号')" prop="id" sortable="custom" width="170" />
                        <naColAvatar :label="$t('用户名')" prop="userName" width="170" />
                        <el-table-column :label="$t('手机号 / 邮箱')" align="right" prop="mobile" sortable="custom" width="250">
                            <template #default="{ row }">
                                <p>{{ row.mobile }}</p>
                                <p>{{ row.email }}</p>
                            </template>
                        </el-table-column>
                        <naColTags
                            :label="$t('所属部门')"
                            @click="(item) => (this.dialog.deptSave = { row: item, mode: 'view' })"
                            field="name"
                            prop="dept"
                            width="120" />
                        <naColTags
                            :label="$t('所属角色')"
                            @click="(item) => (this.dialog.roleSave = { row: item, mode: 'view' })"
                            field="name"
                            min-width="200"
                            prop="roles" />
                        <el-table-column :label="$t('最后登录')" align="right" prop="lastLoginTime" sortable="custom" width="120">
                            <template #default="{ row }">
                                <span v-time.tip="row.lastLoginTime" :title="row.lastLoginTime"></span>
                            </template>
                        </el-table-column>
                        <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="100">
                            <template #default="{ row }">
                                <el-switch v-model="row.enabled" @change="changeSwitch($event, row)"></el-switch>
                            </template>
                        </el-table-column>
                        <naColOperation :vue="this" width="120" />
                    </scTable>
                </el-col>
            </el-row>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>
    <role-save-dialog
        v-if="dialog.roleSave"
        @closed="dialog.roleSave = null"
        @mounted="$refs.roleSaveDialog.open(dialog.roleSave)"
        ref="roleSaveDialog"></role-save-dialog>
    <dept-save-dialog
        v-if="dialog.deptSave"
        @closed="dialog.deptSave = null"
        @mounted="$refs.deptSaveDialog.open(dialog.deptSave)"
        ref="deptSaveDialog"></dept-save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'

const roleSaveDialog = defineAsyncComponent(() => import('@/views/sys/role/save.vue'))
const deptSaveDialog = defineAsyncComponent(() => import('@/views/sys/dept/save.vue'))
const saveDialog = defineAsyncComponent(() => import('./save.vue'))
const naColAvatar = defineAsyncComponent(() => import('@/components/naColAvatar'))
const naColTags = defineAsyncComponent(() => import('@/components/naColTags'))
export default {
    components: {
        naColAvatar,
        deptSaveDialog,
        roleSaveDialog,
        saveDialog,
        naColTags,
    },
    computed: {
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
        const res = await this.$API.sys_dept.query.post({})

        this.deptTree = [
            {
                id: 0,
                name: '所有部门',
                children: res.data,
            },
        ]
    },
    data() {
        return {
            deptTree: null,
            statistics: {
                total: '...',
            },
            dialog: {},
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [
                        {
                            field: 'enabled',
                            operator: 'eq',
                            value: true,
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
        deptClick(e) {
            this.$refs.search.form.filter.deptId = e.id
            this.$refs.search.search()
        },
        async getStatistics() {
            this.statistics.total = this.$refs.table?.total
            const res = await Promise.all([
                this.$API.sys_user.countBy.post({
                    filter: this.query.filter,
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['Enabled'],
                }),
            ])

            this.statistics.enabled = res[0].data
        },
        async setEnabled(enabled) {
            let loading
            try {
                await this.$confirm(
                    this.$t('确定要 {operator} 选中的 {count} 项吗？', {
                        operator: enabled ? this.$t('启用') : this.$t('禁用'),
                        count: this.selection.length,
                    }),
                    this.$t('提示'),
                    {
                        type: 'warning',
                    },
                )
                loading = this.$loading()
                const res = await Promise.all(this.selection.map((x) => this.$API.sys_user.setEnabled.post(Object.assign(x, { enabled: enabled }))))
                this.$message.success(
                    this.$t('操作成功 {count}/{total} 项', {
                        count: res.map((x) => x.data ?? 0).reduce((a, b) => a + b, 0),
                        total: this.selection.length,
                    }),
                )
            } catch {
                //
            }
            this.$refs.table.refresh()
            loading?.close()
        },
        async changeSwitch(event, row) {
            try {
                await this.$API.sys_user.setEnabled.post(row)
                this.$message.success(this.$t('操作成功'))
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
        //重置
        onReset() {
            Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))
            this.$refs.selectFilter.selected['enabled'] = [true]
            this.$refs.tree.setCurrentKey(0)
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

            if (typeof form.dy.enabled === 'boolean') {
                this.query.dynamicFilter.filters.push({
                    field: 'enabled',
                    operator: 'eq',
                    value: form.dy.enabled,
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

        this.$refs.search.form.dy.enabled = true
        this.$refs.search.keeps.push({
            field: 'enabled',
            value: true,
            type: 'dy',
        })
        this.onReset()
    },
    props: ['keywords', 'roleId', 'deptId'],
    watch: {},
}
</script>

<style scoped>
::v-deep .el-tree-node__content {
    height: 3rem;
}
</style>