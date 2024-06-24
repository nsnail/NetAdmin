<template>
    <el-table-column v-bind="$attrs">
        <template #default="{ row }">
            <div @click="click($TOOL.getNestedProperty(row, $attrs.prop))" class="avatar">
                <el-avatar v-if="$TOOL.getNestedProperty(row, $attrs.nestProp)" :src="getAvatar(row, $attrs.nestProp)" size="small"></el-avatar>
                <div>
                    <p>{{ $TOOL.getNestedProperty(row, $attrs.nestProp) }}</p>
                    <p v-if="$attrs.nestProp2">{{ $TOOL.getNestedProperty(row, $attrs.nestProp2) }}</p>
                </div>
            </div>
            <save-dialog v-if="dialog.save" @closed="dialog.save = false" ref="saveDialog"></save-dialog>
        </template>
    </el-table-column>
</template>
<script>
import saveDialog from '@/views/sys/user/save.vue'

export default {
    components: { saveDialog },
    computed: {},
    created() {},
    data() {
        return {
            dialog: { save: false },
        }
    },
    emits: ['click'],
    methods: {
        async click(id) {
            this.dialog.save = true
            await this.$nextTick()
            await this.$refs.saveDialog.open({ mode: 'view', row: { id: id } })
        },
        //获取头像
        getAvatar(row, prop) {
            return row.avatar ? row.avatar : this.$CONFIG.DEFAULT_AVATAR(this.$TOOL.getNestedProperty(row, prop))
        },
    },
    mounted() {},
    props: {},
    watch: {},
}
</script>
<style lang="scss" scoped>
.avatar {
    div:last-child {
        line-height: 1.2rem;
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