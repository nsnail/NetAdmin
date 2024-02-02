<template>
    <el-card :header="$t('当前已用量')" shadow="never">
        <el-progress :color="color" :format="format" :percentage="value" :stroke-width="20" :text-inside="true" />
        <el-row :gutter="15" style="margin-top: 20px">
            <el-col :lg="6">
                <el-card shadow="never">
                    <sc-statistic :title="$t('文档')" groupSeparator suffix="GB" value="7.41"></sc-statistic>
                </el-card>
            </el-col>
            <el-col :lg="6">
                <el-card shadow="never">
                    <sc-statistic :title="$t('图片')" groupSeparator suffix="GB" value="12.90"></sc-statistic>
                </el-card>
            </el-col>
            <el-col :lg="6">
                <el-card shadow="never">
                    <sc-statistic :title="$t('视音频')" groupSeparator suffix="MB" value="68.79"></sc-statistic>
                </el-card>
            </el-col>
            <el-col :lg="6">
                <el-card shadow="never">
                    <sc-statistic :title="$t('其他')" groupSeparator suffix="GB" value="17.58"></sc-statistic>
                </el-card>
            </el-col>
        </el-row>
    </el-card>
</template>

<script>
import scStatistic from '@/components/scStatistic'

export default {
    components: {
        scStatistic,
    },
    data() {
        return {
            value: 39.58,
            color: [
                { color: '#67C23A', percentage: 40 },
                { color: '#E6A23C', percentage: 60 },
                { color: '#F56C6C', percentage: 80 },
            ],
        }
    },
    methods: {
        format(percentage) {
            return percentage + 'G'
        },
    },
}
</script>

<style></style>