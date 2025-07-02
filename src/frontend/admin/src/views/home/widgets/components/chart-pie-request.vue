<template>
    <el-card :header="$t('流量分布(Today)')" shadow="never" style="height: 25rem">
        <chart-pie
            :api="[
                {
                    file: 'sys_requestlog',
                    name: 'getPieChartByApiSummary',
                    value: [tool.dateFormat(new Date(), 'yyyy-MM-dd'), tool.dateFormat(new Date(), 'yyyy-MM-dd')],
                    radius: ['70%', '100%'],
                },
                {
                    file: 'sys_requestlog',
                    name: 'getPieChartByHttpStatusCode',
                    value: [tool.dateFormat(new Date(), 'yyyy-MM-dd'), tool.dateFormat(new Date(), 'yyyy-MM-dd')],
                    radius: [0, '30%'],
                },
            ]"
            height="20rem" />
    </el-card>
</template>

<script>
import ChartPie from '@/views/home/widgets/components/components/chart-pie'
import tool from '@/utils/tool'

export default {
    computed: {
        tool() {
            return tool
        },
    },
    title: '流量分布(Today)',
    icon: 'el-icon-data-line',
    description: '流量分布(Today)',
    components: {
        ChartPie,
    },
    data() {
        return {}
    },
    async created() {},
    mounted() {},
}
</script>