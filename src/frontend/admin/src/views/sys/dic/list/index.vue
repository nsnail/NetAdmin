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
                            field: ['dy', 'keywords'],
                            placeholder: $t('项名 / 项值'),
                            style: 'width:20rem',
                        },
                    ]"
                    :vue="this"
                    @reset="onReset"
                    @search="onSearch"
                    dateFormat="YYYY-MM-DD HH:mm:ss"
                    dateType="datetimerange"
                    dateValueFormat="YYYY-MM-DD HH:mm:ss"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-button
                    @click="this.dialog.save = { mode: 'add', data: { catalogId: this.catalogId } }"
                    icon="el-icon-plus"
                    type="primary"></el-button>
                <na-button-bulk-del :api="$API.sys_dic.bulkDeleteContent" :vue="this" />
                <el-dropdown v-show="this.selection.length > 0">
                    <el-button type="primary">
                        {{ $t('批量操作') }}
                        <el-icon>
                            <el-icon-arrow-down />
                        </el-icon>
                    </el-button>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item @click="setEnabled(true)">{{ $t('启用') }}</el-dropdown-item>
                            <el-dropdown-item @click="setEnabled(false)">{{ $t('禁用') }}</el-dropdown-item>
                            <el-dropdown-item @click="this.dialog.savebatch = { mode: 'batchedit', data: { catalogId: this.catalogId } }">{{
                                $t('设置项值')
                            }}</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :before-post="(data) => data.dynamicFilter.filters.length > 0"
                :context-menus="['key', 'value', 'enabled', 'createdTime']"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :export-api="$API.sys_dic.exportContent"
                :params="query"
                :query-api="$API.sys_dic.pagedQueryContent"
                :vue="this"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                ref="table"
                remote-sort
                row-key="id"
                stripe>
                <el-table-column type="selection" width="50" />
                <el-table-column :label="$t('项名')" prop="key" sortable="custom" />
                <el-table-column :label="$t('项值')" prop="value" sortable="custom" />
                <el-table-column :label="$t('描述')" prop="summary" sortable="custom" />
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="100">
                    <template #default="{ row }">
                        <el-switch v-model="row.enabled" @change="changeSwitch($event, row)"></el-switch>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom" width="170" />
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: '删除字典项',
                            click: rowDel,
                            type: 'danger',
                        })
                    "
                    :vue="this"
                    width="150" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>

    <savebatch-dialog
        v-if="dialog.savebatch"
        @closed="dialog.savebatch = null"
        @mounted="$refs.savebatchDialog.open(dialog.savebatch)"
        @success="(data, mode) => batchsuccess(data, mode)"
        ref="savebatchDialog"></savebatch-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'

const saveDialog = defineAsyncComponent(() => import('./save.vue'))
const savebatchDialog = defineAsyncComponent(() => import('./savebatch.vue'))
export default {
    components: {
        saveDialog,
        savebatchDialog,
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
            dialog: {},
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
                keywords: this.keywords,
            },
            selection: [],
        }
    },
    inject: ['reload'],
    methods: {
        async batchsuccess(data, mode) {
            if (mode === 'batchedit') {
                let loading
                try {
                    await this.$confirm(`确定修改选中的 ${this.selection.length} 项吗？`, '提示', {
                        type: 'warning',
                    })
                    loading = this.$loading()
                    const res = await Promise.all(this.selection.map((x) => this.$API.sys_dic.editContent.post(Object.assign(x, { value: data }))))
                    this.$message.success(
                        `操作成功 ${res.map((x) => (x.code === 'succeed' ? 1 : 0)).reduce((a, b) => a + b, 0)}/${this.selection.length} 条`,
                    )
                } catch {
                    //
                }
                this.$refs.table.refresh()
                loading?.close()
            }
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
        //表格内开关事件
        async changeSwitch(event, row) {
            try {
                await this.$API.sys_dic.setEnabled.post(row)
                this.$message.success(`操作成功`)
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        async setEnabled(enabled) {
            let loading
            try {
                await this.$confirm(`确定${enabled ? '启用' : '禁用'}选中的 ${this.selection.length} 项吗？`, '提示', {
                    type: 'warning',
                })
                loading = this.$loading()
                const res = await Promise.all(this.selection.map((x) => this.$API.sys_dic.setEnabled.post(Object.assign(x, { enabled: enabled }))))
                this.$message.success(`操作成功 ${res.map((x) => x.data ?? 0).reduce((a, b) => a + b, 0)}/${this.selection.length} 条`)
            } catch {
                //
            }
            this.$refs.table.refresh()
            loading?.close()
        },
        onReset() {
            Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))
        },
        //搜索
        onSearch(form) {
            this.query.dynamicFilter.filters.push({
                field: 'catalogId',
                value: this.catalogId,
                operator: 'eq',
            })

            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime,
                })
            }

            if (form.dy.keywords) {
                this.query.dynamicFilter.filters.push({
                    logic: 'or',
                    filters: [
                        {
                            field: 'key',
                            operator: 'contains',
                            value: form.dy.keywords,
                        },
                        {
                            field: 'value',
                            operator: 'contains',
                            value: form.dy.keywords,
                        },
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

        //删除
        async rowDel(row) {
            try {
                const res = await this.$API.sys_dic.deleteContent.post({ id: row.id })
                this.$message.success(this.$t('删除 {count} 项', { count: res.data }))
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
    },
    mounted() {
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keeps.push({
                field: 'keywords',
                value: this.keywords,
                type: 'root',
            })
        }
    },
    props: { catalogId: Number, keywords: String },
    watch: {
        catalogId() {
            this.$refs.search.reset()
        },
    },
}
</script>

<style scoped></style>