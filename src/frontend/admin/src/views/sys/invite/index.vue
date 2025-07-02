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
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'select-input',
                            field: [
                                'dy',
                                [
                                    { label: $t('用户编号'), key: 'id' },
                                    { label: $t('用户名'), key: 'user.userName' },
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
                    dateType="datetime-range"
                    dateValueFormat="YYYY-MM-DD HH:mm:ss"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-dropdown v-show="this.selection.length > 0">
                    <el-button type="primary">
                        {{ $t('批量操作') }}
                        <el-icon>
                            <el-icon-arrow-down />
                        </el-icon>
                    </el-button>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item @click="setCommissionRatio">设置返佣比率</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :context-menus="['id', 'user.userName', 'createdTime', 'commissionRatio']"
                :context-opers="[]"
                :default-sort="{ prop: 'sort', order: 'descending' }"
                :params="query"
                :query-api="$API.sys_userinvite.query"
                :vue="this"
                @data-change="getStatistics"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                default-expand-all
                hidePagination
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <el-table-column type="selection" width="50" />
                <el-table-column :label="$t('用户编号')" prop="id" sortable="custom" />
                <na-col-avatar :label="$t('用户名')" prop="user.userName" />
                <el-table-column
                    :formatter="(row) => `${(row.commissionRatio / 100).toFixed(2)}%`"
                    :label="$t('返佣比率')"
                    align="right"
                    prop="commissionRatio"
                    sortable="custom" />
                <el-table-column :label="$t('注册时间')" align="right" prop="createdTime" sortable="custom" />
            </sc-table>
        </el-main>
    </el-container>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/na-col-operation'

const naColAvatar = defineAsyncComponent(() => import('@/components/na-col-avatar'))
export default {
    components: {
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
        async setCommissionRatio() {
            let loading
            try {
                const prompt = await this.$prompt(this.$t('1 代表 0.01%'), this.$t('设置返佣比率'), {
                    inputPattern: /^[0-9]\d*$/,
                    inputErrorMessage: this.$t('返佣比率不正确'),
                })
                loading = this.$loading()
                const res = await Promise.all(
                    this.selection.map((x) => this.$API.sys_userinvite.setCommissionRatio.post(Object.assign(x, { commissionRatio: prompt.value }))),
                )
                this.$message.success(
                    this.$t(`操作成功 {count}/{total} 项`, {
                        count: this.selection.length,
                        total: res.map((x) => x.data ?? 0).reduce((a, b) => a + b, 0),
                    }),
                )
                this.$refs.table.refresh()
            } catch {
                //
            }
            loading?.close()
        },
        async getStatistics() {
            this.statistics.total = this.$refs.table?.tableData?.length
        },
        //重置
        onReset() {},
        //搜索
        async onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime.map((x) => x.replace(/ 00:00:00$/, '')),
                })
            }

            if (typeof form.dy['id'] === 'string' && form.dy['id'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'id',
                    operator: 'eq',
                    value: form.dy['id'],
                })
            }

            if (typeof form.dy['user.userName'] === 'string' && form.dy['user.userName'].trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'user.userName',
                    operator: 'eq',
                    value: form.dy['user.userName'],
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

        this.onReset()
    },
    props: ['keywords'],
    watch: {},
}
</script>

<style scoped />