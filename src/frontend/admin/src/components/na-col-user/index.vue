<template>
    <el-table-column v-bind="$attrs">
        <template #default="{ row }">
            <div :style="{ display: $TOOL.getNestedProperty(row, $attrs.prop) ? 'flex' : 'none' }" class="el-table-column-avatar">
                <el-avatar
                    v-if="$TOOL.getNestedProperty(row, $attrs.nestProp)"
                    :src="getAvatar(row, $attrs.nestProp)"
                    @click="click($TOOL.getNestedProperty(row, $attrs.prop))"
                    size="small"
                    style="cursor: pointer" />
                <div>
                    <p>{{ $TOOL.getNestedProperty(row, $attrs.nestProp) }}</p>
                    <p v-if="$attrs.nestProp2">{{ $TOOL.getNestedProperty(row, $attrs.nestProp2) }}</p>
                </div>
            </div>
            <save-dialog v-if="dialog.save" @closed="dialog.save = null" @mounted="$refs.saveDialog.open(dialog.save)" ref="saveDialog" />
        </template>
    </el-table-column>
</template>
<script>
import { defineAsyncComponent } from 'vue'
const saveDialog = defineAsyncComponent(() => import('@/views/sys/user/save'))

export default {
    components: { saveDialog },
    computed: {},
    created() {},
    data() {
        return {
            dialog: {},
        }
    },
    emits: ['click'],
    methods: {
        async click(id) {
            if (!this.clickOpenDialog) {
                return
            }
            this.dialog.save = { mode: 'view', row: { id: id } }
        },
        //获取头像
        getAvatar(row, prop) {
            return row.avatar ? row.avatar : this.$CONFIG.DEFAULT_AVATAR(this.$TOOL.getNestedProperty(row, prop))
        },
    },
    mounted() {},
    props: {
        clickOpenDialog: { type: Boolean, default: true },
    },
    watch: {},
}
</script>
<style scoped />