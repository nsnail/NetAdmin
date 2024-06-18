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
                            placeholder: $t('部门编号 / 部门名称 / 备注'),
                            style: 'width:25rem',
                        },
                    ]"
                    :vue="this"
                    @reset="Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))"
                    @search="onSearch"
                    ref="search" />
            </div>
            <div class="right-panel">
                <na-button-add :vue="this" />
                <na-button-bulk-del :api="$API.sys_dept.bulkDelete" :vue="this" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                v-loading="loading"
                :apiObj="$API.sys_dept.query"
                :context-menus="['id', 'name', 'sort', 'enabled', 'createdTime', 'summary']"
                :default-sort="{ prop: 'sort', order: 'descending' }"
                :params="query"
                :vue="this"
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
                <el-table-column type="selection" width="50" />
                <el-table-column :label="$t('部门编号')" prop="id" sortable="custom" />
                <el-table-column :label="$t('部门名称')" prop="name" sortable="custom" />
                <el-table-column :label="$t('排序')" align="right" prop="sort" sortable="custom" />
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="100">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" @change="changeSwitch($event, scope.row)"></el-switch>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('备注')" prop="summary" />
                <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom" />
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: '删除部门',
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
                await this.$API.sys_dept.setEnabled.post(row)
                this.$message.success(this.$t('操作成功'))
            } catch {
                //
            }
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
                const res = await this.$API.sys_dept.delete.post({ id: row.id })
                this.$message.success(this.$t('删除 {count} 项', { count: res.data }))
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
    mounted() {},
    watch: {},
}
</script>

<style scoped></style>