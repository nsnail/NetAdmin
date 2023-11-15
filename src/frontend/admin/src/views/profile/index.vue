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
                    <el-menu :default-active="$route.path" class="menu">
                        <el-menu-item-group v-for="group in menu" :key="$t(group.groupName)" :title="$t(group.groupName)">
                            <el-menu-item v-for="item in group.list" :key="item.component" :index="item.component" @click="openPage">
                                <el-icon v-if="item.icon">
                                    <component :is="item.icon" />
                                </el-icon>
                                <template #title>
                                    <span>{{ $t(item.title) }}</span>
                                </template>
                            </el-menu-item>
                        </el-menu-item-group>
                    </el-menu>
                </el-main>
            </el-container>
        </el-aside>
        <el-main>
            <router-view v-slot="{ Component }">
                <component :is="Component" />
            </router-view>
        </el-main>
    </el-container>
</template>

<script>
export default {
    data() {
        return {
            menu: [
                {
                    groupName: '基本设置',
                    list: [
                        {
                            icon: 'el-icon-postcard',
                            title: '账号信息',
                            component: '/profile/account',
                        },
                        {
                            icon: 'el-icon-operation',
                            title: '系统设置',
                            component: '/profile/settings',
                        },
                    ],
                },
                {
                    groupName: '数据管理',
                    list: [
                        {
                            icon: 'el-icon-bell',
                            title: '我的消息',
                            component: '/profile/message',
                        },
                    ],
                },
            ],
            user: {},
        }
    },
    created() {
        this.user = this.$GLOBAL.user
    },
    methods: {
        openPage(item) {
            this.$router.push({ path: item.index })
        },
    },
}
</script>