<template>
    <el-dialog v-model="visible" :title="`${titleMap[mode]}`" @closed="$emit('closed')" destroy-on-close>
        <el-form :model="form" :rules="rules" label-position="top" ref="form">
            <el-row class="items-center justify-content-center">
                <el-col v-if="mode === 'edit'" :lg="10">
                    <na-form-phone
                        v-model="form.originverifySmsCodeReq"
                        :code-field="['originverifySmsCodeReq.code', 'code']"
                        :code-label="$t('原手机验证码')"
                        :phone-field="['originverifySmsCodeReq.destDevice', 'destDevice']"
                        :phone-label="$t('原手机号码')"
                        :phone-place-holder="$GLOBAL.user.mobile"
                        :vue="this"
                        form-name="form" />
                </el-col>
                <el-col v-if="mode === 'edit'" :lg="4" class="text-center arrow">
                    <el-icon>
                        <el-icon-right />
                    </el-icon>
                </el-col>
                <el-col :lg="10">
                    <na-form-phone
                        v-model="form.newverifySmsCodeReq"
                        :code-field="['newverifySmsCodeReq.code', 'code']"
                        :code-label="$t('新手机验证码')"
                        :phone-field="['newverifySmsCodeReq.destDevice', 'destDevice']"
                        :phone-label="$t('新手机号码')"
                        :phone-place-holder="$t('新手机号码')"
                        :vue="this"
                        form-name="form" />
                </el-col>
            </el-row>
        </el-form>

        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </el-dialog>
</template>

<script>
import naFormPhone from '@/components/naFormPhone'
import phoneConfig from '@/config/naFormPhone'

export default {
    components: {
        naFormPhone,
    },
    created() {},

    data() {
        return {
            //表单数据
            form: {
                newverifySmsCodeReq: {},
                originverifySmsCodeReq: {},
            },
            loading: false,
            mode: 'add',
            //验证规则
            rules: {
                originverifySmsCodeReq: {
                    destDevice: phoneConfig.mobile(this),
                    code: phoneConfig.code(this),
                },
                newverifySmsCodeReq: {
                    destDevice: [phoneConfig.mobile(this), phoneConfig.mobileNoUsed(this, () => this.$GLOBAL.user.id)],
                    code: phoneConfig.code(this),
                },
            },
            titleMap: {
                add: this.$t('绑定手机'),
                edit: this.$t('更换手机'),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        open(data) {
            this.mode = data.mode
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
                const res = await this.$API.sys_user.setMobile.post(this.form)
                this.$emit('success', res.data, this.mode)
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

<style scoped>
.arrow {
    font-size: 200%;
}

.button >>> .el-form-item__content {
    justify-content: center;
}
</style>