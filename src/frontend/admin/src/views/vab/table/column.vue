<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <el-button icon="el-icon-plus" type="primary"></el-button>
                <el-button icon="el-icon-delete" plain type="danger"></el-button>
            </div>
            <div class="right-panel">
                <el-alert :closable="false" title="请在右下角第二个按钮体验自定义列" type="warning" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable ref="table" :apiObj="list.apiObj" :column="list.column" row-key="id" stripe tableName="listCustomColumn">
                <el-table-column type="selection" width="50"></el-table-column>
                <template #progress="scope">
                    <el-progress :percentage="scope.row.progress" />
                </template>
            </scTable>
        </el-main>
    </el-container>
</template>

<script>
export default {
    name: 'tableCustomColumn',
    data() {
        return {
            list: {
                apiObj: this.$API.demo.list,
                column: [
                    {
                        label: '姓名',
                        prop: 'name',
                        width: '150',
                    },
                    {
                        label: '性别',
                        prop: 'sex',
                        width: '150',
                        filters: [
                            { text: '男', value: '男' },
                            { text: '女', value: '女' },
                        ],
                    },
                    {
                        label: '邮箱',
                        prop: 'email',
                        width: '250',
                        hide: true,
                    },
                    {
                        label: '评分',
                        prop: 'num',
                        width: '150',
                        sortable: true,
                    },
                    {
                        label: '进度',
                        prop: 'progress',
                        width: '250',
                        sortable: true,
                    },
                    {
                        label: '注册时间',
                        prop: 'datetime',
                        width: '150',
                        sortable: true,
                    },
                ],
            },
        }
    },
}
</script>

<style></style>