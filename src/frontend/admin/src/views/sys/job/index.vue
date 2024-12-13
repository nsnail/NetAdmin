<template>
    <el-container>
        <el-header style="border: none">
            <el-tabs v-model="tabId" class="w100p">
                <el-tab-pane :label="$t('作业管理')" name="all"></el-tab-pane>
                <el-tab-pane :label="$t('作业日志')" name="log"></el-tab-pane>
            </el-tabs>
        </el-header>
        <el-main class="nopadding">
            <component :is="tabId" :status-codes="statusCodes || dataStatusCode" />
        </el-main>
    </el-container>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const log = defineAsyncComponent(() => import('@/views/sys/job/record/index.vue'))
const all = defineAsyncComponent(() => import('@/views/sys/job/all/index.vue'))

export default {
    components: { all, log },
    computed: {},
    created() {
        if (this.$route.query.view === 'log') {
            this.tabId = 'log'
        }

        if (this.$route.query.view === 'fail') {
            this.tabId = 'log'
            this.dataStatusCode = ['300,399', '400,499', '500,599', '900,999']
            this.$TOOL.data.set('APP_SET_FAIL_JOB_VIEW_TIME', this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd hh:mm:ss'))
        }
    },
    data() {
        return {
            dataStatusCode: null,
            tabId: 'all',
        }
    },
    inject: ['reload'],
    methods: {},
    mounted() {},
    watch: {
        tab: {
            immediate: true,
            deep: true,
            handler(n) {
                this.tabId = n
            },
        },
    },
    props: {
        tab: {
            type: String,
            default: 'all',
        },
        statusCodes: {
            type: Array,
            default: null,
        },
    },
}
</script>
<style scoped></style>