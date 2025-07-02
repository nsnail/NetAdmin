<template>
    <el-card :header="$t('我的消息')" shadow="never" style="height: 100%">
        <list />
    </el-card>
</template>
<script>
import list from '@/views/profile/message/components/list'

export default {
    components: {
        list,
    },
}
</script>
<style scoped>
/deep/ .el-card__body {
    padding: 0;
    height: calc(100% - 3rem);
}
</style>