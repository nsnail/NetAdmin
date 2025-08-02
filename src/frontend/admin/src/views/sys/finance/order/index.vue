<template>
    <el-container>
        <el-header v-loading="statistics.total === '...'" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <sc-statistic :title="$t('总数')" :value="statistics.total" group-separator />
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
        <el-header class="el-header-select-filter">
            <sc-select-filter
                :data="[
                    {
                        title: $t('订单状态'),
                        key: 'depositOrderStatus',
                        options: [
                            { label: '全部', value: '' },
                            ...Object.entries(this.$GLOBAL.enums.depositOrderStatues).map((x) => {
                                return {
                                    value: x[0],
                                    label: x[1][1],
                                    badge: this.statistics.depositOrderStatus?.find(
                                        (y) => y.key.depositOrderStatus.toLowerCase() === x[0].toLowerCase(),
                                    )?.value,
                                }
                            }),
                        ],
                    },
                    {
                        title: $t('支付方式'),
                        key: 'paymentMode',
                        options: [
                            { label: '全部', value: '' },
                            ...Object.entries(this.$GLOBAL.enums.paymentModes).map((x) => {
                                return {
                                    value: x[0],
                                    label: x[1][1],
                                    badge: this.statistics.paymentMode?.find((y) => y.key.paymentMode.toLowerCase() === x[0].toLowerCase())?.value,
                                }
                            }),
                        ],
                    },
                ]"
                :label-width="15"
                @on-change="filterChange"
                ref="selectFilter" />
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
                                    { label: $t('订单编号'), key: 'id' },
                                    { label: $t('用户名'), key: 'owner.userName' },
                                    { label: $t('用户编号'), key: 'ownerId' },
                                    { label: $t('充值点数'), key: 'depositPoint' },
                                ],
                            ],
                            placeholder: $t('检索内容'),
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
            <div class="right-panel">
                <el-button @click="this.dialog.save = { mode: 'add' }" icon="el-icon-plus" type="primary" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :context-extra="{ id: ['createdTime'], ownerId: ['owner.userName'] }"
                :context-menus="[
                    'id',
                    'ownerId',
                    'createdTime',
                    'paymentMode',
                    'amount',
                    'depositPoint',
                    'toPointRate',
                    'owner.userName',
                    'depositOrderStatus',
                    'actualPayAmount',
                ]"
                :context-opers="['view']"
                :default-sort="{ prop: 'id', order: 'descending' }"
                :export-api="$API.sys_depositorder.export"
                :params="query"
                :query-api="$API.sys_depositorder.pagedQuery"
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
                <na-col-id :label="$t('订单编号')" prop="id" sortable="custom" width="170" />
                <na-col-user
                    :label="$t('归属用户')"
                    :showUserDialog="$GLOBAL.hasApiPermission('api/sys/user/get')"
                    header-align="center"
                    nestProp="owner"
                    prop="ownerId"
                    sortable="custom"
                    width="170" />
                <na-col-indicator
                    :label="$t('订单状态')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.depositOrderStatues).map((x) => {
                            return { value: x[0], text: `${x[1][1]}`, type: x[1][2], pulse: x[1][3] === 'true' }
                        })
                    "
                    align="center"
                    prop="depositOrderStatus"
                    sortable="custom" />
                <el-table-column
                    :formatter="(row) => $TOOL.groupSeparator(row.depositPoint)"
                    :label="$t('充值点数')"
                    align="right"
                    prop="depositPoint"
                    sortable="custom" />
                <el-table-column
                    :formatter="(row) => $TOOL.groupSeparator(row.toPointRate)"
                    :label="$t('兑换比率')"
                    align="right"
                    prop="toPointRate"
                    sortable="custom" />
                <el-table-column
                    :formatter="(row) => $TOOL.groupSeparator(row.actualPayAmount / 1000)"
                    :label="$t('支付金额')"
                    align="right"
                    prop="actualPayAmount"
                    sortable="custom" />
                <na-col-indicator
                    :label="$t('支付方式')"
                    :options="
                        Object.entries(this.$GLOBAL.enums.paymentModes).map((x) => {
                            return { value: x[0], text: `${x[1][1]}`, type: x[1][2], pulse: x[1][3] === 'true' }
                        })
                    "
                    align="center"
                    prop="paymentMode"
                    sortable="custom" />
                <na-col-operation
                    :buttons="[
                        naColOperation.buttons[0],
                        {
                            icon: 'el-icon-credit-card',
                            title: '支付',
                            click: async (row, vue) => {
                                vue.dialog.save = { row, mode: 'pay' }
                            },
                            condition: (row) => {
                                return row.depositOrderStatus === 'waitingForPayment'
                            },
                        },
                    ]"
                    :vue="this"
                    width="120" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog" />
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/na-col-operation'
const naColUser = defineAsyncComponent(() => import('@/components/na-col-user'))
const saveDialog = defineAsyncComponent(() => import('./save'))
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
                this.$API.sys_depositorder.countBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['PaymentMode'],
                }),
                this.$API.sys_depositorder.countBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['DepositOrderStatus'],
                }),
            ])
            this.statistics.paymentMode = res[0].data
            this.statistics.depositOrderStatus = res[1].data
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

            if (typeof form.dy['paymentMode'] === 'string' && form.dy['paymentMode'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'paymentMode',
                    operator: 'eq',
                    value: form.dy['paymentMode'],
                })
            }

            if (typeof form.dy['depositOrderStatus'] === 'string' && form.dy['depositOrderStatus'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'depositOrderStatus',
                    operator: 'eq',
                    value: form.dy['depositOrderStatus'],
                })
            }

            if (typeof form.dy['depositPoint'] === 'string' && form.dy['depositPoint'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'depositPoint',
                    operator: 'eq',
                    value: form.dy['depositPoint'],
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

<style scoped />