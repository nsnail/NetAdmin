<template>
    <el-select
        v-model="defaultValue"
        :clearable="clearable"
        :collapse-tags="collapseTags"
        :collapse-tags-tooltip="collapseTagsTooltip"
        :disabled="disabled"
        :filter-method="filterMethod"
        :filterable="filterable"
        :multiple="multiple"
        :placeholder="placeholder"
        :size="size"
        @clear="clear"
        @remove-tag="removeTag"
        @visible-change="visibleChange"
        ref="select">
        <template #empty>
            <div v-loading="loading" class="scTable-select__table">
                <div class="scTable-select__header">
                    <slot :form="formData" :submit="formSubmit" name="header" />
                </div>
                <el-table
                    :data="tableData"
                    :highlight-current-row="!multiple"
                    @row-click="click"
                    @select="select"
                    @select-all="selectAll"
                    max-height="30rem"
                    ref="table">
                    <el-table-column v-if="multiple" type="selection" width="45" />
                    <el-table-column v-else type="index" width="45">
                        <template #default="scope"
                            ><span>{{ scope.$index + (currentPage - 1) * pageSize + 1 }}</span></template
                        >
                    </el-table-column>
                    <slot />
                </el-table>
                <div class="scTable-select__page">
                    <el-pagination
                        v-model:currentPage="currentPage"
                        :page-size="pageSize"
                        :total="total"
                        @current-change="reload"
                        background
                        layout="prev, pager, next"
                        small />
                </div>
            </div>
        </template>
    </el-select>
</template>

<script>
import config from '@/config/table-select'

export default {
    props: {
        modelValue: null,
        queryApi: {
            type: Object,
            default: () => {},
        },
        params: {
            type: Object,
            default: () => {},
        },
        placeholder: { type: String },
        size: { type: String, default: 'default' },
        clearable: { type: Boolean, default: false },
        multiple: { type: Boolean, default: false },
        filterable: { type: Boolean, default: false },
        collapseTags: { type: Boolean, default: false },
        collapseTagsTooltip: { type: Boolean, default: false },
        disabled: { type: Boolean, default: false },
        tableWidth: { type: Number, default: 40 },
        mode: { type: String, default: 'popover' },
        props: {
            type: Object,
            default: () => {},
        },
    },
    data() {
        return {
            loading: false,
            keyword: null,
            defaultValue: [],
            tableData: [],
            pageSize: config.pageSize,
            total: 0,
            currentPage: 1,
            defaultProps: {
                label: config.props.label,
                value: config.props.value,
                page: config.request.page,
                pageSize: config.request.pageSize,
                keyword: config.request.keyword,
            },
            formData: {},
        }
    },
    computed: {},
    watch: {
        modelValue: {
            handler() {
                this.defaultValue = this.modelValue
                this.autoCurrentLabel()
            },
            deep: true,
        },
    },
    mounted() {
        this.defaultProps = Object.assign(this.defaultProps, this.props)
        this.defaultValue = this.modelValue
        this.autoCurrentLabel()
    },
    methods: {
        //表格显示隐藏回调
        visibleChange(visible) {
            if (visible) {
                this.currentPage = 1
                this.keyword = null
                this.formData = {}
                this.getData()
            } else {
                this.autoCurrentLabel()
            }
        },
        //获取表格数据
        async getData() {
            this.loading = true
            const reqData = {
                [this.defaultProps.page]: this.currentPage,
                [this.defaultProps.pageSize]: this.pageSize,
                [this.defaultProps.keyword]: this.keyword,
            }
            Object.assign(reqData, this.params, this.formData)
            const res = await this.queryApi.post(reqData)
            const parseData = config.parseData(res)
            this.tableData = parseData.rows ?? []
            this.total = parseData.total
            this.loading = false
            //表格默认赋值
            await this.$nextTick(() => {
                if (this.multiple) {
                    this.defaultValue.forEach((row) => {
                        const setrow = this.tableData.filter((item) => item[this.defaultProps.value] === row[this.defaultProps.value])
                        if (setrow.length > 0) {
                            this.$refs.table.toggleRowSelection(setrow[0], true)
                        }
                    })
                } else {
                    const setrow = this.tableData.filter(
                        (item) => item[this.defaultProps.value] === (this.defaultValue ?? {})[this.defaultProps.value],
                    )
                    this.$refs.table.setCurrentRow(setrow[0])
                }
                this.$refs.table.setScrollTop(0)
            })
        },
        //插糟表单提交
        formSubmit() {
            this.currentPage = 1
            this.keyword = null
            this.getData()
        },
        //分页刷新表格
        reload() {
            this.getData()
        },
        //自动模拟options赋值
        autoCurrentLabel() {
            this.$nextTick(() => {
                if (this.multiple) {
                    this.$refs.select.states.selected.forEach((item) => {
                        item.currentLabel = item.value[this.defaultProps.label]
                    })
                } else {
                    this.$refs.select.states.selectedLabel = (this.defaultValue ?? {})[this.defaultProps.label]
                }
            })
        },
        //表格勾选事件
        select(rows, row) {
            const isSelect = rows.length && rows.indexOf(row) !== -1
            if (isSelect) {
                this.defaultValue.push(row)
            } else {
                this.defaultValue.splice(
                    this.defaultValue.findIndex((item) => item[this.defaultProps.value] === row[this.defaultProps.value]),
                    1,
                )
            }
            this.autoCurrentLabel()
            this.$emit('update:modelValue', this.defaultValue)
            this.$emit('change', this.defaultValue)
        },
        //表格全选事件
        selectAll(rows) {
            const isAllSelect = rows.length > 0
            if (isAllSelect) {
                rows.forEach((row) => {
                    const isHas = this.defaultValue.find((item) => item[this.defaultProps.value] === row[this.defaultProps.value])
                    if (!isHas) {
                        this.defaultValue.push(row)
                    }
                })
            } else {
                this.tableData.forEach((row) => {
                    const isHas = this.defaultValue.find((item) => item[this.defaultProps.value] === row[this.defaultProps.value])
                    if (isHas) {
                        this.defaultValue.splice(
                            this.defaultValue.findIndex((item) => item[this.defaultProps.value] === row[this.defaultProps.value]),
                            1,
                        )
                    }
                })
            }
            this.autoCurrentLabel()
            this.$emit('update:modelValue', this.defaultValue)
            this.$emit('change', this.defaultValue)
        },
        click(row) {
            if (this.multiple) {
                //处理多选点击行
            } else {
                this.defaultValue = row
                this.$refs.select.blur()
                this.autoCurrentLabel()
                this.$emit('update:modelValue', this.defaultValue)
                this.$emit('change', this.defaultValue)
            }
        },
        //tags删除后回调
        removeTag(tag) {
            const row = this.findRowByKey(tag[this.defaultProps.value])
            this.$refs.table.toggleRowSelection(row, false)
            this.$emit('update:modelValue', this.defaultValue)
        },
        //清空后的回调
        clear() {
            this.$emit('update:modelValue', this.defaultValue)
        },
        // 关键值查询表格数据行
        findRowByKey(value) {
            return this.tableData.find((item) => item[this.defaultProps.value] === value)
        },
        filterMethod(keyword) {
            if (!keyword) {
                this.keyword = null
                return false
            }
            this.keyword = keyword
            this.getData()
        },
        // 触发select隐藏
        blur() {
            this.$refs.select.blur()
        },
        // 触发select显示
        focus() {
            this.$refs.select.focus()
        },
    },
}
</script>

<style scoped>
.scTable-select__table {
    padding: 1rem;
}

.scTable-select__page {
    padding-top: 1rem;
}
</style>