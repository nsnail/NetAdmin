<template>
    <el-dialog v-model="visible" :title="$t('修改密码')" @closed="$emit('closed')" destroy-on-close>
        <el-form :model="form" :rules="rules" label-width="15rem" ref="form">
            <el-form-item :label="$t('旧密码')" prop="oldPassword">
                <el-input v-model="form.oldPassword" :placeholder="$t('请输入当前密码')" clearable maxlength="16" show-password type="password" />
                <div class="el-form-item-msg">{{ $t('必须提供当前登录用户密码才能进行更改') }}</div>
            </el-form-item>
            <el-form-item :label="$t('新密码')" prop="newPassword">
                <el-input v-model="form.newPassword" :placeholder="$t('请输入新密码')" clearable maxlength="16" show-password type="password" />
                <sc-password-strength v-model="form.newPassword" />
                <div class="el-form-item-msg">{{ $t('请输入包含英文、数字的8位以上密码') }}</div>
            </el-form-item>
            <el-form-item :label="$t('确认新密码')" prop="confirmNewPassword">
                <el-input
                    v-model="form.confirmNewPassword"
                    :placeholder="$t('请再次输入新密码')"
                    clearable
                    maxlength="16"
                    show-password
                    type="password" />
            </el-form-item>
        </el-form>

        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </el-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const scPasswordStrength = defineAsyncComponent(() => import('@/components/sc-password-strength'))
import naFormPassword from '@/config/na-form-password'

export default {
    components: {
        scPasswordStrength,
    },
    created() {},

    data() {
        return {
            form: {},
            loading: false,
            //验证规则
            rules: {
                oldPassword: naFormPassword.passwordText(this),
                newPassword: naFormPassword.passwordText(this),
                confirmNewPassword: naFormPassword.passwordText2(this, () => this.form.newPassword),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        open() {
            this.visible = true
            return this
        },
        //表单提交方法
        async submit() {
            const valid = await this.$refs.form.validate().catch(() => {})
            if (!valid) {
                return false
            }

            this.loading = true
            try {
                const res = await this.$API.sys_user.setPassword.post(this.form)
                this.$emit('success', res.data)
                this.visible = false
                this.$message.success(this.$t('操作成功'))
            } catch {}
            this.loading = false
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>
<style scoped />