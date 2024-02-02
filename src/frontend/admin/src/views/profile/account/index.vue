<template>
    <el-card :header="$t('账号信息')" shadow="never">
        <el-form :model="form" label-width="10rem" ref="form">
            <el-form-item :label="$t('头像')">
                <sc-upload v-model="form.avatar" :onSuccess="updateUser" :title="$t('上传头像')"></sc-upload>
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
                    <el-button @click="setPasswordClick">设置密码</el-button>
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
                    <el-button @click="setEmailClick">设置邮箱</el-button>
                </div>
            </el-form-item>
        </el-form>
    </el-card>

    <set-mobile-dialog v-if="dialog.setMobile" @closed="dialog.setMobile = false" @success="setSuccess" ref="setMobileDialog"></set-mobile-dialog>
    <set-password-dialog v-if="dialog.setPassword" @closed="dialog.setPassword = false" ref="setPasswordDialog"></set-password-dialog>
    <set-email-dialog v-if="dialog.setEmail" @closed="dialog.setEmail = false" @success="setSuccess" ref="setEmailDialog"></set-email-dialog>
</template>

<script>
import setMobileDialog from '@/views/profile/account/set-mobile.vue'
import setPasswordDialog from '@/views/profile/account/set-password.vue'
import setEmailDialog from '@/views/profile/account/set-email.vue'

export default {
    components: { setMobileDialog, setPasswordDialog, setEmailDialog },
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
            this.dialog.setPassword = true
            await this.$nextTick()
            this.$refs.setPasswordDialog.open()
        },
        async setEmailClick() {
            this.dialog.setEmail = true
            await this.$nextTick()
            this.$refs.setEmailDialog.open()
        },
        async setMobileClick() {
            this.dialog.setMobile = true
            await this.$nextTick()
            this.$refs.setMobileDialog.open(this.form.mobile ? 'edit' : 'add')
        },
    },

    created() {
        this.form = this.$GLOBAL.user
    },
    data() {
        return {
            dialog: {
                setPassword: false,
                setMobile: false,
            },
            form: {},
        }
    },
}
</script>

<style scoped></style>