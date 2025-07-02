<template>
    <div v-loading="loading">
        <sc-echarts v-bind="$attrs" :option="option" />
    </div>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const scEcharts = defineAsyncComponent(() => import('@/components/sc-echarts'))

export default {
    components: {
        scEcharts,
    },
    props: ['api'],
    data() {
        return {
            loading: true,
            option: {
                tooltip: {
                    trigger: 'axis',
                },
                xAxis: {
                    type: 'category',
                    data: [],
                },
                yAxis: {
                    type: 'value',
                },
                series: [],
            },
        }
    },
    async created() {
        const res = await Promise.all(
            this.api.map((x) => {
                return this.$API[x.file][x.name].post({
                    dynamicFilter: {
                        field: 'createdTime',
                        operator: 'dateRange',
                        value: x.value,
                    },
                })
            }),
        )
        this.loading = false
        this.option.xAxis.data = res[res.length - 1].data.map((x) => {
            return x.timestamp
        })
        let i = 0
        this.option.series = res.map((x) => {
            return { name: this.api[i++].label, type: 'line', data: x.data.map((y) => y.value), symbol: 'none', areaStyle: {} }
        })
    },
    mounted() {},
}
</script>