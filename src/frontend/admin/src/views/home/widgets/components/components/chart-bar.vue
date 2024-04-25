<template>
    <div v-loading="loading">
        <scEcharts :option="option" height="20rem"></scEcharts>
    </div>
</template>

<script>
import scEcharts from '@/components/scEcharts'

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
        this.option.xAxis.data = res[0].data.map((x) => {
            return x.timestamp
        })
        this.option.series = res.map((x) => {
            return { type: 'line', data: x.data.map((y) => y.value), symbol: 'none', areaStyle: {} }
        })
    },
    mounted() {},
}
</script>