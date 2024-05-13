<template>
    <el-table-column v-bind="$attrs">
        <template #default="scope">
            <el-text @click="click(scope.row)" style="cursor: pointer" tag="ins">
                {{ tool.getNestedProperty(scope.row, $attrs.prop) }}
            </el-text>
        </template>
    </el-table-column>
</template>
<script>
import tool from '@/utils/tool'
export default {
    emits: ['click'],
    props: {},
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
    methods: {
        async click(row) {
            this.$emit('click', row)
        },
    },
}
</script>
<style scoped></style>