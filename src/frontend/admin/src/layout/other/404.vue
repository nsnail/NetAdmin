<template>
    <div class="router-err">
        <div class="router-err__icon">
            <img src="@/assets/img/404.png" />
        </div>
        <div class="router-err__content">
            <h2>{{ $t('无权限或找不到页面') }}</h2>
            <p>{{ $t('当前页面无权限访问或者打开了一个不存在的链接，请检查当前账户权限和链接的可访问性。') }}</p>
            <el-button @click="goHome" plain round type="primary">{{ $t('返回首页') }}</el-button>
            <el-button @click="goLogin" plain round type="primary">{{ $t('重新登录') }}</el-button>
            <el-button @click="goBack" round type="primary">{{ $t('返回上一页') }}</el-button>
        </div>
    </div>
</template>

<script>
export default {
    methods: {
        goHome() {
            location.href = '/'
        },
        goBack() {
            this.$router.go(-1)
        },
        goLogin() {
            this.$router.push('/guest/login')
        },
    },
}
</script>

<style scoped>
.router-err {
    display: flex;
    width: 70rem;
    margin: 4rem auto;
    align-items: center;
}

.router-err__icon {
    width: 30rem;
}

.router-err__icon img {
    width: 100%;
}

.router-err__content {
    flex: 1;
    padding: 3rem;
}

.router-err__content h2 {
    font-size: 2rem;
}

.router-err__content p {
    font-size: 1.1rem;
    color: #999;
    margin: 1rem 0 2.5rem 0;
    line-height: 1.5;
}

@media (max-width: 77rem) {
    .router-err {
        display: block;
        width: 100%;
        margin-top: 1rem;
        text-align: center;
    }

    .router-err__icon {
        width: 22rem;
        margin: 0 auto;
    }
}
</style>