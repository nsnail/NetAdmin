import config from '@/config'

//系统路由
const routes = [
    {
        name: 'layout',
        path: '/',
        component: () => import(/* webpackChunkName: "layout" */ '@/layout'),
        redirect: config.DASHBOARD_URL || '/home',
        children: [
            {
                path: '/profile',
                component: () => import(/* webpackChunkName: "userRegister" */ '@/views/profile'),
                meta: {
                    title: '基本资料',
                },
                children: [
                    {
                        path: '/profile/settings',
                        component: () => import(/* webpackChunkName: "userRegister" */ '@/views/profile/settings.vue'),
                        meta: {
                            title: '系统设置',
                        },
                    },
                    {
                        path: '/profile/message',
                        component: () => import(/* webpackChunkName: "userRegister" */ '@/views/profile/message/index.vue'),
                        meta: {
                            title: '消息中心',
                        },
                    },
                    {
                        path: '/profile/account',
                        component: () => import(/* webpackChunkName: "userRegister" */ '@/views/profile/account/index.vue'),
                        meta: {
                            title: '基本资料',
                        },
                    },
                    {
                        path: '/profile/token',
                        component: () => import(/* webpackChunkName: "userRegister" */ '@/views/profile/token/index.vue'),
                        meta: {
                            title: '授权信息',
                        },
                    },
                    {
                        path: '/profile/logs',
                        component: () => import(/* webpackChunkName: "userRegister" */ '@/views/profile/logs.vue'),
                        meta: {
                            title: '登录日志',
                        },
                    },
                ],
            },
        ],
    },
    {
        path: '/guest/login',
        component: () => import(/* webpackChunkName: "login" */ '@/views/guest/login'),
        meta: {
            title: '登录',
        },
    },
    {
        path: '/guest/register',
        component: () => import(/* webpackChunkName: "userRegister" */ '@/views/guest/register.vue'),
        meta: {
            title: '注册',
        },
    },
    {
        path: '/guest/reset-password',
        component: () => import(/* webpackChunkName: "resetPassword" */ '@/views/guest/resetPassword'),
        meta: {
            title: '重置密码',
        },
    },
    {
        path: '/guest/view-doc/:id',
        component: () => import(/* webpackChunkName: "view-doc" */ '@/views/guest/view-doc'),
        meta: {
            title: '查看文档',
        },
    },
]

export default routes