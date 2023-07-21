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
                <div class="right-panel-search">
                    <el-input
                        v-model="search.dynamicFilter.value"
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
            <scTable
                ref="table"
                :apiObj="list.apiObj"
                row-key="id"
                stripe
                @selection-change="selectionChange"
            >
                <el-table-column type="selection"></el-table-column>
                <el-table-column label="ID" prop="id"></el-table-column>
                <el-table-column label="状态" prop="enabled">
                    <template #default="scope">
                        <sc-status-indicator
                            v-if="scope.row.enabled"
                            type="success"
                        ></sc-status-indicator>
                        <sc-status-indicator
                            v-if="!scope.row.enabled"
                            pulse
                            type="danger"
                        ></sc-status-indicator>
                    </template>
                </el-table-column>
                <el-table-column align="right" fixed="right" label="操作">
                    <template #default="scope">
                        <el-button
                            plain
                            size="small"
                            @click="table_view(scope.row)"
                            >查看
                        </el-button>
                        <el-button
                            plain
                            size="small"
                            type="primary"
                            @click="table_edit(scope.row)"
                            >编辑
                        </el-button>
                        <el-popconfirm
                            title="确定删除吗？"
                            @confirm="table_del(scope.row, scope.$index)"
                        >
                            <template #reference>
                                <el-button plain size="small" type="danger"
                                    >删除
                                </el-button>
                            </template>
                        </el-popconfirm>
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

    <el-drawer
        v-model="dialog.info"
        destroy-on-close
        direction="rtl"
        size="80%"
        title="详细"
    >
        <info ref="infoDialog"></info>
    </el-drawer>
</template>

<script>
import saveDialog from "./save";
import info from "./info";

export default {
    name: "example",
    components: {
        saveDialog,
        info,
    },
    data() {
        return {
            dialog: {
                save: false,
                info: false,
            },
            list: {
                apiObj: this.$API.tpl_example.pagedQuery,
            },
            selection: [],
            search: {
                dynamicFilter: {
                    field: "id",
                    operator: "contains",
                    value: "",
                },
            },
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
            this.dialog.info = true;
            this.$nextTick(() => {
                this.$refs.infoDialog.setData(row);
            });
        },
        //删除明细
        async table_del(row, index) {
            const reqData = { id: row.id };
            try {
                await this.$API.tpl_example.delete.post(reqData);
                this.$refs.table.removeIndex(index);
                this.$message.success("删除成功");
            } catch {}
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

            try {
                await this.$API.tpl_example.bulkDelete.post({
                    items: this.selection,
                });
                this.$refs.table.removeKeys(this.selection.map((x) => x.id));
                this.$message.success("操作成功");
            } catch {}
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
                this.$refs.table.updateKey(data);
            }

            //当然也可以暴力的直接刷新表格
            // this.$refs.table.refresh()
        },
        //搜索
        upSearch() {
            this.$refs.table.upData(this.search);
        },
    },
};
</script>

<style></style>