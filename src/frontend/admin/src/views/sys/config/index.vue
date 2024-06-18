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
                @on-change="filterChange"
                label-width="10"
                ref="selectFilter"></sc-select-filter>
        </el-header>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[]"
                    :vue="this"
                    @reset="Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))"
                    @search="onSearch"
                    ref="search" />
            </div>
            <div class="right-panel">
                <na-button-add :vue="this" />
                <na-button-bulk-del :api="$API.sys_config.bulkDelete" :vue="this" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                v-loading="loading"
                :apiObj="$API.sys_config.pagedQuery"
                :context-menus="['id', 'userRegisterConfirm', 'enabled', 'createdTime']"
                :params="query"
                :vue="this"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                ref="table"
                row-key="id"
                stripe>
                <el-table-column type="selection" />
                <el-table-column :label="$t('配置编号')" align="center" prop="id" width="170" />
                <el-table-column :label="$t('用户注册')" align="center">
                    <el-table-column :label="$t('默认部门')" align="center" prop="userRegisterDept.name" width="150" />
                    <el-table-column :label="$t('默认角色')" align="center" prop="userRegisterRole.name" width="150" />
                    <el-table-column :label="$t('人工审核')" align="center" prop="userRegisterConfirm" width="120">
                        <template #default="scope">
                            <el-switch v-model="scope.row.userRegisterConfirm" @change="changeSwitch($event, scope.row)"></el-switch>
                        </template>
                    </el-table-column>
                </el-table-column>
                <el-table-column :label="$t('启用')" align="center" prop="enabled" width="100">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" @change="changeSwitch($event, scope.row)"></el-switch>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('创建时间')" align="center" prop="createdTime" width="170" />
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
                    :vue="this"
                    width="170" />
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
import tool from '@/utils/tool'

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
                info: false,
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
                await this.$API.sys_config.edit.post(row)
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
                const res = await this.$API.sys_config.delete.post({ id: row.id })
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
}
</script>

<style scoped></style>