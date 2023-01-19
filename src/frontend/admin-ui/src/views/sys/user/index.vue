<template>
    <el-container>
        <el-aside width="200px" v-loading="showTreeloading" class="el-aside-filter">
            <el-collapse v-model="activeNames">
                <el-collapse-item title="部门筛选" name="1">
                    <el-container>
                        <el-header>
                            <el-input placeholder="输入关键字进行过滤" v-model="deptFilterText" clearable></el-input>
                        </el-header>
                        <el-main class="nopadding">
                            <el-tree ref="dept" class="menu" node-key="id" :data="dept" :current-node-key="''"
                                     :props="deptProps"
                                     :highlight-current="true" :expand-on-click-node="false"
                                     :filter-node-method="deptFilterNode" @node-click="deptClick"></el-tree>
                        </el-main>
                    </el-container>
                </el-collapse-item>
                <el-collapse-item title="角色筛选" name="2">
                    <el-container>
                        <el-header>
                            <el-input placeholder="输入关键字进行过滤" v-model="roleFilterText" clearable></el-input>
                        </el-header>
                        <el-main class="nopadding">
                            <el-tree ref="role" class="menu" node-key="id" :data="role" :current-node-key="''"
                                     :props="roleProps"
                                     :highlight-current="true" :expand-on-click-node="false"
                                     :filter-node-method="roleFilterNode" @node-click="roleClick"></el-tree>
                        </el-main>
                    </el-container>
                </el-collapse-item>
                <el-collapse-item title="岗位筛选" name="3">
                    <el-container>
                        <el-header>
                            <el-input placeholder="输入关键字进行过滤" v-model="posiFilterText" clearable></el-input>
                        </el-header>
                        <el-main class="nopadding">
                            <el-tree ref="posi" class="menu" node-key="id" :data="posi" :current-node-key="''"
                                     :highlight-current="true" :expand-on-click-node="false"
                                     :props="posiProps"
                                     :filter-node-method="posiFilterNode" @node-click="posiClick"></el-tree>
                        </el-main>
                    </el-container>
                </el-collapse-item>
            </el-collapse>
        </el-aside>
        <el-container>
            <el-header>
                <div class="left-panel">
                    <el-button type="primary" icon="el-icon-plus" @click="add"></el-button>
                    <el-button type="danger" plain icon="el-icon-delete" :disabled="selection.length==0"
                               @click="batch_del"></el-button>
                    <el-button type="primary" plain :disabled="selection.length==0">分配角色</el-button>
                    <el-button type="primary" plain :disabled="selection.length==0">密码重置</el-button>
                </div>
                <div class="right-panel">
                    <div class="right-panel-search">
                        <el-input v-model="search.keywords" placeholder="登录账号 / 手机号"
                                  clearable></el-input>
                        <el-button type="primary" icon="el-icon-search" @click="upsearch"></el-button>
                    </div>
                </div>
            </el-header>
            <el-main class="nopadding">
                <scTable ref="table" :apiObj="apiObj" @selection-change="selectionChange" stripe remoteSort
                         remoteFilter>
                    <el-table-column type="selection"></el-table-column>
                    <el-table-column label="ID" prop="id" sortable='custom'></el-table-column>
                    <el-table-column label="用户名" prop="userName" sortable='custom'>
                        <template #default="scope">
                            <div style="display: flex;flex-direction: row;">
                                <div style="margin-right: 10px">
                                    <el-avatar :src="getAvatar(scope)" size="small"></el-avatar>
                                </div>
                                <div style="flex-grow: 1">{{ scope.row.userName }}</div>
                            </div>
                        </template>

                    </el-table-column>
                    <el-table-column label="手机号" prop="mobile" sortable='custom'></el-table-column>
                    <el-table-column label="所属角色">
                        <template #default="scope">
                            <el-tag v-for="(item,index) in scope.row.roles" :key="index">
                                {{ item.name }}
                            </el-tag>
                        </template>
                    </el-table-column>
                    <el-table-column label="所属部门" prop="dept.name" align="center" width="120" sortable="custom"
                                     sort-by="deptid">
                    </el-table-column>
                    <el-table-column label="岗位">
                        <template #default="scope">
                            <el-tag v-for="(item,index) in scope.row.positions" :key="index">
                                {{ item.name }}
                            </el-tag>
                        </template>
                    </el-table-column>
                    <el-table-column label="状态" width="80" align="center"
                                     column-key="filterUserName" :filters="[{text: '启用', value: '1'}, {text:
                                          '未启用', value: '0'}]" prop="enabled">
                        <template #default="scope">
                            <sc-status-indicator v-if="scope.row.enabled" type="success"></sc-status-indicator>
                            <sc-status-indicator v-if="!scope.row.enabled" pulse type="danger"></sc-status-indicator>
                        </template>
                    </el-table-column>
                    <el-table-column label="加入时间" prop="createdTime"
                                     sortable='custom'></el-table-column>
                    <el-table-column label="操作" fixed="right" align="right">
                        <template #default="scope">
                            <el-button-group>
                                <el-button text type="primary" size="small"
                                           @click="table_show(scope.row, scope.$index)">查看
                                </el-button>
                                <el-button text type="primary" size="small"
                                           @click="table_edit(scope.row, scope.$index)">编辑
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
    </el-container>

    <save-dialog v-if="dialog.save" ref="saveDialog" @success="handleSuccess" @closed="dialog.save=false"></save-dialog>

    <el-drawer v-model="dialog.info" :size="800" title="详细" direction="rtl" destroy-on-close>
        <info ref="infoDialog"></info>
    </el-drawer>
</template>

<script>
import saveDialog from './save'
import info from './info'

export default {
    name: 'user',
    components: {
        saveDialog, info
    },
    data() {
        return {
            queryParams: {
                filter: {deptId: 0, roleId: 0},
                dynamicFilter: {
                    logic: 'and',
                    filters: []
                }
            },
            activeNames: ['1', '2', '3'],
            dialog: {
                save: false,
                info: false
            },
            showTreeloading: false,
            deptFilterText: '',
            roleFilterText: '',
            posiFilterText: '',
            dept: [],
            deptProps: {
                label: (data) => data.name
            },
            role: [],
            roleProps: {
                label: (data) => data.name
            },
            posi: [],
            posiProps: {
                label: (data) => data.name
            },
            apiObj: this.$API.sys_user.pagedQuery,
            selection: [],
            search: {
                keywords: null
            }
        }
    },
    watch: {
        deptFilterText(val) {
            this.$refs.dept.filter(val);
        },
        roleFilterText(val) {
            this.$refs.role.filter(val);
        },
        posiFilterText(val) {
            this.$refs.posi.filter(val);
        }
    },
    computed: {},
    mounted() {
        this.getTree()
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
        async table_del(row, index) {
            var reqData = {id: row.id}
            var res = await this.$API.demo.post.post(reqData);
            if (res.code == 200) {
                //这里选择刷新整个表格 OR 插入/编辑现有表格数据
                this.$refs.table.tableData.splice(index, 1);
                this.$message.success("删除成功")
            } else {
                this.$alert(res.message, "提示", {type: 'error'})
            }
        },
        //批量删除
        async batch_del() {
            this.$confirm(`确定删除选中的 ${this.selection.length} 项吗？`, '提示', {
                type: 'warning'
            }).then(() => {
                const loading = this.$loading();
                this.selection.forEach(item => {
                    this.$refs.table.tableData.forEach((itemI, indexI) => {
                        if (item.id === itemI.id) {
                            this.$refs.table.tableData.splice(indexI, 1)
                        }
                    })
                })
                loading.close();
                this.$message.success("操作成功")
            }).catch(() => {

            })
        },
        //表格选择后回调事件
        selectionChange(selection) {
            this.selection = selection;
        },
        //加载树数据
        async getTree() {
            this.showTreeloading = true;

            //加载部门数据
            var res = await this.$API.sys_dept.query.post();
            var allNode = {id: '', name: '所有部门'}
            res.data.unshift(allNode);
            this.dept = res.data;

            //加载角色数据
            res = await this.$API.sys_role.query.post();
            allNode = {id: '', name: '所有角色'}
            res.data.unshift(allNode);
            this.role = res.data;


            //加载岗位数据
            res = await this.$API.sys_position.query.post();
            allNode = {id: '', name: '所有岗位'}
            res.data.unshift(allNode);
            this.posi = res.data;

            this.showTreeloading = false;
        },
        //部门树过滤
        deptFilterNode(value, data) {
            if (!value) return true;
            return data.name.indexOf(value) !== -1;
        },
        //部门树点击事件
        deptClick(data) {
            this.queryParams.filter.deptId = data.id ? data.id : 0;
            this.$refs.table.reload(this.queryParams)
        },
        //角色树过滤
        roleFilterNode(value, data) {
            if (!value) return true;
            return data.name.indexOf(value) !== -1;
        },
        //角色树点击事件
        roleClick(data) {
            this.queryParams.filter.roleId = data.id ? data.id : 0;
            this.$refs.table.reload(this.queryParams)
        },
        //岗位树过滤
        posiFilterNode(value, data) {
            if (!value) return true;
            return data.name.indexOf(value) !== -1;
        },
        //岗位树点击事件
        posiClick(data) {
            this.queryParams.filter.positionId = data.id ? data.id : 0;
            this.$refs.table.reload(this.queryParams)
        },
        //搜索
        upsearch() {
            this.queryParams.dynamicFilter = {
                logic: 'or',
                filters: [
                    {
                        field: 'userName',
                        operator: 'contains',
                        value: this.search.keywords
                    },
                    {
                        field: 'mobile',
                        operator: 'contains',
                        value: this.search.keywords
                    }
                ]
            };
            this.$refs.table.upData(this.queryParams)
        },
        //本地更新数据
        handleSuccess(data, mode) {
            if (mode == 'add') {
                this.$refs.table.tableData.unshift(data)
            } else if (mode == 'edit') {
                this.$refs.table.tableData.filter(item => item.id === data.id).forEach(item => {
                    Object.keys(item).forEach(x => delete item[x])
                    Object.assign(item, data)
                })
            }
        },
        //获取头像
        getAvatar(scope) {
            return scope.row.avatar ? scope.row.avatar : 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAIAAAC0Ujn1AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAflJREFUeNqs1u1P2kAYAHB6B+21UmZbatEAki2+MKIR42TGaHRLjBr/Yj/7wWRb3If5gjHbfIlBjIqCUCq0PmaJLqG9IwdP+qV3vV+v1/Z5Ttg/Ke39tm+q7VD/wlBx/j0J7xzWbccL9TVgosCivrv/Atgw8yI9JuZGVXNQjCkROL2tOpc39q8/D7bj0gcy6NSQvDoTR4LwdidVhOPDyMD29/Jd9YkyFlH6pAhanjb+d19DFvHSlOHX0x09kYpGcOAFMPf0kMxJJ02ZvlyWRjhpWcJ0moiIk240Gf9Rw2lz0o92i04/Nnhpl/Hhhtqux0mnLcZrzCQUTrrV9lh/M++CFM9rtOXyvL+lOif98+T++r4Z1Ht0Vju9anDSEKXbQBqSFH0sgz6+qMGDd7Y/1J96pav1FuTPzvZvxYrrej3REOWK09l4XWkyB7Jp30RBRNwH2jf/MZMimzZi/kk5l1HFMOKnFYK/zMaDqsxqPo6QwENDtd2YtwakwOI5rJO1OZOS0/3p8VR0s2CpMqMoJzSytWAldKmrig7cQk4fMUiX+w14rPVPVvGi9qNYcVpuIP1xVM2PvaOU2sACnYwmTbJ7cHdefssqSJFeXkUYCysz8flJjcN9nf7XvFnIahi/gMCiwphMROFzVstYSu/7sWxaXczpAAL7LMAA6FWV/DBrhrIAAAAASUVORK5CYII='
        }
    }
}
</script>

<style scoped>
.el-aside-filter {
    background: var(--el-bg-color);
    padding: 10px;
}
</style>