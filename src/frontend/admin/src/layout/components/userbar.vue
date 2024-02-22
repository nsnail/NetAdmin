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
        <div @click="tasks" class="tasks panel-item">
            <el-icon>
                <sc-icon-ScheduledJob />
            </el-icon>
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
        <el-dropdown @command="handleUser" class="user panel-item" trigger="click">
            <div class="user-avatar">
                <el-avatar :size="30" :src="user.avatar ? user.avatar : $CONFIG.DEFAULT_AVATAR"></el-avatar>
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
        <tasks></tasks>
    </el-drawer>
</template>

<script>
import search from './search.vue'
import tasks from './tasks.vue'
import message from '@/views/profile/message/components/list.vue'
export default {
    components: {
        search,
        tasks,
        message,
    },
    watch: {
        'config.dark'(val) {
            if (val) {
                document.documentElement.classList.add('dark')
                this.$TOOL.data.set('APP_DARK', val)
            } else {
                document.documentElement.classList.remove('dark')
                this.$TOOL.data.remove('APP_DARK')
            }
        },
    },
    data() {
        return {
            config: {
                dark: this.$TOOL.data.get('APP_DARK') || false,
            },
            user: {},
            userName: '',
            userNameF: '',
            searchVisible: false,
            tasksVisible: false,
            msg: false,
            unreadCnt: 0,
        }
    },
    async created() {
        this.user = this.$GLOBAL.user
        const res = await this.$API.sys_sitemsg.unreadCount.post()
        this.unreadCnt = res.data
    },
    methods: {
        configDark() {
            this.config.dark = !this.config.dark
        },
        gotoMsgCenter() {
            this.$router.push({ path: '/profile/message' })
            this.msg = false
        },
        //个人信息
        handleUser(command) {
            if (command === 'uc') {
                this.$router.push({ path: '/profile/account' })
            }
            if (command === 'cmd') {
                this.$router.push({ path: '/cmd' })
            }
            if (command === 'clearCache') {
                this.$confirm('清除缓存会将系统初始化，包括登录状态、主题、语言设置等，是否继续？', '提示', {
                    type: 'info',
                })
                    .then(() => {
                        const loading = this.$loading()
                        this.$TOOL.data.clear()
                        this.$TOOL.cookie.clear()
                        this.$router.replace({ path: '/guest/login' })
                        setTimeout(() => {
                            loading.close()
                            location.reload()
                        }, 1000)
                    })
                    .catch(() => {
                        //取消
                    })
            }
            if (command === 'outLogin') {
                this.$confirm('确认是否退出当前用户？', '提示', {
                    type: 'warning',
                    confirmButtonText: '退出',
                    confirmButtonClass: 'el-button--danger',
                })
                    .then(() => {
                        this.$TOOL.cookie.clear()
                        this.$router.replace({ path: '/guest/login' })
                    })
                    .catch(() => {
                        //取消退出
                    })
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
}
</script>

<style scoped>
.user-bar {
    display: flex;
    align-items: center;
    height: 100%;
}

.user-bar .panel-item {
    padding: 0 10px;
    cursor: pointer;
    height: 100%;
    display: flex;
    align-items: center;
}

.user-bar .panel-item i {
    font-size: 16px;
}

.user-bar .panel-item:hover {
    background: rgba(0, 0, 0, 0.1);
}

.user-bar .user-avatar {
    height: 49px;
    display: flex;
    align-items: center;
}

.user-bar .user-avatar label {
    display: inline-block;
    margin-left: 5px;
    font-size: 12px;
    cursor: pointer;
}
</style>