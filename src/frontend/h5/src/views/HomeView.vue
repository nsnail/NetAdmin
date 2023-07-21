<script setup>
import {getCurrentInstance, ref} from "vue";

let page = 1
const articles = ref([])
const categories = ref([{id: 0, title: '所有商品'}])
const loading = ref(false)
const finished = ref(false)
const refreshing = ref(false)
const showFilter = ref(true)
const _this = getCurrentInstance().proxy

const filterMall = ref('');
const sorter = ref('1worthy');
const filterDate = ref('03hours');
const optionMall = [
    {text: '全部商城', value: ''},
    {
        text: '淘宝/天猫', value: '淘宝精选,天猫国际,天猫国际官方直营,天猫精选,天猫超市'
    },
    {text: '拼多多', value: '拼多多'},
    {
        text: '京东', value: '京东,京东国际,京喜特价'
    },
    {text: '唯品会', value: '唯品会'},
    {text: '苏宁易购', value: '苏宁易购'},


];
const optionSorter = [
    {text: '最值', order: 'descending', value: '1worthy'},
    {text: '最新', order: 'descending', value: '1createdTime'},
    {text: '最热', order: 'descending', value: '1comment'},
    {text: '低价', order: 'ascending', value: '1priceFen'},
    {text: '高价', order: 'descending', value: '2priceFen'},
];
const optionDate = [
    {text: '3小时', value: '03hours'},
    {text: '6小时', value: '06hours'},
    {text: '9小时', value: '09hours'},
    {text: '12小时', value: '12hours'},
    {text: '24小时', value: '24hours'},
    {text: '不限时间', value: ''},
];

const dynamicFilter = {
    logic: 'and',
    filters: [{
        field: "status",
        operator: "eq",
        value: 'published',
    }]
}

if (_this.$route.query.cId) {
    categories.value.unshift({id: _this.$route.query.cId, title: _this.$route.query.cName})
}
let categoryId = ref(categories.value[0].id)
loadCategories()

async function onRefresh() {
    articles.value = [];
    page = 1;
    finished.value = false;
    loading.value = true;
    await loadArticles();
    refreshing.value = false
}


async function loadCategories() {
    let res = await _this.$API['biz/artcategory'].query.post({
        dynamicFilter: {
            logic: "and",
            filters: [
                {
                    field: "parentId",
                    operator: "eq",
                    value: 0
                }
            ]
        }
    })
    categories.value = categories.value.concat(res.data)
}

async function loadArticles() {

    dynamicFilter.filters = dynamicFilter.filters.filter(x => x.field !== 'mall' && x.field !== 'createdTime')
    if (filterMall.value) {
        dynamicFilter.filters.push({field: 'mall', operator: 'any', value: filterMall.value.split(',')})
    }
    if (filterDate.value) {
        dynamicFilter.filters.push({
            field: 'createdTime',
            operator: 'dateRange',
            value: [new Date(new Date().getTime() - 60 * 60 * 1000 * parseInt(filterDate.value.substring(0, 2))).toISOString().replace('T', ' ').replace(/\.\d+Z/, ''), new Date().toISOString().replace('T', ' ').replace(/\.\d+Z/, '')]
        })
    }

    let res = await _this.$API['biz/article'].pagedQuery.post({
        pageSize: 10,
        page: page++,
        filter: {
            categoryId: categoryId.value
        },
        dynamicFilter: dynamicFilter,
        order: optionSorter.find(x => x.value === sorter.value).order,
        prop: sorter.value.substring(1)

    })
    if (res.data.rows) {
        articles.value = articles.value.concat(res.data.rows)
    }
    if (!res.data.rows || res.data.rows.length < 10) {
        finished.value = true;
    }
    loading.value = false;
}

window.addEventListener("scroll", () => {
    showFilter.value = window.scrollY < 200
});


</script>

<template>
    <div class="bg-gray-50">
        <van-search class="w-full" placeholder="请输入搜索关键词"/>
        <van-tabs v-model:active="categoryId" sticky swipeable @change="onRefresh">
            <template #nav-bottom>
                <van-dropdown-menu v-show="showFilter">
                    <van-dropdown-item v-model="filterMall" :options="optionMall" @change="onRefresh"/>
                    <van-dropdown-item v-model="filterDate" :options="optionDate" @change="onRefresh"/>
                    <van-dropdown-item v-model="sorter" :options="optionSorter" @change="onRefresh"/>
                </van-dropdown-menu>
            </template>
            <van-tab v-for="(category,i) in categories" :key="i" :name="category.id" :title="category.title">
                <van-pull-refresh v-model="refreshing" @refresh="onRefresh">
                    <van-list
                        v-model:loading="loading"
                        :finished="finished"
                        class="min-h-screen mb-12"
                        finished-text="没有更多了"
                        @load="loadArticles"
                    >
                        <div v-for="(article,i) in articles" :key="i"
                             class="grid grid-cols-12 bg-white gap-2 m-2 text-sm rounded-lg overflow-hidden">
                            <div class="col-span-4">
                                <van-image :src="`http://172.29.19.105:9000/ddf/${article.id}${article.picExtension}`" class="w-full" lazy-load>
                                </van-image>
                            </div>
                            <div class="col-span-8 py-1">
                                <p>{{ article.title }}</p>
                                <p class="text-red-600">{{ article.price }}</p>
                            </div>
                            <div class="col-span-2 text-center">
                                <van-icon name="good-job-o"/>
                                {{ article.worthy }}
                            </div>
                            <div class="col-span-2 text-center">
                                <van-icon name="good-job-o"/>
                                {{ article.unworthy }}
                            </div>
                            <div class="col-span-8 text-right text-green-600 pr-1">{{ article.timeAgo }}</div>
                        </div>
                    </van-list>
                </van-pull-refresh>
            </van-tab>
        </van-tabs>


    </div>
</template>