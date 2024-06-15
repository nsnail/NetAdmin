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
                            placeholder: '角色编号 / 角色名称 / 备注',
                            style: 'width:20rem',
                        },
                        {
                            type: 'select',
                            field: ['dy', 'ignorePermissionControl'],
                            options: [
                                { label: '是', value: true },
                                { label: '否', value: false },
                            ],
                            placeholder: '无限权限',
                            style: 'width:15rem',
                        },
                        {
                            type: 'select',
                            field: ['dy', 'displayDashboard'],
                            options: [
                                { label: '是', value: true },
                                { label: '否', value: false },
                            ],
                            placeholder: '显示仪表板',
                            style: 'width:15rem',
                        },
                    ]"
                    :vue="this"
                    @reset="Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))"
                    @search="onSearch"
                    ref="search" />
            </div>
            <div class="right-panel">
                <na-button-add :vue="this" />
                <el-button :disabled="selection.length === 0" @click="batchDel" icon="el-icon-delete" plain type="danger"></el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                v-loading="loading"
                :apiObj="$API.sys_role.pagedQuery"
                :context-menus="['id', 'name', 'sort', 'enabled', 'ignorePermissionControl', 'dataScope', 'displayDashboard', 'createdTime']"
                :default-sort="{ prop: 'sort', order: 'descending' }"
                :params="query"
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
                <el-table-column :label="$t('角色编号')" prop="id" sortable="custom" />
                <el-table-column :label="$t('角色名称')" prop="name" sortable="custom" />
                <el-table-column :label="$t('排序')" align="right" prop="sort" sortable="custom" />
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="80">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" @change="changeSwitch($event, scope.row)"></el-switch>
                    </template>
                </el-table-column>
                <na-col-indicator
                    :label="$t('无限权限')"
                    :options="[
                        { text: '是', type: 'success', value: true, pulse: true },
                        { text: '否', type: 'danger', value: false },
                    ]"
                    align="center"
                    prop="ignorePermissionControl"
                    sortable="custom"></na-col-indicator>
                <na-col-indicator
                    :label="$t('数据范围')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.dataScopes).map((x) => {
                            return { value: x[0], text: x[1][1], type: x[1][2] }
                        })
                    "
                    prop="dataScope"
                    sortable="custom"
                    width="100">
                </na-col-indicator>

                <na-col-indicator
                    :label="$t('显示仪表板')"
                    :options="[
                        { text: '是', type: 'success', value: true },
                        { text: '否', type: 'danger', value: false },
                    ]"
                    align="center"
                    prop="displayDashboard"
                    sortable="custom" />

                <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom" />
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: '删除角色',
                            click: rowDel,
                            type: 'danger',
                        })
                    "
                    :vue="this" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = false"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>
</template>

<script>
import saveDialog from './save'
import naColOperation from '@/config/naColOperation'
import table from '@/config/table'

export default {
    components: {
        saveDialog,
    },
    computed: {
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    created() {},
    data() {
        return {
            dialog: {
                save: false,
            },
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
        async changeSwitch(event, row) {
            try {
                await this.$API.sys_role.setEnabled.post(row)
                this.$message.success(`操作成功`)
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        async batchDel() {
            let loading
            try {
                await this.$confirm(`确定删除选中的 ${this.selection.length} 项吗？`, '提示', {
                    type: 'warning',
                })
                loading = this.$loading()
                const res = await this.$API.sys_role.bulkDelete.post({
                    items: this.selection,
                })
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            loading?.close()
            this.$refs.table.refresh()
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
                this.$refs.search.search()
            })
        },
        async rowDel(row) {
            try {
                const res = await this.$API.sys_role.delete.post({ id: row.id })
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime,
                })
            }

            if (typeof form.dy.ignorePermissionControl === 'boolean') {
                this.query.dynamicFilter.filters.push({
                    field: 'ignorePermissionControl',
                    operator: 'eq',
                    value: form.dy.ignorePermissionControl,
                })
            }

            if (typeof form.dy.enabled === 'boolean') {
                this.query.dynamicFilter.filters.push({
                    field: 'enabled',
                    operator: 'eq',
                    value: form.dy.enabled,
                })
            }

            if (typeof form.dy.displayDashboard === 'boolean') {
                this.query.dynamicFilter.filters.push({
                    field: 'displayDashboard',
                    operator: 'eq',
                    value: form.dy.displayDashboard,
                })
            }
            this.$refs.table.upData()
        },
    },
    mounted() {},
    watch: {},
}
</script>

<style scoped></style>