<script setup>
import {getCurrentInstance, onMounted, ref} from "vue";

let _this
const form = ref({
    account: '',
    password: ''
})
onMounted(async () => {
    _this = getCurrentInstance().proxy
    form.value.account = _this.$TOOL.data.get('mobile')
})

async function onSubmit() {
    try {
        await _this.$API['sys/user'].loginByPwd.post(form.value)
    } catch {
        /* empty */
    }
}
</script>

<template>
    <div class="grid place-items-center h-screen">
        <van-form class="w-full" @submit="onSubmit">
            <van-cell-group inset>
                <van-field
                    v-model="form.account"
                    :rules="[{ required: true, message: '请填写账号' }]"
                    label="账号"
                    name="账号"
                    placeholder="用户名 / 手机号"
                />
                <van-field
                    v-model="form.password"
                    :rules="[{
                        required: true,
                        message: '8位以上数字字母组合',
                        pattern: new RegExp(getCurrentInstance().proxy['$CONFIG'].STRINGS['RGX_PASSWORD']),
                    }]"
                    label="密码"
                    name="密码"
                    placeholder="登录密码"
                    type="password"
                />
            </van-cell-group>

            <van-cell-group inset>
                <van-cell class="my-4">
                    <template #value>
                        <van-row>
                            <van-col :span="12" class="text-right"><a href="/account/reset-pwd">找回密码</a></van-col>
                            <van-col :span="12" class="text-right"><a href="/account/login-by-sms">短信登录</a></van-col>
                        </van-row>
                    </template>
                </van-cell>
            </van-cell-group>
            <div class="flex flex-col gap-4 mx-6">
                <van-button block native-type="submit" round type="primary">
                    登录
                </van-button>
                <van-button block round @click="$router.push('/account/register')">
                    还没有账号？
                </van-button>
            </div>
        </van-form>
    </div>
</template>
<style>
</style>