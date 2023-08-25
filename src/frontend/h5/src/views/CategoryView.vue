<script setup>
import {getCurrentInstance, ref} from "vue";
import router from "@/router";

const categories = ref([])
const activeId = ref(0)
const activeIndex = ref(0)
const _this = getCurrentInstance().proxy
loadCategories()


async function loadCategories() {
    let res = await _this.$API['biz/productcategory'].query.post()
    categories.value = JSON.parse(JSON.stringify(res.data).replaceAll("categoryName", 'text'))
}


function clickItem(item) {
    router.push({name: 'home', query: {cId: item.id, cName: item.text}})
}
</script>

<template>
    <van-tree-select
        v-model:active-id="activeId"
        v-model:main-active-index="activeIndex"
        :items="categories"
        @click-item="clickItem"
    />
</template>