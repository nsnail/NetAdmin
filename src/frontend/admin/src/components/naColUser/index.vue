<template>
    <el-table-column v-bind="$attrs">
        <template #default="scope">
            <div @click="click(tool.getNestedProperty(scope.row, $attrs.prop))" class="avatar">
                <el-avatar
                    v-if="tool.getNestedProperty(scope.row, $attrs.nestProp)"
                    :src="getAvatar(scope, $attrs.nestProp)"
                    size="small"></el-avatar>
                <div>
                    <p>{{ tool.getNestedProperty(scope.row, $attrs.nestProp) }}</p>
                    <p v-if="$attrs.nestProp2">{{ tool.getNestedProperty(scope.row, $attrs.nestProp2) }}</p>
                </div>
            </div>
            <save-dialog v-if="dialog.save" @closed="dialog.save = false" ref="saveDialog"></save-dialog>
        </template>
    </el-table-column>
</template>
<style lang="scss" scoped>
.avatar {
    div:last-child {
        line-height: 1rem;
        p:last-child {
            color: var(--el-color-info-light-3);
        }
    }
    justify-content: center;
    align-items: center;
    cursor: pointer;
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
        getAvatar(scope, prop) {
            return scope.row.avatar ? scope.row.avatar : this.$CONFIG.DEFAULT_AVATAR(tool.getNestedProperty(scope.row, prop))
        },
    },
}
</script>