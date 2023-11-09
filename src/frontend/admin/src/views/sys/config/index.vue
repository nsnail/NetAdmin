<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <na-button-add :vue="this"></na-button-add>
                <el-button :disabled="selection.length === 0" icon="el-icon-delete" plain type="danger" @click="batchDel"></el-button>
            </div>
            <div class="right-panel"></div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                ref="table"
                :apiObj="$API.sys_config.pagedQuery"
                row-key="id"
                stripe
                @selection-change="
                    (items) => {
                        selection = items
                    }
                ">
                <el-table-column type="selection"></el-table-column>
                <el-table-column :label="$t('配置编号')" prop="id"></el-table-column>
                <el-table-column :label="$t('用户注册')">
                    <el-table-column :label="$t('默认部门')" prop="userRegisterDept.name"></el-table-column>
                    <el-table-column :label="$t('默认角色')" prop="userRegisterRole.name"></el-table-column>
                    <el-table-column :label="$t('人工审核')" prop="userRegisterConfirm">
                        <template #default="scope">
                            <el-switch v-model="scope.row.userRegisterConfirm" @change="changeSwitch($event, scope.row)"></el-switch>
                        </template>
                    </el-table-column>
                </el-table-column>

                <el-table-column :label="$t('启用')" prop="enabled">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" @change="changeSwitch($event, scope.row)"></el-switch>
                    </template>
                </el-table-column>
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
        ref="saveDialog"
        @closed="dialog.save = false"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"></save-dialog>
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
            dialog: {
                save: false,
                info: false,
            },
            selection: [],
            search: {
                dynamicFilter: {
                    field: 'id',
                    operator: 'contains',
                    value: '',
                },
            },
        }
    },
    mounted() {},
    methods: {
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