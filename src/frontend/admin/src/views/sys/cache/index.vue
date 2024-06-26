<template>
    <el-container>
        <el-header style="flex-direction: column; height: auto">
            <div v-loading="statistics.version === ''" class="w100p">
                <el-row :gutter="15">
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic :title="$t('Redis 版本')" :value="statistics.version" group-separator></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic
                                :suffix="$t('天')"
                                :title="$t('Redis 运行时间')"
                                :value="parseInt(statistics.upTime / 86400)"
                                group-separator></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic
                                :title="$t('CPU 使用率')"
                                :value="statistics.upTime ? (statistics.usedCpu / statistics.upTime).toFixed(2) : 0"
                                group-separator
                                suffix="%"></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic
                                :title="$t('内存使用量')"
                                :value="(statistics.usedMemory / 1024 / 1024).toFixed(2)"
                                group-separator
                                suffix="MiB"></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic :title="$t('缓存数量')" :value="statistics.dbSize" group-separator></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic
                                :title="$t('缓存命中率')"
                                :value="((statistics.keyspaceHits / (statistics.keyspaceMisses + statistics.keyspaceHits)) * 100).toFixed(2)"
                                group-separator
                                suffix="%"></sc-statistic>
                        </el-card>
                    </el-col>
                </el-row>
            </div>
        </el-header>

        <el-main class="nopadding">
            <sc-table :apiObj="$API.sys_cache.getAllEntries" :params="query" @row-click="rowClick" ref="table" row-key="key" stripe>
                <el-table-column :label="$t('键名')" prop="key" show-overflow-tooltip />
                <el-table-column :label="$t('键值')" prop="data" show-overflow-tooltip />
                <el-table-column :label="$t('滑动过期')" align="right" prop="sldExpTime" width="200" />
                <el-table-column :label="$t('绝对过期')" align="right" prop="absExpTime" width="200" />
            </sc-table>
        </el-main>
    </el-container>
    <na-info v-if="dialog.info" ref="info"></na-info>
</template>

<script>
import naInfo from '@/components/naInfo/index.vue'

export default {
    components: {
        naInfo,
    },
    data() {
        return {
            dialog: {
                info: false,
            },
            query: {
                filter: {
                    dbIndex: 1,
                },
            },
            statistics: {
                keyspaceHits: 0,
                keyspaceMisses: 0,
                upTime: 0,
                usedCpu: 0,
                usedMemory: 0,
                version: '',
            },
        }
    },
    methods: {
        async rowClick(row) {
            this.dialog.info = true
            await this.$nextTick()
            this.$refs.info.open(this.$TOOL.sortProperties(row), this.$t('缓存详情'))
        },
        async cacheStatistics() {
            try {
                const res = await this.$API.sys_cache.cacheStatistics.post()
                if (res.data) {
                    this.statistics = res.data
                }
            } catch {
                //
            }
        },
    },
    mounted() {
        this.cacheStatistics()
    },
    watch: {
        'query.filter.dbIndex': {
            handler() {
                this.$refs.table.upData()
            },
        },
    },
}
</script>

<style scoped>
.el-main {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}
.el-card {
    overflow: unset;
}
</style>