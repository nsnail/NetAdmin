<template>
    <el-main>
        <widgets v-if="dashboard" @on-mounted="onMounted"></widgets>
        <work v-else @on-mounted="onMounted"></work>
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