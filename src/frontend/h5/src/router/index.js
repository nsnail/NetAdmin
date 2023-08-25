import {createRouter, createWebHistory} from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'home',
            component: HomeView
        },
        {
            path: '/about',
            name: 'about',
            // route level code-splitting
            // this generates a separate chunk (About.[hash].js) for this route
            // which is lazy-loaded when the route is visited.
            component: () => import('../views/AboutView.vue')
        },
        {
            path: '/category',
            name: 'category',
            component: () => import('../views/CategoryView.vue')
        },
        {
            path: '/product',
            name: 'product',
            component: () => import('../views/ProductView.vue')
        },
        {
            path: '/account/register',
            name: 'register',
            component: () => import('../views/account/RegisterView.vue')
        },
        {
            path: '/account/login-by-pwd',
            name: 'loginByPwd',
            component: () => import('../views/account/LoginByPwdView.vue')
        },
        {
            path: '/account/login-by-sms',
            name: 'loginBySms',
            component: () => import('../views/account/LoginBySmsView.vue')
        },
        {
            path: '/account/reset-pwd',
            name: 'resetPwd',
            component: () => import('../views/account/ResetPwdView.vue')
        },
        {
            path: '/member',
            name: 'memberIndex',
            component: () => import('../views/member/IndexView.vue')
        }
    ]
})

export default router