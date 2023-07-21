<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <el-button
                    :loading="loading"
                    icon="sc-icon-sync"
                    type="primary"
                    @click="sync"
                    >同步接口
                </el-button>
            </div>
            <div class="right-panel"></div>
        </el-header>
        <el-main class="nopadding">
            <scTable
                ref="table"
                :apiObj="apiObj"
                default-expand-all
                hidePagination
                row-key="id"
                @selection-change="selectionChange"
            >
                <el-table-column
                    label="接口路径"
                    prop="id"
                    width="500"
                ></el-table-column>
                <el-table-column
                    label="接口名称"
                    prop="name"
                    width="250"
                ></el-table-column>
                <el-table-column
                    label="请求方式"
                    prop="method"
                ></el-table-column>
                <el-table-column
                    label="接口描述"
                    min-width="300"
                    prop="summary"
                ></el-table-column>
            </scTable>
        </el-main>
    </el-container>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            loading: false,
            dialog: {
                save: false,
                info: false,
            },
            apiObj: this.$API.sys_api.query,
            selection: [],
            search: {
                keyword: null,
            },
        };
    },
    methods: {
        //同步
        sync() {
            this.loading = true;
            this.$nextTick(async () => {
                await this.$API.sys_api.sync.post();
                this.$refs.table.refresh();
                this.$message.success("同步成功");
                this.loading = false;
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
            this.dialog.info = true;
            this.$nextTick(() => {
                this.$refs.infoDialog.setData(row);
            });
        },
        //删除
        async table_del(row) {
            const reqData = { id: row.id };
            try {
                await this.$API.sys_api.delete.post(reqData);
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