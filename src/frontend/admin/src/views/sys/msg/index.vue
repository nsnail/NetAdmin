<template>
    <el-container>
        <el-header v-loading="total === '...'" style="height: auto; padding: 1rem 1rem 0 1rem; display: block">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <sc-statistic :value="total" group-separator title="总数"></sc-statistic>
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
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
                :label-width="10"
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
                            style: 'width:25rem',
                        },
                    ]"
                    :vue="this"
                    @reset="Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))"
                    @search="onSearch"
                    dateFormat="YYYY-MM-DD HH:mm:ss"
                    dateType="datetimerange"
                    dateValueFormat="YYYY-MM-DD HH:mm:ss"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-button @click="(this.dialog.save = { mode: 'add' })" icon="el-icon-plus" type="primary"></el-button>
                <na-button-bulk-del :api="$API.sys_sitemsg.bulkDelete" :vue="this" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :context-menus="['id', 'createdUserName', 'msgType', 'title', 'summary', 'createdTime']"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :export-api="$API.sys_sitemsg.export"
                :on-command="this.getStatistics"
                :params="query"
                :query-api="$API.sys_sitemsg.pagedQuery"
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
                <na-col-id :label="$t('消息编号')" prop="id" sortable="custom" width="170" />
                <na-col-avatar :label="$t('用户名')" prop="createdUserName" />
                <na-col-indicator
                    :label="$t('消息类型')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.siteMsgTypes).map((x) => {
                            return { value: x[0], text: x[1][1], type: x[1][2] }
                        })
                    "
                    align="center"
                    prop="msgType"
                    sortable="custom"
                    width="150" />
                <el-table-column :label="$t('消息主题')" prop="title" show-overflow-tooltip sortable="custom" />
                <el-table-column :label="$t('消息摘要')" prop="summary" show-overflow-tooltip sortable="custom" />

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
                    :vue="this"
                    width="150" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="(dialog.save = null)"
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
            total: '...',
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
            const res = await this.$API.sys_sitemsg.count.post(this.query)
            this.total = res.data
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
        async rowDel(row) {
            try {
                const res = await this.$API.sys_sitemsg.delete.post({ id: row.id })
                this.$message.success(this.$t('删除 {count} 项', { count: res.data }))
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        async onSearch(form) {
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
            await this.getStatistics()
        },
    },
    async mounted() {
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keeps.push({
                field: 'keywords',
                value: this.keywords,
                type: 'root',
            })
        }
        await this.getStatistics()
    },
    props: ['keywords'],
    watch: {},
}
</script>

<style scoped></style>