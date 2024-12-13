<template>
    <el-container v-loading="loading">
        <el-main>
            <el-empty v-if="jobs.length === 0" :image-size="120">
                <template #description>
                    <h2>{{ $t('没有正在执行的作业') }}</h2>
                </template>
                <p style="color: var(--el-color-info); line-height: 1.5; margin: 0 3rem">
                    在处理耗时过久的作业时为了不阻碍正在处理的工作，可在作业中心进行异步执行。
                </p>
            </el-empty>
            <el-row :gutter="10">
                <el-col :lg="12">
                    <el-empty v-if="!failJobs">
                        <template #description>
                            <p>{{ $TOOL.time.getFormatTime(new Date(failJobViewTime).getTime()) }}</p>
                            <p>至今</p>
                            <p>未发现新的异常作业</p>
                        </template>
                    </el-empty>
                    <el-card v-else v-for="job in failJobs" :class="`user-bar-jobs-item alert`" :key="job.job.id" shadow="hover">
                        <div class="user-bar-jobs-item-body">
                            <div class="jobIcon">
                                {{ job.httpStatusCode.toUpperCase().slice(0, 2) }}
                            </div>
                            <div class="jobMain">
                                <div class="title">
                                    <h2>{{ job.job.jobName }}</h2>
                                    <p>{{ $t('出错时间：') }}<span v-time.tip="job.createdTime" :title="job.createdTime"></span></p>
                                    <p>
                                        执行耗时：<span>{{ $TOOL.groupSeparator(job.duration) }} ms</span>
                                    </p>
                                </div>
                                <div class="bottom">
                                    <div class="status failJobs">
                                        {{ job.responseBody }}
                                    </div>
                                    <div class="handler">
                                        <el-button
                                            @click="dialog.jobRecordSave = { mode: 'view', row: { id: job.id } }"
                                            circle
                                            icon="el-icon-view"
                                            type="danger"></el-button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </el-card>
                </el-col>
                <el-col :lg="12">
                    <el-card
                        v-for="job in jobs"
                        :class="`user-bar-jobs-item ${job.lastStatusCode === 'oK' ? '' : 'alert'}`"
                        :key="job.id"
                        shadow="hover">
                        <div class="user-bar-jobs-item-body">
                            <div class="jobIcon">
                                {{ job.lastStatusCode?.toUpperCase().slice(0, 2) }}
                            </div>
                            <div class="jobMain">
                                <div class="title">
                                    <h2>{{ job.jobName }}</h2>
                                    <p>{{ $t('上次执行：') }}<span v-time.tip="job.lastExecTime" :title="job.lastExecTime"></span></p>
                                    <p>
                                        下次执行：<span>{{ job.nextExecTime }}</span>
                                    </p>
                                </div>
                                <div class="bottom">
                                    <div class="status">
                                        <el-tag v-if="job.status === 'running'" type="warning">{{ $t('执行中') }}</el-tag>
                                        <el-tag v-if="job.status === 'idle'" :type="job.lastStatusCode === 'oK' ? 'primary' : 'danger'"
                                            >{{ $t('空闲') }}
                                        </el-tag>
                                    </div>
                                    <div class="handler">
                                        <el-button
                                            :type="job.lastStatusCode === 'oK' ? 'primary' : 'danger'"
                                            @click="dialog.jobSave = { mode: 'view', row: { id: job.id }, tabId: 'record' }"
                                            circle
                                            icon="el-icon-view"></el-button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </el-card>
                </el-col>
            </el-row>
        </el-main>
        <el-footer class="flex" style="justify-content: space-evenly; height: unset">
            <div v-if="failJobs">
                <el-badge :hidden="fail === 0" :value="`${$TOOL.time.getFormatTime(new Date(failJobViewTime).getTime())} 至今 ${fail}个`">
                    <el-button
                        @click="
                            () => {
                                this.$router.push({ path: '/sys/job', query: { view: 'fail' } })
                                this.$emit('closed')
                            }
                        "
                        plain
                        type="danger"
                        >{{ $t('异常日志') }}</el-button
                    >
                </el-badge>
            </div>
            <el-button @click="refresh" circle icon="el-icon-refresh"></el-button>
            <div>
                <el-badge :hidden="jobsCnt === 0" :value="jobsCnt">
                    <el-button
                        @click="
                            () => {
                                this.$router.push({ path: '/sys/job' })
                                this.$emit('closed')
                            }
                        "
                        >{{ $t('作业管理') }}</el-button
                    >
                </el-badge>
            </div>
        </el-footer>
    </el-container>

    <jobSaveDialog
        v-if="dialog.jobSave"
        @closed="dialog.jobSave = null"
        @mounted="$refs.jobSaveDialog.open(dialog.jobSave)"
        ref="jobSaveDialog"></jobSaveDialog>
    <jobRecordSaveDialog
        v-if="dialog.jobRecordSave"
        @closed="dialog.jobRecordSave = null"
        @mounted="$refs.jobRecordSaveDialog.open(dialog.jobRecordSave)"
        ref="jobRecordSaveDialog"></jobRecordSaveDialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'

const jobSaveDialog = defineAsyncComponent(() => import('@/views/sys/job/all/save.vue'))
const jobRecordSaveDialog = defineAsyncComponent(() => import('@/views/sys/job/record/save.vue'))

export default {
    computed: {
        failJobViewTime() {
            return this.$TOOL.data.get('APP_SET_FAIL_JOB_VIEW_TIME') ?? this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd')
        },
    },
    components: {
        jobSaveDialog,
        jobRecordSaveDialog,
    },
    data() {
        return {
            dialog: {},
            loading: false,
            jobs: [],
            jobsCnt: 0,
            failJobs: [],
        }
    },
    emits: ['closed'],
    inject: ['reload'],
    methods: {
        async getData() {
            this.loading = true
            const res = await Promise.all([
                this.$API.sys_job.pagedQuery.post({
                    prop: 'nextExecTime',
                    order: 'ascending',
                    dynamicFilter: {
                        field: 'enabled',
                        value: true,
                        operator: 'eq',
                    },
                }),
                this.$API.sys_job.pagedQueryRecord.post({
                    dynamicFilter: {
                        filters: [
                            {
                                logic: 'or',
                                filters: [
                                    {
                                        field: 'httpStatusCode',
                                        operator: 'range',
                                        value: '300,399',
                                    },
                                    {
                                        field: 'httpStatusCode',
                                        operator: 'range',
                                        value: '400,499',
                                    },
                                    {
                                        field: 'httpStatusCode',
                                        operator: 'range',
                                        value: '500,599',
                                    },
                                    {
                                        field: 'httpStatusCode',
                                        operator: 'range',
                                        value: '900,999',
                                    },
                                ],
                            },
                        ],
                        field: 'createdTime',
                        operator: 'greaterThan',
                        value: this.failJobViewTime,
                    },
                }),
            ])

            this.jobs = res[0].data.rows
            this.jobsCnt = res[0].data.total
            this.failJobs = res[1].data.rows
            this.loading = false
        },
        refresh() {
            this.getData()
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
    color: var(--el-color-info);
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

.status.failJobs {
    color: var(--el-color-danger);
    width: 18rem;
    overflow: hidden;
}
</style>