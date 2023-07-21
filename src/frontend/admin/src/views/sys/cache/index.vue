<template>
    <el-container>
        <el-main>
            <el-card header="缓存统计" shadow="never">
                <el-row :gutter="15" style="margin-top: 20px">
                    <el-col :lg="6">
                        <el-card shadow="never">
                            <sc-statistic
                                :value="statistics.currentEntryCount"
                                groupSeparator
                                title="缓存数量"
                            ></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="6">
                        <el-card shadow="never">
                            <sc-statistic
                                :value="statistics.currentEstimatedSize"
                                groupSeparator
                                title="缓存大小"
                            ></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="6">
                        <el-card shadow="never">
                            <sc-statistic
                                :value="statistics.totalHits"
                                groupSeparator
                                title="缓存命中"
                            ></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="6">
                        <el-card shadow="never">
                            <sc-statistic
                                :value="statistics.totalMisses"
                                groupSeparator
                                title="缓存穿透"
                            ></sc-statistic>
                        </el-card>
                    </el-col>
                </el-row>
            </el-card>

            <el-card header="缓存管理" shadow="never" style="margin-top: 20px">
                <el-row :gutter="15" style="margin-top: 20px">
                    <el-col :lg="6">
                        <el-popconfirm
                            title="确定清空缓存吗？"
                            @confirm="clearCache"
                        >
                            <template #reference>
                                <el-button round size="large" type="primary"
                                    >清空缓存</el-button
                                >
                            </template>
                        </el-popconfirm>
                    </el-col>
                </el-row>
            </el-card>
        </el-main>
    </el-container>
</template>

<script>
import scStatistic from "@/components/scStatistic";

export default {
    components: {
        scStatistic,
    },
    data() {
        return {
            statistics: {
                currentEntryCount: 0,
                currentEstimatedSize: 0,
                totalHits: 0,
                totalMisses: 0,
            },
            value: 39.58,
            color: [
                { color: "#67C23A", percentage: 40 },
                { color: "#E6A23C", percentage: 60 },
                { color: "#F56C6C", percentage: 80 },
            ],
        };
    },
    mounted() {
        this.cacheStatistics();
    },
    methods: {
        format(percentage) {
            return percentage + "G";
        },
        async cacheStatistics() {
            try {
                const res = await this.$API.sys_cache.cacheStatistics.post();
                if (res.data) {
                    this.statistics = res.data;
                }
            } catch {}
        },
        async clearCache() {
            try {
                await this.$API.sys_cache.clear.post();
                this.$message.success("清空成功");
                await this.cacheStatistics();
            } catch {}
        },
    },
};
</script>

<style></style>