<template>
    <el-container>
        <el-main>
            <el-card shadow="never" header="缓存统计">
                <el-row :gutter="15" style="margin-top: 20px;">
                    <el-col :lg="6">
                        <el-card shadow="never">
                            <sc-statistic title="缓存数量" :value="statistics.currentEntryCount"
                                          groupSeparator></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="6">
                        <el-card shadow="never">
                            <sc-statistic title="缓存大小" :value="statistics.currentEstimatedSize"
                                          groupSeparator></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="6">
                        <el-card shadow="never">
                            <sc-statistic title="缓存命中" :value="statistics.totalHits" groupSeparator></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="6">
                        <el-card shadow="never">
                            <sc-statistic title="缓存穿透" :value="statistics.totalMisses"
                                          groupSeparator></sc-statistic>
                        </el-card>
                    </el-col>
                </el-row>
            </el-card>

            <el-card shadow="never" header="缓存管理" style="margin-top: 20px;">
                <el-row :gutter="15" style="margin-top: 20px;">
                    <el-col :lg="6">
                        <el-popconfirm title="确定清空缓存吗？" @confirm="clearCache">
                            <template #reference>
                                <el-button type="primary" size="large" round>清空缓存</el-button>
                            </template>
                        </el-popconfirm>


                    </el-col>
                </el-row>
            </el-card>
        </el-main>
    </el-container>
</template>

<script>
import scStatistic from '@/components/scStatistic';

export default {
    components: {
        scStatistic
    },
    data() {
        return {
            statistics: {},
            value: 39.58,
            color: [
                {color: '#67C23A', percentage: 40},
                {color: '#E6A23C', percentage: 60},
                {color: '#F56C6C', percentage: 80},
            ]
        }
    },
    mounted() {
        this.cacheStatistics()
    },
    methods: {
        format(percentage) {
            return percentage + "G"
        },
        async cacheStatistics() {
            try {
                const res = await this.$API.sys_cache.cacheStatistics.post()
                this.statistics = res.data;

            } catch {

            }
        },
        async clearCache() {
            try {
                await this.$API.sys_cache.clear.post()
                this.$message.success('清空成功')
                this.cacheStatistics()

            } catch {

            }
        }
    }
}
</script>

<style>
</style>