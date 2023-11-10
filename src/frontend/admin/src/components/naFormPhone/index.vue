<template>
    <el-form-item :label="phoneLabel" :prop="Array.isArray(phoneField) ? phoneField[0] : phoneField">
        <el-input
            v-model="form[Array.isArray(phoneField) ? phoneField[1] : phoneField]"
            :placeholder="phonePlaceHolder"
            clearable
            maxlength="11"
            oninput="value=value.replace(/\D/g,'')"
            prefix-icon="el-icon-iphone">
            <template #prepend>+86</template>
        </el-input>
    </el-form-item>
    <el-form-item :label="codeLabel" :prop="Array.isArray(codeField) ? codeField[0] : codeField">
        <div class="msg-yzm">
            <el-input
                v-model="form[Array.isArray(codeField) ? codeField[1] : codeField]"
                :placeholder="$t('短信验证码')"
                clearable
                maxlength="4"
                oninput="value=value.replace(/\D/g,'')"
                prefix-icon="el-icon-message"></el-input>
            <el-button :disabled="sendDisabled" @click="getYzm"
                >获取验证码<span v-if="sendDisabled"> ({{ waitSecs }})</span></el-button
            >
        </div>
    </el-form-item>
    <na-verify ref="verify" :imgSize="{ width: '310px', height: '155px' }" captchaType="blockPuzzle" mode="pop" @success="captchaSuccess"></na-verify>
</template>

<script>
import naVerify from '@/components/naVerifition/index.vue'

export default {
    emits: [],
    props: {
        modelValue: { type: Object },
        vue: { type: Object },
        formName: { type: String },
        phonePlaceHolder: { type: String, default: '手机号码' },
        phoneField: { type: Object },
        codeField: { type: Object },
        phoneLabel: { type: String },
        codeLabel: { type: String },
    },
    data() {
        return {
            sendDisabled: false,
            waitSecs: 0,
            loading: false,
            form: {},
        }
    },
    watch: {
        form: {
            deep: true,
            handler(n) {
                this.$emit('update:modelValue', n)
            },
        },

        modelValue: {
            immediate: true,
            deep: true,
            handler(n) {
                this.form = n ?? {}
            },
        },
    },

    mounted() {},
    created() {},
    components: { naVerify },
    computed: {},
    methods: {
        async captchaSuccess(obj) {
            this.sendDisabled = true
            try {
                await this.$API.sys_verifycode.sendVerifyCode.post({
                    destDevice: this.form[Array.isArray(this.phoneField) ? this.phoneField[1] : this.phoneField],
                    type: 'login',
                    deviceType: 'mobile',
                    verifyCaptchaReq: obj,
                })
                this.$message.success('发送成功')
                this.waitSecs = 60
                const t = setInterval(() => {
                    this.waitSecs -= 1
                    if (this.waitSecs < 1) {
                        clearInterval(t)
                        this.sendDisabled = false
                        this.waitSecs = 0
                    }
                }, 1000)
            } catch {
                this.loading = false
            }
        },
        async getYzm() {
            if (
                !(await this.vue.$refs[this.formName]
                    .validateField(Array.isArray(this.phoneField) ? this.phoneField[0] : this.phoneField)
                    .catch(() => {}))
            ) {
                return false
            }
            this.$refs.verify.show()
        },
    },
}
</script>

<style scoped>
.msg-yzm {
    display: flex;
    width: 100%;
    gap: 0.5rem;
}
</style>