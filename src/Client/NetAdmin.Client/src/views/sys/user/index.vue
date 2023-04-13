<template>
    <el-container>
        <el-aside
            v-loading="showTreeloading"
            class="el-aside-filter"
            width="200px"
        >
            <el-collapse v-model="activeNames">
                <el-collapse-item name="1" title="部门筛选">
                    <el-container>
                        <el-header>
                            <el-input
                                v-model="deptFilterText"
                                clearable
                                placeholder="输入关键字进行过滤"
                            ></el-input>
                        </el-header>
                        <el-main class="nopadding">
                            <el-tree
                                ref="dept"
                                :current-node-key="''"
                                :data="dept"
                                :expand-on-click-node="false"
                                :filter-node-method="deptFilterNode"
                                :highlight-current="true"
                                :props="deptProps"
                                class="menu"
                                node-key="id"
                                @node-click="deptClick"
                            ></el-tree>
                        </el-main>
                    </el-container>
                </el-collapse-item>
                <el-collapse-item name="2" title="角色筛选">
                    <el-container>
                        <el-header>
                            <el-input
                                v-model="roleFilterText"
                                clearable
                                placeholder="输入关键字进行过滤"
                            ></el-input>
                        </el-header>
                        <el-main class="nopadding">
                            <el-tree
                                ref="role"
                                :current-node-key="''"
                                :data="role"
                                :expand-on-click-node="false"
                                :filter-node-method="roleFilterNode"
                                :highlight-current="true"
                                :props="roleProps"
                                class="menu"
                                node-key="id"
                                @node-click="roleClick"
                            ></el-tree>
                        </el-main>
                    </el-container>
                </el-collapse-item>
                <el-collapse-item name="3" title="岗位筛选">
                    <el-container>
                        <el-header>
                            <el-input
                                v-model="posiFilterText"
                                clearable
                                placeholder="输入关键字进行过滤"
                            ></el-input>
                        </el-header>
                        <el-main class="nopadding">
                            <el-tree
                                ref="posi"
                                :current-node-key="''"
                                :data="posi"
                                :expand-on-click-node="false"
                                :filter-node-method="posiFilterNode"
                                :highlight-current="true"
                                :props="posiProps"
                                class="menu"
                                node-key="id"
                                @node-click="posiClick"
                            ></el-tree>
                        </el-main>
                    </el-container>
                </el-collapse-item>
            </el-collapse>
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
                    <form class="right-panel-search" @submit.prevent="upSearch">
                        <el-input
                            v-model="queryParams.keywords"
                            clearable
                            placeholder="用户编号 / 用户名 / 手机号"
                            style="width: 200px"
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
                    :apiObj="apiObj"
                    :default-sort="{ prop: 'createdTime', order: 'descending' }"
                    :filter-method="filterMethod"
                    :params="queryParams"
                    remote-filter
                    remote-sort
                    stripe
                    @selection-change="selectionChange"
                >
                    <el-table-column type="selection"></el-table-column>
                    <el-table-column
                        label="用户编号"
                        prop="id"
                        sortable="custom"
                        width="150"
                    ></el-table-column>
                    <el-table-column
                        label="用户名"
                        prop="userName"
                        sortable="custom"
                    >
                        <template #default="scope">
                            <div style="display: flex; flex-direction: row">
                                <div style="margin-right: 10px">
                                    <el-avatar
                                        :src="getAvatar(scope)"
                                        size="small"
                                    ></el-avatar>
                                </div>
                                <div style="flex-grow: 1">
                                    {{ scope.row.userName }}
                                </div>
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column
                        label="手机号"
                        prop="mobile"
                        sortable="custom"
                        width="120"
                    ></el-table-column>
                    <el-table-column label="所属角色">
                        <template #default="scope">
                            <el-tag
                                v-for="(item, index) in scope.row.roles"
                                :key="index"
                                @click="userRoleClick(item)"
                            >
                                {{ item.name }}
                            </el-tag>
                        </template>
                    </el-table-column>
                    <el-table-column
                        align="center"
                        label="所属部门"
                        prop="dept.name"
                        sort-by="deptid"
                        sortable="custom"
                        width="120"
                    >
                        <template #default="scope">
                            <el-tag @click="userDeptClick(scope.row.dept)">
                                {{ scope.row.dept.name }}
                            </el-tag>
                        </template>
                    </el-table-column>
                    <el-table-column label="岗位" width="120">
                        <template #default="scope">
                            <el-tag
                                v-for="(item, index) in scope.row.positions"
                                :key="index"
                                @click="userPosiClick(item)"
                            >
                                {{ item.name }}
                            </el-tag>
                        </template>
                    </el-table-column>
                    <el-table-column
                        :filters="[
                            { text: '启用', value: true },
                            { text: '禁用', value: false },
                        ]"
                        align="center"
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
                        label="加入时间"
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

    <save-dialog
        v-if="dialog.save"
        ref="saveDialog"
        @closed="dialog.save = false"
        @success="handleSuccess"
    ></save-dialog>
    <role-save-dialog
        v-if="dialog.roleSave"
        ref="roleSaveDialog"
        @closed="dialog.roleSave = false"
    >
    </role-save-dialog>
    <dept-save-dialog
        v-if="dialog.deptSave"
        ref="deptSaveDialog"
        @closed="dialog.deptSave = false"
    >
    </dept-save-dialog>
    <posi-save-dialog
        v-if="dialog.posiSave"
        ref="posiSaveDialog"
        @closed="dialog.posiSave = false"
    >
    </posi-save-dialog>
</template>

<script>
import saveDialog from "./save";
import roleSaveDialog from "@/views/sys/role/save.vue";
import deptSaveDialog from "@/views/sys/dept/save.vue";
import posiSaveDialog from "@/views/sys/position/save.vue";

export default {
    components: {
        saveDialog,
        roleSaveDialog,
        deptSaveDialog,
        posiSaveDialog,
    },
    data() {
        return {
            loading: false,
            queryParams: {
                filter: { deptId: 0, roleId: 0 },
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
            activeNames: ["1", "2", "3"],
            dialog: {
                roleSave: false,
                posiSave: false,
                deptSave: false,
                save: false,
                info: false,
            },
            showTreeloading: false,
            deptFilterText: "",
            roleFilterText: "",
            posiFilterText: "",
            dept: [],
            deptProps: {
                label: (data) => data.name,
            },
            role: [],
            roleProps: {
                label: (data) => data.name,
            },
            posi: [],
            posiProps: {
                label: (data) => data.name,
            },
            apiObj: this.$API.sys_user.pagedQuery,
            selection: [],
        };
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
        },
    },
    computed: {},
    mounted() {
        this.getTree();
    },
    methods: {
        async userRoleClick(item) {
            this.loading = true;
            const role = await this.$API.sys_role.query.post({
                filter: { id: item.id },
            });
            this.loading = false;
            this.dialog.roleSave = true;
            this.$nextTick().then(() => {
                this.$refs.roleSaveDialog
                    .open("view")
                    .setData(role.data[0])
                    .loadTree();
            });
        },
        async userDeptClick(item) {
            this.loading = true;
            const dept = await this.$API.sys_dept.query.post({
                filter: { id: item.id },
            });
            this.loading = false;
            this.dialog.deptSave = true;
            this.$nextTick().then(() => {
                this.$refs.deptSaveDialog.open("view").setData(dept.data[0]);
            });
        },
        async userPosiClick(item) {
            this.loading = true;
            const posi = await this.$API.sys_position.query.post({
                filter: { id: item.id },
            });
            this.loading = false;
            this.dialog.posiSave = true;
            this.$nextTick().then(() => {
                this.$refs.posiSaveDialog.open("view").setData(posi.data[0]);
            });
        },
        filterMethod(filters) {
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
            this.$refs.table.upData();
        },
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
        table_view(row) {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("view").setData(row);
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
        async getTree() {
            this.showTreeloading = true;

            //加载部门数据
            let res = await this.$API.sys_dept.query.post();
            let allNode = { id: "", name: "所有部门" };
            res.data.unshift(allNode);
            this.dept = res.data;

            //加载角色数据
            res = await this.$API.sys_role.query.post();
            allNode = { id: "", name: "所有角色" };
            res.data.unshift(allNode);
            this.role = res.data;

            //加载岗位数据
            res = await this.$API.sys_position.query.post();
            allNode = { id: "", name: "所有岗位" };
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
            this.$refs.table.upData();
        },
        //角色树过滤
        roleFilterNode(value, data) {
            if (!value) return true;
            return data.name.indexOf(value) !== -1;
        },
        //角色树点击事件
        roleClick(data) {
            this.queryParams.filter.roleId = data.id ? data.id : 0;
            this.$refs.table.upData();
        },
        //岗位树过滤
        posiFilterNode(value, data) {
            if (!value) return true;
            return data.name.indexOf(value) !== -1;
        },
        //岗位树点击事件
        posiClick(data) {
            this.queryParams.filter.positionId = data.id ? data.id : 0;
            this.$refs.table.upData();
        },
        //搜索
        upSearch() {
            this.queryParams.dynamicFilter.filters[0].filters = [
                {
                    field: "userName",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "mobile",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
                {
                    field: "id",
                    operator: "contains",
                    value: this.queryParams.keywords,
                },
            ];
            this.$refs.table.upData();
        },
        //本地更新数据
        handleSuccess(data, mode) {
            if (mode === "add") {
                this.$refs.table.tableData.unshift(data);
            } else if (mode === "edit") {
                this.$refs.table.tableData
                    .filter((item) => item.id === data.id)
                    .forEach((item) => {
                        Object.keys(item).forEach((x) => delete item[x]);
                        Object.assign(item, data);
                    });
            }
        },
        //获取头像
        getAvatar(scope) {
            return scope.row.avatar
                ? scope.row.avatar
                : this.$CONFIG.DEF_AVATAR;
        },
    },
};
</script>

<style scoped>
.el-aside-filter {
    background: var(--el-bg-color);
    padding: 10px;
}
</style>