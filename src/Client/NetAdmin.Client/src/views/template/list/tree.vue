<template>
    <el-container>
        <el-aside width="200px">
            <el-container>
                <el-header>
                    <el-input
                        v-model="groupFilterText"
                        clearable
                        placeholder="输入关键字进行过滤"
                    ></el-input>
                </el-header>
                <el-main class="nopadding">
                    <el-tree
                        ref="group"
                        :current-node-key="''"
                        :data="groupData"
                        :expand-on-click-node="false"
                        :filter-node-method="groupFilterNode"
                        :highlight-current="true"
                        class="menu"
                        node-key="id"
                        @node-click="groupClick"
                    ></el-tree>
                </el-main>
            </el-container>
        </el-aside>
        <el-container>
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
                    <el-table-column
                        type="selection"
                        width="50"
                    ></el-table-column>
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
    </el-container>
</template>

<script>
export default {
    name: "listTree",
    data() {
        return {
            groupFilterText: "",
            groupData: [
                {
                    id: "",
                    label: "所有",
                },
                {
                    id: "1",
                    label: "华东总部",
                    children: [
                        {
                            id: "11",
                            label: "售前客服部",
                        },
                        {
                            id: "12",
                            label: "技术研发部",
                        },
                        {
                            id: "13",
                            label: "行政人事部",
                        },
                    ],
                },
                {
                    id: "2",
                    label: "华难总部",
                    children: [
                        {
                            id: "21",
                            label: "售前客服部",
                        },
                        {
                            id: "22",
                            label: "技术研发部",
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
    watch: {
        groupFilterText(val) {
            this.$refs.group.filter(val);
        },
    },
    methods: {
        //树过滤
        groupFilterNode(value, data) {
            if (!value) return true;
            return data.label.indexOf(value) !== -1;
        },
        //树点击事件
        groupClick(data) {
            const params = {
                groupId: data.id,
            };
            this.$refs.table.reload(params);
        },
        //搜索
        upSearch() {
            this.$refs.table.upData(this.search);
        },
    },
};
</script>

<style></style>