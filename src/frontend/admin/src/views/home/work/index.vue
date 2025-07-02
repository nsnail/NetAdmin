<template>
    <el-card :header="$t('我的常用')" shadow="never">
        <my-app />
    </el-card>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const myApp = defineAsyncComponent(() => import('./components/my-app'))

export default {
    components: {
        myApp,
    },
    data() {
        return {}
    },
    mounted() {
        this.$nextTick(() => this.$emit('on-mounted'))
    },
    methods: {},
}
</script>

<style scoped />