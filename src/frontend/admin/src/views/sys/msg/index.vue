<template>
    <el-container>
        <el-header v-loading="statistics.total === '...'" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <scStatistic :title="$t('总数')" :value="statistics.total" group-separator></scStatistic>
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
        <el-header class="el-header-select-filter">
            <scSelectFilter
                :data="[
                    {
                        title: $t('消息类型'),
                        key: 'msgType',
                        options: [
                            { label: $t('全部'), value: '' },
                            ...Object.entries(this.$GLOBAL.enums.siteMsgTypes).map((x) => {
                                return {
                                    value: x[0],
                                    label: x[1][1],
                                    badge: this.statistics.msgType?.find((y) => y.key.msgType.toLowerCase() === x[0].toLowerCase())?.value,
                                }
                            }),
                        ],
                    },
                ]"
                :label-width="10"
                @on-change="filterChange"
                ref="selectFilter"></scSelectFilter>
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
                <el-button @click="this.dialog.save = { mode: 'add' }" icon="el-icon-plus" type="primary"></el-button>
                <naButtonBulkDel :api="$API.sys_sitemsg.bulkDelete" :vue="this" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable
                :context-menus="['id', 'createdUserName', 'msgType', 'title', 'summary', 'createdTime']"
                :context-multi="{ id: ['createdTime'] }"
                :default-sort="{ prop: 'id', order: 'descending' }"
                :export-api="$API.sys_sitemsg.export"
                :params="query"
                :query-api="$API.sys_sitemsg.pagedQuery"
                :vue="this"
                @data-change="getStatistics"
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
                <el-table-column type="selection" width="50" />
                <naColId :label="$t('消息编号')" prop="id" sortable="custom" width="170" />
                <naColAvatar :label="$t('用户名')" min-width="100" prop="createdUserName" />
                <naColIndicator
                    :label="$t('消息类型')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.siteMsgTypes).map((x) => {
                            return { value: x[0], text: x[1][1], type: x[1][2], pulse: x[1][3] === 'true' }
                        })
                    "
                    align="center"
                    prop="msgType"
                    sortable="custom"
                    width="150" />
                <el-table-column :label="$t('消息主题')" min-width="200" prop="title" show-overflow-tooltip sortable="custom" />
                <el-table-column :label="$t('消息摘要')" min-width="400" prop="summary" show-overflow-tooltip sortable="custom" />

                <naColOperation
                    :buttons="naColOperation.buttons.concat(naColOperation.delButton($t('删除消息'), $API.sys_sitemsg.delete))"
                    :vue="this"
                    width="120" />
            </scTable>
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
const naColAvatar = defineAsyncComponent(() => import('@/components/naColAvatar'))
const saveDialog = defineAsyncComponent(() => import('./save.vue'))
export default {
    components: {
        saveDialog,
        naColAvatar,
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
                this.$API.sys_sitemsg.countBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['MsgType'],
                }),
            ])

            this.statistics.msgType = res[0].data
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
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

            await this.$refs.table.upData()
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
    },
    props: ['keywords'],
    watch: {},
}
</script>

<style scoped></style>