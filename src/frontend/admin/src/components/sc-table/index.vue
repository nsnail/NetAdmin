<template>
    <div v-loading="loading" :style="{ height: _height }" class="scTable" ref="scTableMain">
        <div :style="{ height: _table_height }" class="scTable-table">
            <el-table
                v-bind="$attrs"
                :border="config.border"
                :data="tableData"
                :default-expand-all="config.defaultExpandAll"
                :default-sort="defaultSort"
                :height="height === 'auto' ? null : '100%'"
                :key="toggleIndex"
                :row-key="rowKey"
                :size="config.size"
                :stripe="config.stripe"
                :summary-method="remoteSummary ? remoteSummaryMethod : summaryMethod"
                @cell-click="cellClickMethod"
                @filter-change="filterChange"
                @row-contextmenu="rowContextmenu"
                @row-dblclick="dbClick"
                @sort-change="sortChange"
                ref="scTable"
                tooltip-effect="light">
                <slot />
                <template v-for="(item, index) in userColumn" :key="index">
                    <el-table-column
                        v-if="!item.hide"
                        :column-key="item.prop"
                        :filter-method="remoteFilter || !item.filters ? null : filterHandler"
                        :filters="item.filters"
                        :fixed="item.fixed"
                        :label="item.label"
                        :prop="item.prop"
                        :show-overflow-tooltip="item.showOverflowTooltip"
                        :sortable="item.sortable"
                        :width="item.width">
                        <template #default="scope">
                            <slot v-bind="scope" :name="item.prop">
                                {{ scope.row[item.prop] }}
                            </slot>
                        </template>
                    </el-table-column>
                </template>
                <el-table-column min-width="1" />
                <template #empty>
                    <el-empty :description="emptyText" :image-size="100" />
                </template>
            </el-table>
        </div>
        <div v-if="!hidePagination || !hideDo" class="scTable-page">
            <div class="scTable-pagination">
                <el-pagination
                    v-if="!hidePagination"
                    v-model:currentPage="currentPage"
                    :layout="paginationLayout"
                    :page-size="scPageSize"
                    :page-sizes="pageSizes"
                    :pager-count="pagerCount"
                    :total="total"
                    @current-change="paginationChange"
                    @update:page-size="pageSizeChange"
                    background
                    size="small" />
            </div>
            <div v-if="!hideDo" class="scTable-do">
                <el-button
                    v-if="exportApi"
                    :title="$t('导出文件')"
                    @click="exportData"
                    circle
                    icon="el-icon-download"
                    plain
                    style="margin-left: 1rem"
                    type="primary" />
                <el-button v-if="!hideRefresh" :title="$t('刷新')" @click="refresh" circle icon="el-icon-refresh" style="margin-left: 1rem" />
                <el-popover
                    v-if="column"
                    :hide-after="0"
                    :title="$t('列设置')"
                    :width="500"
                    @after-leave="customColumnShow = false"
                    @show="customColumnShow = true"
                    placement="top"
                    trigger="click">
                    <template #reference>
                        <el-button circle icon="el-icon-set-up" style="margin-left: 1rem" />
                    </template>
                    <column-setting
                        v-if="customColumnShow"
                        :column="userColumn"
                        @back="columnSettingBack"
                        @save="columnSettingSave"
                        @userChange="columnSettingChange"
                        ref="columnSetting" />
                </el-popover>
                <el-popover v-if="!hideSetting" :hide-after="0" :title="$t('表格设置')" :width="400" placement="top" trigger="click">
                    <template #reference>
                        <el-button circle icon="el-icon-setting" style="margin-left: 1rem" />
                    </template>
                    <el-form label-position="left" label-width="10rem">
                        <el-form-item :label="$t('表格尺寸')">
                            <el-radio-group v-model="config.size" @change="configSizeChange" size="small">
                                <el-radio-button value="large">{{ $t('大') }}</el-radio-button>
                                <el-radio-button value="default">{{ $t('正常') }}</el-radio-button>
                                <el-radio-button value="small">{{ $t('小') }}</el-radio-button>
                            </el-radio-group>
                        </el-form-item>
                        <el-form-item :label="$t('样式')">
                            <el-checkbox v-model="config.border" :label="$t('纵向边框')" />
                            <el-checkbox v-model="config.stripe" :label="$t('斑马纹')" />
                        </el-form-item>
                    </el-form>
                </el-popover>
            </div>
        </div>
    </div>
    <sc-contextmenu @command="contextMenuCommand" @visible-change="contextMenuVisibleChange" ref="contextmenu">
        <sc-contextmenu-item
            v-for="(menu, index) in contextMenus.filter((x) => {
                if (current.column?.property === x) {
                    return true
                }
                return this.contextMulti && !!this.contextMulti[current.column?.property]?.find((y) => y === x)
            })"
            :command="menu"
            :key="index"
            :title="`${menu}`">
            <sc-contextmenu-item :command="`${menu}^|^Equal^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" title="=" />
            <sc-contextmenu-item :command="`${menu}^|^NotEqual^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" title="≠" />
            <sc-contextmenu-item :command="`${menu}^|^GreaterThan^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" divided title="＞" />
            <sc-contextmenu-item :command="`${menu}^|^GreaterThanOrEqual^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" title="≥" />
            <sc-contextmenu-item :command="`${menu}^|^LessThan^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" title="＜" />
            <sc-contextmenu-item :command="`${menu}^|^LessThanOrEqual^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" title="≤" />
            <sc-contextmenu-item :command="`${menu}^|^Contains^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" :title="$t('包含')" divided />
            <sc-contextmenu-item :command="`${menu}^|^NotContains^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" :title="$t('不含')" />
            <sc-contextmenu-item
                :command="`${menu}^|^StartsWith^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('以 x 开始')"
                divided />
            <sc-contextmenu-item :command="`${menu}^|^NotStartsWith^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" :title="$t('非 x 开始')" />
            <sc-contextmenu-item :command="`${menu}^|^EndsWith^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" :title="$t('以 x 结束')" />
            <sc-contextmenu-item :command="`${menu}^|^NotEndsWith^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" :title="$t('非 x 结束')" />
            <sc-contextmenu-item :command="`${menu}^|^Range^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" :title="$t('数值范围')" divided />
            <sc-contextmenu-item :command="`${menu}^|^DateRange^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" :title="$t('日期范围')" />
            <sc-contextmenu-item :command="`${menu}^|^Any^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" :title="$t('为其一')" divided />
            <sc-contextmenu-item :command="`${menu}^|^NotAny^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" :title="$t('非其一')" />
            <sc-contextmenu-item
                :command="`${menu}^|^order-ascending^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('顺序排序')"
                divided />
            <sc-contextmenu-item
                :command="`${menu}^|^order-descending^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('倒序排序')" />
        </sc-contextmenu-item>
        <sc-contextmenu-item :title="$t('复制')" command="copy" divided icon="el-icon-copy-document" suffix="C" />
        <sc-contextmenu-item v-if="contextOpers.includes('add')" :title="$t('新建')" command="add" divided icon="el-icon-plus" suffix="A" />
        <sc-contextmenu-item v-if="contextOpers.includes('view')" :title="$t('查看')" command="view" icon="el-icon-view" suffix="V" />
        <sc-contextmenu-item v-if="contextOpers.includes('edit')" :title="$t('编辑')" command="edit" icon="el-icon-edit" suffix="E" />
        <sc-contextmenu-item v-if="contextOpers.includes('del')" :title="$t('删除')" command="del" icon="el-icon-delete" suffix="D" />
        <sc-contextmenu-item
            v-for="(adv, index) in contextAdvs"
            :command="adv"
            :divided="adv.divided"
            :icon="adv.icon"
            :key="index"
            :title="adv.label" />
        <sc-contextmenu-item v-if="exportApi" :title="$t('导出文件')" command="export" divided icon="el-icon-download" />
        <sc-contextmenu-item :title="$t('重新加载')" command="refresh" divided icon="el-icon-refresh" suffix="R" />
    </sc-contextmenu>
    <field-filter ref="fieldFilterDialog" />
</template>
<script>
import config from '@/config/table'
import { defineAsyncComponent } from 'vue'

const columnSetting = defineAsyncComponent(() => import('./column-setting'))
const scContextmenuItem = defineAsyncComponent(() => import('@/components/sc-context-menu/item'))
const scContextmenu = defineAsyncComponent(() => import('@/components/sc-context-menu'))
const fieldFilter = defineAsyncComponent(() => import('./field-filter'))

import { h } from 'vue'
import tool from '@/utils/tool'

export default {
    name: 'scTable',
    components: {
        scContextmenu,
        scContextmenuItem,
        columnSetting,
        fieldFilter,
    },
    props: {
        dblClickDisable: { type: Boolean, default: false },
        vue: { type: Object },
        contextMenus: { type: Array },
        contextOpers: { type: Array, default: ['copy', 'add', 'view', 'edit', 'del'] },
        contextAdvs: { type: Array, default: [] },
        contextMulti: { type: Object },
        tableName: { type: String, default: '' },
        beforePost: {
            type: Function,
        },
        queryApi: {
            type: Object,
            default: () => {},
        },
        exportApi: {
            type: Object,
            default: null,
        },
        params: { type: Object, default: () => ({}) },
        data: {
            type: Object,
            default: () => {},
        },
        height: { type: [String, Number], default: '100%' },
        size: { type: String, default: 'default' },
        border: { type: Boolean, default: false },
        stripe: { type: Boolean, default: false },
        defaultExpandAll: { type: Boolean, default: false },
        pageSize: { type: Number, default: config.pageSize },
        pageSizes: { type: Array, default: config.pageSizes },
        rowKey: { type: String, default: '' },
        summaryMethod: { type: Function, default: null },
        filterMethod: { type: Function, default: null },
        cellClickMethod: { type: Function, default: null },
        onCommand: { type: Function, default: null },
        column: {
            type: Object,
            default: () => {},
        },
        defaultSort: {
            type: Object,
            default: () => {},
        },
        remoteSort: { type: Boolean, default: false },
        remoteFilter: { type: Boolean, default: false },
        remoteSummary: { type: Boolean, default: false },
        hidePagination: { type: Boolean, default: false },
        hideDo: { type: Boolean, default: false },
        hideRefresh: { type: Boolean, default: false },
        hideSetting: { type: Boolean, default: false },
        paginationLayout: { type: String, default: config.paginationLayout },
    },
    watch: {
        //监听从props里拿到值了
        data() {
            this.tableData = this.data
            this.total = this.tableData.length
        },
        queryApi() {
            this.tableParams = this.params
            this.refresh()
        },
        column() {
            this.userColumn = this.column
        },
    },
    computed: {
        tool() {
            return tool
        },
        _height() {
            return Number(this.height) ? Number(this.height) + 'px' : this.height
        },
        _table_height() {
            return this.hidePagination && this.hideDo ? '100%' : 'calc(100% - 4rem)'
        },
    },
    data() {
        return {
            pagerCount: 11,
            current: {
                row: null,
                column: null,
            },
            scPageSize: this.pageSize,
            isActivate: true,
            emptyText: '暂无数据',
            toggleIndex: 0,
            tableData: [],
            total: 0,
            currentPage: 1,
            prop: null,
            order: null,
            loading: false,
            tableHeight: '100%',
            tableParams: this.params,
            userColumn: [],
            customColumnShow: false,
            summary: {},
            config: {
                size: this.size,
                border: this.border,
                stripe: this.stripe,
                defaultExpandAll: this.defaultExpandAll,
            },
        }
    },
    async mounted() {
        this.pagerCount = document.body.clientWidth < 1000 ? 3 : 11
        //判断是否开启自定义列
        if (this.column) {
            await this.getCustomColumn()
        } else {
            this.userColumn = this.column
        }
        //判断是否静态数据
        if (this.queryApi) {
            await this.getData()
        } else if (this.data) {
            this.tableData = this.data
            this.total = this.tableData.length
        }
    },
    activated() {
        if (!this.isActivate) {
            this.$refs.scTable.doLayout()
        }
    },
    deactivated() {
        this.isActivate = false
    },
    methods: {
        dbClick(row) {
            if (this.dblClickDisable) {
                return
            }
            if (this.vue.dialog) {
                this.vue.dialog.save = { mode: 'view', row: { id: row.id } }
            }
        },
        async contextMenuCommand(command) {
            if (typeof command === 'object') {
                return command.action(this.vue, this.current.row)
            }
            if (command === 'refresh') {
                this.vue.reload()
                return
            }
            if (command === 'copy') {
                let data = tool.getNestedProperty(this.current.row, this.current.column?.property)
                if (!data) return

                const textarea = document.createElement('textarea')
                textarea.readOnly = true
                textarea.style.position = 'absolute'
                textarea.style.left = '-9999px'
                textarea.value = data
                document.body.appendChild(textarea)
                textarea.select()
                textarea.setSelectionRange(0, textarea.value.length)
                const result = document.execCommand('Copy')
                if (result) {
                    this.$message.success(this.$t('复制成功'))
                }
                document.body.removeChild(textarea)
                return
            }
            if (command === 'view') {
                this.vue.dialog.save = { mode: 'view', row: { id: this.current.row.id } }
                return
            }
            if (command === 'export') {
                await this.exportData()
                return
            }
            if (command === 'add') {
                this.vue.dialog.save = { mode: 'add' }
                return
            }
            if (command === 'edit') {
                this.vue.dialog.save = { mode: 'edit', row: { id: this.current.row.id } }
                return
            }
            if (command === 'del') {
                try {
                    await this.$confirm(h('div', [h('p', this.$t('是否确认删除？')), h('p', this.current.row.id)]), this.$t('提示'), {
                        type: 'warning',
                    })
                } catch {
                    return
                }
                await this.vue.rowDel(this.current.row)
                return
            }
            const kv = command.split('^|^')
            if (kv[1].indexOf('order-') === 0) {
                this.vue.query.prop = kv[0]
                this.vue.query.order = kv[1].substring(6)
                await this.upData()
            } else {
                this.$refs.fieldFilterDialog.open({ field: kv[0], operator: kv[1], value: kv[2] }, (data) => {
                    const value = data.value?.split('\n') ?? ['']
                    this.vue.query.dynamicFilter.filters.push({
                        field: data.field,
                        operator: data.operator,
                        value: value.length === 1 ? value[0] : value,
                    })
                    if (this.onCommand) {
                        this.onCommand(this.vue.query.dynamicFilter.filters)
                    }
                    this.upData()
                })
            }
        },
        contextMenuVisibleChange(visible) {
            if (!visible) {
                this.setCurrentRow()
            }
        },
        rowContextmenu(row, column, event) {
            if (!this.contextMenus) return
            this.current.row = row
            this.current.column = column
            this.setCurrentRow(row)
            this.$refs.contextmenu.openMenu(event)
        },

        //获取列
        async getCustomColumn() {
            this.userColumn = await config.columnSettingGet(this.tableName, this.column)
        },
        getQueryParams() {
            const reqData = {
                [config.request.page]: this.currentPage,
                [config.request.pageSize]: this.scPageSize,
                [config.request.prop]: this.prop,
                [config.request.order]: this.order,
            }
            if (this.hidePagination) {
                delete reqData[config.request.page]
                delete reqData[config.request.pageSize]
            }
            Object.assign(reqData, this.tableParams)

            const ids = [...new Set(this.$refs.scTable.getSelectionRows().map((x) => x.id))].filter((x) => !!x)
            if (ids.length > 0) {
                reqData.dynamicFilter = {
                    filters: [reqData.dynamicFilter],
                    field: 'id',
                    operator: 'Any',
                    value: ids,
                }
            }
            return reqData
        },
        //获取数据
        async getData() {
            const ret = await (async () => {
                this.loading = true

                let res
                let response
                try {
                    const reqData = this.getQueryParams()
                    if (!this.beforePost || this.beforePost(reqData)) res = await this.queryApi.post(reqData)
                } catch (error) {
                    this._clearData()
                    this.loading = false
                    this.emptyText = error.statusText
                    return false
                }
                try {
                    response = config.parseData(res)
                } catch (error) {
                    this._clearData()
                    this.loading = false
                    this.emptyText = '数据格式错误'
                    return false
                }
                if (response.code !== config.successCode) {
                    this._clearData()
                    this.loading = false
                    this.emptyText = response.msg
                } else {
                    this.emptyText = '暂无数据'
                    if (this.hidePagination) {
                        this.tableData = response.data || []
                    } else {
                        this.tableData = response.rows || []
                    }
                    this.total = response.total || 0
                    this.summary = response.summary || {}
                    this.loading = false
                }
                this.$refs.scTable?.setScrollTop(0)
                return res
            })()

            this.$emit('dataChange', ret, this.tableData)
        },
        //清空数据
        _clearData() {
            this.tableData = []
        },
        //分页点击
        paginationChange() {
            this.getData()
        },
        //条数变化
        pageSizeChange(size) {
            this.scPageSize = size
            this.getData()
        },
        //刷新数据
        refresh() {
            this.$refs.scTable.clearSelection()
            this.getData()
        },
        //导出数据
        async exportData() {
            this.loading = true
            try {
                await this.exportApi.post(this.getQueryParams(), { responseType: 'blob' })
                this.$message.success(this.$t('数据已导出（上限 {n} 条）', { n: 50000 }))
            } catch {}
            this.loading = false
        },
        //更新数据 合并上一次params
        async upData(params = null, page = 1) {
            this.currentPage = page
            this.$refs.scTable.clearSelection()
            Object.assign(this.tableParams, params || {})
            await this.getData()
        },
        //重载数据 替换params
        reload(params, page = 1) {
            this.currentPage = page
            this.tableParams = params || {}
            this.$refs.scTable.clearSelection()
            this.$refs.scTable.clearSort()
            this.$refs.scTable.clearFilter()
            this.getData()
        },
        //自定义变化事件
        columnSettingChange(userColumn) {
            this.userColumn = userColumn
            this.toggleIndex += 1
        },
        //自定义列保存
        async columnSettingSave(userColumn) {
            this.$refs.columnSetting.isSave = true
            try {
                await config.columnSettingSave(this.tableName, userColumn)
            } catch (error) {
                this.$message.error('保存失败')
                this.$refs.columnSetting.isSave = false
            }
            this.$message.success('保存成功')
            this.$refs.columnSetting.isSave = false
        },
        //自定义列重置
        async columnSettingBack() {
            this.$refs.columnSetting.isSave = true
            try {
                this.userColumn = await config.columnSettingReset(this.tableName, this.column)
                this.$refs.columnSetting.userColumn = JSON.parse(JSON.stringify(this.userColumn || []))
            } catch (error) {
                this.$message.error('重置失败')
                this.$refs.columnSetting.isSave = false
            }
            this.$refs.columnSetting.isSave = false
        },
        //排序事件
        sortChange(obj) {
            if (!this.remoteSort) {
                return false
            }
            if (obj.column && obj.prop) {
                this.prop = obj.column.sortBy ? obj.column.sortBy : obj.prop
                this.order = obj.order
            } else {
                this.prop = null
                this.order = null
            }
            this.getData()
        },
        //本地过滤
        filterHandler(value, row, column) {
            const property = column.property
            return row[property] === value
        },
        //过滤事件
        filterChange(filters) {
            if (!this.remoteFilter) {
                return false
            }
            if (this.filterMethod) {
                return this.filterMethod(filters)
            }
            Object.keys(filters).forEach((key) => {
                filters[key] = filters[key].join(',')
            })
            this.upData(filters)
        },
        //远程合计行处理
        remoteSummaryMethod(param) {
            const { columns } = param
            const sums = []
            columns.forEach((column, index) => {
                if (index === 0) {
                    sums[index] = '合计'
                    return
                }
                const values = this.summary[column.property]
                if (values) {
                    sums[index] = values
                } else {
                    sums[index] = ''
                }
            })
            return sums
        },
        configSizeChange() {
            this.$refs.scTable.doLayout()
        },
        //插入行 unshiftRow
        unshiftRow(row) {
            this.tableData.unshift(row)
        },
        //插入行 pushRow
        pushRow(row) {
            this.tableData.push(row)
        },
        //根据key覆盖数据
        updateKey(row, rowKey = this.rowKey) {
            this.tableData
                .filter((item) => item[rowKey] === row[rowKey])
                .forEach((item) => {
                    Object.assign(item, row)
                })
        },
        //根据index覆盖数据
        updateIndex(row, index) {
            Object.assign(this.tableData[index], row)
        },
        //根据index删除
        removeIndex(index) {
            this.tableData.splice(index, 1)
        },
        //根据index批量删除
        removeIndexes(indexes = []) {
            indexes.forEach((index) => {
                this.tableData.splice(index, 1)
            })
        },
        //根据key删除
        removeKey(key, rowKey = this.rowKey) {
            this.tableData.splice(
                this.tableData.findIndex((item) => item[rowKey] === key),
                1,
            )
        },
        //根据keys批量删除
        removeKeys(keys = [], rowKey = this.rowKey) {
            keys.forEach((key) => {
                this.tableData.splice(
                    this.tableData.findIndex((item) => item[rowKey] === key),
                    1,
                )
            })
        },
        //原生方法转发
        clearSelection() {
            this.$refs.scTable.clearSelection()
        },
        toggleRowSelection(row, selected) {
            this.$refs.scTable.toggleRowSelection(row, selected)
        },
        toggleAllSelection() {
            this.$refs.scTable.toggleAllSelection()
        },
        toggleRowExpansion(row, expanded) {
            this.$refs.scTable.toggleRowExpansion(row, expanded)
        },
        setCurrentRow(row) {
            this.$refs.scTable.setCurrentRow(row)
        },
        clearSort() {
            this.$refs.scTable.clearSort()
        },
        clearFilter(columnKey) {
            this.$refs.scTable.clearFilter(columnKey)
        },
        doLayout() {
            this.$refs.scTable.doLayout()
        },
        sort(prop, order) {
            this.$refs.scTable.sort(prop, order)
        },
    },
}
</script>
<style scoped>
.scTable {
}

.scTable-table {
    height: calc(100% - 4rem);
}

.scTable-page {
    height: 4rem;
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 0 1rem;
}

.scTable-do {
    white-space: nowrap;
}

.scTable:deep(.el-table__footer) .cell {
    font-weight: bold;
}

.scTable:deep(.el-table__body-wrapper) .el-scrollbar__bar.is-horizontal {
    height: 0.9rem;
    border-radius: 0.9rem;
}

.scTable:deep(.el-table__body-wrapper) .el-scrollbar__bar.is-vertical {
    width: 0.9rem;
    border-radius: 0.9rem;
}
</style>