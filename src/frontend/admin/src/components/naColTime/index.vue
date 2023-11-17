<template>
    <el-table-column :label="label" :prop="prop">
        <template #default="scope">
            {{ tool.dateFormat(scope.row[prop]) }}
        </template>
    </el-table-column>
</template>
<style scoped></style>
<script>
import tool from '@/utils/tool'

export default {
    emits: ['click'],
    props: {
        prop: { type: String },
        label: { type: String },
    },
    data() {
        return {}
    },
    mounted() {},
    created() {},
    components: {},
    computed: {
        tool() {
            return tool
        },
    },
    methods: {},
}
</script>