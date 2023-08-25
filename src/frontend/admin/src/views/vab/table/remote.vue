<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <el-button icon="el-icon-plus" type="primary"></el-button>
                <el-button icon="el-icon-delete" plain type="danger"></el-button>
            </div>
            <div class="right-panel">
                <el-alert :closable="false" title="因演示使用mock数据,所以视图上看不出效果,可以查看控制台的网络请求" type="warning" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable ref="table" :apiObj="list.apiObj" remoteFilter remoteSort remoteSummary row-key="id" show-summary stripe>
                <el-table-column type="selection" width="50"></el-table-column>
                <el-table-column label="姓名" prop="name" width="150"></el-table-column>
                <el-table-column :filters="sexFilters" column-key="sex" label="性别" prop="sex" width="150"></el-table-column>
                <el-table-column label="邮箱" prop="email" width="250"></el-table-column>
                <el-table-column label="评分" prop="num" sortable="custom" width="150"></el-table-column>
                <el-table-column label="进度" prop="progress" sortable width="250"></el-table-column>
                <el-table-column label="注册时间" prop="datetime" sortable="custom" width="150"></el-table-column>
            </scTable>
        </el-main>
    </el-container>
</template>

<script>
export default {
    name: 'tableRemote',
    data() {
        return {
            sexFilters: [
                { text: '男', value: '男' },
                { text: '女', value: '女' },
            ],
            list: {
                apiObj: this.$API.demo.list,
            },
        }
    },
    methods: {},
}
</script>

<style></style>