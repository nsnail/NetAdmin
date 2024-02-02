<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: '部门编号 / 部门名称 / 备注',
                            style: 'width:25rem',
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
                :apiObj="$API.sys_dept.query"
                :default-sort="{ prop: 'sort', order: 'descending' }"
                :params="query"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                default-expand-all
                hidePagination
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <el-table-column type="selection" width="50"></el-table-column>
                <el-table-column :label="$t('部门编号')" prop="id" sortable="custom"></el-table-column>
                <el-table-column :label="$t('部门名称')" prop="name" sortable="custom"></el-table-column>
                <el-table-column :label="$t('排序')" prop="sort" sortable="custom"></el-table-column>
                <na-col-indicator
                    :label="$t('状态')"
                    :options="[
                        { text: '启用', type: 'success', value: true },
                        { text: '禁用', type: 'danger', value: false, pulse: true },
                    ]"
                    prop="enabled"></na-col-indicator>
                <el-table-column :label="$t('创建时间')" prop="createdTime" sortable="custom"></el-table-column>
                <el-table-column :label="$t('备注')" prop="summary"></el-table-column>
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: '删除部门',
                            click: rowDel,
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
                const res = await this.$API.sys_dept.delete.post({ id: row.id })
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
                const res = await this.$API.sys_dept.bulkDelete.post({
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
}
</script>

<style scoped></style>