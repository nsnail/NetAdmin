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
            <scSelect
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
            <naUserSelect
                v-else-if="item.type === 'user-select' && (!item.condition || item.condition())"
                v-model="form[item.field[0]][item.field[1]]"
                v-role="item.role || '*/*/*'"
                :class="item.class"
                :placeholder="item.placeholder"
                :style="item.style"
                clearable
                filterable />
        </template>

        <el-badge :hidden="!vue.query.dynamicFilter.filters?.length" :value="vue.query.dynamicFilter.filters?.length ?? 0">
            <el-button-group>
                <el-button @click="search" icon="el-icon-search" type="primary">{{ $t('查询') }}</el-button>
                <el-popover :title="$t('已应用的查询条件')" :width="popWidth" placement="bottom-end" trigger="hover">
                    <template #reference>
                        <el-button @click="reset" icon="el-icon-refresh-left">{{ $t('重置') }}</el-button>
                    </template>
                    <VAceEditor
                        v-model:value="aceEditorValue"
                        :theme="$TOOL.data.get('APP_SET_DARK') || $CONFIG.APP_SET_DARK ? 'github_dark' : 'github'"
                        lang="json"
                        style="height: 20rem; width: 100%" />
                    <p class="mt-4 flex gap05 items-center" style="justify-content: right">
                        <span class="text-right" style="width: 5rem">{{ $t('全局') }}</span>
                        <el-select v-model="this.aceEditorValue" :key="selectFilterKey" :teleported="false" style="flex-grow: 1">
                            <el-option
                                v-for="(item, i) in $TOOL.data.get('APP_SET_QUERY_FILTERS') || []"
                                :key="i"
                                :label="item.name"
                                :value="item.value" />
                        </el-select>
                        <el-button-group>
                            <el-button @click="saveFilter(true)" plain type="primary">{{ $t('保存') }}</el-button>
                            <el-button @click="delFilter(true)" plain>{{ $t('删除') }}</el-button>
                        </el-button-group>
                    </p>
                    <p class="mt-4 flex gap05 items-center" style="justify-content: right">
                        <span class="text-right" style="width: 5rem">{{ $t('本页') }}</span>
                        <el-select v-model="this.aceEditorValue" :key="selectFilterKey" :teleported="false" style="flex-grow: 1">
                            <el-option
                                v-for="(item, i) in $TOOL.data.get('APP_SET_QUERY_FILTERS_' + this.queryApi) || []"
                                :key="i"
                                :label="item.name"
                                :value="item.value" />
                        </el-select>
                        <el-button-group>
                            <el-button @click="saveFilter(false)" plain type="primary">{{ $t('保存') }}</el-button>
                            <el-button @click="delFilter(false)" plain>{{ $t('删除') }}</el-button>
                        </el-button-group>
                    </p>
                    <p class="mt-4 text-right">
                        <el-button @click="jsonFormat">{{ $t('JSON 格式化') }}</el-button>
                        <el-dropdown :teleported="false">
                            <el-button @click="reSearch" type="primary">{{ $t('重新查询') }}</el-button>
                            <template #dropdown>
                                <el-dropdown-menu>
                                    <el-dropdown-item @click="reSearch(5)">5s</el-dropdown-item>
                                    <el-dropdown-item @click="reSearch(10)">10s</el-dropdown-item>
                                    <el-dropdown-item @click="reSearch(15)">15s</el-dropdown-item>
                                    <el-dropdown-item @click="reSearch(30)">30s</el-dropdown-item>
                                    <el-dropdown-item @click="reSearch(60)">60s</el-dropdown-item>
                                    <el-dropdown-item @click="reSearch(120)">120s</el-dropdown-item>
                                </el-dropdown-menu>
                            </template>
                        </el-dropdown>
                    </p>
                </el-popover>
            </el-button-group>
        </el-badge>
    </form>
</template>
<style scoped></style>
<script>
import tool from '@/utils/tool'
import vkbeautify from 'vkbeautify/index'

import { defineAsyncComponent } from 'vue'
const naUserSelect = defineAsyncComponent(() => import('@/components/naUserSelect'))
const scSelect = defineAsyncComponent(() => import('@/components/scSelect'))
export default {
    components: {
        naUserSelect,
        scSelect,
    },
    emits: ['search', 'reset', 'reSearch'],
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
            autoResearchTimer: null,
            queryApi: null,
            popWidth: '40rem',
            selectFilterKey: 0,
            aceEditorValue: null,
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
                    text: this.$t('本周'),
                    value: () => {
                        // 获取当前日期对象
                        const currentDate = new Date()
                        currentDate.setHours(0, 0, 0, 0)

                        // 获取当前日期是本周的第几天（0代表周日，1代表周一，...，6代表周六）
                        const dayOfWeek = currentDate.getDay()

                        // 获取当前日期距离本周第一天的天数差（负数代表本周之前的天数，正数或零代表本周之后的天数）
                        const diffToFirstDay = dayOfWeek > 0 ? -(dayOfWeek - 1) : -6

                        // 获取本周第一天的日期对象
                        const firstDayOfWeek = new Date(currentDate)
                        firstDayOfWeek.setDate(currentDate.getDate() + diffToFirstDay)
                        return [firstDayOfWeek, new Date()]
                    },
                },
                {
                    text: this.$t('后退一周'),
                    value: () => {
                        try {
                            const start = new Date(this.form.dy[this.dateField][0])
                            const end = new Date(this.form.dy[this.dateField][1])
                            start.setDate(start.getDate() - 7)
                            end.setDate(end.getDate() - 7)
                            return [start, end]
                        } catch {}
                    },
                },
                {
                    text: this.$t('本月'),
                    value: () => {
                        const start = new Date()
                        start.setHours(0, 0, 0, 0)
                        start.setDate(1)
                        return [start, new Date()]
                    },
                },
                {
                    text: this.$t('后退一月'),
                    value: () => {
                        try {
                            const start = new Date(this.form.dy[this.dateField][0])
                            const end = new Date(this.form.dy[this.dateField][1])
                            return [
                                new Date(
                                    start.getMonth() === 0 ? start.getFullYear() - 1 : start.getFullYear(),
                                    start.getMonth() === 0 ? 11 : start.getMonth() - 1,
                                    start.getDate(),
                                    start.getHours(),
                                    start.getMinutes(),
                                    start.getSeconds(),
                                ),
                                new Date(
                                    end.getMonth() === 0 ? end.getFullYear() - 1 : end.getFullYear(),
                                    end.getMonth() === 0 ? 11 : end.getMonth() - 1,
                                    end.getDate(),
                                    end.getHours(),
                                    end.getMinutes(),
                                    end.getSeconds(),
                                ),
                            ]
                        } catch {}
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
    mounted() {
        this.queryApi = this.vue.$refs.table.queryApi.url.toUpperCase()
    },
    watch: {
        'vue.query': {
            immediate: true,
            deep: true,
            handler: function (o, n) {
                this.aceEditorValue = this.vkbeautify.json(n, 2)
            },
        },
    },
    async created() {
        if (document.body.clientWidth < 1000) {
            this.popWidth = '100%'
        }
        this.aceEditorValue = this.vkbeautify.json(this.vue.query, 2)
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
                    text: this.$t('最近整点'),
                    value: () => {
                        const start = new Date()
                        return [
                            new Date(start.getFullYear(), start.getMonth(), start.getDate(), start.getHours(), 0, 0),
                            new Date(start.getFullYear(), start.getMonth(), start.getDate(), start.getHours() + 1, 0, 0),
                        ]
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
        vkbeautify() {
            return vkbeautify
        },
    },
    methods: {
        jsonFormat() {
            try {
                this.aceEditorValue = vkbeautify.json(this.aceEditorValue, 2)
            } catch {
                this.$message.error(this.$t('格式错误'))
            }
        },
        async reSearch(sec) {
            const newParam = JSON.parse(this.aceEditorValue)
            this.vue.$refs.table.tableParams = newParam
            await this.vue.$refs.table.upData()
            await this.$nextTick()
            this.vue.$refs.table.tableParams = this.vue.query
            this.$emit('reSearch', newParam)

            if (typeof sec !== 'number') return
            const timerEl = document.getElementsByClassName('autoResearchTimer')[0]
            if (!timerEl) {
                this.$message({
                    showClose: true,
                    onClose: () => clearInterval(this.autoResearchTimer),
                    type: 'warning',
                    customClass: 'autoResearchTimer',
                    message: this.$t('{s} 秒后刷新...', { s: sec }),
                    duration: 0,
                })
                this.autoResearchTimer = setInterval(() => {
                    const el = document.getElementsByClassName('autoResearchTimer')[0].getElementsByClassName('el-message__content')[0]
                    let num = parseInt(/(\d+)/.exec(el.innerHTML)[0])
                    if (num === 1) {
                        this.reSearch()
                        num = sec + 1
                    }
                    el.innerHTML = el.innerHTML.replace(/\d+/, (num - 1).toString())
                }, 1000)
            }
        },
        async delFilter(isGlobal) {
            const key = isGlobal ? 'APP_SET_QUERY_FILTERS' : 'APP_SET_QUERY_FILTERS_' + this.queryApi
            let filters = this.$TOOL.data.get(key) || []
            filters = filters.filter((x) => x.value !== this.aceEditorValue)
            await this.$TOOL.data.set(key, filters)
            this.$message.success(this.$t('删除成功'))
            this.selectFilterKey = Math.random()
        },
        async saveFilter(isGlobal) {
            const key = isGlobal ? 'APP_SET_QUERY_FILTERS' : 'APP_SET_QUERY_FILTERS_' + this.queryApi
            try {
                const filterName = await this.$prompt('设置一个过滤器名称', '保存查询条件', {
                    inputPattern: /\S/,
                    inputErrorMessage: '名称不能为空',
                })
                let filters = this.$TOOL.data.get(key) || []
                filters = filters.filter((x) => x.name !== filterName.value)
                filters.push({ name: filterName.value, value: this.aceEditorValue })
                await this.$TOOL.data.set(key, filters)
                this.$message.success(this.$t('保存成功'))
            } catch {}
        },
        trimSpaces(key) {
            this.form[key][this.selectInputKey] = this.form[key][this.selectInputKey].replace(/^\s*(.*?)\s*$/g, '$1')
        },
        selectInputChange(item) {
            for (const field of item.field[1]) {
                delete this.form[item.field[0]][field.key]
            }
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