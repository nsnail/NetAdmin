<template>
    <el-container v-loading="loading">
        <el-main>
            <el-empty v-if="jobs.length === 0" :image-size="120">
                <template #description>
                    <h2>{{ $t('没有正在执行的作业') }}</h2>
                </template>
                <p style="color: #999; line-height: 1.5; margin: 0 3rem">
                    在处理耗时过久的作业时为了不阻碍正在处理的工作，可在作业中心进行异步执行。
                </p>
            </el-empty>
            <el-card v-for="job in jobs" :class="`user-bar-jobs-item ${job.lastStatusCode === 'oK' ? '' : 'alert'}`" :key="job.id" shadow="hover">
                <div class="user-bar-jobs-item-body">
                    <div class="jobIcon">
                        {{ job.lastStatusCode }}
                    </div>
                    <div class="jobMain">
                        <div class="title">
                            <h2>{{ job.jobName }}</h2>
                            <p>{{ $t('上次执行：') }}<span v-time.tip="job.lastExecTime"></span></p>
                            <p>
                                下次执行：<span>{{ job.nextExecTime }}</span>
                            </p>
                        </div>
                        <div class="bottom">
                            <div class="status">
                                <el-tag v-if="job.status === 'running'" type="warning">{{ $t('执行中') }}</el-tag>
                                <el-tag v-if="job.status === 'idle'" :type="job.lastStatusCode === 'oK' ? 'primary' : 'danger'">{{
                                    $t('空闲')
                                }}</el-tag>
                            </div>
                            <div class="handler">
                                <el-button
                                    :type="job.lastStatusCode === 'oK' ? 'primary' : 'danger'"
                                    @click="view(job)"
                                    circle
                                    icon="el-icon-view"></el-button>
                            </div>
                        </div>
                    </div>
                </div>
            </el-card>
        </el-main>
        <el-footer class="flex" style="justify-content: space-evenly; height: unset">
            <div>
                <el-badge :hidden="fail === 0" :value="fail">
                    <el-button @click="gotoJob">{{ $t('异常作业') }}</el-button>
                </el-badge>
            </div>
            <div style="text-align: right; flex-grow: 1">
                <el-button @click="refresh" circle icon="el-icon-refresh"></el-button>
            </div>
        </el-footer>
    </el-container>

    <save-dialog v-if="dialog.save" @closed="dialog.save = null" @mounted="$refs.saveDialog.open(dialog.save)" ref="saveDialog"></save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const saveDialog = defineAsyncComponent(() => import('@/views/sys/job/all/save.vue'))
export default {
    components: {
        saveDialog,
    },
    data() {
        return {
            dialog: {},
            loading: false,
            jobs: [],
        }
    },
    emits: ['closed'],
    inject: ['reload'],
    methods: {
        gotoJob() {
            this.$router.push({ path: '/sys/job', query: { view: 'fail' } })
            this.$emit('closed')
        },
        async getData() {
            this.loading = true
            const res = await this.$API.sys_job.query.post({ prop: 'lastStatusCode', order: 'descending' })
            this.jobs = res.data
            this.loading = false
        },
        refresh() {
            this.getData()
        },
        async view(job) {
            this.dialog.save = { mode: 'view', row: { id: job.id }, tabId: 'record' }
        },
    },
    mounted() {
        this.getData()
    },
    props: {
        fail: { type: Number, default: 0 },
    },
    watch: {},
}
</script>

<style scoped>
.user-bar-jobs-item {
    margin-bottom: 0.5rem;
}
.user-bar-jobs-item:hover {
    border-color: var(--el-color-primary);
}

.user-bar-jobs-item.alert:hover {
    border-color: var(--el-color-danger);
}

.user-bar-jobs-item-body {
    display: flex;
}

.user-bar-jobs-item-body .jobIcon {
    width: 3rem;
    height: 3rem;
    background: var(--el-color-primary-light-9);
    margin-right: 2rem;
    display: flex;
    justify-content: center;
    align-items: center;
    color: var(--el-color-primary);
    border-radius: 1.5rem;
}
.user-bar-jobs-item-body .jobMain {
    flex: 1;
}

.user-bar-jobs-item-body .title h2 {
    font-size: 1rem;
}

.user-bar-jobs-item-body .title p {
    font-size: 1rem;
    color: #999;
    margin-top: 0.5rem;
}

.user-bar-jobs-item-body .bottom {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.user-bar-jobs-item.alert .jobIcon {
    background: var(--el-color-danger-light-9);
    color: var(--el-color-danger);
}
</style>