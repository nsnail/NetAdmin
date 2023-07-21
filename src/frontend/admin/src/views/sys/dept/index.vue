<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <el-button
                    icon="el-icon-plus"
                    type="primary"
                    @click="add"
                ></el-button>
            </div>
            <div class="right-panel">
                <form class="right-panel-search" @submit.prevent="upSearch">
                    <el-input
                        v-model="queryParams.keywords"
                        clearable
                        placeholder="部门编号 / 部门名称"
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
                :params="queryParams"
                default-expand-all
                hidePagination
                row-key="id"
                @selection-change="selectionChange"
            >
                <el-table-column label="部门编号" prop="id"></el-table-column>
                <el-table-column label="部门名称" prop="name"></el-table-column>
                <el-table-column label="排序" prop="sort"></el-table-column>
                <el-table-column
                    label="创建时间"
                    prop="createdTime"
                ></el-table-column>
                <el-table-column label="备注" prop="summary"></el-table-column>
                <el-table-column align="right" fixed="right" label="操作">
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
            dialog: {
                save: false,
                info: false,
            },
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
            apiObj: this.$API.sys_dept.query,
            selection: [],
            search: {
                keyword: null,
            },
        };
    },
    methods: {
        //添加
        add() {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open();
            });
        },
        //编辑
        table_edit(row) {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("edit").setData(row);
            });
        },
        //查看
        table_view(row) {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("view").setData(row);
            });
        },
        //删除
        async table_del(row) {
            const reqData = { id: row.id };
            try {
                await this.$API.sys_dept.delete.post(reqData);
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
                .then(() => {
                    const loading = this.$loading();
                    this.$refs.table.refresh();
                    loading.close();
                    this.$message.success("操作成功");
                })
                .catch(() => {});
        },
        //表格选择后回调事件
        selectionChange(selection) {
            this.selection = selection;
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
            if (mode === "add") {
                this.$refs.table.refresh();
            } else if (mode === "edit") {
                this.$refs.table.refresh();
            }
        },
    },
};
</script>

<style></style>