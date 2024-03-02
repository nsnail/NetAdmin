<template>
    <el-card header="dependencies" shadow="never">
        <el-skeleton :loading="loading" :rows="5" animated>
            <template #default>
                <el-descriptions :column="2" border>
                    <el-descriptions-item v-for="(value, key) in packageJson.dependencies" :key="key" :label="key">{{ value }}</el-descriptions-item>
                </el-descriptions>
            </template>
        </el-skeleton>
    </el-card>
    <el-card header="devDependencies" shadow="never">
        <el-skeleton :loading="loading" :rows="5" animated>
            <template #default>
                <el-descriptions :column="2" border>
                    <el-descriptions-item v-for="(value, key) in packageJson.devDependencies" :key="key" :label="key">{{
                        value
                    }}</el-descriptions-item>
                </el-descriptions>
            </template>
        </el-skeleton>
    </el-card>
    <el-card header="assemblies" shadow="never">
        <el-skeleton :loading="loading" :rows="5" animated>
            <template #default>
                <el-descriptions :column="1" border>
                    <el-descriptions-item v-for="(value, key) in modules" :key="key" :label="value.name">{{ value.version }}</el-descriptions-item>
                </el-descriptions>
            </template>
        </el-skeleton>
    </el-card>
</template>

<script>
import packageJson from '/package.json'

export default {
    title: '模块信息',
    icon: 'el-icon-monitor',
    description: '当前项目模块信息',
    data() {
        return {
            loading: true,
            packageJson,
            modules: [],
        }
    },
    created() {
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