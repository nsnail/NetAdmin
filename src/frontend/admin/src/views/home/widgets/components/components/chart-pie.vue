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
                    trigger: 'item',
                    formatter: '{b}<br />{c}（{d}%）',
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
        let i = 0
        this.option.series = res.map((x) => {
            return {
                label: {
                    show: true,
                    alignTo: 'labelLine',
                    formatter: '{b} {d}%',
                },
                radius: this.api[i++].radius,
                type: 'pie',
                data: x.data.map((y) => {
                    return { name: y.key, value: y.value }
                }),
            }
        })
    },
    mounted() {},
}
</script>