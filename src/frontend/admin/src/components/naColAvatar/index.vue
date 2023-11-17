<template>
    <el-table-column :label="label" :prop="prop" sortable="custom">
        <template #default="scope">
            <div class="avatar">
                <el-avatar :src="getAvatar(scope)" size="small"></el-avatar>
                <span>{{ tool.getNestedProperty(scope.row, prop) }}</span>
            </div>
        </template>
    </el-table-column>
</template>
<style scoped>
.avatar {
    display: flex;
    gap: 0.5rem;
}
</style>
<script>
import tool from '../../utils/tool'

export default {
    emits: [],
    props: {
        label: { type: String },
        prop: { type: String },
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
    methods: {
        //获取头像
        getAvatar(scope) {
            return scope.row.avatar ? scope.row.avatar : this.$CONFIG.DEFAULT_AVATAR
        },
    },
}
</script>