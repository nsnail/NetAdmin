<template>
    <el-container>
        <el-aside v-loading="showGrouploading" width="200px">
            <el-container>
                <el-header>
                    <el-input
                        v-model="groupFilterText"
                        clearable
                        placeholder="输入关键字进行过滤"
                    ></el-input>
                </el-header>
                <el-main class="nopadding">
                    <el-tree
                        ref="group"
                        :current-node-key="''"
                        :data="group"
                        :expand-on-click-node="false"
                        :filter-node-method="groupFilterNode"
                        :highlight-current="true"
                        class="menu"
                        node-key="id"
                        @node-click="groupClick"
                    ></el-tree>
                </el-main>
            </el-container>
        </el-aside>
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
                        :disabled="selection.length === 0"
                        plain
                        type="primary"
                        >分配角色
                    </el-button>
                    <el-button
                        :disabled="selection.length === 0"
                        plain
                        type="primary"
                        >密码重置
                    </el-button>
                </div>
                <div class="right-panel">
                    <div class="right-panel-search">
                        <el-input
                            v-model="search.name"
                            clearable
                            placeholder="登录账号 / 姓名"
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
                    remoteFilter
                    remoteSort
                    stripe
                    @selection-change="selectionChange"
                >
                    <el-table-column
                        type="selection"
                        width="50"
                    ></el-table-column>
                    <el-table-column
                        label="ID"
                        prop="id"
                        sortable="custom"
                        width="80"
                    ></el-table-column>
                    <el-table-column
                        :filters="[
                            { text: '已上传', value: '1' },
                            { text: '未上传', value: '0' },
                        ]"
                        column-key="filterAvatar"
                        label="头像"
                        width="80"
                    >
                        <template #default="scope">
                            <el-avatar
                                :src="scope.row.avatar"
                                size="small"
                            ></el-avatar>
                        </template>
                    </el-table-column>
                    <el-table-column
                        :filters="[
                            { text: '系统账号', value: '1' },
                            { text: '普通账号', value: '0' },
                        ]"
                        column-key="filterUserName"
                        label="登录账号"
                        prop="userName"
                        sortable="custom"
                        width="150"
                    ></el-table-column>
                    <el-table-column
                        label="姓名"
                        prop="name"
                        sortable="custom"
                        width="150"
                    ></el-table-column>
                    <el-table-column
                        label="所属角色"
                        prop="groupName"
                        sortable="custom"
                        width="200"
                    ></el-table-column>
                    <el-table-column
                        label="加入时间"
                        prop="date"
                        sortable="custom"
                        width="170"
                    ></el-table-column>
                    <el-table-column
                        align="right"
                        fixed="right"
                        label="操作"
                        width="160"
                    >
                        <template #default="scope">
                            <el-button-group>
                                <el-button
                                    size="small"
                                    text
                                    type="primary"
                                    @click="table_show(scope.row, scope.$index)"
                                    >查看
                                </el-button>
                                <el-button
                                    size="small"
                                    text
                                    type="primary"
                                    @click="table_edit(scope.row, scope.$index)"
                                    >编辑
                                </el-button>
                                <el-popconfirm
                                    title="确定删除吗？"
                                    @confirm="
                                        table_del(scope.row, scope.$index)
                                    "
                                >
                                    <template #reference>
                                        <el-button
                                            size="small"
                                            text
                                            type="primary"
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
    </el-container>

    <save-dialog
        v-if="dialog.save"
        ref="saveDialog"
        @closed="dialog.save = false"
        @success="handleSuccess"
    ></save-dialog>
</template>

<script>
import saveDialog from "./save";

export default {
    name: "user",
    components: {
        saveDialog,
    },
    data() {
        return {
            dialog: {
                save: false,
            },
            showGrouploading: false,
            groupFilterText: "",
            group: [],
            apiObj: this.$API.system.user.list,
            selection: [],
            search: {
                name: null,
            },
        };
    },
    watch: {
        groupFilterText(val) {
            this.$refs.group.filter(val);
        },
    },
    mounted() {
        this.getGroup();
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
        //删除
        async table_del(row, index) {
            const reqData = { id: row.id };
            const res = await this.$API.demo.post.post(reqData);
            if (res.code === "succeed") {
                //这里选择刷新整个表格 OR 插入/编辑现有表格数据
                this.$refs.table.tableData.splice(index, 1);
                this.$message.success("删除成功");
            } else {
                await this.$alert(res.message, "提示", { type: "error" });
            }
        },
        //批量删除
        async batch_del() {
            this.$confirm(
                `确定删除选中的 ${this.selection.length} 项吗？`,
                "提示",
                {
                    type: "warning",
                }
            )
                .then(() => {
                    const loading = this.$loading();
                    this.selection.forEach((item) => {
                        this.$refs.table.tableData.forEach((itemI, indexI) => {
                            if (item.id === itemI.id) {
                                this.$refs.table.tableData.splice(indexI, 1);
                            }
                        });
                    });
                    loading.close();
                    this.$message.success("操作成功");
                })
                .catch(() => {});
        },
        //表格选择后回调事件
        selectionChange(selection) {
            this.selection = selection;
        },
        //加载树数据
        async getGroup() {
            this.showGrouploading = true;
            const res = await this.$API.system.dept.list.get();
            this.showGrouploading = false;
            const allNode = { id: "", label: "所有" };
            res.data.unshift(allNode);
            this.group = res.data;
        },
        //树过滤
        groupFilterNode(value, data) {
            if (!value) return true;
            return data.label.indexOf(value) !== -1;
        },
        //树点击事件
        groupClick(data) {
            const params = {
                groupId: data.id,
            };
            this.$refs.table.reload(params);
        },
        //搜索
        upSearch() {
            this.$refs.table.upData(this.search);
        },
        //本地更新数据
        handleSuccess(data, mode) {
            if (mode === "add") {
                data.id = new Date().getTime();
                this.$refs.table.tableData.unshift(data);
            } else if (mode === "edit") {
                this.$refs.table.tableData
                    .filter((item) => item.id === data.id)
                    .forEach((item) => {
                        Object.assign(item, data);
                    });
            }
        },
    },
};
</script>

<style></style>