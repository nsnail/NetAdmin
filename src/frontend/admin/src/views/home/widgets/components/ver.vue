<template>
    <el-card :header="$t('版本信息')" shadow="hover">
        <div style="height: 210px; text-align: center">
            <img src="@/assets/img/ver.svg" style="height: 140px" />
            <h2 style="margin-top: 15px">NetAdmin {{ $CONFIG.CORE_VER }}</h2>
            <p style="margin-top: 5px">最新版本 {{ ver }}</p>
        </div>
        <div style="margin-top: 20px">
            <el-button plain round type="primary" @click="golog">更新日志</el-button>
            <el-button plain round type="primary" @click="gogit">gitee</el-button>
        </div>
    </el-card>
</template>

<script>
export default {
    title: '版本信息',
    icon: 'el-icon-monitor',
    description: '当前项目版本信息',
    data() {
        return {
            ver: 'loading...',
        }
    },
    mounted() {
        this.getVer()
    },
    methods: {
        async getVer() {
            const ver = await this.$API.sys_tools.version.post()
            this.ver = ver.data
        },
        golog() {
            window.open('https://gitee.com/lolicode/scui/releases')
        },
        gogit() {
            window.open('https://gitee.com/lolicode/scui')
        },
    },
}
</script>