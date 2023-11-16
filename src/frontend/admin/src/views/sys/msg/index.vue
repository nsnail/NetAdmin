<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('消息编号 / 消息主题 / 消息内容'),
                            style: 'width:25rem',
                        },
                        {
                            type: 'select',
                            field: ['dy', 'msgType'],
                            options: Object.entries(this.$GLOBAL.enums.siteMsgTypes).map((x) => {
                                return { value: x[0], label: x[1][1] }
                            }),
                            placeholder: $t('消息类型'),
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
                <el-table-column prop="id" :label="$t('消息编号')" width="150" />
                <na-col-avatar :label="$t('用户名')" prop="creator.userName" />
                <na-col-indicator
                    width="100"
                    :options="[
                        { text: '私信', type: 'success', value: 'private' },
                        { text: '公告', type: 'warning', value: 'public' },
                    ]"
                    :label="$t('消息类型')"
                    prop="msgType"></na-col-indicator>

                <el-table-column prop="title" :label="$t('消息主题')" min-width="150" show-overflow-tooltip />
                <el-table-column prop="summary" show-overflow-tooltip :label="$t('消息摘要')" min-width="200" />
                <el-table-column prop="createdTime" :label="$t('创建时间')" />
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: $t('删除消息'),
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