<template>
    <el-table-column :label="label" :prop="prop">
        <template #default="{ row }">
            {{ tool.dateFormat(row[prop]) }}
        </template>
    </el-table-column>
</template>
<style scoped />
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