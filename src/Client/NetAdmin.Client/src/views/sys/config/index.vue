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
                <el-table-column label="配置编号" prop="id"></el-table-column>
                <el-table-column label="用户注册">
                    <el-table-column
                        label="默认部门"
                        prop="userRegisterDept.name"
                    ></el-table-column>
                    <el-table-column
                        label="默认角色"
                        prop="userRegisterRole.name"
                    ></el-table-column>
                    <el-table-column
                        label="默认岗位"
                        prop="userRegisterPos.name"
                    ></el-table-column>
                    <el-table-column
                        label="人工审核"
                        prop="userRegisterConfirm"
                        width="80"
                    >
                        <template #default="scope">
                            <el-switch
                                v-model="scope.row.userRegisterConfirm"
                                :loading="scope.row.$switch_userRegisterConfirm"
                                @change="
                                    changeSwitch(
                                        $event,
                                        scope.row,
                                        '$switch_userRegisterConfirm'
                                    )
                                "
                            ></el-switch>
                        </template>
                    </el-table-column>
                </el-table-column>

                <el-table-column label="启用" prop="enabled" width="80">
                    <template #default="scope">
                        <el-switch
                            v-model="scope.row.enabled"
                            :loading="scope.row.$switch_enabled"
                            @change="
                                changeSwitch(
                                    $event,
                                    scope.row,
                                    '$switch_enabled'
                                )
                            "
                        ></el-switch>
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
            list: {
                apiObj: this.$API.sys_config.pagedQuery,
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
        //表格内开关事件
        async changeSwitch(val, row, loading) {
            //1.还原数据
            //2.执行加载
            row[loading] = true;
            let res;
            try {
                res = await this.$API.sys_config.update.post(row);
            } catch {}
            //3.等待接口返回后改变值
            delete row[loading];
            Object.assign(row, res.data);
            this.$message.success(`操作成功`);
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
            try {
                await this.$API.sys_config.delete.post(reqData);
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
                await this.$API.sys_config.bulkDelete.post({
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
            //当然也可以暴力的直接刷新表格
            this.$refs.table.refresh();
        },
    },
};
</script>

<style></style>