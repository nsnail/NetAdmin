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
                <el-button
                    :disabled="selection.length !== 1"
                    plain
                    type="primary"
                    @click="permission"
                    >权限设置
                </el-button>
            </div>
            <div class="right-panel">
                <div class="right-panel-search">
                    <el-input
                        v-model="search.keyword"
                        clearable
                        placeholder="角色名称"
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
                :apiObj="apiObj"
                row-key="id"
                stripe
                @selection-change="selectionChange"
            >
                <el-table-column type="selection" width="50"></el-table-column>
                <el-table-column
                    label="#"
                    type="index"
                    width="50"
                ></el-table-column>
                <el-table-column
                    label="角色名称"
                    prop="label"
                    width="150"
                ></el-table-column>
                <el-table-column
                    label="别名"
                    prop="alias"
                    width="200"
                ></el-table-column>
                <el-table-column
                    label="排序"
                    prop="sort"
                    width="80"
                ></el-table-column>
                <el-table-column label="状态" prop="status" width="80">
                    <template #default="scope">
                        <el-switch
                            v-model="scope.row.status"
                            :loading="scope.row.$switch_status"
                            active-value="1"
                            inactive-value="0"
                            @change="changeSwitch($event, scope.row)"
                        ></el-switch>
                    </template>
                </el-table-column>
                <el-table-column
                    label="创建时间"
                    prop="date"
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
                                size="small"
                                text
                                type="primary"
                                @click="table_show(scope.row, scope.$index)"
                            >
                                查看
                            </el-button>
                            <el-button
                                size="small"
                                text
                                type="primary"
                                @click="table_edit(scope.row, scope.$index)"
                            >
                                编辑
                            </el-button>
                            <el-popconfirm
                                title="确定删除吗？"
                                @confirm="table_del(scope.row, scope.$index)"
                            >
                                <template #reference>
                                    <el-button size="small" text type="primary"
                                        >删除
                                    </el-button>
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

    <permission-dialog
        v-if="dialog.permission"
        ref="permissionDialog"
        @closed="dialog.permission = false"
    ></permission-dialog>
</template>

<script>
import saveDialog from "./save";
import permissionDialog from "./permission";

export default {
    components: {
        saveDialog,
        permissionDialog,
    },
    data() {
        return {
            dialog: {
                save: false,
                permission: false,
            },
            apiObj: this.$API.system.role.list,
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
        table_show(row) {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("show").setData(row);
            });
        },
        //权限设置
        permission() {
            this.dialog.permission = true;
            this.$nextTick(() => {
                this.$refs.permissionDialog.open();
            });
        },
        //删除
        async table_del(row) {
            const reqData = { id: row.id };
            const res = await this.$API.demo.post.post(reqData);
            if (res.code === "succeed") {
                this.$refs.table.refresh();
                this.$message.success("删除成功");
            } else {
                await this.$alert(res.message, "提示", { type: "error" });
            }
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
        upSearch() {},
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