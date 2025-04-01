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
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('姓名 / 爱好 / 种子'),
                            style: 'width:25rem',
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
                <el-button @click="this.dialog.save = { mode: 'add' }" icon="el-icon-plus" type="primary"></el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable
                :context-menus="['id', 'age', 'height', 'interest', 'seed','createdTime']"
                :context-opers="['view']"
                :default-sort="{ prop: 'id', order: 'descending' }"
                :params="query"
                :query-api="$API.adm_girlfriends.pagedQuery"
                :vue="this"
                @data-change="dataChange"
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <naColId :label="$t('编号')" prop="id" sortable="custom" width="170" />
                <el-table-column :label="$t('姓名')" prop="name" sortable="custom" width="150" />
                <el-table-column :label="$t('年龄')" prop="age" sortable="custom" width="150" />
                <el-table-column :label="$t('身高')" prop="height" sortable="custom" width="150" />
                <el-table-column :label="$t('爱好')" prop="interest" sortable="custom" width="240" />
                <el-table-column :label="$t('种子')" prop="seed" sortable="custom" min-width="150" />
                <naColOperation
                    :buttons="naColOperation.buttons.concat(naColOperation.delButton(this.$t('删除女朋友'), $API.adm_girlfriends.delete))"
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
import naInfo from '@/components/naInfo'
import http from '@/utils/request'
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
            statistics: {
                total: '...',
            },
            dialog: {
            },
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
                this.$API.adm_girlfriends.countBy.post({
                    dynamicFilter: {
                        filters: this.query.dynamicFilter.filters,
                    },
                    requiredFields: ['Id'],
                })
            ])
        },
        async dataChange(data) {
            await this.getStatistics()
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
        onReset() {
            if (!this.showFilter) return
            Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))
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
            if (typeof form.dy.loginResult === 'boolean') {
                this.query.dynamicFilter.filters.push(
                    form.dy.loginResult
                        ? {
                            field: 'httpStatusCode',
                            operator: 'range',
                            value: '200,299',
                        }
                        : {
                            field: 'httpStatusCode',
                            operator: 'range',
                            value: '300,999',
                        },
                )
            }

            if (typeof form.dy.errorCode === 'string' && form.dy.errorCode.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'errorCode',
                    operator: 'eq',
                    value: form.dy.errorCode,
                })
            }

            if (typeof form.dy.loginUserName === 'string' && form.dy.loginUserName.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'loginUserName',
                    operator: 'eq',
                    value: form.dy.loginUserName,
                })
            }

            if (typeof form.dy.createdClientIp === 'string' && form.dy.createdClientIp.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'createdClientIp',
                    operator: 'eq',
                    value: form.dy.createdClientIp,
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
    props: { keywords: { type: String }, showFilter: { type: Boolean, default: true } },
    watch: {},
}
</script>

<style scoped></style>