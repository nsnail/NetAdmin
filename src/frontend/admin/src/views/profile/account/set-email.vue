<template>
    <el-dialog v-model="visible" :title="$t('设置邮箱')" :width="800" @closed="$emit('closed')" destroy-on-close>
        <el-form :model="form" :rules="rules" label-position="top" ref="form">
            <el-row class="is-justify-space-evenly">
                <el-col v-if="$GLOBAL.user.mobile" :lg="10">
                    <na-form-phone
                        v-model="form.verifySmsCodeReq"
                        :code-field="['verifySmsCodeReq.code', 'code']"
                        :code-label="$t('手机验证码')"
                        :phone-field="['verifySmsCodeReq.destDevice', 'destDevice']"
                        :phone-label="$t('手机号码')"
                        :phone-place-holder="$GLOBAL.user.mobile"
                        :vue="this"
                        form-name="form" />
                </el-col>
                <el-col :lg="10">
                    <na-form-email
                        v-model="form"
                        :code-label="$t('邮箱验证码')"
                        :email-label="$t('邮箱地址')"
                        :vue="this"
                        code-field="code"
                        email-field="destDevice"
                        form-name="form" />
                </el-col>
            </el-row>
        </el-form>

        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button :loading="loading" @click="submit" type="primary">保 存</el-button>
        </template>
    </el-dialog>
</template>

<script>
import naFormPhone from '@/components/naFormPhone/index.vue'
import phoneConfig from '@/config/naFormPhone'
import emailConfig from '@/config/naFormEmail'

export default {
    created() {},
    components: {
        naFormPhone,
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
                const res = await this.$API.sys_user.setEmail.post(this.form)
                this.$emit('success', res.data)
                this.visible = false
                this.$message.success('操作成功')
            } catch {}
            this.loading = false
        },
    },
    data() {
        return {
            visible: false,
            loading: false,
            form: {
                verifySmsCodeReq: {},
            },
            rules: {
                verifySmsCodeReq: {
                    destDevice: phoneConfig.mobile(this),
                    code: phoneConfig.code(this),
                },

                destDevice: [emailConfig.email(this)],
                code: emailConfig.code(),
            },
        }
    },
}
</script>

<style scoped>
.arrow {
    font-size: 200%;
}

.button >>> .el-form-item__content {
    justify-content: center;
}
</style>