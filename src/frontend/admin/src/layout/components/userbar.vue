<template>
    <div class="user-bar">
        <div @click="configDark" class="tasks panel-item">
            <el-icon>
                <component :is="config.dark ? 'el-icon-sunny' : 'el-icon-moon'" />
            </el-icon>
        </div>
        <div @click="search" class="panel-item hidden-sm-and-down">
            <el-icon>
                <el-icon-search />
            </el-icon>
        </div>
        <div @click="screen" class="screen panel-item hidden-sm-and-down">
            <el-icon>
                <el-icon-full-screen />
            </el-icon>
        </div>
        <div v-auth="'sys/job/userbar'" @click="tasks" class="tasks panel-item">
            <el-badge :hidden="failJobCnt === 0" :value="failJobCnt">
                <el-icon>
                    <sc-icon-ScheduledJob />
                </el-icon>
            </el-badge>
        </div>
        <div @click="showMsg" class="msg panel-item">
            <el-badge :hidden="unreadCnt === 0" :value="unreadCnt" class="badge" type="danger">
                <el-icon>
                    <el-icon-chat-dot-round />
                </el-icon>
            </el-badge>
            <el-drawer v-model="msg" :title="$t('新消息')" append-to-body destroy-on-close>
                <el-container>
                    <el-main style="padding: 0 1rem">
                        <message />
                    </el-main>
                    <el-footer style="height: unset">
                        <el-button @click="gotoMsgCenter">{{ $t('消息中心') }}</el-button>
                    </el-footer>
                </el-container>
            </el-drawer>
        </div>
        <el-dropdown @command="handleUser" class="user panel-item" trigger="hover">
            <div class="user-avatar">
                <el-avatar
                    :size="30"
                    :src="
                        user.avatar
                            ? user.avatar
                            : 'data:image/svg+xml,' +
                              encodeURIComponent(
                                  avatar.createSVG(
                                      `#${Math.abs(this.$TOOL.crypto.hashCode(user.userName)).toString(16).substring(0, 6)}`,
                                      user.userName.slice(0, 1).toUpperCase(),
                                  ).outerHTML,
                              )
                    "></el-avatar>

                <label>{{ user.userName }}</label>
                <el-icon class="el-icon--right">
                    <el-icon-arrow-down />
                </el-icon>
            </div>
            <template #dropdown>
                <el-dropdown-menu>
                    <el-dropdown-item command="uc"> {{ $t('个人中心') }}</el-dropdown-item>
                    <el-dropdown-item command="clearCache"> {{ $t('清除缓存') }}</el-dropdown-item>
                    <el-dropdown-item command="outLogin" divided>{{ $t('退出登录') }}</el-dropdown-item>
                </el-dropdown-menu>
            </template>
        </el-dropdown>
    </div>

    <el-dialog v-model="searchVisible" :title="$t('搜索')" :width="700" center destroy-on-close>
        <search @success="searchVisible = false"></search>
    </el-dialog>

    <el-drawer v-model="tasksVisible" :size="450" :title="$t('作业中心')" destroy-on-close>
        <tasks :fail="failJobCnt" @closed="tasksVisible = false"></tasks>
    </el-drawer>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import avatar from '../../utils/avatar'
const search = defineAsyncComponent(() => import('./search.vue'))
const tasks = defineAsyncComponent(() => import('./tasks.vue'))
const message = defineAsyncComponent(() => import('@/views/profile/message/components/list.vue'))
export default {
    components: {
        search,
        tasks,
        message,
    },
    computed: {
        avatar() {
            return avatar
        },
    },
    async created() {
        this.user = this.$GLOBAL.user
        let res = await this.$API.sys_sitemsg.unreadCount.post()
        this.unreadCnt = res.data

        if (this.$GLOBAL.permissions.some((x) => x === '*/*/*' || x === 'sys/job/userbar')) {
            res = await this.$API.sys_job.countRecord.post({
                dynamicFilter: {
                    filters: [
                        {
                            field: 'createdTime',
                            operator: 'dateRange',
                            value: [
                                this.$TOOL.data.get('APP_SET_FAIL_JOB_VIEW_TIME') ?? this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd'),
                                this.$TOOL.dateFormat(new Date(), 'yyyy-MM-dd hh:mm:ss'),
                            ],
                        },
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
                },
            })
            this.failJobCnt = res.data
        }
    },
    data() {
        return {
            config: {
                dark: this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK,
            },
            user: {},
            userName: '',
            userNameF: '',
            searchVisible: false,
            tasksVisible: false,
            msg: false,
            unreadCnt: 0,
            failJobCnt: 0,
        }
    },
    methods: {
        clearData(fullClear) {
            const loading = this.$loading()
            this.$TOOL.cookie.clear()
            if (fullClear) {
                this.$TOOL.data.clear()
            } else {
                this.$TOOL.data.clearAppSet()
            }
            this.$router.replace({ path: '/guest/login' })
            setTimeout(() => {
                loading.close()
                location.reload()
            }, 1000)
        },
        configDark() {
            this.config.dark = !this.config.dark
        },
        gotoMsgCenter() {
            this.$router.push({ path: '/profile/message' })
            this.msg = false
        },
        //个人信息
        async handleUser(command) {
            if (command === 'uc') {
                this.$router.push({ path: '/profile/account' })
            }
            if (command === 'cmd') {
                this.$router.push({ path: '/cmd' })
            }
            if (command === 'clearCache') {
                try {
                    await this.$confirm(this.$t('清除缓存会将系统初始化，包括登录状态、主题、语言设置等，是否继续？'), this.$t('提示'), {
                        type: 'info',
                    })
                    this.clearData(true)
                } catch {}
            }
            if (command === 'outLogin') {
                try {
                    await this.$confirm(this.$t('确认是否退出当前用户？'), this.$t('提示'), {
                        type: 'warning',
                        confirmButtonText: this.$t('退出'),
                        confirmButtonClass: 'el-button--danger',
                    })
                    this.clearData(false)
                } catch {}
            }
        },
        //全屏
        screen() {
            this.$TOOL.screen(document.documentElement)
        },
        //显示短消息
        showMsg() {
            this.msg = true
        },
        //搜索
        search() {
            this.searchVisible = true
        },
        //任务
        tasks() {
            this.tasksVisible = true
        },
    },
    props: [],
    watch: {
        'config.dark'(val) {
            if (val) {
                document.documentElement.classList.add('dark')
                this.$TOOL.data.set('APP_SET_DARK', val)
            } else {
                document.documentElement.classList.remove('dark')
                this.$TOOL.data.remove('APP_SET_DARK')
            }
        },
    },
}
</script>

<style scoped>
.user-bar {
    display: flex;
    align-items: center;
    height: 100%;
}

.user-bar .panel-item {
    padding: 0 0.5rem;
    cursor: pointer;
    height: 100%;
    display: flex;
    align-items: center;
}

.user-bar .panel-item i {
    font-size: 1.2rem;
}

.user-bar .panel-item:hover {
    background: rgba(0, 0, 0, 0.1);
}

.user-bar .user-avatar {
    height: 4rem;
    display: flex;
    align-items: center;
}

.user-bar .user-avatar label {
    display: inline-block;
    margin-left: 0.4rem;
    font-size: 0.9rem;
    cursor: pointer;
}
</style>