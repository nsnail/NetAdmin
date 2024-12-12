<template>
    <el-main v-loading="loading" :style="{ height: mainHeight, minHeight: '100%' }">
        <widgets v-if="dashboard" @on-customizing="onCustomizing" @on-mounted="this.loading = false"></widgets>
        <work v-if="myapp" @on-mounted="this.loading = false"></work>
    </el-main>

    <el-tour v-model="tour.open" @close="$TOOL.data.set('TOUR_TIP_READ_INDEX', true)">
        <el-tour-step :target="tour.targets.tags" :title="$t('操作指引')"> {{ $t('按下 ALT+Q 可快速关闭当前标签页') }} </el-tour-step>
        <el-tour-step :target="tour.targets.search" :title="$t('操作指引')"> {{ $t('快速查找菜单功能') }} </el-tour-step>
        <el-tour-step :target="tour.targets.userCenter" :title="$t('操作指引')"> {{ $t('修改个人信息和密码') }} </el-tour-step>
        <el-tour-step v-if="dashboard" :target="tour.targets.layoutSetting" :title="$t('操作指引')">
            {{ $t('自定义首页布局') }}
        </el-tour-step>
    </el-tour>
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
            tour: {
                targets: {},
            },
            loading: true,
            mainHeight: 'auto',
            dashboard: false,
            myapp: false,
        }
    },
    async created() {
        //下载配置
        await this.$TOOL.data.downloadConfig()
        this.dashboard = this.$GLOBAL.user.roles.findIndex((x) => x.displayDashboard) >= 0
        this.myapp = !this.dashboard
    },
    mounted() {
        this.$nextTick(() => {
            if (this.$TOOL.data.get('TOUR_TIP_READ_INDEX')) {
                return
            }

            const timer = setInterval(() => {
                this.tour.targets.tags = document.getElementsByClassName('adminui-tags')[0]
                this.tour.targets.search = document.getElementsByClassName('userbar-btn-search')[0]
                this.tour.targets.userCenter = document.getElementsByClassName('user-center')[0]
                if (this.dashboard) {
                    this.tour.targets.layoutSetting = document.getElementsByClassName('layout-setting')[0]
                }
                if (this.tour.targets.tags && this.tour.targets.search && this.tour.targets.userCenter) {
                    if (this.dashboard && !this.tour.targets.layoutSetting) {
                        return
                    }
                    this.tour.open = true
                    clearInterval(timer)
                }
            }, 1000)
        })
    },
    methods: {
        onCustomizing(isCustomizing) {
            this.mainHeight = isCustomizing ? '100%' : 'auto'
        },
    },
}
</script>

<style scoped></style>