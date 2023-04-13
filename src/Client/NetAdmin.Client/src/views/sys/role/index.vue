<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <el-button
                    icon="el-icon-plus"
                    type="primary"
                    @click="add"
                ></el-button>
                <el-button
                    :disabled="selection.length === 0"
                    icon="el-icon-delete"
                    plain
                    type="danger"
                    @click="batch_del"
                ></el-button>
            </div>
            <div class="right-panel">
                <form class="right-panel-search" @submit.prevent="upSearch">
                    <el-input
                        v-model="queryParams.keywords"
                        clearable
                        placeholder="角色编号 / 角色名称"
                    ></el-input>
                    <el-button
                        icon="el-icon-search"
                        type="primary"
                        @click="upSearch"
                    ></el-button>
                </form>
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable
                ref="table"
                :apiObj="apiObj"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :params="queryParams"
                remote-filter
                remote-sort
                row-key="id"
                stripe
                @selection-change="selectionChange"
            >
                <el-table-column type="selection" width="50"></el-table-column>
                <el-table-column
                    label="角色编号"
                    prop="id"
                    sortable="custom"
                    width="150"
                ></el-table-column>
                <el-table-column
                    label="角色名称"
                    prop="name"
                    sortable="custom"
                    width="150"
                ></el-table-column>
                <el-table-column
                    label="排序"
                    prop="sort"
                    width="100"
                ></el-table-column>
                <el-table-column
                    align="center"
                    label="无限权限"
                    prop="enabled"
                    width="80"
                >
                    <template #default="scope">
                        <sc-status-indicator
                            v-if="scope.row.ignorePermissionControl"
                            type="success"
                        ></sc-status-indicator>
                        <sc-status-indicator
                            v-if="!scope.row.ignorePermissionControl"
                            type="danger"
                        ></sc-status-indicator>
                    </template>
                </el-table-column>

                <el-table-column
                    label="数据范围"
                    prop="dataScope"
                    sortable="custom"
                >
                    <template #default="scope">
                        {{
                            this.$CONFIG.ENUMS.dataScopes[
                                scope.row.dataScope
                            ][1]
                        }}
                    </template>
                </el-table-column>
                <el-table-column
                    align="center"
                    label="显示仪表板"
                    prop="displayDashboard"
                >
                    <template #default="scope">
                        <sc-status-indicator
                            v-if="scope.row.displayDashboard"
                            type="success"
                        ></sc-status-indicator>
                        <sc-status-indicator
                            v-if="!scope.row.displayDashboard"
                            type="danger"
                        ></sc-status-indicator>
                    </template>
                </el-table-column>
                <el-table-column
                    label="创建时间"
                    prop="createdTime"
                    sortable="custom"
                    width="180"
                ></el-table-column>
                <el-table-column
                    label="备注"
                    min-width="150"
                    prop="remark"
                ></el-table-column>
                <el-table-column
                    align="right"
                    fixed="right"
                    label="操作"
                    width="170"
                >
                    <template #default="scope">
                        <el-button-group>
                            <el-button
                                icon="el-icon-view"
                                size="small"
                                @click="table_view(scope.row)"
                            ></el-button>
                            <el-button
                                icon="el-icon-edit"
                                size="small"
                                type="primary"
                                @click="table_edit(scope.row)"
                            ></el-button>
                            <el-popconfirm
                                title="确定删除吗？"
                                @confirm="table_del(scope.row, scope.$index)"
                            >
                                <template #reference>
                                    <el-button
                                        icon="el-icon-delete"
                                        size="small"
                                        type="danger"
                                    ></el-button>
                                </template>
                            </el-popconfirm>
                        </el-button-group>
                    </template>
                </el-table-column>
            </scTable>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        ref="saveDialog"
        @closed="dialog.save = false"
        @success="handleSaveSuccess"
    ></save-dialog>
</template>

<script>
import saveDialog from "./save";

export default {
    components: {
        saveDialog,
    },
    data() {
        return {
            queryParams: {
                dynamicFilter: {
                    logic: "and",
                    filters: [
                        {
                            logic: "or",
                            filters: [],
                        },
                    ],
                },
            },
            dialog: {
                save: false,
                info: false,
            },
            apiObj: this.$API.sys_role.pagedQuery,
            selection: [],
            search: {
                keyword: null,
            },
            role: null,
        };
    },
    methods: {
        //添加
        add() {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open().loadTree();
            });
        },
        //编辑
        table_edit(row) {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("edit").setData(row).loadTree();
            });
        },
        //查看
        table_view(row) {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("view").setData(row).loadTree();
            });
        },
        //删除
        async table_del(row) {
            const reqData = { id: row.id };
            try {
                await this.$API.sys_role.delete.post(reqData);
                this.$refs.table.refresh();
                this.$message.success("删除成功");
            } catch {}
        },
        //批量删除
        async batch_del() {
            this.$confirm(
                `确定删除选中的 ${this.selection.length} 项吗？如果删除项中含有子集将会被一并删除`,
                "提示",
                {
                    type: "warning",
                }
            )
                .then(async () => {
                    const loading = this.$loading();
                    try {
                        const res = await this.$API.sys_role.bulkDelete.post({
                            items: this.selection,
                        });
                        this.$refs.table.refresh();
                        this.$message.success(`删除 ${res.data} 条`);
                    } catch {}
                    loading.close();
                })
                .catch(() => {});
        },
        //表格选择后回调事件
        selectionChange(selection) {
            this.selection = selection;
        },
        //表格内开关
        changeSwitch(val, row) {
            row.status = row.status === "1" ? "0" : "1";
            row.$switch_status = true;
            setTimeout(() => {
                delete row.$switch_status;
                row.status = val;
                this.$message.success("操作成功");
            }, 500);
        },
        //搜索
        upSearch() {
            this.queryParams.dynamicFilter.filters[0].filters = [
                {
                    field: "id",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "name",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
            ];
            this.$refs.table.upData();
        },
        //根据ID获取树结构
        filterTree(id) {
            let target = null;

            function filter(tree) {
                tree.forEach((item) => {
                    if (item.id === id) {
                        target = item;
                    }
                    if (item.children) {
                        filter(item.children);
                    }
                });
            }

            filter(this.$refs.table.tableData);
            return target;
        },
        //本地更新数据
        handleSaveSuccess(data, mode) {
            //为了减少网络请求，直接变更表格内存数据
            if (mode === "add") {
                this.$refs.table.unshiftRow(data);
            } else if (mode === "edit") {
                this.$refs.table.refresh();
            }
        },
    },
};
</script>

<style></style>