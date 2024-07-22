<template>
    <el-card :header="$t('访问趋势(Today)')" shadow="never" style="height: 25rem">
        <chart-bar
            :api="[
                {
                    file: 'sys_log',
                    name: 'getBarChart',
                    label: '今日',
                    value: ['DateTime.Now.Date', 'DateTime.Now.Date.AddDays(1)'],
                },
                {
                    file: 'sys_log',
                    name: 'getBarChart',
                    label: '昨日',
                    value: ['DateTime.Now.Date.AddDays(-1)', 'DateTime.Now.Date'],
                },
            ]"
            height="20rem"></chart-bar>
    </el-card>
</template>

<script>
import ChartBar from '@/views/home/widgets/components/components/chart-bar.vue'
import tool from '@/utils/tool'

export default {
    computed: {
        tool() {
            return tool
        },
    },
    title: '访问趋势(Today)',
    icon: 'el-icon-data-line',
    description: '访问趋势(Today)',
    components: {
        ChartBar,
    },
    data() {
        return {}
    },
    async created() {},
    mounted() {},
}
</script>