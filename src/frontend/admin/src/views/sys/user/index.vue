<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: '用户编号 / 用户名 / 手机号 / 邮箱 / 备注',
                            style: 'width:25rem',
                        },
                        {
                            type: 'remote-select',
                            field: ['filter', 'roleId'],
                            api: $API.sys_role.query,
                            config: { props: { label: 'name', value: 'id' } },
                            placeholder: '所属角色',
                            style: 'width:15rem',
                        },
                        {
                            type: 'cascader',
                            field: ['filter', 'deptId'],
                            api: $API.sys_dept.query,
                            props: { label: 'name', value: 'id', checkStrictly: true, expandTrigger: 'hover', emitPath: false },
                            placeholder: '所属部门',
                            style: 'width:15rem',
                        },
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
                    @search="onSearch" />
            </div>
            <div class="right-panel">
                <na-button-add :vue="this" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                v-loading="loading"
                :apiObj="$API.sys_user.pagedQuery"
                :context-menus="['id', 'userName', 'mobile', 'email', 'enabled', 'createdTime']"
                :context-opers="['view', 'edit']"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
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
                <el-table-column type="selection"></el-table-column>
                <el-table-column :label="$t('用户编号')" prop="id" sortable="custom" width="150"></el-table-column>
                <na-col-avatar :label="$t('用户名')" prop="userName" />
                <el-table-column :label="$t('手机号')" align="center" prop="mobile" sortable="custom" width="120"></el-table-column>
                <el-table-column :label="$t('邮箱')" prop="email" sortable="custom"></el-table-column>
                <na-col-tags :label="$t('所属角色')" @click="(item) => openDialog('sys_role', item.id, 'roleSave')" field="name" prop="roles" />
                <na-col-tags :label="$t('所属部门')" @click="(item) => openDialog('sys_dept', item.id, 'deptSave')" field="name" prop="dept" />
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="80">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" @change="changeSwitch($event, scope.row)"></el-switch>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom"></el-table-column>
                <na-col-operation :vue="this"></na-col-operation>
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = false"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>
    <role-save-dialog v-if="dialog.roleSave" @closed="dialog.roleSave = false" ref="roleSaveDialog"></role-save-dialog>
    <dept-save-dialog v-if="dialog.deptSave" @closed="dialog.deptSave = false" ref="deptSaveDialog"></dept-save-dialog>
</template>

<script>
import saveDialog from './save'
import roleSaveDialog from '@/views/sys/role/save.vue'
import deptSaveDialog from '@/views/sys/dept/save.vue'
import table from '@/config/table'

export default {
    components: {
        saveDialog,
        roleSaveDialog,
        deptSaveDialog,
    },
    inject: ['reload'],
    data() {
        return {
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
            },
            dialog: {
                roleSave: false,
                deptSave: false,
                save: false,
            },
            selection: [],
        }
    },
    watch: {},
    computed: {
        table() {
            return table
        },
    },
    mounted() {},
    created() {},
    methods: {
        //表格内开关事件
        async changeSwitch(event, row) {
            try {
                await this.$API.sys_user.setEnabled.post(row)
                this.$message.success(`操作成功`)
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        async openDialog(api, id, dialog) {
            this.loading = true
            const res = await this.$API[api].query.post({
                filter: { id: id },
            })
            this.loading = false
            this.dialog[dialog] = true
            await this.$nextTick()
            this.$refs[`${dialog}Dialog`].open('view', res.data[0])
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