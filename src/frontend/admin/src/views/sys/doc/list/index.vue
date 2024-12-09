<template>
    <el-container>
        <el-header v-loading="statistics.total === '...'" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <sc-statistic :value="statistics.total" group-separator title="总数"></sc-statistic>
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
        <el-header class="el-header-select-filter">
            <sc-select-filter
                :data="[
                    {
                        title: $t('启用状态'),
                        key: 'enabled',
                        options: [
                            { label: $t('全部'), value: '' },
                            { label: $t('启用'), value: true, badge: statistics.enabled?.find((x) => x.key.enabled === 'True')?.value },
                            { label: $t('禁用'), value: false, badge: statistics.enabled?.find((x) => x.key.enabled === 'False')?.value },
                        ],
                    },
                    {
                        title: $t('档案可见性'),
                        key: 'visibility',
                        options: [
                            { label: $t('全部'), value: '' },
                            ...Object.entries(this.$GLOBAL.enums.archiveVisibilities).map((x) => {
                                return {
                                    value: x[0],
                                    label: x[1][1],
                                    badge: this.statistics.visibility?.find((y) => y.key.visibility.toLowerCase() === x[0].toLowerCase())?.value,
                                }
                            }),
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
                            placeholder: $t('文档标题'),
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
                <na-button-bulk-del :api="$API.sys_doc.bulkDeleteContent" :vue="this" />
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
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :before-post="(data) => data.dynamicFilter.filters.length > 0"
                :context-menus="['title', 'enabled', 'createdTime', 'id', 'visibility']"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :export-api="$API.sys_doc.exportContent"
                :params="query"
                :query-api="$API.sys_doc.pagedQueryContent"
                :vue="this"
                @data-change="getStatistics"
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
                <na-col-id :label="$t('唯一编码')" prop="id" sortable="custom" width="170" />
                <el-table-column :label="$t('文档标题')" min-width="200" prop="title" sortable="custom" />
                <na-col-indicator
                    :label="$t('档案可见性')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.archiveVisibilities).map((x) => {
                            return { value: x[0], text: x[1][1], type: x[1][2] }
                        })
                    "
                    align="center"
                    prop="visibility"
                    sortable="custom"
                    width="150" />
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="100">
                    <template #default="{ row }">
                        <el-switch v-model="row.enabled" @change="changeSwitch($event, row)"></el-switch>
                    </template>
                </el-table-column>
                <na-col-operation
                    :buttons="[
                        {
                            icon: 'el-icon-view',
                            title: '查看',
                            click: viewClick,
                        },
                        {
                            icon: 'el-icon-edit',
                            title: '编辑',
                            click: async (row, vue) => {
                                vue.dialog.save = { row, mode: 'edit' }
                            },
                        },
                        {
                            icon: 'el-icon-share',
                            click: share,
                            title: '分享文档',
                        },
                        {
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: '删除文档',
                            click: this.rowDel,
                            type: 'danger',
                        },
                    ]"
                    :vue="this"
                    width="180" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'

const saveDialog = defineAsyncComponent(() => import('./save.vue'))
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
            statistics: {
                total: '...',
            },
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
        async getStatistics() {
            this.statistics.total = this.$refs.table?.total
            const res = await Promise.all([
                this.$API.sys_doc.contentCountBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['Enabled'],
                }),
                this.$API.sys_doc.contentCountBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['Visibility'],
                }),
            ])
            this.statistics.enabled = res[0].data
            this.statistics.visibility = res[1].data
        },
        viewClick(row) {
            window.open(window.location.origin + `/guest/view-doc/${row.id}`)
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
                await this.$API.sys_doc.setEnabled.post(row)
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
                const res = await Promise.all(this.selection.map((x) => this.$API.sys_doc.setEnabled.post(Object.assign(x, { enabled: enabled }))))
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
        async onSearch(form) {
            this.query.dynamicFilter.filters.push({
                field: 'catalogId',
                value: this.catalogId,
                operator: 'eq',
            })

            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime.map((x) => x.replace(/ 00:00:00$/, '')),
                })
            }

            if (typeof form.dy.enabled === 'boolean') {
                this.query.dynamicFilter.filters.push({
                    field: 'enabled',
                    operator: 'eq',
                    value: form.dy.enabled,
                })
            }

            if (typeof form.dy.visibility === 'string' && form.dy.visibility.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'visibility',
                    operator: 'eq',
                    value: form.dy.visibility,
                })
            }

            await this.$refs.table.upData()
        },
        async share(row) {
            const textarea = document.createElement('textarea')
            textarea.readOnly = 'readonly'
            textarea.style.position = 'absolute'
            textarea.style.left = '-9999px'
            textarea.value = window.location.origin + `/guest/view-doc/${row.id}`
            document.body.appendChild(textarea)
            textarea.select()
            textarea.setSelectionRange(0, textarea.value.length)
            const result = document.execCommand('Copy')
            if (result) {
                this.$message.success(this.$t('文档链接已复制'))
            }
            document.body.removeChild(textarea)
        },
        //删除
        async rowDel(row) {
            try {
                const res = await this.$API.sys_doc.deleteContent.post({ id: row.id })
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