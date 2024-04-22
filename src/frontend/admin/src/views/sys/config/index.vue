<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'select',
                            field: ['dy', 'enabled'],
                            options: [
                                { label: '启用', value: true },
                                { label: '禁用', value: false },
                            ],
                            placeholder: '是否启用',
                            style: 'width:10rem',
                        },
                    ]"
                    :vue="this"
                    @search="onSearch"
                    ref="search" />
            </div>
            <div class="right-panel">
                <na-button-add :vue="this"></na-button-add>
                <el-button :disabled="selection.length === 0" @click="batchDel" icon="el-icon-delete" plain type="danger"></el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
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
                <el-table-column align="center" type="selection"></el-table-column>
                <el-table-column :label="$t('配置编号')" align="center" prop="id"></el-table-column>
                <el-table-column :label="$t('用户注册')" align="center">
                    <el-table-column :label="$t('默认部门')" align="center" prop="userRegisterDept.name"></el-table-column>
                    <el-table-column :label="$t('默认角色')" align="center" prop="userRegisterRole.name"></el-table-column>
                    <el-table-column :label="$t('人工审核')" align="center" prop="userRegisterConfirm">
                        <template #default="scope">
                            <el-switch v-model="scope.row.userRegisterConfirm" @change="changeSwitch($event, scope.row)"></el-switch>
                        </template>
                    </el-table-column>
                </el-table-column>

                <el-table-column :label="$t('启用')" align="center" prop="enabled">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" @change="changeSwitch($event, scope.row)"></el-switch>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('创建时间')" align="center" prop="createdTime"></el-table-column>
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
import tool from '@/utils/tool'

export default {
    inject: ['reload'],
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
            dialog: {
                save: false,
                info: false,
            },
            selection: [],
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
            },
        }
    },
    mounted() {},
    methods: {
        //搜索
        onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                const start = new Date(form.dy.createdTime[0])
                const end = new Date(form.dy.createdTime[1])
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: [
                        tool.dateFormat(start.setDate(start.getDate() + 1)).substring(0, 10),
                        tool.dateFormat(end.setDate(end.getDate() + 1)).substring(0, 10),
                    ],
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

        //表格内开关事件
        async changeSwitch(event, row) {
            try {
                await this.$API.sys_config.update.post(row)
                this.$message.success(`操作成功`)
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        //删除明细
        async rowDel(row) {
            try {
                const res = await this.$API.sys_config.delete.post({ id: row.id })
                this.$refs.table.refresh()
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
        },
        //批量删除
        async batchDel() {
            const confirmRes = await this.$confirm(`确定删除选中的 ${this.selection.length} 项吗？`, '提示', {
                type: 'warning',
                confirmButtonText: '删除',
                confirmButtonClass: 'el-button--danger',
            }).catch(() => {})

            if (!confirmRes) {
                return false
            }

            try {
                await this.$API.sys_config.bulkDelete.post({
                    items: this.selection,
                })
                this.$refs.table.removeKeys(this.selection.map((x) => x.id))
                this.$message.success('操作成功')
            } catch {
                //
            }
        },
    },
}
</script>

<style></style>