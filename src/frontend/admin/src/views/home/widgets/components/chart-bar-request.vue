<template>
    <el-card :header="$t('流量趋势(Today)')" shadow="never" style="height: 25rem">
        <chart-bar
            :api="[
                {
                    file: 'sys_requestlog',
                    name: 'getBarChart',
                    label: '今日',
                    value: ['DateTime.Now.Date', 'DateTime.Now.Date.AddDays(1)'],
                },
                {
                    file: 'sys_requestlog',
                    name: 'getBarChart',
                    label: '昨日',
                    value: ['DateTime.Now.Date.AddDays(-1)', 'DateTime.Now.Date'],
                },
            ]"
            height="20rem" />
    </el-card>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const chartBar = defineAsyncComponent(() => import('./components/chart-bar'))
import tool from '@/utils/tool'

export default {
    computed: {
        tool() {
            return tool
        },
    },
    title: '流量趋势(Today)',
    icon: 'el-icon-data-line',
    description: '流量趋势(Today)',
    components: {
        chartBar,
    },
    data() {
        return {}
    },
    async created() {},
    mounted() {},
}
</script>