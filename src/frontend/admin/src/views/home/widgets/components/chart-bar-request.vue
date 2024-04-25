<template>
    <el-card :header="$t('访问趋势')" shadow="never" style="height: 25rem">
        <chart-bar
            :api="[
                {
                    file: 'sys_log',
                    name: 'getBarChart',
                    value: [tool.dateFormat(new Date(), 'yyyy-MM-dd'), tool.dateFormat(new Date(), 'yyyy-MM-dd')],
                },
                {
                    file: 'sys_log',
                    name: 'getBarChart',
                    value: [
                        tool.dateFormat(new Date().setDate(new Date().getDate() - 1), 'yyyy-MM-dd'),
                        tool.dateFormat(new Date().setDate(new Date().getDate() - 1), 'yyyy-MM-dd'),
                    ],
                },
            ]"></chart-bar>
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
    title: '访问趋势',
    icon: 'el-icon-data-line',
    description: '访问趋势',
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