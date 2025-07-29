<template>
    <el-table-column :label="label" :prop="prop" sortable="custom">
        <template #default="{ row }">
            <div class="na-col-avatar-div0">
                <el-avatar :src="getAvatar(row, prop, avatar)" size="small" />
                <el-link v-if="this.click" @click="this.click(row)"> {{ tool.getNestedProperty(row, prop) }}</el-link>
                <span v-else> {{ tool.getNestedProperty(row, prop) }}</span>
            </div>
        </template>
    </el-table-column>
</template>
<style>
.na-col-avatar-div0 {
    display: flex;
    gap: 0.5rem;
}
</style>
<script>
import tool from '@/utils/tool'

export default {
    emits: [],
    props: {
        label: { type: String },
        prop: { type: String },
        avatar: { type: String },
        click: { type: Function },
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
        getAvatar(row, prop, avatar) {
            return avatar
                ? tool.getNestedProperty(row, avatar)
                : row.avatar
                  ? row.avatar
                  : this.$CONFIG.DEFAULT_AVATAR(tool.getNestedProperty(row, prop))
        },
    },
}
</script>