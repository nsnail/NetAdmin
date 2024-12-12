<template>
    <el-card :header="$t('我的常用')" shadow="never">
        <myapp></myapp>
    </el-card>
</template>

<script>
import myapp from './components/myapp'

export default {
    components: {
        myapp,
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

<style scoped></style>