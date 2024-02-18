<template>
    <el-container v-loading="loading">
        <el-main>
            <el-empty v-if="jobs.length === 0" :image-size="120">
                <template #description>
                    <h2>没有正在执行的任务</h2>
                </template>
                <p style="color: #999; line-height: 1.5; margin: 0 3rem">
                    在处理耗时过久的任务时为了不阻碍正在处理的工作，可在任务中心进行异步执行。
                </p>
            </el-empty>
            <el-card v-for="job in jobs" :key="job.id" class="user-bar-jobs-item" shadow="hover">
                <div class="user-bar-jobs-item-body">
                    <div class="jobIcon">
                        {{ job.lastStatusCode }}
                    </div>
                    <div class="jobMain">
                        <div class="title">
                            <h2>{{ job.jobName }}</h2>
                            <p>上次执行：<span v-time.tip="job.lastExecTime"></span></p>
                            <p>
                                下次执行：<span>{{ job.nextExecTime }}</span>
                            </p>
                        </div>
                        <div class="bottom">
                            <div class="status">
                                <el-tag v-if="job.status === 'running'" type="info">执行中</el-tag>
                                <el-tag v-if="job.status === 'idle'">空闲</el-tag>
                            </div>
                            <div class="handler">
                                <el-button v-if="job.status === 'idle'" @click="view(job)" circle icon="el-icon-view" type="primary"></el-button>
                            </div>
                        </div>
                    </div>
                </div>
            </el-card>
        </el-main>
        <el-footer style="text-align: right">
            <el-button @click="refresh" circle icon="el-icon-refresh"></el-button>
        </el-footer>
    </el-container>
    <save-dialog v-if="dialog.save" @closed="dialog.save = false" ref="saveDialog"></save-dialog>
</template>

<script>
import saveDialog from '@/views/sys/job/save.vue'
export default {
    components: {
        saveDialog,
    },
    data() {
        return {
            dialog: {
                save: false,
            },
            loading: false,
            jobs: [],
        }
    },
    mounted() {
        this.getData()
    },
    methods: {
        async getData() {
            this.loading = true
            const res = await this.$API.sys_job.query.post({ prop: 'lastExecTime', order: 'descending' })
            this.jobs = res.data
            this.loading = false
        },
        refresh() {
            this.getData()
        },
        async view(job) {
            this.dialog.save = true
            await this.$nextTick()
            await this.$refs.saveDialog.open('view', { id: job.id })
        },
    },
}
</script>

<style scoped>
.user-bar-jobs-item {
    margin-bottom: 0.5rem;
}

.user-bar-jobs-item:hover {
    border-color: var(--el-color-primary);
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
</style>