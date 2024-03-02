<template>
    <el-card :class="loading ? '' : 'aboutTop'" shadow="never">
        <el-skeleton :loading="loading" :rows="5" animated>
            <template #default>
                <div class="aboutTop-info">
                    <img alt="" src="@/assets/img/logo.png" />
                    <h2>{{ packageJson.name }}</h2>
                    <p>{{ ver }}</p>
                </div>
            </template>
        </el-skeleton>
    </el-card>
</template>

<script>
import packageJson from '/package.json'

export default {
    title: '版本信息',
    icon: 'el-icon-monitor',
    description: '当前项目版本信息',
    data() {
        return {
            loading: true,
            packageJson,
            ver: null,
        }
    },
    created() {
        this.getVer()
    },
    methods: {
        async getVer() {
            const res = await this.$API.sys_tools.getVersion.post()
            this.ver = res.data
            this.loading = false
        },
    },
}
</script>

<style scoped>
.aboutTop {
    border: 0;
    background: linear-gradient(to right, rgb(66, 76, 80), #ccc);
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