<template>
    <el-table-column :label="label" :prop="`${prop}.${field}`">
        <template #default="scope">
            <template v-for="(item, i) in Array.isArray(scope.row[prop]) ? scope.row[prop] : [scope.row[prop]]" :key="i">
                <el-tag v-if="item" @click="$emit('click', item)">
                    {{ item ? item[field] : '' }}
                </el-tag>
            </template>
        </template>
    </el-table-column>
</template>
<style scoped></style>
<script>
export default {
    emits: ['click'],
    props: {
        prop: { type: String },
        field: { type: String },
        label: { type: String },
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