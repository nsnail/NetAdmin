<template>
    <el-card :header="$t('登录日志')" shadow="never">
        <login-log :owner-id="$GLOBAL.user.id.toString()" :show-filter="false" />
    </el-card>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const loginLog = defineAsyncComponent(() => import('@/views/sys/log/login'))

export default {
    components: { loginLog },
    data() {
        return {}
    },
}
</script>

<style />