<template>
    <el-container>
        <el-header>
            <div class="common-header-left">
                <div class="common-header-logo">
                    <img :alt="$CONFIG.APP_NAME" class="logo-fill-color" src="@/assets/img/logo.svg" />
                    <label>{{ $CONFIG.APP_NAME }}</label>
                </div>
                <div class="common-header-title">{{ title }}</div>
            </div>
            <div class="common-header-right">
                <router-link to="/guest/login">{{ $t('返回登录') }}</router-link>
            </div>
        </el-header>
        <el-main>
            <div class="common-container">
                <h2 class="common-title">{{ title }}</h2>
                <div class="common-main el-card">
                    <slot></slot>
                </div>
            </div>
        </el-main>
    </el-container>
</template>

<script>
export default {
    props: {
        title: { type: String, default: '' },
    },
}
</script>

<style></style>