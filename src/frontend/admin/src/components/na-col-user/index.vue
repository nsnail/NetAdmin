<template>
    <el-table-column v-bind="$attrs">
        <template #default="{ row }">
            <core v-bind="$attrs" :row="row" />
        </template>
    </el-table-column>
</template>
<script>
import core from '@/components/na-col-user/core.vue'
export default {
    components: { core },
}
</script>
<style scoped />