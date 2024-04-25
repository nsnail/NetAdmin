<template>
    <el-container>
        <el-header style="height: auto; padding: 0 1rem">
            <sc-select-filter
                :data="[
                    {
                        title: $t('消息类型'),
                        key: 'msgType',
                        options: [
                            { label: $t('全部'), value: '' },
                            ...Object.entries(this.$GLOBAL.enums.siteMsgTypes).map((x) => {
                                return { value: x[0], label: x[1][1] }
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
                            placeholder: $t('消息编号 / 消息主题 / 消息内容'),
                            style: 'width:20rem',
                        },
                    ]"
                    :vue="this"
                    @reset="Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))"
                    @search="onSearch"
                    ref="search" />
            </div>
            <div class="right-panel">
                <na-button-add :vue="this" />
                <el-button :disabled="selection.length === 0" @click="batchDel" icon="el-icon-delete" plain type="danger"></el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                v-loading="loading"
                :apiObj="$API.sys_sitemsg.pagedQuery"
                :context-menus="['id', 'createdUserName', 'msgType', 'title', 'summary', 'createdTime']"
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
                <el-table-column :label="$t('消息编号')" prop="id" sortable="custom" width="150" />
                <na-col-avatar :label="$t('用户名')" prop="createdUserName" />
                <na-col-indicator
                    :label="$t('消息类型')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.siteMsgTypes).map((x) => {
                            return { value: x[0], text: x[1][1] }
                        })
                    "
                    prop="msgType"
                    width="100">
                </na-col-indicator>
                <el-table-column :label="$t('消息主题')" prop="title" show-overflow-tooltip sortable="custom" />
                <el-table-column :label="$t('消息摘要')" prop="summary" show-overflow-tooltip sortable="custom" />
                <el-table-column :label="$t('创建时间')" align="right" prop="createdTime" sortable="custom" width="170" />
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: $t('删除消息'),
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
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'

export default {
    components: {
        saveDialog,
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
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value
            })
            this.$refs.search.search()
        },
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

        //搜索
        onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime,
                })
            }

            if (typeof form.dy.msgType === 'string' && form.dy.msgType.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'msgType',
                    operator: 'eq',
                    value: form.dy.msgType,
                })
            }

            this.$refs.table.upData()
        },
    },
}
</script>

<style scoped></style>