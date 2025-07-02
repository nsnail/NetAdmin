<template>
    <div :style="{ height: height, width: width }" ref="scEcharts"></div>
</template>

<script>
import * as echarts from 'echarts'
import t from './echarts-theme-t.js'
const unwarp = (obj) => obj && (obj.__v_raw || obj.valueOf() || obj)

export default {
    ...echarts,
    name: 'scEcharts',
    props: {
        height: { type: String, default: '100%' },
        width: { type: String, default: '100%' },
        nodata: { type: Boolean, default: false },
        option: {
            type: Object,
            default: () => {},
        },
    },
    data() {
        return {
            isActivat: false,
            myChart: null,
        }
    },
    watch: {
        option: {
            deep: true,
            handler(v) {
                unwarp(this.myChart)?.setOption(v, { notMerge: true })
            },
        },
    },
    computed: {
        myOptions: function () {
            return this.option || {}
        },
    },
    activated() {
        if (!this.isActivat) {
            this.$nextTick(() => {
                this.myChart.resize()
            })
        }
    },
    deactivated() {
        this.isActivat = false
    },
    async mounted() {
        this.isActivat = true
        await this.$nextTick()
        echarts.registerTheme('t', await t.build())
        this.draw()
    },
    methods: {
        draw() {
            const myChart = echarts.init(this.$refs.scEcharts, 't')
            myChart.setOption(this.myOptions)
            this.myChart = myChart
            window.addEventListener('resize', () => myChart.resize())
        },
        resize(width) {
            this.myChart.resize({ width: width || 'auto' })
        },
    },
}
</script>