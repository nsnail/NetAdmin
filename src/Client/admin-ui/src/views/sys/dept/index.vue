<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <el-button type="primary" icon="el-icon-plus" @click="add"></el-button>
            </div>
            <div class="right-panel">
                <div class="right-panel-search">
                    <el-input v-model="search.keyword" placeholder="部门名称" clearable></el-input>
                    <el-button type="primary" icon="el-icon-search" @click="upsearch"></el-button>
                </div>
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable ref="table" :apiObj="apiObj" row-key="id" @selection-change="selectionChange" hidePagination
                     default-expand-all>
                <el-table-column label="部门名称" prop="name" width="250"></el-table-column>
                <el-table-column label="排序" prop="sort" width="150"></el-table-column>
                <el-table-column label="启用" prop="enabled" width="80">
                    <template #default="scope">
                        <el-switch v-model="scope.row.enabled" disabled></el-switch>
                    </template>
                </el-table-column>
                <el-table-column label="创建时间" prop="createdTime" width="180"></el-table-column>
                <el-table-column label="备注" prop="summary" min-width="300"></el-table-column>
                <el-table-column label="操作" fixed="right" align="right" width="170">
                    <template #default="scope">
                        <el-button-group>
                            <el-button text type="primary" size="small" @click="table_show(scope.row, scope.$index)">
                                查看
                            </el-button>
                            <el-button text type="primary" size="small" @click="table_edit(scope.row, scope.$index)">
                                编辑
                            </el-button>
                            <el-popconfirm title="确定删除吗？" @confirm="table_del(scope.row, scope.$index)">
                                <template #reference>
                                    <el-button text type="primary" size="small">删除</el-button>
                                </template>
                            </el-popconfirm>
                        </el-button-group>
                    </template>
                </el-table-column>

            </scTable>
        </el-main>
    </el-container>

    <save-dialog v-if="dialog.save" ref="saveDialog" @success="handleSaveSuccess"
                 @closed="dialog.save=false"></save-dialog>
    <el-drawer v-model="dialog.info" :size="800" title="详细" direction="rtl" destroy-on-close>
        <info ref="infoDialog"></info>
    </el-drawer>

</template>

<script>
import saveDialog from './save'
import info from "./info.vue"

export default {
    name: 'dept',
    components: {
        saveDialog, info
    },
    data() {
        return {
            dialog: {
                save: false,
                info: false
            },
            apiObj: this.$API.sys_dept.query,
            selection: [],
            search: {
                keyword: null
            }
        }
    },
    methods: {
        //添加
        add() {
            this.dialog.save = true
            this.$nextTick(() => {
                this.$refs.saveDialog.open()
            })
        },
        //编辑
        table_edit(row) {
            this.dialog.save = true
            this.$nextTick(() => {
                this.$refs.saveDialog.open('edit').setData(row)
            })
        },
        //查看
        table_show(row) {
            this.dialog.info = true
            this.$nextTick(() => {
                this.$refs.infoDialog.setData(row)
            })
        },
        //删除
        async table_del(row) {
            var reqData = {id: row.id}
            try {
                await this.$API.sys_dept.delete.post(reqData);
                this.$refs.table.refresh()
                this.$message.success("删除成功")
            } catch {
            }
        },
        //批量删除
        async batch_del() {
            this.$confirm(`确定删除选中的 ${this.selection.length} 项吗？如果删除项中含有子集将会被一并删除`, '提示', {
                type: 'warning'
            }).then(() => {
                const loading = this.$loading();
                this.$refs.table.refresh()
                loading.close();
                this.$message.success("操作成功")
            }).catch(() => {

            })
        },
        //表格选择后回调事件
        selectionChange(selection) {
            this.selection = selection;
        },
        //搜索
        upsearch() {

        },
        //根据ID获取树结构
        filterTree(id) {
            var target = null;

            function filter(tree) {
                tree.forEach(item => {
                    if (item.id == id) {
                        target = item
                    }
                    if (item.children) {
                        filter(item.children)
                    }
                })
            }

            filter(this.$refs.table.tableData)
            return target
        },
        //本地更新数据
        handleSaveSuccess(data, mode) {
            if (mode == 'add') {
                this.$refs.table.refresh()
            } else if (mode == 'edit') {
                this.$refs.table.refresh()
            }
        }
    }
}
</script>

<style>
</style>