<template>
    <div v-if="pageLoading">
        <el-main>
            <el-card shadow="never">
                <el-skeleton :rows="1"></el-skeleton>
            </el-card>
            <el-card shadow="never" style="margin-top: 1rem">
                <el-skeleton></el-skeleton>
            </el-card>
        </el-main>
    </div>
    <widgets v-if="dashboard" @on-mounted="onMounted"></widgets>
    <work v-else @on-mounted="onMounted"></work>
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
            pageLoading: true,
            dashboard: false,
        }
    },
    created() {
        this.dashboard = this.$GLOBAL.user.roles.findIndex((x) => x.displayDashboard) >= 0
    },
    mounted() {},
    methods: {
        onMounted() {
            this.pageLoading = false
        },
    },
}
</script>

<style></style>