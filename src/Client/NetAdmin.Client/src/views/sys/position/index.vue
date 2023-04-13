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
                        placeholder="岗位编号 / 岗位名称"
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
                :apiObj="list.apiObj"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :params="queryParams"
                remote-sort
                row-key="id"
                stripe
                @selection-change="selectionChange"
            >
                <el-table-column type="selection"></el-table-column>
                <el-table-column label="岗位编号" prop="id"></el-table-column>
                <el-table-column label="岗位名称" prop="name"></el-table-column>

                <el-table-column
                    label="排序"
                    prop="sort"
                    width="100"
                ></el-table-column>
                <el-table-column
                    label="创建时间"
                    prop="createdTime"
                    sortable="custom"
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
            list: {
                apiObj: this.$API.sys_position.pagedQuery,
            },
            selection: [],
        };
    },
    mounted() {},
    methods: {
        //窗口新增
        add() {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open();
            });
        },
        //窗口编辑
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
        //删除明细
        async table_del(row, index) {
            const reqData = { id: row.id };
            const res = await this.$API.demo.post.post(reqData);
            if (res.code === "succeed") {
                this.$refs.table.removeIndex(index);
                this.$message.success("删除成功");
            } else {
                await this.$alert(res.message, "提示", { type: "error" });
            }
        },
        //批量删除
        async batch_del() {
            const confirmRes = await this.$confirm(
                `确定删除选中的 ${this.selection.length} 项吗？`,
                "提示",
                {
                    type: "warning",
                    confirmButtonText: "删除",
                    confirmButtonClass: "el-button--danger",
                }
            ).catch(() => {});

            if (!confirmRes) {
                return false;
            }

            const ids = this.selection.map((v) => v.id);
            this.$refs.table.removeKeys(ids);
            this.$message.success("操作成功");
        },
        //表格选择后回调事件
        selectionChange(selection) {
            this.selection = selection;
        },
        //本地更新数据
        handleSaveSuccess(data, mode) {
            //为了减少网络请求，直接变更表格内存数据
            if (mode === "add") {
                this.$refs.table.unshiftRow(data);
            } else if (mode === "edit") {
                this.$refs.table.refresh();
            }

            //当然也可以暴力的直接刷新表格
            // this.$refs.table.refresh()
        },
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
    },
};
</script>

<style></style>