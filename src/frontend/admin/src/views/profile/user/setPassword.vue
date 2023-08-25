<template>
    <el-dialog v-model="visible" :width="800" destroy-on-close title="修改密码" @closed="$emit('closed')">
        <el-form ref="form" :model="form" :rules="rules" label-width="120">
            <el-form-item label="旧密码" prop="oldPassword">
                <el-input v-model="form.oldPassword" clearable maxlength="16" placeholder="请输入当前密码" show-password type="password"></el-input>
                <div class="el-form-item-msg">必须提供当前登录用户密码才能进行更改</div>
            </el-form-item>
            <el-form-item label="新密码" prop="newPassword">
                <el-input v-model="form.newPassword" clearable maxlength="16" placeholder="请输入新密码" show-password type="password"></el-input>
                <sc-password-strength v-model="form.newPassword"></sc-password-strength>
                <div class="el-form-item-msg">请输入包含英文、数字的8位以上密码</div>
            </el-form-item>
            <el-form-item label="确认新密码" prop="confirmNewPassword">
                <el-input
                    v-model="form.confirmNewPassword"
                    clearable
                    maxlength="16"
                    placeholder="请再次输入新密码"
                    show-password
                    type="password"></el-input>
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button :loading="loading" type="primary" @click="submit">保 存</el-button>
        </template>
    </el-dialog>
</template>

<script>
import scPasswordStrength from '@/components/scPasswordStrength/index.vue'
import naFormPassword from '@/config/naFormPassword'

export default {
    created() {},
    components: {
        scPasswordStrength,
    },
    methods: {
        open() {
            this.visible = true
            return this
        },
        async submit() {
            if (!(await this.$refs.form.validate().catch(() => {}))) {
                return false
            }

            this.loading = true
            try {
                const res = await this.$API.sys_user.setPassword.post(this.form)
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success('操作成功')
            } catch {
                //
            }
            this.loading = false
        },
    },
    data() {
        return {
            visible: false,
            loading: false,
            form: {},
            rules: {
                oldPassword: naFormPassword.passwordText(this),
                newPassword: naFormPassword.passwordText(this),
                confirmNewPassword: naFormPassword.passwordText2(() => this.form.newPassword),
            },
        }
    },
}
</script>