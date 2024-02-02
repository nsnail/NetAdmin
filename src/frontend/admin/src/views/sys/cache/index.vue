<template>
    <el-container>
        <el-main>
            <el-card :header="$t('缓存统计')" shadow="never">
                <el-row :gutter="15">
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic :value="statistics.version" groupSeparator title="Redis 版本"></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic
                                :suffix="$t('天')"
                                :value="parseInt(statistics.upTime / 86400)"
                                groupSeparator
                                title="Redis 运行时间"></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic
                                :value="statistics.upTime ? (statistics.usedCpu / statistics.upTime).toFixed(2) : 0"
                                groupSeparator
                                suffix="%"
                                title="CPU 使用率"></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic
                                :title="$t('内存使用量')"
                                :value="(statistics.usedMemory / 1024 / 1024).toFixed(2)"
                                groupSeparator
                                suffix="MiB"></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic :title="$t('缓存数量')" :value="statistics.dbSize" groupSeparator></sc-statistic>
                        </el-card>
                    </el-col>
                    <el-col :lg="4">
                        <el-card shadow="never">
                            <sc-statistic
                                :title="$t('缓存命中率')"
                                :value="((statistics.keyspaceHits / (statistics.keyspaceMisses + statistics.keyspaceHits)) * 100).toFixed(2)"
                                groupSeparator
                                suffix="%"></sc-statistic>
                        </el-card>
                    </el-col>
                </el-row>
            </el-card>

            <el-card :header="$t('缓存管理')" shadow="never">
                <el-container>
                    <el-aside>
                        <el-menu :default-active="query.filter.dbIndex" class="el-menu-vertical-demo">
                            <el-menu-item v-for="(i, index) in 16" :index="index" :key="index" @click="this.query.filter.dbIndex = index">
                                <el-icon>
                                    <el-icon-notebook></el-icon-notebook>
                                </el-icon>
                                <span slot="title">DB{{ index }}</span>
                            </el-menu-item>
                        </el-menu>
                    </el-aside>
                    <el-main>
                        <sc-table :apiObj="$API.sys_cache.getAllEntries" :params="query" @row-click="rowClick" ref="table" row-key="key" stripe>
                            <el-table-column :label="$t('键名')" :min-width="300" prop="key" show-overflow-tooltip />
                            <el-table-column :label="$t('键值')" prop="data" show-overflow-tooltip />
                            <el-table-column :label="$t('滑动过期')" align="right" prop="sldExp" />
                            <el-table-column :label="$t('绝对过期')" align="right" prop="absExp" />
                        </sc-table>
                    </el-main>
                </el-container>
            </el-card>
        </el-main>
    </el-container>
    <na-info v-if="dialog.info" ref="info"></na-info>
</template>

<script>
import scStatistic from '@/components/scStatistic'
import naInfo from '@/components/naInfo/index.vue'
import tool from '@/utils/tool'

export default {
    components: {
        scStatistic,
        naInfo,
    },
    data() {
        return {
            query: {
                filter: {
                    dbIndex: 0,
                },
            },
            dialog: {
                info: false,
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
    methods: {
        async rowClick(row) {
            this.dialog.info = true
            await this.$nextTick()
            this.$refs.info.open(tool.sortProperties(row), `缓存详情`)
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
}
</script>

<style scoped>
.el-main {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}
</style>