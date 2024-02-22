<template>
    <el-container>
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
                            field: ['dy', 'enabled'],
                            options: [
                                { label: '启用', value: true },
                                { label: '禁用', value: false },
                            ],
                            placeholder: '状态',
                            style: 'width:15rem',
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
                    @search="onSearch" />
            </div>
            <div class="right-panel">
                <na-button-add :vue="this"></na-button-add>
                <el-button :disabled="selection.length === 0" @click="batchDel" icon="el-icon-delete" plain type="danger"></el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :apiObj="$API.sys_role.pagedQuery"
                :default-sort="{ prop: 'sort', order: 'descending' }"
                :params="query"
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
                <el-table-column type="selection" width="50"></el-table-column>
                <el-table-column :label="$t('角色编号')" prop="id" sortable="custom"></el-table-column>
                <el-table-column :label="$t('角色名称')" prop="name" sortable="custom"></el-table-column>
                <el-table-column :label="$t('排序')" prop="sort" sortable="custom"></el-table-column>
                <na-col-indicator
                    :label="$t('状态')"
                    :options="[
                        { text: '启用', type: 'success', value: true },
                        { text: '禁用', type: 'danger', value: false, pulse: true },
                    ]"
                    prop="enabled"></na-col-indicator>
                <na-col-indicator
                    :label="$t('无限权限')"
                    :options="[
                        { text: '是', type: 'success', value: true, pulse: true },
                        { text: '否', type: 'danger', value: false },
                    ]"
                    prop="ignorePermissionControl"></na-col-indicator>

                <el-table-column :label="$t('数据范围')" prop="dataScope" sortable="custom">
                    <template #default="scope">
                        {{ this.$GLOBAL.enums.dataScopes[scope.row.dataScope][1] }}
                    </template>
                </el-table-column>

                <na-col-indicator
                    :label="$t('显示仪表板')"
                    :options="[
                        { text: '是', type: 'success', value: true },
                        { text: '否', type: 'danger', value: false },
                    ]"
                    prop="displayDashboard"></na-col-indicator>

                <el-table-column :label="$t('创建时间')" prop="createdTime" sortable="custom"></el-table-column>
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: '删除角色',
                            click: rowDel,
                        })
                    "
                    :vue="this"></na-col-operation>
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
    computed: {
        table() {
            return table
        },
        naColOperation() {
            return naColOperation
        },
    },
    components: {
        saveDialog,
    },
    data() {
        return {
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
            },
            dialog: {
                save: false,
            },
            selection: [],
        }
    },
    methods: {
        //删除
        async rowDel(row) {
            try {
                const res = await this.$API.sys_role.delete.post({ id: row.id })
                this.$refs.table.refresh()
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
        },
        //批量删除
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
                this.$refs.table.refresh()
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            loading?.close()
        },

        //搜索
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
}
</script>

<style scoped></style>