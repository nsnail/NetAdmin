<template>
    <el-card v-loading="loading" class="main" shadow="never">
        <div class="wrap">
            <img alt="" src="@/assets/img/logo.png" />
            <h2>{{ packageJson.name }}</h2>
            <p>{{ ver }}</p>
            <el-link href="https://github.com/nsnail/NetAdmin" target="_blank">{{ $t('喜欢就点个 Star⭐️ 吧！') }}</el-link>
        </div>
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
.main,
.wrap {
    text-align: center;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}
.main {
    height: 25rem;
}
.wrap {
    gap: 1rem;
}
</style>