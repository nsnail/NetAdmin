<template>
    <el-container>
        <el-aside
            v-loading="showTreeloading"
            class="el-aside-filter"
            width="220px"
        >
            <el-collapse v-model="activeNames">
                <el-collapse-item name="1" title="状态筛选">
                    <el-container>
                        <el-header>
                            <el-input
                                v-model="searchStatusText"
                                clearable
                                placeholder="输入关键字进行过滤"
                            ></el-input>
                        </el-header>
                        <el-main class="nopadding">
                            <el-tree
                                ref="statusTree"
                                :data="statusFilters"
                                :default-expanded-keys="['所有状态']"
                                :expand-on-click-node="false"
                                :filter-node-method="statusFilterNode"
                                :highlight-current="true"
                                class="menu"
                                current-node-key="所有状态"
                                default-expand-all
                                node-key="label"
                                @node-click="statusTreeClick"
                            >
                            </el-tree>
                        </el-main>
                    </el-container>
                </el-collapse-item>
                <el-collapse-item name="2" title="服务筛选">
                    <el-container>
                        <el-header>
                            <el-input
                                v-model="searchApiText"
                                clearable
                                placeholder="输入关键字进行过滤"
                            ></el-input>
                        </el-header>
                        <el-main class="nopadding">
                            <el-header>
                                <el-button @click="quickLoginLog"
                                    >登录日志
                                </el-button>
                            </el-header>
                            <el-main>
                                <el-tree
                                    ref="apiTree"
                                    :data="apiFilters"
                                    :default-expanded-keys="['所有服务']"
                                    :expand-on-click-node="true"
                                    :filter-node-method="apiFilterNode"
                                    :highlight-current="true"
                                    :props="apiTreeProps"
                                    class="menu"
                                    current-node-key="所有服务"
                                    node-key="id"
                                    @node-click="apiTreeClick"
                                >
                                </el-tree>
                            </el-main>
                        </el-main>
                    </el-container>
                </el-collapse-item>
            </el-collapse>
        </el-aside>
        <el-container>
            <el-main class="nopadding">
                <el-container>
                    <el-header>
                        <div class="left-panel">
                            <el-date-picker
                                v-model="date"
                                end-placeholder="结束日期"
                                range-separator="至"
                                start-placeholder="开始日期"
                                type="datetimerange"
                                @change="dateChange"
                            ></el-date-picker>
                        </div>
                        <div class="right-panel"></div>
                    </el-header>
                    <el-header style="height: 150px">
                        <scEcharts
                            :option="logsChartOption"
                            height="100%"
                        ></scEcharts>
                    </el-header>
                    <el-main class="nopadding">
                        <scTable
                            ref="table"
                            :apiObj="apiObj"
                            highlightCurrentRow
                            remoteFilter
                            remoteSort
                            stripe
                            @row-click="rowClick"
                        >
                            <el-table-column
                                label="状态码"
                                prop="httpStatusCode"
                                sortable="custom"
                                width="100"
                            ></el-table-column>
                            <el-table-column
                                label="日志时间"
                                prop="createdTime"
                                sortable="custom"
                                width="170"
                            ></el-table-column>
                            <el-table-column label="请求服务">
                                <el-table-column
                                    label="路径"
                                    prop="apiId"
                                    sortable="custom"
                                    width="300"
                                ></el-table-column>
                                <el-table-column
                                    label="描述"
                                    prop="apiSummary"
                                    width="150"
                                ></el-table-column>
                                <el-table-column
                                    label="方法"
                                    prop="method"
                                    sortable="custom"
                                    width="100"
                                ></el-table-column>
                                <el-table-column
                                    label="耗时(毫秒)"
                                    prop="duration"
                                    sortable="custom"
                                    width="120"
                                >
                                    <template #default="scope">
                                        {{ scope.row.duration / 1000 }}
                                    </template>
                                </el-table-column>
                            </el-table-column>
                            <el-table-column
                                label="用户"
                                prop="createdUserName"
                                sortable="custom"
                                width="150"
                            >
                                <template #default="scope">
                                    {{
                                        scope.row.apiId == this.apiIdLogin
                                            ? scope.row.extraData
                                            : scope.row.createdUserName
                                    }}
                                </template>
                            </el-table-column>
                            <el-table-column
                                label="客户端IP"
                                prop="clientIp"
                                sortable="custom"
                                width="150"
                            ></el-table-column>
                            <el-table-column
                                label="操作系统"
                                prop="os"
                                sortable="custom"
                                width="150"
                            ></el-table-column>
                        </scTable>
                    </el-main>
                </el-container>
            </el-main>
        </el-container>
    </el-container>

    <el-drawer
        v-model="infoDrawer"
        destroy-on-close
        size="80%"
        title="日志详情"
    >
        <info ref="info"></info>
    </el-drawer>
</template>

<script>
import info from "./info.vue";
import scEcharts from "@/components/scEcharts/index.vue";
import tool from "@/utils/tool";

export default {
    components: {
        info,
        scEcharts,
    },
    watch: {
        searchStatusText(val) {
            this.$refs.statusTree.filter(val);
        },
        searchApiText(val) {
            this.$refs.apiTree.filter(val);
        },
    },
    data() {
        return {
            apiIdLogin: "api/sys/user/pwd.login",
            searchStatusText: "",
            searchApiText: "",
            showTreeloading: false,
            activeNames: ["1", "2"],
            queryParams: {
                dynamicFilter: null,
            },
            infoDrawer: false,
            logsChartOption: {
                color: ["#409eff", "#e6a23c", "#f56c6c"],
                grid: {
                    top: "0px",
                    left: "10px",
                    right: "10px",
                    bottom: "0px",
                },
                tooltip: {
                    trigger: "axis",
                },
                xAxis: {
                    type: "category",
                    boundaryGap: false,
                    data: [
                        "2021-07-01",
                        "2021-07-02",
                        "2021-07-03",
                        "2021-07-04",
                        "2021-07-05",
                        "2021-07-06",
                        "2021-07-07",
                        "2021-07-08",
                        "2021-07-09",
                        "2021-07-10",
                        "2021-07-11",
                        "2021-07-12",
                        "2021-07-13",
                        "2021-07-14",
                        "2021-07-15",
                    ],
                },
                yAxis: {
                    show: false,
                    type: "value",
                },
                series: [
                    {
                        data: [
                            120, 200, 150, 80, 70, 110, 130, 120, 200, 150, 80,
                            70, 110, 130, 70, 110,
                        ],
                        type: "bar",
                        stack: "log",
                        barWidth: "15px",
                    },
                    {
                        data: [
                            15, 26, 7, 12, 13, 9, 21, 15, 26, 7, 12, 13, 9, 21,
                            12, 3,
                        ],
                        type: "bar",
                        stack: "log",
                        barWidth: "15px",
                    },
                    {
                        data: [
                            0, 0, 0, 120, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                        ],
                        type: "bar",
                        stack: "log",
                        barWidth: "15px",
                    },
                ],
            },
            statusFilters: [
                { label: "所有状态" },
                {
                    label: "成功请求",
                    field: "httpStatusCode",
                    operator: "eq",
                    value: 200,
                },
                {
                    label: "失败请求",
                    field: "httpStatusCode",
                    operator: "NotAny",
                    value: "200",
                    children: [
                        {
                            label: "400",
                            field: "httpStatusCode",
                            operator: "eq",
                            value: 400,
                        },
                        {
                            label: "401",
                            field: "httpStatusCode",
                            operator: "eq",
                            value: 401,
                        },
                        {
                            label: "403",
                            field: "httpStatusCode",
                            operator: "eq",
                            value: 403,
                        },
                        {
                            label: "404",
                            field: "httpStatusCode",
                            operator: "eq",
                            value: 404,
                        },
                        {
                            label: "500",
                            field: "httpStatusCode",
                            operator: "eq",
                            value: 500,
                        },
                        {
                            label: "其它",
                            field: "httpStatusCode",
                            operator: "NotAny",
                            value: "200,400,401,403,404,500",
                        },
                    ],
                },
            ],
            apiFilters: [],
            apiTreeProps: {
                label: "summary",
            },
            date: [],
            apiObj: this.$API.sys_log.pagedQuery,
            search: {
                keyword: "",
            },
            statusFilter: null,
            apiFilter: null,
        };
    },
    mounted() {
        this.getTree();
    },
    methods: {
        async quickLoginLog() {
            this.$refs.apiTree.setCurrentKey(this.apiIdLogin);
            await this.apiTreeClick({ id: this.apiIdLogin });
        },
        //服务树过滤
        apiFilterNode(value, data) {
            if (!value) return true;
            return data.summary.indexOf(value) !== -1;
        },
        //状态树过滤
        statusFilterNode(value, data) {
            if (!value) return true;
            return data.label.indexOf(value) !== -1;
        },
        //加载树数据
        async getTree() {
            this.showTreeloading = true;

            //加载服务数据
            const res = await this.$API.sys_api.query.post();
            const allNode = { summary: "所有服务", label: "所有服务" };
            res.data.unshift(allNode);
            this.apiFilters = res.data;
            this.showTreeloading = false;
        },
        upSearch() {},
        async dateChange() {
            await this.reload();
        },
        async reload() {
            this.queryParams.dynamicFilter = { logic: "and", filters: [] };
            if (this.statusFilter) {
                this.queryParams.dynamicFilter.filters.push(this.statusFilter);
            }
            if (this.apiFilter) {
                this.queryParams.dynamicFilter.filters.push(this.apiFilter);
            }

            if (this.date && this.date.length === 2) {
                this.queryParams.dynamicFilter.filters.push({
                    field: "createdTime",
                    operator: "dateRange",
                    value: this.date.map((x) => tool.dateFormat(x)),
                });
            }
            this.$refs.table.reload(this.queryParams);
        },
        async statusTreeClick(data) {
            this.statusFilter = data;
            await this.reload();
        },
        async apiTreeClick(data) {
            this.apiFilter = data.id
                ? { field: "apiId", operator: "eq", value: data.id }
                : null;
            await this.reload();
        },
        async rowClick(row) {
            this.infoDrawer = true;
            this.$nextTick().then(async () => {
                const res = await this.$API.sys_log.query.post({
                    filter: { id: row.id },
                });
                this.$refs.info.setData(res.data[0]);
            });
        },
    },
};
</script>

<style scoped>
.el-aside-filter {
    background: var(--el-bg-color);
    padding: 10px;
}
</style>