<template>
    <div v-if="avatar" class="d1">
        <el-avatar :src="avatar" @click="click" size="small" />
        <div class="d2">
            <p>{{ user?.userName }}</p>
            <p class="p1">{{ user?.id }}</p>
        </div>
    </div>
    <save-dialog v-if="dialog.save" @closed="dialog.save = null" @mounted="$refs.saveDialog.open(dialog.save)" ref="saveDialog" />
</template>
<script>
import { defineAsyncComponent } from 'vue'
const saveDialog = defineAsyncComponent(() => import('@/views/sys/power/user/save'))

export default {
    components: { saveDialog },
    computed: {},
    created() {
        if (this.nestProp === 'createdUserName') {
            this.user = {
                userName: this.$TOOL.getNestedProperty(this.row, 'createdUserName'),
                id: this.$TOOL.getNestedProperty(this.row, 'createdUserId'),
            }
        } else {
            this.user = this.nestProp ? this.$TOOL.getNestedProperty(this.row, this.nestProp) : this.row
        }
        this.avatar = this.user?.avatar ?? (this.user?.userName ? this.$CONFIG.DEFAULT_AVATAR(this.user.userName) : null)
    },
    data() {
        return {
            dialog: {},
            user: null,
            avatar: null,
        }
    },
    emits: ['click'],
    methods: {
        async click() {
            if (!this.showUserDialog || !this.user?.id) {
                return
            }
            this.dialog.save = { mode: 'view', row: { id: this.user?.id } }
        },
    },
    mounted() {},
    props: {
        showUserDialog: { type: Boolean, default: true },
        row: { type: Object },
        nestProp: { type: String },
    },
    watch: {},
}
</script>
<style lang="scss" scoped>
.el-avatar {
    cursor: pointer;
}
.d1 {
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 0.5rem;
}
.d2 {
    line-height: 1.2rem;
}
.p1 {
    color: var(--el-color-info-light-3);
}
</style>