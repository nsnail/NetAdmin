<template>
    <el-container>
        <el-header>
            <div class="left-panel"></div>
            <div class="right-panel">
                <form class="right-panel-search" @submit.prevent="upSearch">
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
                </form>
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable
                ref="table"
                :apiObj="list.apiObj"
                :default-sort="{ prop: 'heartTime', order: 'descending' }"
                :filter-method="filterMethod"
                :params="queryParams"
                remote-filter
                remote-sort
                row-key="id"
                stripe
                @selection-change="selectionChange"
            >
                <el-table-column
                    label="设备编号"
                    prop="deviceNo"
                    sortable="custom"
                ></el-table-column>
                <el-table-column
                    label="设备IP"
                    prop="ip"
                    sortable="custom"
                ></el-table-column>
                <el-table-column
                    :filters="useTypeFilters"
                    column-key="useType"
                    label="平台"
                    prop="useType"
                    width="120"
                >
                </el-table-column>
                <el-table-column
                    label="软件版本"
                    prop="appVersion"
                    sortable="custom"
                    width="200"
                >
                </el-table-column>
                <el-table-column
                    label="已分配账号"
                    prop="shareNumber"
                    sortable="custom"
                    width="120"
                >
                </el-table-column>
                <el-table-column
                    label="已使用账号"
                    prop="usedNum"
                    sortable="custom"
                    width="120"
                >
                </el-table-column>
                <el-table-column
                    label="设备坑位上限"
                    prop="upLimitNum"
                    sortable="custom"
                    width="120"
                >
                </el-table-column>
                <el-table-column
                    :filters="[
                        { text: '启用', value: true },
                        { text: '禁用', value: false },
                    ]"
                    column-key="enabled"
                    label="启用"
                    prop="enabled"
                    width="80"
                >
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
                    :filters="onlineFilters"
                    column-key="onlineStatus"
                    label="在线"
                    prop="onlineStatus"
                    width="80"
                >
                    <template #default="scope">
                        <sc-status-indicator
                            v-if="scope.row.onlineStatus === 'online'"
                            type="success"
                        ></sc-status-indicator>
                        <sc-status-indicator
                            v-else
                            pulse
                            type="danger"
                        ></sc-status-indicator>
                    </template>
                </el-table-column>
                <el-table-column
                    label="最后心跳"
                    prop="heartTime"
                    sortable="custom"
                    width="180"
                ></el-table-column>
                <el-table-column
                    label="创建时间"
                    prop="createdTime"
                    sortable="custom"
                    width="180"
                ></el-table-column>
                <el-table-column align="right" fixed="right" label="操作">
                    <template #default="scope">
                        <el-button-group>
                            <el-button
                                icon="el-icon-view"
                                size="small"
                                @click="table_view(scope.row)"
                            ></el-button>
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
            list: {
                apiObj: this.$API.res_device.pagedQuery,
            },
            selection: [],
        };
    },
    mounted() {},
    computed: {
        useTypeFilters() {
            return Object.entries(this.$CONFIG.ENUMS.useTypes).map((x) => {
                return {
                    text: x[1][1],
                    value: x[1][0],
                };
            });
        },
        onlineFilters() {
            return Object.entries(this.$CONFIG.ENUMS.onlineStatues).map((x) => {
                return {
                    text: x[1][1],
                    value: x[1][0],
                };
            });
        },
    },
    methods: {
        filterMethod(filters) {
            if (filters.useType) {
                this.queryParams.dynamicFilter.filters =
                    this.queryParams.dynamicFilter.filters.filter(
                        (x) => x.field !== "useType"
                    );
                if (filters.useType.length > 0) {
                    this.queryParams.dynamicFilter.filters.push({
                        field: "useType",
                        operator: "Any",
                        value: filters.useType,
                    });
                }
            }
            if (filters.enabled) {
                this.queryParams.dynamicFilter.filters =
                    this.queryParams.dynamicFilter.filters.filter(
                        (x) => x.field !== "enabled"
                    );
                if (filters.enabled.length > 0) {
                    this.queryParams.dynamicFilter.filters.push({
                        field: "enabled",
                        operator: "Any",
                        value: filters.enabled,
                    });
                }
            }

            if (filters.onlineStatus) {
                this.queryParams.dynamicFilter.filters =
                    this.queryParams.dynamicFilter.filters.filter(
                        (x) => x.field !== "onlineStatus"
                    );
                if (filters.onlineStatus.length > 0) {
                    this.queryParams.dynamicFilter.filters.push({
                        field: "onlineStatus",
                        operator: "Any",
                        value: filters.onlineStatus,
                    });
                }
            }

            this.$refs.table.upData();
        },
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
                this.$refs.table.updateKey(data);
            }
            //当然也可以暴力的直接刷新表格
            // this.$refs.table.refresh()
        },
        upSearch() {
            this.queryParams.dynamicFilter.filters[0].filters = [
                {
                    field: "deviceNo",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "ip",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "appVersion",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
            ];
            this.$refs.table.upData(this.search);
        },
    },
};
</script>

<style></style>