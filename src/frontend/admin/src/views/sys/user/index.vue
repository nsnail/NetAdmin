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
                    ]"
                    :vue="this"
                    @reset="Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))"
                    @search="onSearch"
                    ref="search" />
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
                <el-table-column type="selection" />
                <el-table-column :label="$t('用户编号')" prop="id" sortable="custom" width="150" />
                <na-col-avatar :label="$t('用户名')" prop="userName" />
                <el-table-column :label="$t('手机号')" align="center" prop="mobile" sortable="custom" width="120" />
                <el-table-column :label="$t('邮箱')" prop="email" sortable="custom" />
                <na-col-tags :label="$t('所属角色')" @click="(item) => openDialog('sys_role', item.id, 'roleSave')" field="name" prop="roles" />
                <na-col-tags :label="$t('所属部门')" @click="(item) => openDialog('sys_dept', item.id, 'deptSave')" field="name" prop="dept" />
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="80">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" @change="changeSwitch($event, scope.row)"></el-switch>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom" />
                <na-col-operation :vue="this" />
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
        deptSaveDialog,
        roleSaveDialog,
        saveDialog,
    },
    computed: {
        table() {
            return table
        },
    },
    created() {},
    data() {
        return {
            dialog: {
                deptSave: false,
                roleSave: false,
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
                await this.$API.sys_user.setEnabled.post(row)
                this.$refs.table.refresh()
                this.$message.success(`操作成功`)
            } catch {
                //
            }
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
                this.$refs.search.search()
            })
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