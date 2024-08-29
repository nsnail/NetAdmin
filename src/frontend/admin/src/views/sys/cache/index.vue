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
        <el-header>
            <div class="left-panel">
                <form @keyup.enter="search" @submit.prevent="search" class="right-panel-search">
                    <el-input v-model="this.query.keywords" :placeholder="$t('关键词')" clearable style="width: 25rem" />
                    <el-button-group>
                        <el-button @click="search" icon="el-icon-search" type="primary">{{ $t('查询') }}</el-button>
                        <el-button @click="reset" icon="el-icon-refresh-left">{{ $t('重置') }}</el-button>
                    </el-button-group>
                </form>
            </div>
            <div class="right-panel">
                <el-button :disabled="selection.length === 0" @click="batchDel" icon="el-icon-delete" plain type="danger"></el-button>
            </div>
        </el-header>
        <el-main v-loading="loading" class="nopadding">
            <sc-table
                v-if="tableData.length > 0"
                :data="tableData"
                :page-size="100"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                hide-refresh
                pagination-layout="total"
                stripe>
                <el-table-column type="selection" />
                <el-table-column :label="$t('键名')" prop="key" />
                <el-table-column :label="$t('数据类型')" align="center" prop="type" width="100" />
                <el-table-column :label="$t('过期时间')" align="right" prop="expireTime" width="200" />
                <na-col-operation
                    :buttons="[
                        {
                            icon: 'el-icon-view',
                            click: viewClick,
                        },
                        {
                            icon: 'el-icon-delete',
                            type: 'danger',
                            confirm: true,
                            title: '删除缓存',
                            click: delClick,
                        },
                    ]"
                    :vue="this"
                    width="100" />
            </sc-table>
            <el-empty v-else></el-empty>
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
            selection: [],
            loading: true,
            dialog: {
                info: false,
            },
            query: {
                keywords: '',
            },
            tableData: [],
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
        //批量删除
        async batchDel() {
            let loading
            try {
                await this.$confirm(`确定删除选中的 ${this.selection.length} 项吗？`, '提示', {
                    type: 'warning',
                })
                loading = this.$loading()
                const res = await this.$API.sys_cache.bulkDeleteEntry.post({
                    items: this.selection,
                })
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            await this.getData()
            loading?.close()
        },
        reset() {
            this.query.keywords = ''
            this.search()
        },
        search() {
            this.getData()
        },
        async delClick(row) {
            try {
                const res = await this.$API.sys_cache.deleteEntry.post({ key: row.key })
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            await this.getData()
        },
        async viewClick(row) {
            this.dialog.info = true
            await this.$nextTick()
            await this.$refs.info.open(
                () => this.$t('缓存详情'),
                () => this.$API.sys_cache.getEntry.post({ key: row.key }),
            )
        },
        async getData() {
            this.loading = true
            try {
                const res = await this.$API.sys_cache.getAllEntries.post(this.query)
                if (res.data) {
                    this.tableData = res.data
                }
            } catch {
                //
            }
            this.loading = false
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
    created() {
        this.cacheStatistics()
        this.getData()
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