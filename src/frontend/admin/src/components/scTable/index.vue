<!--
 * @Description: 数据表格组件
 * @version: 1.11
 * @Author: sakuya
 * @Date: 2021年11月29日21:51:15
 * @LastEditors: sakuya
 * @LastEditTime: 2023年3月2日10:43:35
-->
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
                @sort-change="sortChange"
                ref="scTable">
                <slot></slot>
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
                    <el-empty :description="emptyText" :image-size="100"></el-empty>
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
                    :small="true"
                    :total="total"
                    @current-change="paginationChange"
                    @update:page-size="pageSizeChange"
                    background></el-pagination>
            </div>
            <div v-if="!hideDo" class="scTable-do">
                <el-button v-if="!hideRefresh" @click="refresh" circle icon="el-icon-refresh" style="margin-left: 1rem"></el-button>
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
                        <el-button circle icon="el-icon-set-up" style="margin-left: 1rem"></el-button>
                    </template>
                    <columnSetting
                        v-if="customColumnShow"
                        :column="userColumn"
                        @back="columnSettingBack"
                        @save="columnSettingSave"
                        @userChange="columnSettingChange"
                        ref="columnSetting"></columnSetting>
                </el-popover>
                <el-popover v-if="!hideSetting" :hide-after="0" :title="$t('表格设置')" :width="400" placement="top" trigger="click">
                    <template #reference>
                        <el-button circle icon="el-icon-setting" style="margin-left: 1rem"></el-button>
                    </template>
                    <el-form label-position="left" label-width="10rem">
                        <el-form-item :label="$t('表格尺寸')">
                            <el-radio-group v-model="config.size" @change="configSizeChange" size="small">
                                <el-radio-button label="large">{{ $t('大') }}</el-radio-button>
                                <el-radio-button label="default">{{ $t('正常') }}</el-radio-button>
                                <el-radio-button label="small">{{ $t('小') }}</el-radio-button>
                            </el-radio-group>
                        </el-form-item>
                        <el-form-item :label="$t('样式')">
                            <el-checkbox v-model="config.border" :label="$t('纵向边框')"></el-checkbox>
                            <el-checkbox v-model="config.stripe" :label="$t('斑马纹')"></el-checkbox>
                        </el-form-item>
                    </el-form>
                </el-popover>
            </div>
        </div>
    </div>
    <sc-contextmenu @command="contextMenuCommand" @visible-change="contextMenuVisibleChange" ref="contextmenu">
        <sc-contextmenu-item
            v-for="(menu, index) in contextMenus.filter((x) => x === current.column.property)"
            :command="menu"
            :key="index"
            :title="`${menu}`">
            <sc-contextmenu-item :command="`${menu}^|^Equal^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" title="="></sc-contextmenu-item>
            <sc-contextmenu-item :command="`${menu}^|^NotEqual^|^${tool.getNestedProperty(current.row, menu) ?? ''}`" title="≠"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^GreaterThan^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                divided
                title="＞"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^GreaterThanOrEqual^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                title="≥"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^LessThan^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                title="＜"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^LessThanOrEqual^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                title="≤"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^Contains^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('包含')"
                divided></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^NotContains^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('不含')"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^StartsWith^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('以 x 开始')"
                divided></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^NotStartsWith^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('非 x 开始')"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^EndsWith^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('以 x 结束')"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^NotEndsWith^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('非 x 结束')"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^Range^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('数值范围')"
                divided></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^DateRange^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('日期范围')"></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^Any^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('为其一')"
                divided></sc-contextmenu-item>
            <sc-contextmenu-item
                :command="`${menu}^|^NotAny^|^${tool.getNestedProperty(current.row, menu) ?? ''}`"
                :title="$t('非其一')"></sc-contextmenu-item>
        </sc-contextmenu-item>
        <sc-contextmenu-item
            v-if="contextOpers.includes('view')"
            :title="$t('查看')"
            command="view"
            divided
            icon="el-icon-view"></sc-contextmenu-item>
        <sc-contextmenu-item v-if="contextOpers.includes('edit')" :title="$t('编辑')" command="edit" icon="el-icon-edit"></sc-contextmenu-item>
        <sc-contextmenu-item v-if="contextOpers.includes('del')" :title="$t('删除')" command="del" icon="el-icon-delete"></sc-contextmenu-item>
        <sc-contextmenu-item
            v-for="(adv, index) in contextAdvs"
            :command="adv"
            :divided="adv.divided"
            :icon="adv.icon"
            :key="index"
            :title="adv.label">
        </sc-contextmenu-item>
        <sc-contextmenu-item :title="$t('重新加载')" command="refresh" divided icon="el-icon-refresh" suffix="Ctrl+R"></sc-contextmenu-item>
    </sc-contextmenu>
</template>
<script>
import config from '@/config/table'
import columnSetting from './columnSetting'
import scContextmenuItem from '@/components/scContextmenu/item.vue'
import scContextmenu from '@/components/scContextmenu/index.vue'
import { h } from 'vue'
import tool from '@/utils/tool'

export default {
    name: 'scTable',
    components: {
        scContextmenu,
        scContextmenuItem,
        columnSetting,
    },
    props: {
        vue: { type: Object },
        contextMenus: { type: Array },
        contextOpers: { type: Array, default: ['view', 'edit', 'del'] },
        contextAdvs: { type: Array, default: [] },
        tableName: { type: String, default: '' },
        beforePost: {
            type: Function,
        },
        apiObj: {
            type: Object,
            default: () => {},
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
        apiObj() {
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
            pagerCount: 10,
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
    mounted() {
        this.pagerCount = document.body.clientWidth < 1000 ? 3 : 10
        //判断是否开启自定义列
        if (this.column) {
            this.getCustomColumn()
        } else {
            this.userColumn = this.column
        }
        //判断是否静态数据
        if (this.apiObj) {
            this.getData()
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
        async contextMenuCommand(command) {
            if (typeof command === 'object') {
                return command.action()
            }
            if (command === 'refresh') {
                this.vue.reload()
                return
            }
            if (command === 'view') {
                this.vue.dialog.save = true
                await this.$nextTick()
                await this.vue.$refs.saveDialog.open('view', { id: this.current.row.id })
                return
            }
            if (command === 'edit') {
                this.vue.dialog.save = true
                await this.$nextTick()
                await this.vue.$refs.saveDialog.open('edit', { id: this.current.row.id })
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
            let value
            try {
                value = await this.$prompt(this.$t('仅显示 {field} {operator}：', { field: kv[0], operator: kv[1] }), this.$t('高级筛选'), {
                    inputplaceholder: this.$t('一行一个'),
                    inputPattern: /.*/,
                    inputType: 'textarea',
                    inputValue: kv[2],
                })
            } catch {
                return
            }
            value = value.value?.split('\n') ?? ['']
            this.vue.query.dynamicFilter.filters.push({
                field: kv[0],
                operator: kv[1],
                value: value.length === 1 ? value[0] : value,
            })
            this.upData()
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
        //获取数据
        async getData() {
            this.loading = true
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
            let res
            let response
            try {
                if (!this.beforePost || this.beforePost(reqData)) res = await this.apiObj.post(reqData)
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
            this.$emit('dataChange', res, this.tableData)
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
        //更新数据 合并上一次params
        upData(params = null, page = 1) {
            this.currentPage = page
            this.$refs.scTable.clearSelection()
            Object.assign(this.tableParams, params || {})
            this.getData()
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
                this.$refs.columnSetting.usercolumn = JSON.parse(JSON.stringify(this.userColumn || []))
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