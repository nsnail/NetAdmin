<template>
    <el-container>
        <el-header class="header-tabs">
            <el-tabs v-model="groupId" type="card" @tab-change="tabChange">
                <el-tab-pane label="所有" name="0"></el-tab-pane>
                <el-tab-pane label="未完成" name="1"></el-tab-pane>
                <el-tab-pane label="已退回" name="2"></el-tab-pane>
                <el-tab-pane label="已关闭" name="3"></el-tab-pane>
                <el-tab-pane label="已完成" name="4"></el-tab-pane>
            </el-tabs>
        </el-header>
        <el-header style="height: auto">
            <sc-select-filter
                :data="filterData"
                :label-width="80"
                @on-change="filterChange"
            ></sc-select-filter>
        </el-header>
        <el-header>
            <div class="left-panel">
                <el-button icon="el-icon-plus" type="primary"></el-button>
                <el-button
                    icon="el-icon-delete"
                    plain
                    type="danger"
                ></el-button>
            </div>
            <div class="right-panel">
                <div class="right-panel-search">
                    <el-input
                        v-model="search.keyword"
                        clearable
                        placeholder="关键词"
                    ></el-input>
                    <el-button
                        icon="el-icon-search"
                        type="primary"
                        @click="upSearch"
                    ></el-button>
                </div>
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable ref="table" :apiObj="list.apiObj" row-key="id" stripe>
                <el-table-column type="selection" width="50"></el-table-column>
                <el-table-column
                    label="姓名"
                    prop="name"
                    width="150"
                ></el-table-column>
                <el-table-column
                    label="性别"
                    prop="sex"
                    width="150"
                ></el-table-column>
                <el-table-column
                    label="邮箱"
                    prop="email"
                    width="250"
                ></el-table-column>
                <el-table-column
                    label="评分"
                    prop="num"
                    width="150"
                ></el-table-column>
                <el-table-column
                    label="注册时间"
                    prop="datetime"
                    sortable
                    width="150"
                ></el-table-column>
                <el-table-column
                    align="right"
                    fixed="right"
                    label="操作"
                    width="160"
                >
                    <template #default>
                        <el-button-group>
                            <el-button size="small" text type="primary"
                                >查看
                            </el-button>
                            <el-button size="small" text type="primary"
                                >编辑
                            </el-button>
                            <el-button size="small" text type="primary"
                                >删除
                            </el-button>
                        </el-button-group>
                    </template>
                </el-table-column>
            </scTable>
        </el-main>
    </el-container>
</template>

<script>
import scSelectFilter from "@/components/scSelectFilter";

export default {
    name: "listTab",
    components: {
        scSelectFilter,
    },
    data() {
        return {
            groupId: "0",
            filterData: [
                {
                    title: "所属行业",
                    key: "type",
                    multiple: true,
                    options: [
                        {
                            label: "全部",
                            value: "",
                        },
                        {
                            label: "汽车",
                            value: "1",
                        },
                        {
                            label: "大健康",
                            value: "2",
                        },
                        {
                            label: "节能环保",
                            value: "3",
                        },
                        {
                            label: "智能信息",
                            value: "4",
                        },
                        {
                            label: "文化旅游",
                            value: "5",
                        },
                        {
                            label: "新材料",
                            value: "6",
                        },
                        {
                            label: "高端装备",
                            value: "7",
                        },
                        {
                            label: "其他行业",
                            value: "99",
                        },
                    ],
                },
                {
                    title: "所属区域",
                    key: "area",
                    options: [
                        {
                            label: "全部",
                            value: "",
                        },
                        {
                            label: "华东",
                            value: "HD",
                        },
                        {
                            label: "华北",
                            value: "HB",
                        },
                        {
                            label: "华南",
                            value: "HN",
                        },
                        {
                            label: "华中",
                            value: "HZ",
                        },
                        {
                            label: "华西南",
                            value: "HXN",
                        },
                        {
                            label: "东北",
                            value: "DB",
                        },
                    ],
                },
            ],
            list: {
                apiObj: this.$API.demo.list,
            },
            search: {
                keyword: "",
            },
        };
    },
    methods: {
        //搜索
        upSearch() {
            this.$refs.table.upData(this.search);
        },
        //标签切换
        tabChange(name) {
            const params = {
                groupId: name,
            };
            this.$refs.table.reload(params);
        },
        filterChange(data) {
            this.$refs.table.upData(data);
        },
    },
};
</script>

<style></style>