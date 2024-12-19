<template>
    <el-card :header="$t('基本资料')" shadow="never">
        <el-form :model="form" label-width="15rem" ref="form">
            <el-form-item :label="$t('头像')">
                <scUpload v-model="form.avatar" :onSuccess="updateUser" :title="$t('上传头像')"></scUpload>
            </el-form-item>
            <el-form-item :label="$t('用户编号')">
                <el-input v-model="form.id" readonly></el-input>
            </el-form-item>
            <el-form-item :label="$t('用户名称')">
                <el-input v-model="form.userName" readonly></el-input>
            </el-form-item>
            <el-form-item :label="$t('密码')">
                <div class="flex w100p gap05">
                    <el-input readonly value="******"></el-input>
                    <el-button @click="setPasswordClick">{{ $t('设置密码') }}</el-button>
                </div>
            </el-form-item>
            <el-form-item :label="$t('注册时间')">
                <el-input v-model="form.createdTime" readonly></el-input>
            </el-form-item>
            <el-form-item :label="$t('手机号码')">
                <div class="flex w100p gap05">
                    <el-input v-model="form.mobile" readonly></el-input>
                    <el-button @click="setMobileClick">{{ form.mobile ? $t('更换手机') : $t('绑定手机') }}</el-button>
                </div>
            </el-form-item>
            <el-form-item :label="$t('电子邮箱')">
                <div class="flex w100p gap05">
                    <el-input v-model="form.email" readonly></el-input>
                    <el-button @click="setEmailClick">{{ $t('设置邮箱') }}</el-button>
                </div>
            </el-form-item>
        </el-form>
    </el-card>

    <set-mobile-dialog
        v-if="dialog.setMobile"
        @closed="dialog.setMobile = null"
        @mounted="$refs.setMobileDialog.open(dialog.setMobile)"
        @success="setSuccess"
        ref="setMobileDialog"></set-mobile-dialog>
    <set-password-dialog
        v-if="dialog.setPassword"
        @closed="dialog.setPassword = null"
        @mounted="$refs.setPasswordDialog.open(dialog.setPassword)"
        ref="setPasswordDialog"></set-password-dialog>
    <set-email-dialog
        v-if="dialog.setEmail"
        @closed="dialog.setEmail = null"
        @mounted="$refs.setEmailDialog.open(dialog.setEmail)"
        @success="setSuccess"
        ref="setEmailDialog"></set-email-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const setMobileDialog = defineAsyncComponent(() => import('@/views/profile/account/set-mobile.vue'))
const setPasswordDialog = defineAsyncComponent(() => import('@/views/profile/account/set-password.vue'))
const setEmailDialog = defineAsyncComponent(() => import('@/views/profile/account/set-email.vue'))
const scUpload = defineAsyncComponent(() => import('@/components/scUpload'))

export default {
    components: { setMobileDialog, setPasswordDialog, setEmailDialog, scUpload },
    created() {
        this.form = this.$GLOBAL.user
    },
    data() {
        return {
            dialog: {},
            form: {},
        }
    },
    methods: {
        updateUser(res) {
            try {
                this.$API.sys_user.setAvatar.post({ avatar: res.data })
                this.$message.success(this.$t('操作成功'))
            } catch {
                //
            }
        },
        setSuccess(data) {
            this.form = this.$GLOBAL.user = data
        },
        async setPasswordClick() {
            this.dialog.setPassword = {}
        },
        async setEmailClick() {
            this.dialog.setEmail = {}
        },
        async setMobileClick() {
            this.dialog.setMobile = { mode: this.form.mobile ? 'edit' : 'add' }
        },
    },
    props: [],
    watch: {},
}
</script>

<style scoped></style>