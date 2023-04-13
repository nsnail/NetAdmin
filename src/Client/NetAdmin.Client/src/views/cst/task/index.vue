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
                v-loading="loading"
                :apiObj="list.apiObj"
                :cell-click-method="cellClick"
                :default-sort="{ prop: 'startedTime', order: 'descending' }"
                :filter-method="filterMethod"
                :params="queryParams"
                remote-filter
                remote-sort
                row-key="id"
                stripe
                @selection-change="selectionChange"
            >
                <el-table-column
                    label="任务编号"
                    prop="id"
                    sortable="custom"
                    width="200"
                ></el-table-column>
                <el-table-column
                    label="机器人编号"
                    prop="robotId"
                    sortable="custom"
                    width="150"
                >
                    <template #default="scope">
                        {{ scope.row.robotId.substring(24, 36) }}
                    </template>
                </el-table-column>
                <el-table-column
                    label="应用编号"
                    prop="appId"
                    sortable="custom"
                    width="180"
                >
                    <template #default="scope">
                        {{ scope.row.appId }}
                    </template>
                </el-table-column>
                <el-table-column
                    :filters="cmdFilters"
                    column-key="cmd"
                    label="任务类型"
                    prop="cmd"
                    width="150"
                >
                    <template #default="scope">
                        <sc-status-indicator
                            :style="`background: #${Math.abs(
                                this.$TOOL.crypto.hashCode(scope.row.cmd)
                            )
                                .toString(16)
                                .substring(0, 6)}`"
                        ></sc-status-indicator>
                        {{ this.$CONFIG.ENUMS.taskCmds[scope.row.cmd][1] }}
                    </template>
                </el-table-column>
                <el-table-column
                    :filters="statusFilters"
                    align="center"
                    column-key="taskStatus"
                    label="任务状态"
                    prop="taskStatus"
                    width="100"
                >
                    <template #default="scope">
                        <el-tag
                            :type="
                                scope.row.taskStatus === 'callbacked'
                                    ? 'success'
                                    : scope.row.taskStatus.indexOf('fail') > 0
                                    ? 'danger'
                                    : 'warning'
                            "
                            >{{
                                this.$CONFIG.ENUMS.taskStatues[
                                    scope.row.taskStatus
                                ][1]
                            }}
                        </el-tag>
                    </template>
                </el-table-column>
                <el-table-column
                    :filters="bizFilters"
                    align="center"
                    column-key="bizStatus"
                    label="业务状态"
                    prop="bizStatus"
                    width="100"
                >
                    <template #default="scope">
                        <el-icon
                            v-if="scope.row.bizStatus === 'succeed'"
                            style="color: #67c23a"
                        >
                            <el-icon-check />
                        </el-icon>
                        <sc-status-indicator
                            v-else-if="scope.row.bizStatus === 'failed'"
                            pulse
                            type="danger"
                        ></sc-status-indicator>
                        <sc-status-indicator
                            v-else
                            type="warning"
                        ></sc-status-indicator>
                    </template>
                </el-table-column>
                <el-table-column
                    label="启动时间"
                    prop="startedTime"
                    sortable="custom"
                ></el-table-column>
                <el-table-column
                    align="right"
                    label="完成用时"
                    prop="finishedDuration"
                    sortable="custom"
                    width="130"
                >
                    <template #default="scope">
                        <span v-if="scope.row.finishedDuration">
                            {{
                                Math.round(scope.row.finishedDuration * 100) /
                                100
                            }}s
                        </span>
                    </template>
                </el-table-column>

                <el-table-column
                    align="right"
                    label="回调用时"
                    prop="callbackedDuration"
                    sortable="custom"
                    width="130"
                >
                    <template #default="scope">
                        <span v-if="scope.row.callbackedDuration">
                            {{
                                Math.round(scope.row.callbackedDuration * 100) /
                                100
                            }}s
                        </span>
                    </template>
                </el-table-column>

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

    <app-save-dialog
        v-if="dialog.appSave"
        ref="appSaveDialog"
        @closed="dialog.appSave = false"
    ></app-save-dialog>

    <robot-save-dialog
        v-if="dialog.robotSave"
        ref="robotSaveDialog"
        @closed="dialog.robotSave = false"
    ></robot-save-dialog>

    <el-drawer
        v-model="dialog.info"
        destroy-on-close
        direction="rtl"
        size="80%"
        title="查看任务"
    >
        <info ref="infoDialog"></info>
    </el-drawer>
</template>

<script>
import saveDialog from "./save";
import appSaveDialog from "@/views/cst/app/save.vue";
import robotSaveDialog from "@/views/res/robot/save.vue";
import info from "./info";
import ScStatusIndicator from "@/components/scMini/scStatusIndicator.vue";
import ScTable from "@/components/scTable/index.vue";

export default {
    computed: {
        statusFilters() {
            return Object.entries(this.$CONFIG.ENUMS.taskStatues).map((x) => {
                return {
                    text: x[1][1],
                    value: x[1][0],
                };
            });
        },
        bizFilters() {
            return Object.entries(this.$CONFIG.ENUMS.bizStatues).map((x) => {
                return {
                    text: x[1][1],
                    value: x[1][0],
                };
            });
        },
        cmdFilters() {
            return Object.entries(this.$CONFIG.ENUMS.taskCmds).map((x) => {
                return {
                    text: x[1][1],
                    value: x[1][0],
                };
            });
        },
    },
    components: {
        ScTable,
        ScStatusIndicator,
        saveDialog,
        appSaveDialog,
        robotSaveDialog,
        info,
    },
    data() {
        return {
            loading: false,
            dialog: {
                robotSave: false,
                appSave: false,
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
                apiObj: this.$API.tsk_view.pagedQuery,
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
            this.dialog.info = true;
            this.$nextTick(() => {
                this.$refs.infoDialog.setData(row);
            });
        },
        //删除明细
        async table_del(row, index) {
            const reqData = { id: row.id };
            const res = await this.$API.tsk_view.delete.post(reqData);
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
                const res = await this.$API.tsk_view.bulkDelete.post({
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
                const res = await this.$API.tsk_view.bulkUpdateEnabled.post({
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
                const res = await this.$API.tsk_view.bulkUpdateEnabled.post({
                    items: this.selection,
                });
                this.$refs.table.refresh();
                this.$message.success(`禁用了 ${res.data} 项`);
            } catch {}
        },
        async cellClick(item, i) {
            if (i.property === "appId") {
                this.loading = true;
                const app = await this.$API.cst_app.query.post({
                    filter: { id: item.appId },
                });
                this.loading = false;
                this.dialog.appSave = true;
                this.$nextTick().then(() =>
                    this.$refs.appSaveDialog.open("view").setData(app.data[0])
                );
            } else if (i.property === "robotId") {
                this.loading = true;
                const robot = await this.$API.res_robot.query.post({
                    filter: { id: item.robotId },
                });
                this.loading = false;
                this.dialog.robotSave = true;
                this.$nextTick().then(() =>
                    this.$refs.robotSaveDialog
                        .open("view")
                        .setData(robot.data[0])
                );
            }
        },
        filterMethod(filters) {
            if (filters.taskStatus) {
                this.queryParams.dynamicFilter.filters =
                    this.queryParams.dynamicFilter.filters.filter(
                        (x) => x.field !== "taskStatus"
                    );
                if (filters.taskStatus.length > 0) {
                    this.queryParams.dynamicFilter.filters.push({
                        field: "taskStatus",
                        operator: "Any",
                        value: filters.taskStatus,
                    });
                }
            }
            if (filters.bizStatus) {
                this.queryParams.dynamicFilter.filters =
                    this.queryParams.dynamicFilter.filters.filter(
                        (x) => x.field !== "bizStatus"
                    );
                if (filters.bizStatus.length > 0) {
                    this.queryParams.dynamicFilter.filters.push({
                        field: "bizStatus",
                        operator: "Any",
                        value: filters.bizStatus,
                    });
                }
            }
            if (filters.cmd) {
                this.queryParams.dynamicFilter.filters =
                    this.queryParams.dynamicFilter.filters.filter(
                        (x) => x.field !== "cmd"
                    );
                if (filters.cmd.length > 0) {
                    this.queryParams.dynamicFilter.filters.push({
                        field: "cmd",
                        operator: "Any",
                        value: filters.cmd,
                    });
                }
            }
            this.$refs.table.upData();
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
                    field: "appId",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "robotId",
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