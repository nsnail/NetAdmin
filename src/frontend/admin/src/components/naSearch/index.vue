<template>
    <form @keyup.enter="search" @submit.prevent="search" class="right-panel-search">
        <el-date-picker
            v-if="hasDate"
            v-model="form.dy[this.dateField]"
            :end-placeholder="$t('结束日期')"
            :format="dateFormat"
            :range-separator="$t('至')"
            :shortcuts="dateShortCuts"
            :start-placeholder="$t('开始日期')"
            :teleported="false"
            :type="dateType"
            :value-format="dateValueFormat"></el-date-picker>

        <template v-for="(item, i) in controls" :key="i">
            <el-input
                v-if="item.type === 'input' && (!item.condition || item.condition())"
                v-model="form[item.field[0]][item.field[1]]"
                v-role="item.role || '*/*/*'"
                :class="item.class"
                :placeholder="item.placeholder"
                :style="item.style"
                clearable />
            <el-input
                v-if="item.type === 'select-input' && (!item.condition || item.condition())"
                v-model="form[item.field[0]][selectInputKey]"
                v-role="item.role || '*/*/*'"
                :class="item.class"
                :placeholder="item.placeholder"
                :style="item.style"
                @change="trimSpaces(item.field[0])"
                clearable>
                <template #prepend>
                    <el-select v-model="selectInputKey" :placeholder="$t('查询字段')" :style="item.selectStyle" @change="selectInputChange(item)">
                        <el-option v-for="(field, j) in item.field[1]" :label="field.label" :value="field.key" />
                    </el-select>
                </template>
            </el-input>
            <sc-select
                v-else-if="item.type === 'remote-select' && (!item.condition || item.condition())"
                v-model="form[item.field[0]][item.field[1]]"
                v-role="item.role || '*/*/*'"
                :class="item.class"
                :config="item.config"
                :placeholder="item.placeholder"
                :query-api="item.api"
                :style="item.style"
                clearable
                filterable />
            <el-select
                v-else-if="item.type === 'select' && (!item.condition || item.condition())"
                v-model="form[item.field[0]][item.field[1]]"
                v-role="item.role || '*/*/*'"
                :class="item.class"
                :multiple="item.multiple === true"
                :placeholder="item.placeholder"
                :style="item.style"
                clearable
                collapse-tags
                filterable>
                <el-option v-for="(option, j) in item.options" :key="j" :label="option.label" :value="option.value" />
            </el-select>
            <el-cascader
                v-else-if="casLoaded && item.type === 'cascader' && (!item.condition || item.condition())"
                v-model="form[item.field[0]][item.field[1]]"
                v-role="item.role || '*/*/*'"
                :class="item.class"
                :options="item.options"
                :placeholder="item.placeholder"
                :props="item.props"
                :style="item.style"
                clearable
                filterable />
        </template>

        <el-badge :hidden="vue.query.dynamicFilter.filters.length === 0" :value="vue.query.dynamicFilter.filters.length">
            <el-button-group>
                <el-button @click="search" icon="el-icon-search" type="primary">{{ $t('查询') }}</el-button>
                <el-popover :title="$t('已应用的查询条件')" placement="bottom-end" trigger="hover" width="40rem">
                    <template #reference>
                        <el-button @click="reset" icon="el-icon-refresh-left">{{ $t('重置') }}</el-button>
                    </template>
                    <v-ace-editor
                        :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'github_dark' : 'github'"
                        :value="vkbeautify().json(vue.query, 2)"
                        lang="json"
                        style="height: 20rem; width: 100%" />
                </el-popover>
            </el-button-group>
        </el-badge>
    </form>
</template>
<style scoped></style>
<script>
import tool from '@/utils/tool'
import vkbeautify from 'vkbeautify/index'

export default {
    emits: ['search', 'reset'],
    props: {
        dateField: { type: String, default: 'createdTime' },
        hasDate: { type: Boolean, default: true },
        dateType: { type: String, default: 'daterange' },
        dateFormat: { type: String, default: 'YYYY-MM-DD' },
        dateValueFormat: { type: String, default: 'YYYY-MM-DD' },
        controls: { type: Array },
        vue: { type: Object },
    },
    data() {
        return {
            selectInputKey: null,
            dateShortCuts: [
                {
                    text: this.$t('今日'),
                    value: () => {
                        const start = new Date()
                        start.setHours(0, 0, 0, 0)
                        return [start, start]
                    },
                },
                {
                    text: this.$t('昨日'),
                    value: () => {
                        const start = new Date()
                        start.setHours(0, 0, 0, 0)
                        start.setTime(start.getTime() - 3600 * 1000 * 24)
                        return [start, start]
                    },
                },
                {
                    text: this.$t('后退一日'),
                    value: () => {
                        try {
                            const start = new Date(new Date(this.form.dy[this.dateField][0]) - 3600 * 1000 * 24)
                            const end = new Date(new Date(this.form.dy[this.dateField][1]) - 3600 * 1000 * 24)
                            return [start, end]
                        } catch {}
                    },
                },
                {
                    text: this.$t('最近三日'),
                    value: () => {
                        const start = new Date()
                        start.setHours(0, 0, 0, 0)
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 2)
                        const end = new Date()
                        end.setHours(0, 0, 0, 0)
                        return [start, end]
                    },
                },
                {
                    text: this.$t('最近一周'),
                    value: () => {
                        const start = new Date()
                        start.setHours(0, 0, 0, 0)
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 6)
                        const end = new Date()
                        end.setHours(0, 0, 0, 0)
                        return [start, end]
                    },
                },
                {
                    text: this.$t('最近一月'),
                    value: () => {
                        const start = new Date()
                        start.setHours(0, 0, 0, 0)
                        start.setMonth(start.getMonth() - 1)
                        const end = new Date()
                        end.setHours(0, 0, 0, 0)
                        return [start, end]
                    },
                },
                {
                    text: this.$t('最近三月'),
                    value: () => {
                        const start = new Date()
                        start.setHours(0, 0, 0, 0)
                        start.setMonth(start.getMonth() - 3)
                        const end = new Date()
                        end.setHours(0, 0, 0, 0)
                        return [start, end]
                    },
                },
                {
                    text: this.$t('最近六月'),
                    value: () => {
                        const start = new Date()
                        start.setHours(0, 0, 0, 0)
                        start.setMonth(start.getMonth() - 6)
                        const end = new Date()
                        end.setHours(0, 0, 0, 0)
                        return [start, end]
                    },
                },
                {
                    text: this.$t('最近一年'),
                    value: () => {
                        const start = new Date()
                        start.setHours(0, 0, 0, 0)
                        start.setFullYear(start.getFullYear() - 1)
                        const end = new Date()
                        end.setHours(0, 0, 0, 0)
                        return [start, end]
                    },
                },
            ],
            options: [
                {
                    label: '订单号',
                    value: 'id',
                    type: 'text',
                    selected: true,
                    placeholder: this.$t('请输入订单号'),
                },
                {
                    label: '类型',
                    value: 'type',
                    type: 'select',
                    operator: '=',
                    selected: true,
                    placeholder: this.$t('请选择类型'),
                    extend: {
                        data: [
                            {
                                label: '选项1',
                                value: '1',
                            },
                            {
                                label: '选项2',
                                value: '2',
                            },
                        ],
                    },
                },
                {
                    label: '类型(多选)',
                    value: 'type2',
                    type: 'select',
                    operator: '=',
                    placeholder: this.$t('请选择类型'),
                    extend: {
                        multiple: true,
                        data: [
                            {
                                label: '选项1',
                                value: '1',
                            },
                            {
                                label: '选项2',
                                value: '2',
                            },
                        ],
                    },
                },
                {
                    label: '通知(异步)',
                    value: 'noticeType',
                    type: 'select',
                    operator: '=',
                    placeholder: this.$t('请选择通知类型'),
                    extend: {
                        request: async () => {
                            const list = await this.$API.system.dic.get.get()
                            return list.data.map((item) => {
                                return {
                                    label: item.label,
                                    value: item.value,
                                }
                            })
                        },
                    },
                },
                {
                    label: '通知(远程搜索)',
                    value: 'noticeType2',
                    type: 'select',
                    operator: '=',
                    placeholder: this.$t('请输入关键词后检索'),
                    extend: {
                        remote: true,
                        request: async (query) => {
                            const data = {
                                keyword: query,
                            }
                            const list = await this.$API.system.dic.get.get(data)
                            return list.data.map((item) => {
                                return {
                                    label: item.label,
                                    value: item.value,
                                }
                            })
                        },
                    },
                },
                {
                    label: '关键词(标签)',
                    value: 'tags',
                    type: 'tags',
                    operator: 'include',
                    operators: [
                        {
                            label: '包含',
                            value: 'include',
                        },
                        {
                            label: '不包含',
                            value: 'notinclude',
                        },
                    ],
                },
                {
                    label: '开关(可重复)',
                    value: 'switch',
                    type: 'switch',
                    repeat: true,
                    operator: '=',
                },
                {
                    label: '日期单选',
                    value: 'date',
                    type: 'date',
                    operator: '=',
                    operators: [
                        {
                            label: '等于',
                            value: '=',
                        },
                        {
                            label: '不等于',
                            value: '!=',
                        },
                    ],
                },
                {
                    label: '日期范围',
                    value: 'date2',
                    type: 'daterange',
                },
                {
                    label: '自定义日期',
                    value: 'date3',
                    type: 'customDate',
                    placeholder: this.$t('请选择月份'),
                    extend: {
                        dateType: 'month',
                        valueFormat: 'YYYY-MM',
                    },
                },
            ],
            casLoaded: false,
            keeps: [],
            form: {
                root: {},
                filter: {},
                dy: {},
            },
        }
    },
    mounted() {},
    async created() {
        this.selectInputKey = this.controls.find((x) => x.type === 'select-input')?.field[1][0].key
        if (this.dateType === 'datetimerange') {
            this.dateShortCuts.unshift(
                {
                    text: this.$t('最近一时'),
                    value: () => {
                        const start = new Date()
                        const end = new Date()
                        start.setTime(start.getTime() - 3600 * 1000)
                        return [start, end]
                    },
                },
                {
                    text: this.$t('后退一时'),
                    value: () => {
                        try {
                            const start = new Date(new Date(this.form.dy[this.dateField][0]) - 3600 * 1000)
                            const end = new Date(new Date(this.form.dy[this.dateField][1]) - 3600 * 1000)
                            return [start, end]
                        } catch {}
                    },
                },
                {
                    text: this.$t('昨日此时'),
                    value: () => {
                        return [
                            new Date(new Date(new Date().getTime() - 86400 * 1000).setHours(0, 0, 0, 0)),
                            new Date(new Date().getTime() - 86400 * 1000),
                        ]
                    },
                },
            )
        }
        this.controls
            .filter((x) => x.type === 'cascader')
            .forEach((x) => {
                x.options = x.api.post()
            })
        for (const y of this.controls.filter((x) => x.type === 'cascader')) {
            y.options = (await y.options).data
        }
        this.casLoaded = true
    },
    computed: {
        tool() {
            return tool
        },
    },
    methods: {
        trimSpaces(key) {
            this.form[key][this.selectInputKey] = this.form[key][this.selectInputKey].replace(/^\s*(.*?)\s*$/g, '$1')
        },
        selectInputChange(item) {
            for (const field of item.field[1]) {
                delete this.form[item.field[0]][field.key]
            }
        },
        vkbeautify() {
            return vkbeautify
        },
        search() {
            const parentQuery = this.clearParentQuery()
            Object.assign(parentQuery, this.form.root || {})
            Object.assign(parentQuery.filter, this.form.filter || {})
            this.$emit('search', this.form)
        },
        clearParentQuery() {
            const parentQuery = this.vue.query
            Object.keys(parentQuery).forEach((x) => {
                if (!['dynamicFilter', 'filter'].includes(x)) delete parentQuery[x]
            })
            parentQuery.filter = {}
            parentQuery.dynamicFilter.filters = []
            return parentQuery
        },
        reset() {
            Object.keys(this.form.root).forEach((x) => {
                delete this.form.root[x]
            })
            Object.keys(this.form.filter).forEach((x) => {
                delete this.form.filter[x]
            })
            Object.keys(this.form.dy).forEach((x) => {
                delete this.form.dy[x]
            })
            for (const keep of this.keeps) {
                this.form[keep.type][keep.field] = keep.value
            }
            this.$emit('reset')
            this.search()
        },
    },
}
</script>