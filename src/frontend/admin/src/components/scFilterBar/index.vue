<!--
 * @Descripttion: 过滤器V2
 * @version: 2.6
 * @Author: sakuya
 * @Date: 2021年7月30日14:48:41
 * @LastEditors: Xujianchen
 * @LastEditTime: 2023-03-19 11:45:18
-->

<template>
    <div class="sc-filterBar">
        <slot :filterLength="filterObjLength" :openFilter="openFilter">
            <el-badge :hidden="filterObjLength <= 0" :value="filterObjLength" type="danger">
                <el-button @click="openFilter" icon="el-icon-filter"></el-button>
            </el-badge>
        </slot>

        <el-drawer v-model="drawer" :size="650" :title="$t('过滤器')" append-to-body>
            <el-container v-loading="saveLoading">
                <el-main style="padding: 0">
                    <el-tabs class="root">
                        <el-tab-pane lazy>
                            <template #label>
                                <div class="tabs-label">{{ $t('过滤项') }}</div>
                            </template>
                            <el-scrollbar>
                                <div class="sc-filter-main">
                                    <h2>{{ $t('设置过滤条件') }}</h2>
                                    <div v-if="filter.length <= 0" class="nodata">{{ $t('没有默认过滤条件，请点击增加过滤项') }}</div>
                                    <table v-else>
                                        <colgroup>
                                            <col width="50" />
                                            <col width="140" />
                                            <col v-if="showOperator" width="120" />
                                            <col />
                                            <col width="40" />
                                        </colgroup>
                                        <tr v-for="(item, index) in filter" :key="index">
                                            <td>
                                                <el-tag :disable-transitions="true">{{ index + 1 }}</el-tag>
                                            </td>
                                            <td>
                                                <py-select
                                                    v-model="item.field"
                                                    :filter="filter"
                                                    :options="fields"
                                                    :placeholder="$t('过滤字段')"
                                                    @change="fieldChange(item)"
                                                    filterable>
                                                </py-select>
                                            </td>
                                            <td v-if="showOperator">
                                                <el-select v-model="item.operator" :placeholder="$t('运算符')">
                                                    <el-option
                                                        v-for="ope in item.field.operators || operator"
                                                        :key="ope.value"
                                                        :label="ope.label"
                                                        :value="ope.value"></el-option>
                                                </el-select>
                                            </td>
                                            <td>
                                                <el-input
                                                    v-if="!item.field.type"
                                                    v-model="item.value"
                                                    :placeholder="$t('请选择过滤字段')"
                                                    disabled></el-input>
                                                <!-- 输入框 -->
                                                <el-input
                                                    v-if="item.field.type === 'text'"
                                                    v-model="item.value"
                                                    :placeholder="item.field.placeholder || '请输入'"></el-input>
                                                <!-- 下拉框 -->
                                                <el-select
                                                    v-if="item.field.type === 'select'"
                                                    v-model="item.value"
                                                    :loading="item.selectLoading"
                                                    :multiple="item.field.extend.multiple"
                                                    :placeholder="item.field.placeholder || '请选择'"
                                                    :remote="item.field.extend.remote"
                                                    :remote-method="
                                                        (query) => {
                                                            remoteMethod(query, item)
                                                        }
                                                    "
                                                    @visible-change="visibleChange($event, item)"
                                                    filterable>
                                                    <el-option
                                                        v-for="field in item.field.extend.data"
                                                        :key="field.value"
                                                        :label="field.label"
                                                        :value="field.value"></el-option>
                                                </el-select>
                                                <!-- 日期 -->
                                                <el-date-picker
                                                    v-if="item.field.type === 'date'"
                                                    v-model="item.value"
                                                    :placeholder="item.field.placeholder || '请选择日期'"
                                                    style="width: 100%"
                                                    type="date"
                                                    value-format="YYYY-MM-DD"></el-date-picker>
                                                <!-- 日期范围 -->
                                                <el-date-picker
                                                    v-if="item.field.type === 'daterange'"
                                                    v-model="item.value"
                                                    end-placeholder="$t('结束日期')"
                                                    start-placeholder="$t('开始日期')"
                                                    style="width: 100%"
                                                    type="daterange"
                                                    value-format="YYYY-MM-DD"></el-date-picker>
                                                <!-- 日期时间 -->
                                                <el-date-picker
                                                    v-if="item.field.type === 'datetime'"
                                                    v-model="item.value"
                                                    :placeholder="item.field.placeholder || '请选择日期'"
                                                    style="width: 100%"
                                                    type="datetime"
                                                    value-format="YYYY-MM-DD HH:mm:ss"></el-date-picker>
                                                <!-- 日期时间范围 -->
                                                <el-date-picker
                                                    v-if="item.field.type === 'datetimerange'"
                                                    v-model="item.value"
                                                    end-placeholder="$t('结束日期')"
                                                    start-placeholder="$t('开始日期')"
                                                    style="width: 100%"
                                                    type="datetimerange"
                                                    value-format="YYYY-MM-DD HH:mm:ss"></el-date-picker>
                                                <!-- 自定义日期 -->
                                                <el-date-picker
                                                    v-if="item.field.type === 'customDate'"
                                                    v-model="item.value"
                                                    :placeholder="item.field.placeholder || '请选择'"
                                                    :type="item.field.extend.dateType || 'date'"
                                                    :value-format="item.field.extend.valueFormat"
                                                    end-placeholder="$t('结束日期')"
                                                    start-placeholder="$t('开始日期')"
                                                    style="width: 100%"></el-date-picker>
                                                <!-- 开关 -->
                                                <el-switch
                                                    v-if="item.field.type === 'switch'"
                                                    v-model="item.value"
                                                    active-value="1"
                                                    inactive-value="0"></el-switch>
                                                <!-- 标签 -->
                                                <el-select
                                                    v-if="item.field.type === 'tags'"
                                                    v-model="item.value"
                                                    :placeholder="item.field.placeholder || '请输入'"
                                                    allow-create
                                                    default-first-option
                                                    filterable
                                                    multiple
                                                    no-data-text="$t('输入关键词后按回车确认')"></el-select>
                                            </td>
                                            <td>
                                                <el-icon @click="delFilter(index)" class="del">
                                                    <el-icon-delete />
                                                </el-icon>
                                            </td>
                                        </tr>
                                    </table>
                                    <el-button @click="addFilter" icon="el-icon-plus" text type="primary">{{ $t('增加过滤项') }}</el-button>
                                </div>
                            </el-scrollbar>
                        </el-tab-pane>
                        <el-tab-pane lazy>
                            <template #label>
                                <div class="tabs-label">{{ $t('常用') }}</div>
                            </template>
                            <el-scrollbar>
                                <my :data="myFilter" :filterName="filterName" @selectMyfilter="selectMyfilter" ref="my"></my>
                            </el-scrollbar>
                        </el-tab-pane>
                    </el-tabs>
                </el-main>
                <el-footer>
                    <el-button :disabled="filter.length <= 0" @click="ok" type="primary">{{ $t('立即过滤') }}</el-button>
                    <el-button :disabled="filter.length <= 0" @click="saveMy" plain type="primary">{{ $t('另存为常用') }}</el-button>
                    <el-button @click="clear">{{ $t('清空过滤') }}</el-button>
                </el-footer>
            </el-container>
        </el-drawer>
    </div>
</template>

<script>
import config from '@/config/filterBar'
import pySelect from './pySelect'
import my from './my'

export default {
    name: 'filterBar',
    components: {
        pySelect,
        my,
    },
    props: {
        filterName: { type: String, default: '' },
        showOperator: { type: Boolean, default: true },
        options: {
            type: Object,
            default: () => {},
        },
    },
    emits: ['filterChange'],
    data() {
        return {
            drawer: false,
            operator: config.operator,
            fields: this.options,
            filter: [],
            myFilter: [],
            filterObjLength: 0,
            saveLoading: false,
        }
    },
    computed: {
        filterObj() {
            const obj = {}
            this.filter.forEach((item) => {
                obj[item.field.value] = this.showOperator ? `${item.value}${config.separator}${item.operator}` : `${item.value}`
            })
            return obj
        },
    },
    mounted() {
        //默认显示的过滤项
        this.fields.forEach((item) => {
            if (item.selected) {
                this.filter.push({
                    field: item,
                    operator: item.operator || 'include',
                    value: '',
                })
            }
        })
    },
    methods: {
        //打开过滤器
        openFilter() {
            this.drawer = true
        },
        //增加过滤项
        addFilter() {
            //下一组新增过滤
            const filterArr = this.fields.filter((field) => !this.filter.some((item) => field.value === item.field.value && !item.field.repeat))
            if (this.fields.length <= 0 || filterArr.length <= 0) {
                this.$message.warning('无过滤项')
                return false
            }
            const filterNum = filterArr[0]
            this.filter.push({
                field: filterNum,
                operator: filterNum.operator || 'include',
                value: '',
            })
        },
        //删除过滤项
        delFilter(index) {
            this.filter.splice(index, 1)
        },
        //过滤项字段变更事件
        fieldChange(tr) {
            let oldType = tr.field.type
            tr.field.type = ''
            this.$nextTick(() => {
                tr.field.type = oldType
            })
            tr.operator = tr.field.operator || 'include'
            tr.value = ''
        },
        //下拉框显示事件处理异步
        async visibleChange(isopen, item) {
            if (isopen && item.field.extend.request && !item.field.extend.remote) {
                item.selectLoading = true
                try {
                    const data = await item.field.extend.request()
                } catch (error) {
                    console.log(error)
                }
                item.field.extend.data = data
                item.selectLoading = false
            }
        },
        //下拉框显示事件处理异步搜索
        async remoteMethod(query, item) {
            if (!item.field.extend.request) {
                return false
            }
            if (query !== '') {
                item.selectLoading = true
                try {
                    const data = await item.field.extend.request(query)
                } catch (error) {
                    console.log(error)
                }
                item.field.extend.data = data
                item.selectLoading = false
            } else {
                item.field.extend.data = []
            }
        },
        //选择常用过滤
        selectMyfilter(item) {
            //常用过滤回显当前过滤项
            this.filter = []
            this.fields.forEach((field) => {
                const filterValue = item.filterObj[field.value]
                if (filterValue) {
                    const operator = filterValue.split('|')[1]
                    let value = filterValue.split('|')[0]
                    if (field.type === 'select' && field.extend.multiple) {
                        value = value.split(',')
                    } else if (field.type === 'daterange') {
                        value = value.split(',')
                    }
                    this.filter.push({
                        field: field,
                        operator: operator,
                        value: value,
                    })
                }
            })
            this.filterObjLength = Object.keys(item.filterObj).length
            this.$emit('filterChange', item.filterObj)
            this.drawer = false
        },
        //立即过滤
        ok() {
            this.filterObjLength = this.filter.length
            this.$emit('filterChange', this.filterObj)
            this.drawer = false
        },
        //保存常用
        saveMy() {
            this.$prompt('常用过滤名称', '另存为常用', {
                inputplaceholder: $t('请输入识别度较高的常用过滤名称'),
                inputPattern: /\S/,
                inputErrorMessage: '名称不能为空',
            })
                .then(async ({ value }) => {
                    this.saveLoading = true
                    const saveObj = {
                        title: value,
                        filterObj: this.filterObj,
                    }
                    try {
                        const save = await config.saveMy(this.filterName, saveObj)
                    } catch (error) {
                        this.saveLoading = false
                        console.log(error)
                        return false
                    }
                    if (!save) {
                        return false
                    }

                    this.myFilter.push(saveObj)
                    this.$message.success(`${this.filterName} 保存常用成功`)
                    this.saveLoading = false
                })
                .catch(() => {
                    //
                })
        },
        //清空过滤
        clear() {
            this.filter = []
            this.filterObjLength = 0
            this.$emit('filterChange', this.filterObj)
        },
    },
}
</script>

<style scoped>
.tabs-label {
    padding: 0 20px;
}

.nodata {
    height: 46px;
    line-height: 46px;
    margin: 1rem 0;
    border: 1px dashed #e6e6e6;
    color: #999;
    text-align: center;
    border-radius: 3px;
}

.sc-filter-main {
    padding: 20px;
    border-bottom: 1px solid #e6e6e6;
    background: #fff;
}

.sc-filter-main h2 {
    font-size: 0.9rem;
    color: #999;
    font-weight: normal;
}

.sc-filter-main table {
    width: 100%;
    margin: 1rem 0;
}

.sc-filter-main table td {
    padding: 0.4rem 10px 5px 0;
}

.sc-filter-main table td:deep(.el-input .el-input__inner) {
    vertical-align: top;
}

.sc-filter-main table td .el-select {
    display: block;
}

.sc-filter-main table td .el-date-editor.el-input {
    display: block;
    width: 100%;
}

.sc-filter-main table td .del {
    background: #fff;
    color: #999;
    width: 32px;
    height: 32px;
    line-height: 32px;
    text-align: center;
    border-radius: 50%;
    font-size: 0.9rem;
    cursor: pointer;
}

.sc-filter-main table td .del:hover {
    background: #f56c6c;
    color: #fff;
}

.root {
    display: flex;
    height: 100%;
    flex-direction: column;
}

.root:deep(.el-tabs__header) {
    margin: 0;
}

.root:deep(.el-tabs__content) {
    flex: 1;
    background: #f6f8f9;
}

.root:deep(.el-tabs__content) .el-tab-pane {
    overflow: auto;
    height: 100%;
}

.dark .root:deep(.el-tabs__content) {
    background: var(--el-bg-color-overlay);
}

.dark .sc-filter-main {
    background: var(--el-bg-color);
    border-color: var(--el-border-color-light);
}

.dark .sc-filter-main table td .del {
    background: none;
}

.dark .sc-filter-main table td .del:hover {
    background: #f56c6c;
}

.dark .nodata {
    border-color: var(--el-border-color-light);
}
</style>