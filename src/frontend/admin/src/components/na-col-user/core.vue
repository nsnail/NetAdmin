<template>
    <div v-if="user && avatar" class="d1">
        <el-avatar :src="avatar" @click="click" size="small" />
        <div class="d2">
            <p>{{ user[this.userNameFieldName] }}</p>
            <p class="p1">{{ user[this.userIdFieldName] }}</p>
        </div>
    </div>
    <save-dialog v-if="dialog.save" @closed="dialog.save = null" @mounted="$refs.saveDialog.open(dialog.save)" ref="saveDialog" />
    <view-dialog v-if="dialog.view" @closed="dialog.view = null" @mounted="$refs.viewDialog.open(dialog.view)" ref="viewDialog" />
</template>
<script>
import { defineAsyncComponent } from 'vue'
const saveDialog = defineAsyncComponent(() => import('@/views/sys/power/user/save'))
const viewDialog = defineAsyncComponent(() => import('@/views/sys/market/invite/user'))
export default {
    components: { saveDialog, viewDialog },
    computed: {},
    created() {
        this.user = this.userObjPath ? this.$TOOL.getNestedProperty(this.row, this.userObjPath) : this.row
        if (!this.user) return
        this.avatar =
            this.user[this.userAvatarFieldName] ??
            (this.user[this.userNameFieldName] ? this.$CONFIG.DEFAULT_AVATAR(this.user[this.userNameFieldName]) : null)
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
            if (!this.user[this.userIdFieldName]) {
                return
            }
            if (this.$GLOBAL.hasApiPermission(`api/sys/user/get`)) {
                this.dialog.save = { mode: 'view', row: { id: this.user[this.userIdFieldName] } }
            } else {
                this.dialog.view = {
                    data: { user: { id: this.user[this.userIdFieldName], userName: this.user[this.userNameFieldName] }, tabId: `messageOrder` },
                }
            }
        },
    },
    mounted() {},
    props: {
        row: { type: Object },
        userObjPath: { type: String },
        userIdFieldName: { type: String, default: 'id' },
        userNameFieldName: { type: String, default: 'userName' },
        userAvatarFieldName: { type: String, default: 'avatar' },
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