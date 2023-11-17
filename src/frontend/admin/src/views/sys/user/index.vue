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
                        },
                        {
                            type: 'cascader',
                            field: ['filter', 'deptId'],
                            api: $API.sys_dept.query,
                            props: { label: 'name', value: 'id', checkStrictly: true, expandTrigger: 'hover', emitPath: false },
                            placeholder: '所属部门',
                        },
                        {
                            type: 'select',
                            field: ['dy', 'enabled'],
                            options: [
                                { label: '启用', value: true },
                                { label: '禁用', value: false },
                            ],
                            placeholder: '状态',
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
                ref="table"
                v-loading="loading"
                :apiObj="$API.sys_user.pagedQuery"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :params="query"
                remote-filter
                remote-sort
                stripe
                @selection-change="
                    (items) => {
                        selection = items
                    }
                ">
                <el-table-column type="selection"></el-table-column>
                <el-table-column :label="$t('用户编号')" prop="id" sortable="custom" width="150"></el-table-column>
                <na-col-avatar :label="$t('用户名')" prop="userName" />
                <el-table-column :label="$t('手机号')" prop="mobile" sortable="custom" width="120"></el-table-column>
                <el-table-column :label="$t('邮箱')" prop="email" sortable="custom"></el-table-column>
                <na-col-tags field="name" :label="$t('所属角色')" prop="roles" @click="(item) => openDialog('sys_role', item.id, 'roleSave')" />
                <na-col-tags field="name" :label="$t('所属部门')" prop="dept" @click="(item) => openDialog('sys_dept', item.id, 'deptSave')" />
                <na-col-indicator
                    :options="[
                        { text: '启用', type: 'success', value: true },
                        { text: '禁用', type: 'danger', value: false, pulse: true },
                    ]"
                    :label="$t('状态')"
                    prop="enabled"></na-col-indicator>
                <el-table-column :label="$t('创建时间')" prop="createdTime" sortable="custom"></el-table-column>
                <el-table-column :label="$t('备注')" prop="summary" width="50"></el-table-column>
                <na-col-operation :vue="this"></na-col-operation>
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        ref="saveDialog"
        @closed="dialog.save = false"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"></save-dialog>
    <role-save-dialog v-if="dialog.roleSave" ref="roleSaveDialog" @closed="dialog.roleSave = false"></role-save-dialog>
    <dept-save-dialog v-if="dialog.deptSave" ref="deptSaveDialog" @closed="dialog.deptSave = false"></dept-save-dialog>
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