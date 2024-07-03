<template>
    <el-container>
        <el-header style="height: auto; padding: 0 1rem">
            <sc-select-filter
                :data="[
                    {
                        title: $t('启用状态'),
                        key: 'enabled',
                        options: [
                            { label: $t('全部'), value: '' },
                            { label: $t('启用'), value: true },
                            { label: $t('禁用'), value: false },
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
                        {
                            type: 'cascader',
                            field: ['filter', 'deptId'],
                            api: $API.sys_dept.query,
                            props: { label: 'name', value: 'id', checkStrictly: true, expandTrigger: 'hover', emitPath: false },
                            placeholder: $t('所属部门'),
                            style: 'width:15rem',
                        },
                    ]"
                    :vue="this"
                    @reset="Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))"
                    @search="onSearch"
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
            <sc-table
                :context-menus="['id', 'userName', 'mobile', 'email', 'enabled', 'createdTime']"
                :context-opers="['view', 'edit']"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :export-api="$API.sys_user.export"
                :params="query"
                :query-api="$API.sys_user.pagedQuery"
                :vue="this"
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
                <el-table-column type="selection" />
                <na-col-id :label="$t('用户编号')" prop="id" sortable="custom" width="170" />
                <na-col-avatar :label="$t('用户名')" prop="userName" width="170" />
                <el-table-column :label="$t('手机号')" align="center" prop="mobile" sortable="custom" width="120" />
                <el-table-column :label="$t('邮箱')" align="right" prop="email" sortable="custom" />
                <na-col-tags
                    :label="$t('所属角色')"
                    @click="(item) => (this.dialog.roleSave = { row: item, mode: 'view' })"
                    field="name"
                    prop="roles" />
                <na-col-tags
                    :label="$t('所属部门')"
                    @click="(item) => (this.dialog.deptSave = { row: item, mode: 'view' })"
                    field="name"
                    prop="dept"
                    width="200" />
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="100">
                    <template #default="{ row }">
                        <el-switch v-model="row.enabled" @change="changeSwitch($event, row)"></el-switch>
                    </template>
                </el-table-column>
                <na-col-operation :vue="this" width="120" />
            </sc-table>
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
export default {
    components: {
        deptSaveDialog,
        roleSaveDialog,
        saveDialog,
    },
    computed: {
        table() {
            return table
        },
    },
    created() {
        if (this.keywords) {
            this.query.keywords = this.keywords
        }
        if (this.roleId) {
            this.query.filter.roleId = this.roleId
        }
        if (this.deptId) {
            this.query.filter.deptId = this.deptId
        }
    },
    data() {
        return {
            dialog: {},
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
            },
            selection: [],
        }
    },
    inject: ['reload'],
    methods: {
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
        onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime,
                })
            }

            if (typeof form.dy.enabled === 'boolean') {
                this.query.dynamicFilter.filters.push({
                    field: 'enabled',
                    operator: 'eq',
                    value: form.dy.enabled,
                })
            }

            this.$refs.table.upData()
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
    props: ['keywords', 'roleId', 'deptId'],
    watch: {},
}
</script>

<style scoped></style>