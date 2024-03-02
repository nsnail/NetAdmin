<template>
    <el-table-column v-bind="$attrs">
        <template #default="scope">
            <div @click="click(tool.getNestedProperty(scope.row, $attrs.prop))" class="avatar" style="cursor: pointer">
                <el-avatar v-if="tool.getNestedProperty(scope.row, $attrs.nestProp)" :src="getAvatar(scope)" size="small"></el-avatar>
                <el-text tag="ins">{{ tool.getNestedProperty(scope.row, $attrs.nestProp) }}</el-text>
            </div>
            <save-dialog v-if="dialog.save" @closed="dialog.save = false" ref="saveDialog"></save-dialog>
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
import saveDialog from '@/views/sys/user/save.vue'
import tool from '@/utils/tool'

export default {
    emits: ['click'],
    props: {},
    data() {
        return {
            dialog: { save: false },
        }
    },
    mounted() {},
    created() {},
    components: { saveDialog },
    computed: {
        tool() {
            return tool
        },
    },
    methods: {
        async click(id) {
            this.dialog.save = true
            await this.$nextTick()
            await this.$refs.saveDialog.open('view', { id: id })
        },
        //获取头像
        getAvatar(scope) {
            return scope.row.avatar ? scope.row.avatar : this.$CONFIG.DEFAULT_AVATAR
        },
    },
}
</script>