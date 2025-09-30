<template>
    <el-container>
        <!--        仪表板-->
        <el-header v-loading="statistics.total === `...`" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :lg="Math.floor(Object.keys(statistics.sumList).length === 0 ? 24 : 24 / (Object.keys(statistics.sumList).length + 1))">
                    <el-card shadow="never">
                        <sc-statistic :title="this.$t(totalCountLabel)" :value="statistics.total" group-separator />
                    </el-card>
                </el-col>
                <el-col
                    v-for="(item, i) in Object.entries(statistics.sumList).sort((x, y) => x[1][3] - y[1][3])"
                    :key="i"
                    :lg="Math.floor(24 / (Object.keys(statistics.sumList).length + 1)) + (item[1][2] ?? 0)">
                    <el-card shadow="never">
                        <sc-statistic :prefix="item[1][1]" :suffix="item[1][4]" :title="item[0]" :value="item[1][0]" group-separator />
                    </el-card>
                </el-col>
            </el-row>
        </el-header>

        <!--        过滤器-->
        <el-header v-if="selectFilterData.length > 0" class="el-header-select-filter">
            <sc-select-filter :data="selectFilterData" :label-width="10" @on-change="onFilterChange" ref="selectFilter" />
        </el-header>

        <!--        搜索栏-->
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="searchControls"
                    :show-search-mine="showSearchMine"
                    :vue="this"
                    @reset="onReset"
                    @search="onSearch"
                    @search-mine="onSearchMine"
                    dateFormat="YYYY-MM-DD HH:mm:ss"
                    dateType="datetimerange"
                    dateValueFormat="YYYY-MM-DD HH:mm:ss"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-button
                    v-if="operations.includes(`add`)"
                    :disabled="loading"
                    :loading="loading"
                    @click="onAddClick(operations.find((x) => x.add)?.add)"
                    icon="el-icon-plus"
                    type="primary">
                    {{ operations.find((x) => x.add)?.add.label }}
                </el-button>
                <el-button
                    v-if="operations.includes(`del`)"
                    :disabled="this.table.selection.length === 0 || this.loading"
                    @click="onBulkDeleteClick"
                    icon="el-icon-delete"
                    plain
                    type="danger" />
                <el-dropdown v-if="rightButtons.length > 1">
                    <el-button>{{ $t(`更多操作...`) }}</el-button>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <template v-for="(button, i) in rightButtons" :key="i">
                                <el-dropdown-item v-bind="button.props" @click="button.click">{{ button.label }}</el-dropdown-item>
                            </template>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
                <el-button
                    v-bind="rightButtons[0].props"
                    v-else-if="rightButtons.length === 1"
                    :disabled="loading"
                    :loading="loading"
                    @click="
                        async () => {
                            this.loading = true
                            await rightButtons[0].click()
                            this.loading = false
                        }
                    "
                    >{{ rightButtons[0].label }}</el-button
                >
            </div>
        </el-header>

        <!--        表格主体-->
        <el-main class="nopadding">
            <sc-table
                v-bind="
                    Object.assign(
                        {
                            queryApi: $API[entityName].pagedQuery,
                            exportApi: $GLOBAL.hasApiPermission($API[entityName].export?.url.replace(`${config.API_URL}/`, ``))
                                ? $API[entityName].export
                                : null,
                            remoteFilter: true,
                            remoteSort: true,
                            rowKey: `id`,
                            stripe: true,
                            pageSize: 20,
                            defaultSort: { prop: `id`, order: `descending` },
                        },
                        tableProps,
                    )
                "
                :context-extra="this.table.menu.extra"
                :context-menus="
                    Object.entries(this.columns)
                        .filter((x) => x[1].showInContextMenu ?? true)
                        .map((x) => x[0])
                "
                :context-opers="this.operations"
                :params="query"
                :vue="this"
                @data-change="onDataChange"
                @selection-change="onSelectionChange"
                ref="table">
                <el-table-column v-if="showSelection" :reserve-selection="true" type="selection" width="35" />
                <template v-for="(item, i) in columns" :key="i">
                    <component
                        v-bind="item"
                        v-if="item.show.includes(`list`) && (!item.condition || item.condition())"
                        :is="item.is ?? `el-table-column`"
                        :options="
                            item.options ??
                            (item.enum
                                ? Object.entries(this.$GLOBAL.enums[item.enum.name]).map((x) => {
                                      return {
                                          value: x[0],
                                          text: item.enum.text ? item.enum.text(x) : x[1][1],
                                          type: x[1][2],
                                          pulse: x[1][3] === `true`,
                                      }
                                  })
                                : null)
                        "
                        :prop="item.prop ?? i"
                        :sortable="item.sortable ?? `custom`">
                        <template v-if="item.isSwitch" #default="{ row }">
                            <el-switch
                                v-model="row[i]"
                                :disabled="
                                    !item.isSwitch.onChange ||
                                    !$GLOBAL.hasApiPermission($API[entityName][item.isSwitch.onChange]?.url.replace(`${config.API_URL}/`, ``))
                                "
                                @change="onSwitchChange(row, item.isSwitch.onChange)"></el-switch>
                        </template>
                        <template v-else-if="item.relativeTime" #default="{ row }">
                            <span v-time.tip="row[i]">
                                {{ row[i] }}
                            </span>
                        </template>
                        <template v-else-if="item.currency" #default="{ row }">
                            <p :class="{ expense: row[i] < 0 }">
                                <span v-html="item.currency.flag"></span>
                                <span>{{
                                    $TOOL.groupSeparator((row[i] / (item.currency.multiple ?? 100)).toFixed(item.currency.decimal ?? 2))
                                }}</span>
                            </p>
                        </template>
                    </component>
                </template>

                <el-table-column :label="$t(`操作`)" align="right" fixed="right" width="120">
                    <template #default="{ row }">
                        <div class="flex justify-content-right">
                            <el-button-group size="small">
                                <el-button v-if="operations.includes(`view`)" @click="onViewClick(row)" icon="el-icon-view" />
                                <el-button v-if="operations.includes(`edit`)" @click="onEditClick(row)" icon="el-icon-edit" />
                                <el-button v-if="operations.includes(`del`)" @click="onDeleteClick(row)" icon="el-icon-delete" type="danger" />
                            </el-button-group>
                            <el-dropdown v-if="rowButtons?.filter((x) => !x.condition || x.condition(row)).length > 0">
                                <el-button size="small">...</el-button>
                                <template #dropdown>
                                    <el-dropdown-menu>
                                        <template v-for="(button, j) in rowButtons?.filter((x) => !x.condition || x.condition(row))" :key="j">
                                            <el-dropdown-item v-bind="button.props" @click="onRowButtonClick(row, button)" size="small">{{
                                                button.label
                                            }}</el-dropdown-item>
                                        </template>
                                    </el-dropdown-menu>
                                </template>
                            </el-dropdown>
                        </div>
                    </template>
                </el-table-column>
            </sc-table>
        </el-main>
    </el-container>

    <detail-dialog
        v-bind="$props"
        v-if="dialog.detail"
        @closed="dialog.detail = null"
        @mounted="$refs.detailDialog.open(dialog.detail)"
        @success="(data, mode) => tableConfig.handleUpdate($refs.table, data, mode)"
        @tabSuccess="() => $refs.table.refresh()"
        ref="detailDialog" />
</template>

<script>
import { h } from 'vue'
import { defineAsyncComponent } from 'vue'
import tableConfig from '@/config/table'
import naColOperation from '@/config/na-col-operation'
import config from '@/config'
import naColTags from '@/components/na-col-tags'
const naColAvatar = defineAsyncComponent(() => import('@/components/na-col-avatar'))
const detailDialog = defineAsyncComponent(() => import('./detail'))
const naColUser = defineAsyncComponent(() => import('@/components/na-col-user'))
export default {
    components: {
        detailDialog,
        naColAvatar,
        naColUser,
        naColTags,
    },
    computed: {
        config() {
            return config
        },
        naColOperation() {
            return naColOperation
        },
        tableConfig() {
            return tableConfig
        },
    },
    async created() {
        for (const f of this.dyFilters) {
            if (f.operator === 'dateRange') {
                this.query.dynamicFilter.filters.push({
                    field: f.field,
                    operator: f.operator,
                    value: f.value.map((x) => x.replace(/ 00:00:00$/, '')),
                })
            } else this.query.dynamicFilter.filters.push(f)
        }

        const searchFields = []
        this.searchControls = []
        for (const item in this.columns) {
            this.resetSumList(item)

            this.table.menu.extra[item] = this.columns[item].extra
            if (this.columns[item].searchable) {
                searchFields.push({ label: this.columns[item].label, key: item })
            }
        }
        if (searchFields.length > 0) {
            this.searchControls.push({
                type: `select-input`,
                field: [`dy`, searchFields],
                placeholder: this.$t(`检索内容`),
                style: `width:${this.searchInputWidth}rem`,
                selectStyle: `width:8rem`,
            })
        }

        this.searchControls = this.customSearchControls.concat(this.searchControls)

        for (const i in this.columns) {
            const col = this.columns[i]
            if (!col.selectFilter) {
                continue
            }

            this.selectFilterData.push({
                title: col.selectFilter.label ?? col.label,
                key: i,
                sortByBadge: col.selectFilter.sortByBadge ?? false,
                deleteNoBadge: col.selectFilter.deleteNoBadge ?? false,
                options: [{ label: this.$t(`全部`), value: `` }].concat(
                    col.isSwitch
                        ? [
                              { value: true, label: this.$t(`是`) },
                              { value: false, label: this.$t(`否`) },
                          ]
                        : col.enum
                          ? Object.entries(this.$GLOBAL.enums[col.enum.name]).map((x) => {
                                return {
                                    value: x[0],
                                    label: x[1][1],
                                }
                            })
                          : [],
                ),
            })
        }
        for (const filter of this.selectFilters) {
            this.selectFilterData.push({
                title: filter.title,
                key: filter.key,
                options: [{ label: this.$t(`全部`), value: `` }].concat(
                    filter.isSwitch
                        ? filter.isSwitch
                        : Object.entries(this.$GLOBAL.enums[filter.enumName]).map((x) => {
                              return {
                                  value: x[0],
                                  label: x[1][1],
                              }
                          }),
                ),
            })
        }
    },
    data() {
        return {
            // 过滤器数据
            selectFilterData: [],

            // 搜索栏数据
            searchControls: [],

            // 正在加载标记
            loading: false,

            // 仪表盘统计数据
            statistics: {
                total: `...`,
                sumList: {},
            },

            // 对话框
            dialog: {},

            // 表格查询参数
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: this.filter,
                keywords: this.keywords,
            },

            // 表格配置
            table: {
                // 选中项
                selection: [],
                // 右键菜单
                menu: {
                    extra: {},
                    opers: [],
                },
            },
        }
    },
    inject: [`reload`],
    methods: {
        resetSumList(item) {
            if (this.columns[item].sum && (!this.columns[item].condition || this.columns[item].condition())) {
                this.statistics.sumList[this.columns[item].sum.label] = ['...', '', this.columns[item].sum.span, this.columns[item].sum.sort ?? 0]
            }
        },
        // ---------------------------- ↓ 过滤器事件 ----------------------------
        onFilterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === `true` ? true : value === `false` ? false : value
            })
            this.$refs.search.search()
        },
        // ---------------------------- 过滤器事件 ↑ ----------------------------

        // ---------------------------- ↓ 搜索栏事件 ----------------------------
        async onReset() {
            Object.entries(this.$refs.selectFilter?.selected ?? []).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = [``]))
        },
        async onSearch(form) {
            for (const item in this.columns) {
                this.resetSumList(item)

                let field = form.dy[item]
                if (field === undefined) field = form.dy[item[0].toUpperCase() + item.substring(1)]

                if (field === undefined || field === `` || field == null) {
                    continue
                }

                const operator = this.columns[item].operator ?? `eq`
                this.query.dynamicFilter.filters.push({
                    field: item,
                    operator: operator,
                    value: operator === 'dateRange' ? field.map((x) => x.replace(/ 00:00:00$/, '')) : field,
                })
            }

            await this.$refs.table.upData()
        },
        async onSearchMine(form) {
            form.dy['owner.id'] = this.$GLOBAL.user.id
            await this.onSearch(form)
        },
        // ---------------------------- 搜索栏事件 ↑ ----------------------------

        // ---------------------------- ↓ 表格事件 ----------------------------
        async onRowButtonClick(row, button) {
            if (await button.click(row)) {
                this.$message.success(this.$t(`操作成功`))
                await this.$refs.table.upData()
            }
        },
        async onViewClick(row) {
            this.dialog.detail = { mode: `view`, row }
        },
        async onEditClick(row) {
            this.dialog.detail = { mode: `edit`, row }
        },
        async onBulkDeleteClick() {
            let loading
            try {
                await this.$confirm(this.$t(`确定删除选中的 {count} 项吗？`, { count: this.table.selection.length }), this.$t(`提示`), {
                    type: `warning`,
                })
                loading = this.$loading()
                const res = await this.$API[this.entityName].bulkDelete.post({
                    items: this.table.selection,
                })
                this.$refs.table.refresh()
                this.$message.success(this.$t(`删除 {count} 项`, { count: res.data }))
            } catch {
                //
            }
            loading?.close()
        },
        async onDeleteClick(row) {
            try {
                await this.$confirm(h(`div`, [h(`p`, this.$t(`是否确认删除？`)), h(`p`, row.id)]), this.$t(`提示`), {
                    type: `warning`,
                })
            } catch {
                return
            }
            let loading = this.$loading()
            try {
                const res = await this.$API[this.entityName].delete.post({
                    id: row.id,
                })
                this.$message.success(this.$t(`删除 {count} 项`, { count: res.data }))
                this.$refs.table.refresh()
            } catch {}
            loading?.close()
        },
        async onAddClick(item) {
            this.loading = true
            if (item?.preCheck) {
                if (!(await item.preCheck())) {
                    this.loading = false
                    return
                }
            }
            const row = {}
            for (const i in this.columns) {
                if (this.columns[i].default) {
                    Object.assign(row, Object.fromEntries([[i, this.columns[i].default]]))
                }
            }
            this.dialog.detail = { mode: `add`, row }
            this.loading = false
        },
        async onSwitchChange(row, method) {
            try {
                await this.$API[this.entityName][method].post(row)
                this.$message.success(this.$t(`操作成功`))
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        async onDataChange(data) {
            this.statistics.total = this.$refs.table?.total
            if (this.statistics.total === 0) {
                const tableId = this.$refs.table.$refs.scTable.tableId
                let rowLength = {}
                for (const cell of document.getElementsByClassName('el-table__cell')) {
                    for (const cls of cell.classList) {
                        if (cls.indexOf(`${tableId}_column_`) === 0) {
                            rowLength[cls] = (rowLength[cls] ?? 0) + 1
                        }
                    }
                }
                rowLength = Object.entries(rowLength)
                if (rowLength[0][1] > 0) {
                    this.statistics.total = rowLength[0][1] - 1
                }
            }

            const countByCalls = []
            for (const col in this.columns) {
                if (this.columns[col].selectFilter?.countBy) {
                    for (const item of this.selectFilterData.find((x) => x.key === col).options) {
                        delete item.badge
                    }

                    countByCalls.push(
                        this.$API[this.entityName].countBy.post({
                            filter: this.query.filter,
                            dynamicFilter: { filters: this.query.dynamicFilter.filters },
                            requiredFields: [col.replace(/(?:^|\.)[a-z]/g, (m) => m.toUpperCase())],
                        }),
                    )
                }
                if (this.columns[col].sum && (!this.columns[col].condition || this.columns[col].condition())) {
                    const res = await this.$API[this.entityName].sum.post({
                        filter: this.query.filter,
                        dynamicFilter: { filters: this.query.dynamicFilter.filters },
                        requiredFields: [col.replace(/(?:^|\.)[a-z]/g, (m) => m.toUpperCase())],
                    })

                    this.statistics.sumList[this.columns[col].sum.label] = [
                        this.columns[col].sum.currency
                            ? (res.data / (this.columns[col].sum.currency.multiple ?? 100)).toFixed(this.columns[col].sum.currency.decimal ?? 2)
                            : res.data,
                        this.columns[col].sum.currency?.flag,
                        this.columns[col].sum.span,
                        this.columns[col].sum.sort ?? 0,
                        this.columns[col].sum.currency?.suffix,
                    ]
                }
            }

            const res = await Promise.all(countByCalls)
            Object.assign(this.statistics, { res })
            for (const item of res) {
                for (const item2 of item.data ?? []) {
                    const key = Object.entries(item2.key)[0]
                    const options = this.selectFilterData.find((x) => x.key.toLowerCase() === key[0].toLowerCase())?.options
                    const filter = options.find((x) => x.value?.toString().toLowerCase() === key[1]?.toLowerCase())
                    if (!filter) {
                        const newItem = Object.entries(item2.key)[0][1]
                        if (newItem) {
                            options.push({ label: newItem, value: newItem, badge: item2.value })
                        }
                    } else {
                        filter.badge = item2.value
                    }
                }
            }
            while (!this.$t) {
                await new Promise((x) => setTimeout(x, 100))
            }
            for (const item of this.selectFilterData) {
                if (item.sortByBadge) {
                    item.options.sort((x, y) => {
                        if (y.label === this.$t(`全部`)) {
                            return 100000000
                        } else {
                            return (y.badge ?? 0) - (x.badge ?? 0)
                        }
                    })
                }
            }
        },
        onSelectionChange(data) {
            this.table.selection = data
        },
        // ---------------------------- 表格事件 ↑ ----------------------------
    },
    async mounted() {
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keeps.push({
                field: `keywords`,
                value: this.keywords,
                type: `root`,
            })
        }

        for (const f of this.dyFilters) {
            if (this.searchControls.findIndex((x) => x.field[1] === f.field) >= 0) {
                this.$refs.search.selectInputKey = f.field
            }
            this.$refs.search.form.dy[f.field] = f.value
            this.$refs.search.keeps.push({
                field: f.field,
                value: f.value,
                type: 'dy',
            })
        }
    },
    props: {
        dyFilters: { type: Array, default: [] },
        filter: { type: Object, default: {} },
        keywords: { type: String },
        entityName: { type: String },
        summary: { type: String },
        selectFilters: { type: Array, default: [] },
        customSearchControls: { type: Array, default: [] },
        searchInputWidth: { type: Number, default: 25 },
        columns: { type: Object },
        operations: { type: Array, default: [`view`, `add`, `edit`, `del`] },
        rowButtons: { type: Array, default: [] },
        rightButtons: { type: Array, default: [] },
        dialogFullScreen: { type: Array, default: [] },
        tabs: { type: Array },
        tableProps: { type: Object, default: {} },
        formInline: { type: Boolean },
        formLabelWidth: { type: Number },
        showSelection: { type: Boolean, default: true },
        showSearchMine: { type: Boolean, default: false },
        totalCountLabel: { type: String, default: `总数` },
        dialogWidth: { type: String, default: '50%' },
    },
    watch: {},
}
</script>

<style scoped>
.expense {
    color: var(--el-color-danger);
}
</style>