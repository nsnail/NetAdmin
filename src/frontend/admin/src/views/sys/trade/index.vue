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
                        title: $t('交易方向'),
                        key: 'tradeDirection',
                        options: [
                            { label: '全部', value: '' },
                            ...Object.entries(this.$GLOBAL.enums.tradeDirections).map((x) => {
                                return {
                                    value: x[0],
                                    label: x[1][1],
                                    badge: this.statistics.tradeDirection?.find((y) => y.key.tradeDirection.toLowerCase() === x[0].toLowerCase())
                                        ?.value,
                                }
                            }),
                        ],
                    },
                    {
                        title: $t('交易类型'),
                        key: 'tradeType',
                        options: [
                            { label: '全部', value: '' },
                            ...Object.entries(this.$GLOBAL.enums.tradeTypes).map((x) => {
                                return {
                                    value: x[0],
                                    label: x[1][1],
                                    badge: this.statistics.tradeType?.find((y) => y.key.tradeType.toLowerCase() === x[0].toLowerCase())?.value,
                                }
                            }),
                        ],
                    },
                ]"
                :label-width="15"
                @on-change="filterChange"
                ref="selectFilter"></scSelectFilter>
        </el-header>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'select-input',
                            field: [
                                'dy',
                                [
                                    { label: $t('交易编号'), key: 'id' },
                                    { label: $t('用户名'), key: 'owner.userName' },
                                    { label: $t('用户编号'), key: 'ownerId' },
                                ],
                            ],
                            placeholder: $t('匹配内容'),
                            style: 'width:25rem',
                            selectStyle: 'width:8rem',
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
            <div class="right-panel"></div>
        </el-header>
        <el-main class="nopadding">
            <scTable
                :context-menus="[
                    'id',
                    'ownerId',
                    'createdTime',
                    'tradeType',
                    'amount',
                    'balanceBefore',
                    'summary',
                    'owner.userName',
                    'tradeDirection',
                ]"
                :context-multi="{ id: ['createdTime'], ownerId: ['owner.userName'] }"
                :context-opers="['view']"
                :default-sort="{ prop: 'id', order: 'descending' }"
                :export-api="$API.sys_wallettrade.export"
                :params="query"
                :query-api="$API.sys_wallettrade.pagedQuery"
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
                <naColId :label="$t('交易编号')" prop="id" sortable="custom" width="170" />
                <naColUser
                    :clickOpenDialog="$GLOBAL.hasApiPermission('api/sys/user/get')"
                    :label="$t('所属用户')"
                    header-align="center"
                    nestProp="owner.userName"
                    nestProp2="ownerId"
                    prop="ownerId"
                    sortable="custom"
                    width="170"></naColUser>
                <naColIndicator
                    :label="$t('交易方向')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.tradeDirections).map((x) => {
                            return { value: x[0], text: `${x[1][1]}`, type: x[1][2], pulse: x[1][3] === 'true' }
                        })
                    "
                    align="center"
                    prop="tradeDirection"
                    sortable="custom" />
                <naColIndicator
                    :label="$t('交易类型')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.tradeTypes).map((x) => {
                            return { value: x[0], text: `${x[1][1]}`, type: x[1][2], pulse: x[1][3] === 'true' }
                        })
                    "
                    align="center"
                    prop="tradeType"
                    sortable="custom" />
                <el-table-column
                    :formatter="(row) => $TOOL.groupSeparator(row.balanceBefore)"
                    :label="$t('交易前余额')"
                    align="right"
                    prop="balanceBefore"
                    sortable="custom" />
                <el-table-column
                    :formatter="(row) => $TOOL.groupSeparator(row.amount)"
                    :label="$t('发生金额')"
                    align="right"
                    prop="amount"
                    sortable="custom" />
                <el-table-column :label="$t('交易后余额')" align="right">
                    <template #default="{ row }">
                        {{ $TOOL.groupSeparator(row.balanceBefore + row.amount) }}
                    </template>
                </el-table-column>
                <el-table-column :label="$t('备注')" min-width="100" prop="summary" show-overflow-tooltip sortable="custom" />
                <naColOperation :buttons="[naColOperation.buttons[0]]" :vue="this" width="50" />
            </scTable>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => $refs.table.refresh()"
        ref="saveDialog"></save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'
const naColUser = defineAsyncComponent(() => import('@/components/naColUser'))
const saveDialog = defineAsyncComponent(() => import('./save.vue'))
export default {
    components: {
        naColUser,
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
    async created() {
        if (this.ownerId) {
            this.query.dynamicFilter.filters.push({ field: 'ownerId', operator: 'eq', value: this.ownerId })
        }
    },
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
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },

        async getStatistics() {
            this.statistics.total = this.$refs.table?.total
            const res = await Promise.all([
                this.$API.sys_wallettrade.countBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['TradeDirection'],
                }),
                this.$API.sys_wallettrade.countBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['TradeType'],
                }),
            ])
            this.statistics.tradeDirection = res[0].data
            this.statistics.tradeType = res[1].data
        },
        //重置
        onReset() {
            Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))
            if (this.ownerId) {
                this.$refs.search.selectInputKey = 'ownerId'
            }
        },
        //搜索
        async onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime.map((x) => x.replace(/ 00:00:00$/, '')),
                })
            }
            if (typeof form.dy['owner.userName'] === 'string' && form.dy['owner.userName'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'owner.userName',
                    operator: 'eq',
                    value: form.dy['owner.userName'],
                })
            }
            if (typeof form.dy['ownerId'] === 'string' && form.dy['ownerId'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'ownerId',
                    operator: 'eq',
                    value: form.dy['ownerId'],
                })
            }

            if (typeof form.dy['id'] === 'string' && form.dy['id'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'id',
                    operator: 'eq',
                    value: form.dy['id'],
                })
            }

            if (typeof form.dy['tradeType'] === 'string' && form.dy['tradeType'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'tradeType',
                    operator: 'eq',
                    value: form.dy['tradeType'],
                })
            }

            if (typeof form.dy['tradeDirection'] === 'string' && form.dy['tradeDirection'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'tradeDirection',
                    operator: 'eq',
                    value: form.dy['tradeDirection'],
                })
            }
            await this.$refs.table.upData()
        },
    },
    async mounted() {
        if (this.ownerId) {
            this.$refs.search.selectInputKey = 'ownerId'
            this.$refs.search.form.dy.ownerId = this.ownerId
            this.$refs.search.keeps.push({
                field: 'ownerId',
                value: this.ownerId,
                type: 'dy',
            })
        }

        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keeps.push({
                field: 'keywords',
                value: this.keywords,
                type: 'root',
            })
        }

        this.onReset()
    },
    props: ['keywords', 'ownerId'],
    watch: {},
}
</script>

<style scoped></style>