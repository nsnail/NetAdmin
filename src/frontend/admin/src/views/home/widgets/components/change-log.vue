<template>
    <el-card header="changelog" shadow="never">
        <el-skeleton :loading="loading" :rows="10" animated>
            <template #default>
                <div v-html="changeLog" class="change-log"></div>
            </template>
        </el-skeleton>
    </el-card>
</template>

<script>
import markdownit from 'markdown-it'
import { full as emoji } from 'markdown-it-emoji'

export default {
    title: '更新日志',
    icon: 'el-icon-monitor',
    description: '当前项目更新日志',
    components: {},
    data() {
        return {
            loading: true,
            changeLog: '',
        }
    },
    created() {
        this.getChangeLog()
    },
    methods: {
        async getChangeLog() {
            const res = await this.$API.sys_tools.getChangeLog.post()
            this.changeLog = markdownit().use(emoji).render(res.data)
            this.loading = false
        },
    },
}
</script>

<style scoped></style>
<style lang="scss">
.change-log {
    line-height: 3rem;
    li {
        margin: 0 0 0 2rem;
    }
    h1 {
        display: none;
    }
    a {
        color: var(--el-color-primary);
    }
}
</style>