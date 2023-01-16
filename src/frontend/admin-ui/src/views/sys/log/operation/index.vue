<template>
    <el-container>
        <el-aside width="220px">
            <el-tree ref="category" class="menu" node-key="label" :data="category" :default-expanded-keys="['所有日志']"
                     current-node-key="所有日志" :highlight-current="true" :expand-on-click-node="false"
                     default-expand-all @node-click="cateClick">
            </el-tree>
        </el-aside>
        <el-container>
            <el-main class="nopadding">
                <el-container>
                    <el-header>
                        <div class="left-panel">
                            <el-date-picker v-model="date" type="datetimerange" range-separator="至"
                                            start-placeholder="开始日期" end-placeholder="结束日期"
                                            @change="dateChange"></el-date-picker>
                        </div>
                        <div class="right-panel">

                        </div>
                    </el-header>
                    <el-header style="height:150px;">
                        <scEcharts height="100%" :option="logsChartOption"></scEcharts>
                    </el-header>
                    <el-main class="nopadding">
                        <scTable ref="table" :apiObj="apiObj" stripe highlightCurrentRow @row-click="rowClick"
                                 remoteSort remoteFilter>
                            <el-table-column sortable="custom" label="状态码" prop="statusCode"
                                             width="100"></el-table-column>
                            <el-table-column sortable="custom" label="ID" prop="id" width="180"></el-table-column>
                            <el-table-column label="请求接口">
                                <el-table-column sortable="custom" label="路径" prop="apiId"
                                                 width="300"></el-table-column>
                                <el-table-column label="描述" prop="apiSummary"
                                                 width="150"></el-table-column>
                                <el-table-column sortable="custom" label="方法" prop="method"
                                                 width="100"></el-table-column>
                                <el-table-column sortable="custom" label="耗时(毫秒)" prop="duration"
                                                 width="120"></el-table-column>
                            </el-table-column>
                            <el-table-column sortable="custom" label="用户" prop="createdUserName"
                                             width="150"></el-table-column>
                            <el-table-column sortable="custom" label="客户端IP" prop="clientIp"
                                             width="150"></el-table-column>
                            <el-table-column sortable="custom" label="操作系统" prop="os" width="150"></el-table-column>
                            <el-table-column sortable="custom" label="日志时间" prop="createdTime"
                                             width="170"></el-table-column>
                        </scTable>
                    </el-main>
                </el-container>
            </el-main>
        </el-container>
    </el-container>

    <el-drawer v-model="infoDrawer" title="日志详情" :size="1000" destroy-on-close>
        <info ref="info"></info>
    </el-drawer>
</template>

<script>
import info from './info'
import scEcharts from '@/components/scEcharts'
import tool from "@/utils/tool";

export default {
    name: 'log',
    components: {
        info,
        scEcharts
    },
    data() {
        return {
            queryParams: {
                dynamicFilter: null
            },
            infoDrawer: false,
            logsChartOption: {
                color: ['#409eff', '#e6a23c', '#f56c6c'],
                grid: {
                    top: '0px',
                    left: '10px',
                    right: '10px',
                    bottom: '0px'
                },
                tooltip: {
                    trigger: 'axis'
                },
                xAxis: {
                    type: 'category',
                    boundaryGap: false,
                    data: ['2021-07-01', '2021-07-02', '2021-07-03', '2021-07-04', '2021-07-05', '2021-07-06', '2021-07-07', '2021-07-08', '2021-07-09', '2021-07-10', '2021-07-11', '2021-07-12', '2021-07-13', '2021-07-14', '2021-07-15']
                },
                yAxis: {
                    show: false,
                    type: 'value'
                },
                series: [{
                    data: [120, 200, 150, 80, 70, 110, 130, 120, 200, 150, 80, 70, 110, 130, 70, 110],
                    type: 'bar',
                    stack: 'log',
                    barWidth: '15px'
                },
                    {
                        data: [15, 26, 7, 12, 13, 9, 21, 15, 26, 7, 12, 13, 9, 21, 12, 3],
                        type: 'bar',
                        stack: 'log',
                        barWidth: '15px'
                    },
                    {
                        data: [0, 0, 0, 120, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                        type: 'bar',
                        stack: 'log',
                        barWidth: '15px'
                    }]
            },
            category: [
                {label: '所有日志'},
                {label: '成功日志', field: 'statusCode', operator: 'eq', value: 200},
                {
                    label: '错误日志', field: 'statusCode', operator: 'NotAny', value: '200'
                    , children: [
                        {label: '400', field: 'statusCode', operator: 'eq', value: 400},
                        {label: '401', field: 'statusCode', operator: 'eq', value: 401},
                        {label: '403', field: 'statusCode', operator: 'eq', value: 403},
                        {label: '404', field: 'statusCode', operator: 'eq', value: 404},
                        {label: '500', field: 'statusCode', operator: 'eq', value: 500},
                        {label: '其它', field: 'statusCode', operator: 'NotAny', value: '200,400,401,403,404,500'}
                    ]
                },
            ],
            date: [],
            apiObj: this.$API.sys_log.pagedQueryOperationLog,
            search: {
                keyword: ""
            },
            cateFilter: null
        }
    },
    methods: {
        upsearch() {

        },
        async dateChange() {
            await this.reload()
        },
        async reload() {
            this.queryParams.dynamicFilter = {logic: 'and', filters: []};
            if (this.cateFilter) {
                this.queryParams.dynamicFilter.filters.push(this.cateFilter);
            }
            if (this.date && this.date.length == 2) {

                this.queryParams.dynamicFilter.filters.push({
                    field: 'createdTime', operator:
                        'dateRange', value: this.date.map(x => tool.dateFormat(x))
                });
            }
            this.$refs.table.reload(this.queryParams)
        },
        async cateClick(data) {
            this.cateFilter = data;
            await this.reload();
        },
        async rowClick(row) {
            this.infoDrawer = true
            this.$nextTick(async () => {
                const res = await this.$API.sys_log.queryOperationLog.post({filter: {id: row.id}})
                this.$refs.info.setData(res.data[0])
            })
        }
    }
}
</script>

<style>
</style>