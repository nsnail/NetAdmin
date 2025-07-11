﻿<template>
    <el-container>
        <!--        仪表板-->
        <el-header v-loading="statistics.total === `...`" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <sc-statistic :title="$t(`总数`)" :value="statistics.total" group-separator />
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
                    :vue="this"
                    @reset="onReset"
                    @search="onSearch"
                    dateFormat="YYYY-MM-DD HH:mm:ss"
                    dateType="datetimerange"
                    dateValueFormat="YYYY-MM-DD HH:mm:ss"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-button v-if="operations.includes(`add`)" @click="onAddClick" icon="el-icon-plus" type="primary" />
                <el-button
                    v-if="operations.includes(`del`)"
                    :disabled="this.table.selection.length === 0 || this.loading"
                    @click="onBulkDeleteClick"
                    icon="el-icon-delete"
                    plain
                    type="danger" />
            </div>
        </el-header>

        <!--        表格主体-->
        <el-main class="nopadding">
            <sc-table
                :context-extra="this.table.menu.extra"
                :context-menus="Object.keys(this.columns)"
                :context-opers="this.operations"
                :default-sort="{ prop: `id`, order: `descending` }"
                :export-api="$API[entityName].export"
                :params="query"
                :query-api="$API[entityName].pagedQuery"
                :vue="this"
                @data-change="onDataChange"
                @selection-change="onSelectionChange"
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <el-table-column type="selection" width="50" />
                <template v-for="(item, i) in columns" :key="i">
                    <component
                        v-bind="item"
                        v-if="item.show.includes(`list`)"
                        :formatter="item.thousands ? (row) => $TOOL.groupSeparator(row[i]) : undefined"
                        :is="item.is ?? `el-table-column`"
                        :options="
                            item.options ??
                            (item.enum
                                ? Object.entries(this.$GLOBAL.enums[item.enum]).map((x) => {
                                      return {
                                          value: x[0],
                                          text: item.enumText ? item.enumText(x) : x[1][1],
                                          type: x[1][2],
                                          pulse: x[1][3] === `true`,
                                      }
                                  })
                                : null)
                        "
                        :prop="item.prop ?? i"
                        :sortable="item.sortable ?? `custom`">
                        <template v-if="item.isBoolean" #default="{ row }">
                            <el-switch
                                v-model="row[i]"
                                :disabled="
                                    !$GLOBAL.hasApiPermission(
                                        $API[entityName][i === `enabled` ? `setEnabled` : item.onChange].url.replace(`${config.API_URL}/`, ``),
                                    )
                                "
                                @change="onSwitchChange(row, i === `enabled` ? `setEnabled` : item.onChange)"></el-switch>
                        </template>
                    </component>
                </template>

                <el-table-column :label="$t(`操作`)" align="right" fixed="right" width="150">
                    <template #default="{ row }">
                        <el-button-group size="small">
                            <el-button v-if="operations.includes(`view`)" @click="onViewClick(row)" icon="el-icon-view" />
                            <el-button v-if="operations.includes(`edit`)" @click="onEditClick(row)" icon="el-icon-edit" />
                            <el-button v-if="operations.includes(`del`)" @click="onDeleteClick(row)" icon="el-icon-delete" type="danger" />
                        </el-button-group>
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
        ref="detailDialog" />
</template>

<script>
import { h } from 'vue'
import { defineAsyncComponent } from 'vue'
import tableConfig from '@/config/table'
import naColOperation from '@/config/na-col-operation'
import config from '@/config'
const naColAvatar = defineAsyncComponent(() => import('@/components/na-col-avatar'))
const detailDialog = defineAsyncComponent(() => import('./detail'))
const naColUser = defineAsyncComponent(() => import('@/components/na-col-user'))
export default {
    components: {
        detailDialog,
        naColAvatar,
        naColUser,
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
    created() {
        const searchFields = []
        this.searchControls = []
        for (const item in this.columns) {
            this.table.menu.extra[item] = this.columns[item].extra
            if (this.columns[item].searchable) {
                searchFields.push({ label: this.columns[item].label, key: item })
            }
        }
        if (searchFields.length > 0) {
            this.searchControls.push({
                type: `select-input`,
                field: [`dy`, searchFields],
                placeholder: this.$t(`匹配内容`),
                style: `width:25rem`,
                selectStyle: `width:8rem`,
            })
        }

        this.searchControls = this.customSearchControls.concat(this.searchControls)

        for (const i in this.columns) {
            const col = this.columns[i]
            if (!col.countBy) {
                continue
            }

            this.selectFilterData.push({
                title: col.label,
                key: i,
                options: [{ label: this.$t(`全部`), value: `` }].concat(
                    col.isBoolean
                        ? [
                              { value: true, label: this.$t(`是`) },
                              { value: false, label: this.$t(`否`) },
                          ]
                        : Object.entries(this.$GLOBAL.enums[col.enum]).map((x) => {
                              return {
                                  value: x[0],
                                  label: x[1][1],
                              }
                          }),
                ),
            })
        }
        for (const filter of this.selectFilters) {
            this.selectFilterData.push({
                title: filter.title,
                key: filter.key,
                options: [{ label: this.$t(`全部`), value: `` }].concat(
                    filter.isBoolean
                        ? filter.isBoolean
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
            },

            // 对话框
            dialog: {},

            // 表格查询参数
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
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
            Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = [``]))
        },
        async onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: `createdTime`,
                    operator: `dateRange`,
                    value: form.dy.createdTime,
                })
            }
            for (const item in this.columns) {
                let field = form.dy[item]
                if (field === undefined) field = form.dy[item[0].toUpperCase() + item.substring(1)]

                if (field === undefined || field === `` || typeof field === `object`) {
                    continue
                }

                this.query.dynamicFilter.filters.push({
                    field: item,
                    operator: this.columns[item].operator ?? `eq`,
                    value: field,
                })
            }

            await this.$refs.table.upData()
        },
        // ---------------------------- 搜索栏事件 ↑ ----------------------------

        // ---------------------------- ↓ 表格事件 ----------------------------
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
        async onAddClick() {
            this.dialog.detail = { mode: `add` }
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

            const calls = []
            for (const col in this.columns) {
                if (this.columns[col].countBy) {
                    for (const item of this.selectFilterData.find((x) => x.key === col).options) {
                        delete item.badge
                    }

                    calls.push(
                        this.$API[this.entityName].countBy.post({
                            dynamicFilter: { filters: this.query.dynamicFilter.filters },
                            requiredFields: [col.replace(/(?:^|\.)[a-z]/g, (m) => m.toUpperCase())],
                        }),
                    )
                }
            }
            const res = await Promise.all(calls)
            Object.assign(this.statistics, { res })
            for (const item of res) {
                for (const item2 of item.data) {
                    const key = Object.entries(item2.key)[0]
                    const filter = this.selectFilterData
                        .find((x) => x.key.toLowerCase() === key[0].toLowerCase())
                        ?.options.find((x) => x.value.toString().toLowerCase() === key[1].toLowerCase())
                    if (filter) filter.badge = item2.value
                }
            }
            for (const item of this.selectFilterData) {
                item.options.sort((x, y) => (y.label === this.$t(`全部`) ? 999999999 : (y.badge ?? 0 - x.badge ?? 0)))
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
    },
    props: {
        keywords: { type: String },
        entityName: { type: String },
        summary: { type: String },
        selectFilters: { type: Array, default: [] },
        customSearchControls: { type: Array, default: [] },
        columns: { type: Object },
        operations: { type: Array, default: [`view`, `add`, `edit`, `del`] },
        dialogFullScreen: { type: Boolean },
    },
    watch: {},
}
</script>

<style scoped />