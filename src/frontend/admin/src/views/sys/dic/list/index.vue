<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <na-search
                    ref="search"
                    :controls="[
                        {
                            type: 'input',
                            field: ['dy', 'keywords'],
                            placeholder: '项名 / 项值',
                        },
                    ]"
                    :vue="this"
                    @search="onSearch" />
            </div>
            <div class="right-panel">
                <na-button-add :data="{ catalogId: this.catalogId }" :vue="this" />
                <na-button-batch-del :api="$API.sys_dic.bulkDeleteContent" :vue="this" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                ref="table"
                :apiObj="$API.sys_dic.pagedQueryContent"
                :before-post="(data) => data.dynamicFilter.filters.length > 0"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :params="query"
                remote-sort
                row-key="id"
                stripe
                @selection-change="
                    (items) => {
                        selection = items
                    }
                ">
                <el-table-column type="selection" width="50"></el-table-column>
                <el-table-column label="项名" prop="key" sortable="custom"></el-table-column>
                <el-table-column label="项值" prop="value" sortable="custom"></el-table-column>
                <el-table-column label="创建时间" prop="createdTime" sortable="custom"></el-table-column>
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: '删除字典项',
                            click: rowDel,
                        })
                    "
                    :vue="this"></na-col-operation>
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
    computed: {
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    components: { saveDialog },
    props: { catalogId: Number },
    data() {
        return {
            dialog: {
                save: false,
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
    watch: {
        catalogId() {
            this.$refs.search.reset()
        },
    },
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
                this.$refs.table.refresh()
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
        },
    },
}
</script>