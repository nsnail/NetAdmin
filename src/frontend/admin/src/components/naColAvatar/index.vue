<template>
    <el-table-column :label="label" :prop="prop" sortable="custom">
        <template #default="{ row }">
            <div class="avatar">
                <el-avatar :src="getAvatar(row, prop)" size="small"></el-avatar>
                <span>{{ tool.getNestedProperty(row, prop) }}</span>
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
        getAvatar(row, prop) {
            return row.avatar ? row.avatar : this.$CONFIG.DEFAULT_AVATAR(tool.getNestedProperty(row, prop))
        },
    },
}
</script>