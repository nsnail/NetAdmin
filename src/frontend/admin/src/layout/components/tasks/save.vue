<template>
    <sc-dialog v-model="visible" :title="$t('作业中心')" @closed="$emit('closed')" append-to-body destroy-on-close full-screen>
        <job v-if="tabId" :statusCodes="statusCodes" :tab="tabId" />
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const job = defineAsyncComponent(() => import('@/views/sys/job/index.vue'))
export default {
    components: { job },
    data() {
        return {
            statusCodes: null,
            tabId: null,
            loading: true,
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            if (data.tabId === 'fail') {
                data.tabId = 'log'
                this.statusCodes = ['300,399', '400,499', '500,599', '900,999']
                await this.$TOOL.data.set('APP_SET_FAIL_JOB_VIEW_TIME', this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd hh:mm:ss'))
            }
            this.tabId = data.tabId
            return this
        },
        //表单提交方法
        async submit() {},
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>
<style scoped></style>