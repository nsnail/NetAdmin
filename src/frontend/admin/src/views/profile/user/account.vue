<template>
    <el-card header="账号信息" shadow="never">
        <el-form ref="form" :model="form" label-width="10rem">
            <el-form-item label="头像">
                <sc-upload v-model="form.avatar" :onSuccess="updateUser" title="上传头像"></sc-upload>
            </el-form-item>
            <el-form-item label="用户编号">
                <el-input v-model="form.id" readonly></el-input>
            </el-form-item>
            <el-form-item label="用户名称">
                <el-input v-model="form.userName" readonly></el-input>
            </el-form-item>
            <el-form-item label="密码">
                <div class="flex w100p gap05">
                    <el-input readonly value="******"></el-input>
                    <el-button @click="setPasswordClick">设置密码</el-button>
                </div>
            </el-form-item>
            <el-form-item label="注册时间">
                <el-input v-model="form.createdTime" readonly></el-input>
            </el-form-item>
            <el-form-item label="手机号码">
                <div class="flex w100p gap05">
                    <el-input v-model="form.mobile" readonly></el-input>
                    <el-button @click="setMobileClick">{{ `${form.mobile ? '换绑' : '绑定'}手机` }}</el-button>
                </div>
            </el-form-item>
            <el-form-item label="电子邮箱">
                <div class="flex w100p gap05">
                    <el-input v-model="form.email" readonly></el-input>
                    <el-button @click="setEmailClick">设置邮箱</el-button>
                </div>
            </el-form-item>
        </el-form>
    </el-card>

    <set-mobile-dialog v-if="dialog.setMobile" ref="setMobileDialog" @closed="dialog.setMobile = false" @success="setSuccess"></set-mobile-dialog>
    <set-password-dialog v-if="dialog.setPassword" ref="setPasswordDialog" @closed="dialog.setPassword = false"></set-password-dialog>
    <set-email-dialog v-if="dialog.setEmail" ref="setEmailDialog" @closed="dialog.setEmail = false" @success="setSuccess"></set-email-dialog>
</template>

<script>
import setMobileDialog from '@/views/profile/user/setMobile.vue'
import setPasswordDialog from '@/views/profile/user/setPassword.vue'
import setEmailDialog from '@/views/profile/user/setEmail.vue'

export default {
    components: { setMobileDialog, setPasswordDialog, setEmailDialog },
    methods: {
        updateUser(res) {
            try {
                this.$API.sys_user.setAvatar.post({ avatar: res.data })
                this.$message.success('操作成功')
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