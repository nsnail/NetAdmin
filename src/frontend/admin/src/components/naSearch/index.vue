<template>
    <form class="right-panel-search" @keyup.enter="search" @submit.prevent="search">
        <el-date-picker
            v-if="hasDate"
            v-model="form.dy.createdTime"
            :shortcuts="[
                {
                    text: '今天',
                    value: () => {
                        const start = new Date()
                        return [start, start]
                    },
                },
                {
                    text: '昨天',
                    value: () => {
                        const start = new Date()
                        start.setTime(new Date().getTime() - 3600 * 1000 * 24)
                        return [start, start]
                    },
                },
                {
                    text: '最近三天',
                    value: () => {
                        const start = new Date()
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 3)
                        return [start, new Date()]
                    },
                },
                {
                    text: '最近一周',
                    value: () => {
                        const start = new Date()
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 7)
                        return [start, new Date()]
                    },
                },
                {
                    text: '最近一月',
                    value: () => {
                        const start = new Date()
                        start.setMonth(start.getMonth() - 1)
                        return [start, new Date()]
                    },
                },
                {
                    text: '最近三月',
                    value: () => {
                        const start = new Date()
                        start.setMonth(start.getMonth() - 3)
                        return [start, new Date()]
                    },
                },
                {
                    text: '最近六月',
                    value: () => {
                        const start = new Date()
                        start.setMonth(start.getMonth() - 6)
                        return [start, new Date()]
                    },
                },
                {
                    text: '最近一年',
                    value: () => {
                        const start = new Date()
                        start.setFullYear(start.getFullYear() - 1)
                        return [start, new Date()]
                    },
                },
            ]"
            :teleported="false"
            :end-placeholder="$t('结束日期')"
            :range-separator="$t('至')"
            :start-placeholder="$t('开始日期')"
            type="daterange"
            value-format="YYYY-MM-DD"></el-date-picker>

        <template v-for="(item, i) in controls" :key="i">
            <el-input
                v-if="item.type === 'input' && (!item.condition || item.condition())"
                v-model="form[item.field[0]][item.field[1]]"
                :class="item.class"
                :placeholder="item.placeholder"
                :style="item.style"
                clearable />
            <sc-select
                v-else-if="item.type === 'remote-select' && (!item.condition || item.condition())"
                v-model="form[item.field[0]][item.field[1]]"
                :apiObj="item.api"
                :class="item.class"
                :config="item.config"
                :placeholder="item.placeholder"
                :style="item.style"
                clearable
                filterable />
            <el-select
                v-else-if="item.type === 'select' && (!item.condition || item.condition())"
                v-model="form[item.field[0]][item.field[1]]"
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
                :class="item.class"
                :options="item.options"
                :placeholder="item.placeholder"
                :props="item.props"
                :style="item.style"
                clearable
                filterable />
        </template>

        <el-button-group>
            <el-button icon="el-icon-search" type="primary" @click="search">查询</el-button>
            <el-button icon="el-icon-refresh-left" @click="tool.refreshTab(vue)">重置</el-button>
        </el-button-group>
    </form>
</template>
<style scoped></style>
<script>
import tool from '@/utils/tool'

export default {
    emits: ['search'],
    props: {
        hasDate: { type: Boolean, default: true },
        controls: { type: Array },
        vue: { type: Object },
    },
    data() {
        return {
            casLoaded: false,
            form: {
                root: {},
                filter: {},
                dy: {},
            },
        }
    },
    mounted() {},
    async created() {
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
    components: {},
    computed: {
        tool() {
            return tool
        },
    },
    methods: {
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

            this.search()
        },
    },
}
</script>