<template>
    <el-card :header="$t('登录日志')" shadow="never">
        <login-log :ownerId="$GLOBAL.user.id.toString()" :show-filter="false" />
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

<style />