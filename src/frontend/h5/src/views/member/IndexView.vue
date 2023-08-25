<script setup>
import {getCurrentInstance, onMounted, ref} from "vue";

const memberInfo = ref({sysUser: {userName: ''}})
let _this
onMounted(async () => {
    _this = getCurrentInstance().proxy
    const res = await _this.$API['biz/member'].memberInfo.post()
    memberInfo.value = res.data
    console.log(memberInfo.value)
})
</script>

<template>
    <div class="m-4 grid grid-rows-2 grid-cols-4 grid-flow-col gap-4">
        <div class="row-span-2">
            <van-image height="4rem" round src="https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg" width="4rem"></van-image>
        </div>
        <div class="col-span-3 text-sm">{{ memberInfo.sysUser.userName }}</div>
        <div class='col-span-3 text-sm'>{{ memberInfo.sysUser.mobile }}</div>
    </div>
    <van-cell-group>
        <van-cell is-link title="设置" to="/"></van-cell>
        <van-cell is-link title="设置" to="/"></van-cell>
        <van-cell is-link title="设置" to="/"></van-cell>
        <van-cell is-link title="设置" to="/"></van-cell>
        <van-cell is-link title="设置" to="/"></van-cell>
        <van-cell is-link title="设置" to="/"></van-cell>
        <van-cell is-link title="设置" to="/"></van-cell>
        <van-cell is-link title="设置" to="/"></van-cell>
    </van-cell-group>

</template>