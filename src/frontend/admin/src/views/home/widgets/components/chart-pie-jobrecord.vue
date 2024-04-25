<template>
    <el-card :header="$t('作业分布')" shadow="never" style="height: 25rem">
        <chart-pie
            :api="[
                {
                    file: 'sys_job',
                    name: 'getRecordPieChartByName',
                    value: [tool.dateFormat(new Date(), 'yyyy-MM-dd'), tool.dateFormat(new Date(), 'yyyy-MM-dd')],
                    radius: ['70%', '100%'],
                },
                {
                    file: 'sys_job',
                    name: 'getRecordPieChartByHttpStatusCode',
                    value: [tool.dateFormat(new Date(), 'yyyy-MM-dd'), tool.dateFormat(new Date(), 'yyyy-MM-dd')],
                    radius: [0, '30%'],
                },
            ]"></chart-pie>
    </el-card>
</template>

<script>
import ChartPie from '@/views/home/widgets/components/components/chart-pie.vue'
import tool from '@/utils/tool'

export default {
    computed: {
        tool() {
            return tool
        },
    },
    title: '作业分布',
    icon: 'el-icon-data-line',
    description: '作业分布',
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