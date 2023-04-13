<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <el-button
                    v-auth="`api/cst/app/create`"
                    icon="el-icon-plus"
                    type="primary"
                    @click="add"
                ></el-button>
                <el-button
                    v-auth="`api/cst/app/register.new.app`"
                    icon="el-icon-plus"
                    type="primary"
                    @click="register"
                    >注册新应用
                </el-button>
                <el-button
                    :disabled="selection.length === 0"
                    icon="el-icon-delete"
                    plain
                    type="danger"
                    @click="batch_del"
                ></el-button>
                <el-button
                    v-auth="`api/cst/app/update.enabled`"
                    :disabled="selection.length === 0"
                    icon="el-icon-check"
                    plain
                    type="success"
                    @click="batch_enable"
                    >启用
                </el-button>
                <el-button
                    v-auth="`api/cst/app/update.enabled`"
                    :disabled="selection.length === 0"
                    icon="el-icon-check"
                    plain
                    type="warning"
                    @click="batch_disable"
                    >禁用
                </el-button>
            </div>
            <form class="right-panel" @submit.prevent="upSearch">
                <div class="right-panel-search">
                    <el-input
                        v-model="queryParams.keywords"
                        clearable
                        placeholder="关键词"
                    ></el-input>
                    <el-button
                        icon="el-icon-search"
                        type="primary"
                        @click="upSearch"
                    ></el-button>
                </div>
            </form>
        </el-header>
        <el-main class="nopadding">
            <scTable
                ref="table"
                v-loading="loading"
                :apiObj="list.apiObj"
                :cell-click-method="cellClick"
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
                    label="应用编号"
                    prop="id"
                    sortable="custom"
                    width="150"
                ></el-table-column>
                <el-table-column
                    label="应用名称"
                    prop="summary"
                    width="200"
                ></el-table-column>
                <el-table-column
                    label="所属用户"
                    prop="userApp.user.userName"
                    sortable="custom"
                    width="150"
                ></el-table-column>
                <el-table-column
                    label="回调地址"
                    prop="callbackUrl"
                    show-overflow-tooltip
                    width="250"
                ></el-table-column>
                <el-table-column
                    label="推送地址"
                    prop="pushUrl"
                    show-overflow-tooltip
                    width="250"
                ></el-table-column>
                <el-table-column label="启用" prop="enabled" width="80">
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
                <el-table-column
                    label="创建时间"
                    prop="createdTime"
                    sortable="custom"
                ></el-table-column>

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
    <user-save-dialog
        v-if="dialog.userSave"
        ref="userSaveDialog"
        @closed="dialog.userSave = false"
    >
    </user-save-dialog>
</template>

<script>
import saveDialog from "./save";
import userSaveDialog from "@/views/sys/user/save";

export default {
    components: {
        saveDialog,
        userSaveDialog,
    },
    data() {
        return {
            loading: false,
            dialog: {
                userSave: false,
                save: false,
                info: false,
            },
            list: {
                apiObj: this.$API.cst_app.pagedQuery,
            },
            selection: [],
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
        };
    },
    mounted() {},
    methods: {
        async cellClick(item, i) {
            if (i.property === "userApp.user.userName") {
                this.loading = true;
                const user = await this.$API.sys_user.query.post({
                    filter: { id: item.userApp.user.id },
                });
                this.loading = false;
                this.dialog.userSave = true;
                this.$nextTick().then(() => {
                    this.$refs.userSaveDialog
                        .open("view")
                        .setData(user.data[0]);
                });
            }
        },
        //窗口新增
        add() {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open();
            });
        },
        //窗口注册
        register() {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("reg");
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
            const res = await this.$API.cst_app.delete.post(reqData);
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
            try {
                const res = await this.$API.cst_app.bulkDelete.post({
                    items: ids.map((x) => {
                        return { id: x };
                    }),
                });
                this.$emit("success", res.data, this.mode);
                this.$message.success(`删除了 ${res.data} 项`);
            } catch {}
        },
        //批量启用
        async batch_enable() {
            const confirmRes = await this.$confirm(
                `确定启用选中的 ${this.selection.length} 项吗？`,
                "提示",
                {
                    type: "warning",
                    confirmButtonText: "启用",
                    confirmButtonClass: "el-button--danger",
                }
            ).catch(() => {});

            if (!confirmRes) {
                return false;
            }
            this.selection.forEach((x) => {
                x.enabled = true;
            });
            try {
                const res = await this.$API.cst_app.bulkUpdateEnabled.post({
                    items: this.selection,
                });
                this.$refs.table.refresh();
                this.$message.success(`启用了 ${res.data} 项`);
            } catch {}
        },
        //批量禁用
        async batch_disable() {
            const confirmRes = await this.$confirm(
                `确定禁用选中的 ${this.selection.length} 项吗？`,
                "提示",
                {
                    type: "warning",
                    confirmButtonText: "禁用",
                    confirmButtonClass: "el-button--danger",
                }
            ).catch(() => {});

            if (!confirmRes) {
                return false;
            }
            this.selection.forEach((x) => {
                x.enabled = false;
            });
            try {
                const res = await this.$API.cst_app.bulkUpdateEnabled.post({
                    items: this.selection,
                });
                this.$refs.table.refresh();
                this.$message.success(`禁用了 ${res.data} 项`);
            } catch {}
        },
        //表格选择后回调事件
        selectionChange(selection) {
            this.selection = selection;
        },
        //本地更新数据
        handleSaveSuccess(data, mode) {
            //为了减少网络请求，直接变更表格内存数据
            if (mode === "add" || mode === "reg") {
                this.$refs.table.unshiftRow(data);
            } else if (mode === "edit") {
                this.$refs.table.updateKey(data);
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
                    field: "summary",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "callbackUrl",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "pushUrl",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "userApp.user.userName",
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