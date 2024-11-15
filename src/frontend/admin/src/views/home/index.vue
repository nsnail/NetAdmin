<template>
    <div v-if="loading" v-loading="true" style="height: 100%"></div>
    <el-main v-else :style="{ height: mainHeight }">
        <widgets v-if="dashboard" @on-customizing="onCustomizing"></widgets>
        <work v-else></work>
    </el-main>
</template>

<script>
import { defineAsyncComponent } from 'vue'

const work = defineAsyncComponent(() => import('./work'))
const widgets = defineAsyncComponent(() => import('./widgets'))

export default {
    components: {
        work,
        widgets,
    },
    data() {
        return {
            loading: true,
            mainHeight: 'auto',
            dashboard: false,
        }
    },
    async created() {
        //下载配置
        await this.$TOOL.data.downloadConfig()
        this.dashboard = this.$GLOBAL.user.roles.findIndex((x) => x.displayDashboard) >= 0
        this.loading = false
    },
    mounted() {},
    methods: {
        onCustomizing(isCustomizing) {
            this.mainHeight = isCustomizing ? '100%' : 'auto'
        },
    },
}
</script>

<style scoped></style>