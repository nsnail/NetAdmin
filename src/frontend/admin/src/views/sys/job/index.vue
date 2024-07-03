<template>
    <el-container>
        <el-header style="border: none">
            <el-tabs v-model="tabId" class="w100p">
                <el-tab-pane :label="$t('所有作业')" name="all"></el-tab-pane>
                <el-tab-pane :label="$t('异常作业')" name="fail"></el-tab-pane>
            </el-tabs>
        </el-header>
        <el-main class="nopadding">
            <component :is="tabId" :status-codes="['300,399', '400,499', '500,599', '900,999']" />
        </el-main>
    </el-container>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const fail = defineAsyncComponent(() => import('@/views/sys/job/record/index.vue'))
const all = defineAsyncComponent(() => import('@/views/sys/job/all/index.vue'))

export default {
    components: { all, fail },
    computed: {},
    created() {
        if (this.$route.query.view === 'fail') {
            this.tabId = 'fail'
            this.$TOOL.data.set('APP_SET_FAIL_JOB_VIEW_TIME', this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd hh:mm:ss'))
        }
    },
    data() {
        return {
            tabId: 'all',
        }
    },
    inject: ['reload'],
    methods: {},
    mounted() {},
    watch: {},
}
</script>
<style scoped></style>