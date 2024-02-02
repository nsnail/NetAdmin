<template>
    <el-main>
        <el-row>
            <el-col :lg="24">
                <el-card class="aboutTop" shadow="never">
                    <div class="aboutTop-info">
                        <img alt="" src="@/assets/img/logo.png" />
                        <h2>{{ packageJson.name }}</h2>
                        <p>{{ ver }}</p>
                    </div>
                </el-card>
                <el-card header="dependencies" shadow="never">
                    <el-descriptions :column="3" border>
                        <el-descriptions-item v-for="(value, key) in packageJson.dependencies" :key="key" :label="key">{{
                            value
                        }}</el-descriptions-item>
                    </el-descriptions>
                </el-card>
                <el-card header="devDependencies" shadow="never">
                    <el-descriptions :column="3" border>
                        <el-descriptions-item v-for="(value, key) in packageJson.devDependencies" :key="key" :label="key">{{
                            value
                        }}</el-descriptions-item>
                    </el-descriptions>
                </el-card>
                <el-card header="Assemblies" shadow="never">
                    <el-descriptions :column="2" border>
                        <el-descriptions-item v-for="(value, key) in modules" :key="key" :label="value.name">{{
                            value.version
                        }}</el-descriptions-item>
                    </el-descriptions>
                </el-card>
            </el-col>
        </el-row>
    </el-main>
</template>

<script>
import packageJson from '/package.json'

export default {
    title: '版本信息',
    icon: 'el-icon-monitor',
    description: '当前项目版本信息',
    data() {
        return {
            packageJson,
            ver: '',
            modules: [],
        }
    },
    created() {
        this.getVer()
        this.getModules()
    },
    methods: {
        async getVer() {
            const res = await this.$API.sys_tools.getVersion.post()
            this.ver = res.data
        },
        async getModules() {
            const res = await this.$API.sys_tools.getModules.post()
            this.modules = res.data
        },
    },
}
</script>

<style scoped>
.aboutTop {
    border: 0;
    background: linear-gradient(to right, #8e54e9, #4776e6);
    color: #fff;
}
.aboutTop-info {
    text-align: center;
}
.aboutTop-info img {
    width: 10rem;
}
.aboutTop-info h2 {
    font-size: 2rem;
    margin-top: 1rem;
}
.aboutTop-info p {
    margin-top: 1rem;
}
</style>