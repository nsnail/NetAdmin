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
                    dateType="datetimerange"
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
                        <el-dropdown-menu> </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable
                :context-menus="['id', 'user.userName', 'createdTime']"
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
                <naColAvatar :label="$t('用户名')" prop="user.userName" />
                <el-table-column :label="$t('注册时间')" prop="createdTime" sortable="custom" />
            </scTable>
        </el-main>
    </el-container>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'

const naColAvatar = defineAsyncComponent(() => import('@/components/naColAvatar'))
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

<style scoped></style>