<template>
    <el-table-column v-bind="$attrs">
        <template #default="{ row }">
            <p>{{ row.id }}</p>
            <p v-if="showTime" class="color-secondary">{{ row.createdTime }}</p>
            <slot :data="row"></slot>
        </template>
    </el-table-column>
</template>
<script>
export default {
    emits: [],
    props: {
        showTime: {
            type: Boolean,
            default: true,
        },
    },
    data() {
        return {}
    },
    mounted() {},
    created() {},
    components: {},
    computed: {},
    methods: {},
}
</script>
<style scoped></style>