<template>
    <div v-loading="loading">
        <el-card :header="$t('开发依赖')" shadow="never">
            <el-descriptions :column="2" border>
                <el-descriptions-item v-for="(value, key) in packageJson.devDependencies" :key="key" :label="key">{{ value }}</el-descriptions-item>
            </el-descriptions>
        </el-card>
        <el-card :header="$t('运行依赖')" shadow="never">
            <el-descriptions :column="2" border>
                <el-descriptions-item v-for="(value, key) in packageJson.dependencies" :key="key" :label="key">{{ value }}</el-descriptions-item>
            </el-descriptions>
            <el-descriptions :column="1" border style="margin-top: 1rem">
                <el-descriptions-item v-for="(value, key) in modules" :key="key" :label="value.name">{{ value.version }}</el-descriptions-item>
            </el-descriptions>
        </el-card>
    </div>
</template>

<script>
import packageJson from '/package.json'

export default {
    title: '外部组件',
    icon: 'el-icon-monitor',
    description: '引用的外部组件信息',
    data() {
        return {
            loading: true,
            packageJson,
            modules: [],
        }
    },
    created() {
        this.title = this.$t('模块信息')
        this.description = this.$t('当前项目模块信息')
        this.getModules()
    },
    methods: {
        async getModules() {
            const res = await this.$API.sys_tools.getModules.post()
            this.modules = res.data
            this.loading = false
        },
    },
}
</script>

<style scoped></style>
<script setup></script>