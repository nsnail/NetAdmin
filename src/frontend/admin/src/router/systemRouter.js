import config from '@/config'

//系统路由
const routes = [
    {
        name: 'layout',
        path: '/',
        component: () => import(/* webpackChunkName: "layout" */ '@/layout'),
        redirect: config.DASHBOARD_URL || '/home',
        children: [],
    },
    {
        path: '/anonymous/login',
        component: () => import(/* webpackChunkName: "login" */ '@/views/anonymous/login'),
        meta: {
            title: '登录',
        },
    },
    {
        path: '/anonymous/register',
        component: () => import(/* webpackChunkName: "userRegister" */ '@/views/anonymous/register.vue'),
        meta: {
            title: '注册',
        },
    },
    {
        path: '/anonymous/reset-password',
        component: () => import(/* webpackChunkName: "resetPassword" */ '@/views/anonymous/resetPassword'),
        meta: {
            title: '重置密码',
        },
    },
]

export default routes