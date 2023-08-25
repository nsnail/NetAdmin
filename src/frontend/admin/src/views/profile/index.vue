<template>
    <el-container class="page-user">
        <el-aside>
            <el-container>
                <el-header>
                    <div class="user-info-top">
                        <el-avatar :size="70" :src="user.avatar ? user.avatar : $CONFIG.DEFAULT_AVATAR"></el-avatar>
                        <h2>{{ user.userName }}</h2>
                        <p>
                            <el-tag v-for="(item, i) in user.roles" :key="i" effect="dark" round size="large">{{ item.name }}</el-tag>
                        </p>
                    </div>
                </el-header>
                <el-main class="nopadding">
                    <el-menu :default-active="page" class="menu">
                        <el-menu-item-group v-for="group in menu" :key="group.groupName" :title="group.groupName">
                            <el-menu-item v-for="item in group.list" :key="item.component" :index="item.component" @click="openPage">
                                <el-icon v-if="item.icon">
                                    <component :is="item.icon" />
                                </el-icon>
                                <template #title>
                                    <span>{{ item.title }}</span>
                                </template>
                            </el-menu-item>
                        </el-menu-item-group>
                    </el-menu>
                </el-main>
            </el-container>
        </el-aside>
        <el-main>
            <Suspense>
                <template #default>
                    <component :is="page" />
                </template>
                <template #fallback>
                    <el-skeleton :rows="3" />
                </template>
            </Suspense>
        </el-main>
    </el-container>
</template>

<script>
import { defineAsyncComponent } from 'vue'

export default {
    components: {
        account: defineAsyncComponent(() => import('./user/account')),
        settings: defineAsyncComponent(() => import('./user/settings')),
        pushSettings: defineAsyncComponent(() => import('./user/pushSettings')),
        setMobile: defineAsyncComponent(() => import('./user/setMobile')),
        space: defineAsyncComponent(() => import('./user/space')),
        logs: defineAsyncComponent(() => import('./user/logs')),
    },
    data() {
        return {
            menu: [
                {
                    groupName: '基本设置',
                    list: [
                        {
                            icon: 'el-icon-postcard',
                            title: '账号信息',
                            component: 'account',
                        },
                        {
                            icon: 'el-icon-operation',
                            title: '个人设置',
                            component: 'settings',
                        },
                        {
                            icon: 'el-icon-bell',
                            title: '通知设置',
                            component: 'pushSettings',
                        },
                    ],
                },
                {
                    groupName: '数据管理',
                    list: [
                        {
                            icon: 'el-icon-coin',
                            title: '存储空间信息',
                            component: 'space',
                        },
                        {
                            icon: 'el-icon-clock',
                            title: '操作日志',
                            component: 'logs',
                        },
                    ],
                },
            ],
            user: {},
            page: 'account',
        }
    },
    created() {
        this.user = this.$GLOBAL.user
    },
    methods: {
        openPage(item) {
            this.page = item.index
        },
    },
}
</script>