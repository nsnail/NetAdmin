<template>
    <el-card shadow="hover" header="版本信息">
        <div style="height: 210px;text-align: center;">
            <img src="img/ver.svg" style="height:140px"/>
            <h2 style="margin-top: 15px;">{{ ver }}</h2>
        </div>
    </el-card>
</template>

<script>
export default {
    title: "版本信息",
    icon: "el-icon-monitor",
    description: "当前项目版本信息",
    data() {
        return {
            ver: 'loading...'
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
            window.open("https://gitee.com/lolicode/scui/releases")
        },
        gogit() {
            window.open("https://gitee.com/lolicode/scui")
        }
    }
}
</script>