<template>
    <el-table-column v-bind="$attrs" :prop="prop">
        <template #default="{ row }">
            <el-tag
                v-if="row[prop]"
                :type="['success', 'danger', 'info', 'primary', 'warning'][$TOOL.crypto.hashCode(row[prop]) % 5]"
                @click="$emit('click', row)">
                {{ row[prop] }}
            </el-tag>
        </template>
    </el-table-column>
</template>
<script>
export default {
    emits: ['click'],
    props: {
        prop: { type: String },
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
<style scoped>
.el-tag {
    cursor: pointer;
}
</style>