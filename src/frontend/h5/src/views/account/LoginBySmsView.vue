<script setup>
import {getCurrentInstance, onMounted, ref, watch} from 'vue'
import router from '@/router'
import Verify from "@/components/verifition/Verify.vue";

let _this
const form = ref({
    code: '',
    mobile: ''
})
const waitSecReSend = ref(0)
const loadingReSend = ref(true)
const loadingNextStep = ref(false)
const showKeyBoardMobile = ref(true)
const showKeyboardCode = ref(true)
let timerReSend
let stepActive = ref(0)
onMounted(async () => {
    _this = getCurrentInstance().proxy
    form.value.mobile = _this.$TOOL.data.get('mobile')
})


watch(waitSecReSend, (newValue) => {
    if (newValue === 0) {
        clearInterval(timerReSend)
        loadingReSend.value = false
    }
})

watch(form.value, (newValue) => {
    if (newValue.mobile && newValue.mobile.length === 11) {
        showKeyBoardMobile.value = false
        _this.$TOOL.data.set('mobile', newValue.mobile)
    }
    if (newValue.code && newValue.code.length === 4) {
        showKeyboardCode.value = false
    }
})

async function onSubmit() {
    if (stepActive.value === 0) {
        _this.$refs.verify.show()
    } else if (stepActive.value === 1) {
        loadingNextStep.value = true
        try {
            await _this.$API['sys/user'].loginBySms.post({
                destMobile: form.value.mobile, code: form.value.code
            })
            stepActive.value++
        } catch {
            /* empty */
        }
        loadingNextStep.value = false
    }
    if (stepActive.value === 2) {
        await router.push('/member')
    }
}

async function captchaSuccess(data) {
    const res = await _this.$API['sys/sms'].sendSmsCode.post({
        destMobile: form.value.mobile,
        verifyCaptchaReq: data
    })
    if (res.data.code) {
        form.value.code = res.data.code
    }
    stepActive.value = 1
    waitSecReSend.value = 60
    loadingReSend.value = true
    timerReSend = setInterval(() => {
        --waitSecReSend.value
    }, 1000)
}
</script>

<template>
    <div class="grid place-items-center h-screen">
        <van-form class="w-full" @submit="onSubmit">
            <van-cell-group inset>
                <van-steps :active="stepActive">
                    <van-step>填写手机号</van-step>
                    <van-step>接收验证短信</van-step>
                    <van-step>登录成功</van-step>
                </van-steps>

                <van-field
                    v-if="stepActive === 0"
                    v-model="form.mobile"
                    :rules="[
          { required: true, message: '请填写正确的手机号码' },
          {
            pattern: new RegExp(getCurrentInstance().proxy['$CONFIG'].STRINGS['RGX_MOBILE']),
            message: '请填写正确的手机号码'
          }        ]"
                    label="手机号"
                    label-class="field_label_mobile"
                    label-width="3rem"
                >
                    <template #input>
                        <van-password-input
                            :focused="showKeyBoardMobile"
                            :mask="false"
                            :value="form.mobile"
                            class="w-full"
                            length="11"
                            @focus="showKeyBoardMobile = true"
                        />
                        <van-number-keyboard
                            v-model="form.mobile"
                            :show="showKeyBoardMobile"
                            maxlength="11"
                            @blur="showKeyBoardMobile = false"
                        />
                    </template>
                </van-field>
                <van-field
                    v-if="stepActive === 1"
                    v-model="form.code"
                    :rules="[
          { required: true, message: '请填写正确的验证码' },
          {
            pattern: new RegExp(getCurrentInstance().proxy['$CONFIG'].STRINGS['RGX_SMSCODE']),
            message: '请填写正确的验证码'
          }
        ]"
                    label="验证码"
                    label-class="field_label_mobile"
                    label-width="3rem"
                >
                    <template #input>
                        <van-password-input
                            :focused="showKeyboardCode"
                            :mask="false"
                            :value="form.code"
                            class="w-full"
                            length="4"
                            @focus="showKeyboardCode = true"
                        />
                        <van-number-keyboard
                            v-model="form.code"
                            :show="showKeyboardCode"
                            maxlength="4"
                            @blur="showKeyboardCode = false"
                        />
                    </template>
                </van-field>
            </van-cell-group>
            <div class="flex flex-col gap-4 mx-6 mt-4">
                <van-button
                    v-if="stepActive === 1"
                    :disabled="loadingReSend"
                    :loading="loadingReSend"
                    :loading-text="`未收到？点击重发（${waitSecReSend}秒）`"
                    block
                    plain
                    round
                    type="primary"
                    @click="_this.$refs.verify.show()"
                >
                    未收到？点击重发
                </van-button>

                <van-button
                    :disabled="loadingNextStep"
                    :loadingNextStep="loadingNextStep"
                    block
                    native-type="submit"
                    round
                    type="primary"
                >{{ stepActive === 3 ? '进入会员中心' : '下一步' }}
                </van-button>
            </div>


            <Verify
                ref="verify"
                :imgSize="{ width: '310px', height: '155px' }"
                captchaType="blockPuzzle"
                mode="pop"
                @success="captchaSuccess"
            ></Verify>
        </van-form>
    </div>
</template>
<style>
.field_label_mobile {
    line-height: var(--van-password-input-height);
}
</style>