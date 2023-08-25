<script setup>
import {RouterView} from 'vue-router'
import router from "@/router";
import {computed, ref} from "vue";

console.log(router.currentRoute.value.path)
const noFooterRoutes = ref(['/product', '/account'])
const currentRoutePath = computed(() => router.currentRoute.value.path);
const shouldHideFooter = computed(() => {
    return noFooterRoutes.value.some(route => currentRoutePath.value.startsWith(route));
});

</script>

<template>
    <header></header>
    <main>
        <RouterView/>
    </main>
    <footer v-if="!shouldHideFooter">
        <van-tabbar route>
            <van-tabbar-item icon="home-o" to="/">首页</van-tabbar-item>
            <van-tabbar-item icon="apps-o" to="/category">分类</van-tabbar-item>
            <van-tabbar-item icon="user-o" to="/member">个人中心</van-tabbar-item>
        </van-tabbar>
    </footer>
    <van-back-top bottom="80"/>
</template>