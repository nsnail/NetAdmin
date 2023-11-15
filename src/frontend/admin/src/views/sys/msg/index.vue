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
                <el-button :disabled="selection.length === 0" icon="el-icon-delete" plain type="danger" @click="batchDel"></el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                ref="table"
                v-loading="loading"
                :apiObj="$API.sys_sitemsg.pagedQuery"
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
                <el-table-column prop="id" label="消息编号" />
                <el-table-column prop="msgType" label="消息类型" />
                <el-table-column prop="title" label="消息主题" />
                <el-table-column prop="createdTime" label="创建时间" />
                <el-table-column prop="modifiedTime" label="修改时间" />
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: '删除消息',
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
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'

export default {
    components: {
        saveDialog,
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
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    mounted() {},
    created() {},
    methods: {
        //删除
        async rowDel(row) {
            try {
                const res = await this.$API.sys_sitemsg.delete.post({ id: row.id })
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
                const res = await this.$API.sys_sitemsg.bulkDelete.post({
                    items: this.selection,
                })
                this.$refs.table.refresh()
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            loading?.close()
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