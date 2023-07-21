<template>
    <el-container>
        <el-aside v-loading="showDicloading" width="300px">
            <el-container>
                <el-header>
                    <el-input
                        v-model="dicFilterText"
                        clearable
                        placeholder="输入关键字进行过滤"
                    ></el-input>
                </el-header>
                <el-main class="nopadding">
                    <el-tree
                        ref="dic"
                        :data="dicList"
                        :expand-on-click-node="false"
                        :filter-node-method="dicFilterNode"
                        :highlight-current="true"
                        :props="dicProps"
                        class="menu"
                        default-expand-all
                        node-key="id"
                        @node-click="dicClick"
                    >
                        <template #default="{ node, data }">
                            <span class="custom-tree-node">
                                <span class="label">{{ data.name }}</span>
                                <span class="code">{{ data.code }}</span>
                                <span class="do">
                                    <el-button-group>
                                        <el-button
                                            icon="el-icon-edit"
                                            size="small"
                                            @click.stop="dicEdit(data)"
                                        ></el-button>
                                        <el-button
                                            icon="el-icon-delete"
                                            size="small"
                                            @click.stop="dicDel(node, data)"
                                        ></el-button>
                                    </el-button-group>
                                </span>
                            </span>
                        </template>
                    </el-tree>
                </el-main>
                <el-footer style="height: 51px">
                    <el-button
                        icon="el-icon-plus"
                        size="small"
                        style="width: 100%"
                        type="primary"
                        @click="addDic"
                    >
                        字典分类
                    </el-button>
                </el-footer>
            </el-container>
        </el-aside>
        <el-container class="is-vertical">
            <el-header>
                <div class="left-panel">
                    <el-button
                        icon="el-icon-plus"
                        type="primary"
                        @click="addInfo"
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
                            placeholder="项名称 / 键值"
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
                    :apiObj="listApi"
                    :default-sort="{ prop: 'createdTime', order: 'descending' }"
                    :paginationLayout="'prev, pager, next'"
                    :params="queryParams"
                    remote-sort
                    row-key="id"
                    stripe
                    @selection-change="selectionChange"
                >
                    <el-table-column
                        type="selection"
                        width="50"
                    ></el-table-column>
                    <el-table-column
                        label="项名称"
                        prop="key"
                        sortable="custom"
                    ></el-table-column>
                    <el-table-column
                        label="键值"
                        prop="value"
                        sortable="custom"
                    ></el-table-column>
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
                                    @confirm="
                                        table_del(scope.row, scope.$index)
                                    "
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
    </el-container>

    <dic-dialog
        v-if="dialog.dic"
        ref="dicDialog"
        @closed="dialog.dic = false"
        @success="handleDicSuccess"
    ></dic-dialog>

    <save-dialog
        v-if="dialog.save"
        ref="saveDialog"
        @closed="dialog.save = false"
        @success="handleListSuccess"
    ></save-dialog>
</template>

<script>
import dicDialog from "./dic";
import saveDialog from "./save";

export default {
    components: {
        dicDialog,
        saveDialog,
    },
    data() {
        return {
            dialog: {
                dic: false,
                save: false,
            },
            showDicloading: true,
            dicList: [],
            dicFilterText: "",
            dicProps: {
                label: "name",
            },
            queryParams: {
                dynamicFilter: {
                    logic: "and",
                    filters: [
                        {
                            logic: "or",
                            filters: [],
                        },
                        {},
                    ],
                },
            },
            listApi: null,
            selection: [],
        };
    },
    watch: {
        dicFilterText(val) {
            this.$refs.dic.filter(val);
        },
    },
    mounted() {
        this.getDic();
    },
    methods: {
        upSearch() {
            this.queryParams.dynamicFilter.filters[0].filters = [
                {
                    field: "key",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "value",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
            ];
            this.$refs.table.upData();
        },
        //加载树数据
        async getDic() {
            const res = await this.$API.sys_dic.queryCatalog.post();
            this.showDicloading = false;
            this.dicList = res.data;

            //获取第一个节点,设置选中 & 加载明细列表
            const firstNode = this.dicList[0];
            if (firstNode) {
                this.$nextTick().then(() => {
                    this.$refs.dic.setCurrentKey(firstNode.id);
                });
                this.queryParams.dynamicFilter.filters[1] = {
                    field: "catalogid",
                    operator: "eq",
                    value: firstNode.id,
                };

                this.listApi = this.$API.sys_dic.pagedQueryContent;
            }
        },
        //树过滤
        dicFilterNode(value, data) {
            if (!value) return true;
            const targetText = data.name + data.code;
            return targetText.indexOf(value) !== -1;
        },
        //树增加
        addDic() {
            this.dialog.dic = true;
            this.$nextTick(() => {
                this.$refs.dicDialog.open();
            });
        },
        //编辑树
        dicEdit(data) {
            this.dialog.dic = true;
            this.$nextTick(() => {
                const editNode = this.$refs.dic.getNode(data.id);
                data.parentId =
                    editNode.level === 1 ? undefined : editNode.parent.data.id;
                this.$refs.dicDialog.open("edit").setData(data);
            });
        },
        //树点击事件
        dicClick(data) {
            this.queryParams.dynamicFilter.filters[1] = {
                field: "catalogid",
                operator: "eq",
                value: data.id,
            };
            this.$refs.table.upData();
        },
        //删除树
        dicDel(node, data) {
            this.$confirm(`确定删除 ${data.name} 项吗？`, "提示", {
                type: "warning",
            })
                .then(async () => {
                    this.showDicloading = true;
                    try {
                        await this.$API.sys_dic.deleteCatalog.post({
                            id: data.id,
                        });
                    } catch {}
                    //删除节点是否为高亮当前 是的话 设置第一个节点高亮
                    const dicCurrentKey = this.$refs.dic.getCurrentKey();
                    this.$refs.dic.remove(data.id);
                    if (dicCurrentKey === data.id) {
                        const firstNode = this.dicList[0];
                        if (firstNode) {
                            this.$refs.dic.setCurrentKey(firstNode.id);
                            this.$refs.table.upData({
                                code: firstNode.code,
                            });
                        } else {
                            this.listApi = null;
                            this.$refs.table.tableData = [];
                        }
                    }

                    this.showDicloading = false;
                    this.$message.success("操作成功");
                })
                .catch(() => {});
        },
        //添加明细
        addInfo() {
            this.dialog.save = true;
            this.$nextTick(() => {
                const dicCurrentKey = this.$refs.dic.getCurrentKey();
                const data = {
                    catalogId: dicCurrentKey,
                };
                this.$refs.saveDialog.open().setData(data);
            });
        },
        //编辑明细
        table_view(row) {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("view").setData(row);
            });
        },
        //编辑明细
        table_edit(row) {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("edit").setData(row);
            });
        },
        //删除明细
        async table_del(row, index) {
            const reqData = { id: row.id };
            try {
                await this.$API.sys_dic.deleteContent.post(reqData);
                this.$refs.table.tableData.splice(index, 1);
                this.$message.success("删除成功");
            } catch {}
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
                .then(async () => {
                    const loading = this.$loading();
                    try {
                        const res =
                            await this.$API.sys_dic.bulkDeleteContent.post({
                                ids: this.selection.map((x) => x.id),
                            });
                        this.$message.success(`删除成功 ${res.data} 条`);
                    } catch {}
                    this.selection.forEach((item) => {
                        this.$refs.table.tableData.forEach((itemI, indexI) => {
                            if (item.id === itemI.id) {
                                this.$refs.table.tableData.splice(indexI, 1);
                            }
                        });
                    });
                    loading.close();
                })
                .catch(() => {});
        },
        //表格选择后回调事件
        selectionChange(selection) {
            this.selection = selection;
        },
        //表格内开关事件
        async changeSwitch(val, row) {
            //1.还原数据
            //row.yx = row.yx == '1' ? '0' : '1'
            //2.执行加载
            row.$switch_yx = true;
            //3.等待接口返回后改变值
            try {
                const res = await this.$API.sys_dic.updateContent.post(row);
                row.version = res.data.version;
                this.$message.success("操作成功");
            } catch {}
            delete row.$switch_yx;
        },
        //本地更新数据
        handleDicSuccess(data, mode) {
            if (mode === "add") {
                if (this.dicList.length > 0) {
                    this.$refs.table.upData({
                        dynamicFilter: {
                            field: "catalogid",
                            operator: "eq",
                            value: data.id,
                        },
                    });
                } else {
                    this.queryParams.dynamicFilter.filters[1] = {
                        field: "catalogid",
                        operator: "eq",
                        value: data.id,
                    };
                    this.listApi = this.$API.sys_dic.pagedQueryContent;
                }
                this.$refs.dic.append(data, data.parentId);
                this.$refs.dic.setCurrentKey(data.id);
            } else if (mode === "edit") {
                const editNode = this.$refs.dic.getNode(data.id);
                //判断是否移动？
                const editNodeParentId =
                    editNode.level === 1 ? undefined : editNode.parent.data.id;
                if (editNodeParentId !== data.parentId) {
                    const obj = editNode.data;
                    this.$refs.dic.remove(data.id);
                    this.$refs.dic.append(obj, data.parentId);
                }
                Object.assign(editNode.data, data);
                this.$refs.dic.setCurrentKey(data.id);
            }
        },
        //本地更新数据
        handleListSuccess(data, mode) {
            if (mode === "add") {
                this.$refs.table.tableData.push(data);
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

<style scoped>
.menu:deep(.el-tree-node__label) {
    display: flex;
    flex: 1;
    height: 100%;
}

.custom-tree-node {
    display: flex;
    flex: 1;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    padding-right: 24px;
    height: 100%;
}

.custom-tree-node .code {
    font-size: 12px;
    color: #999;
}

.custom-tree-node .do {
    display: none;
}

.custom-tree-node:hover .code {
    display: none;
}

.custom-tree-node:hover .do {
    display: inline-block;
}
</style>