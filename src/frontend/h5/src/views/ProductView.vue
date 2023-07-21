<script setup>
import {getCurrentInstance, ref} from "vue";
import router from "@/router";

const product = ref({})
const _this = getCurrentInstance().proxy
loadProduct()


async function loadProduct() {
    let res = await _this.$API['biz/product'].get.post({
        id: router.currentRoute.value.query.id
    })
    product.value = res.data
}


</script>

<template>
    <div class="px-2">
        <div class="w-full relative h-0" style="padding-bottom:100%">
            <van-image :src="product.imageUrl" class="top-0 left-0 w-full h-full" style="position:absolute"></van-image>
        </div>
        <p>{{ product.name }}</p>
        <p class="text-right text-red-600">&yen;{{ product.price / 100 }}</p>
        <p class="text-sm">{{ product.description }}</p>
    </div>
    <van-action-bar>
        <van-action-bar-icon icon="chat-o" text="客服" @click="onClickIcon"/>
        <van-action-bar-icon icon="cart-o" text="购物车" @click="onClickIcon"/>
        <van-action-bar-icon icon="shop-o" text="店铺" @click="onClickIcon"/>
        <van-action-bar-button text="立即购买" type="danger" @click="onClickButton"/>
    </van-action-bar>
</template>