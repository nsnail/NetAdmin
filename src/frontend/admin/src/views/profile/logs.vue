<template>
    <el-card :header="$t('登录日志')" shadow="never">
        <login-log :keywords="$GLOBAL.user.id" :show-filter="false"></login-log>
    </el-card>
</template>

<script>
import LoginLog from '@/views/sys/log/login'

export default {
    components: { LoginLog },
    data() {
        return {}
    },
}
</script>

<style></style>