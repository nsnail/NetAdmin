<template>
    <el-container>
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
                    @search="onSearch"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-button
                    @click="this.dialog.save = { mode: 'add', data: { catalogId: this.catalogId } }"
                    icon="el-icon-plus"
                    type="primary"></el-button>
                <na-button-bulk-del :api="$API.sys_dic.bulkDeleteContent" :vue="this" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :before-post="(data) => data.dynamicFilter.filters.length > 0"
                :context-menus="['key', 'value', 'createdTime']"
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
                <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom" />
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
    created() {
        if (this.keywords) {
            this.query.keywords = this.keywords
        }
    },
    data() {
        return {
            dialog: {},
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