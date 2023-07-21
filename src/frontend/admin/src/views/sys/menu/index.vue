<template>
    <el-container>
        <el-aside v-loading="loading" width="300px">
            <el-container>
                <el-header>
                    <el-input
                        v-model="filterText"
                        clearable
                        placeholder="输入关键字进行过滤"
                    ></el-input>
                </el-header>
                <el-main class="nopadding">
                    <el-tree
                        ref="tree"
                        :data="treeList"
                        :default-expand-all="true"
                        :expand-on-click-node="false"
                        :filter-node-method="treeFilterNode"
                        :props="treeProps"
                        class="tree"
                        draggable
                        highlight-current
                        node-key="id"
                        show-checkbox
                        @node-click="treeClick"
                        @node-drop="nodeDrop"
                    >
                        <template #default="{ node, data }">
                            <span class="custom-tree-node el-tree-node__label">
                                <span class="label">
                                    {{ node.label }}
                                </span>
                                <span class="do">
                                    <el-icon @click.stop="add(node, data)"
                                        ><el-icon-plus
                                    /></el-icon>
                                </span>
                            </span>
                        </template>
                    </el-tree>
                </el-main>
                <el-footer style="height: 51px">
                    <el-button
                        icon="el-icon-plus"
                        size="small"
                        type="primary"
                        @click="add()"
                    ></el-button>
                    <el-button
                        icon="el-icon-delete"
                        plain
                        size="small"
                        type="danger"
                        @click="delTree"
                    ></el-button>
                </el-footer>
            </el-container>
        </el-aside>
        <el-container>
            <el-main ref="main" class="nopadding" style="padding: 20px">
                <save
                    ref="save"
                    :tree="treeList"
                    @success="handleSuccess"
                ></save>
            </el-main>
        </el-container>
    </el-container>
</template>
<script>
import save from "./save";

export default {
    components: {
        save,
    },
    data() {
        return {
            loading: false,
            treeList: [],
            treeProps: {
                label: (data) => {
                    return data.meta.title;
                },
            },
            filterText: "",
        };
    },
    watch: {
        filterText(val) {
            this.$refs.tree.filter(val);
        },
    },
    mounted() {
        this.getTree();
    },
    methods: {
        //加载树数据
        async getTree() {
            this.loading = true;
            const res = await this.$API["sys_menu"].query.post();
            this.loading = false;
            this.treeList = res.data;
        },
        //树点击
        treeClick(data, node) {
            const pid = node.level === 1 ? 0 : node.parent.data.id;
            this.$refs.save.setData(data, pid);
            this.$refs.main.$el.scrollTop = 0;
        },
        //树过滤
        treeFilterNode(value, data) {
            if (!value) return true;
            const targetText = data.meta.title;
            return targetText.indexOf(value) !== -1;
        },
        //树拖拽
        nodeDrop(draggingNode, dropNode, dropType) {
            this.$refs.save.setData({});
            this.$message(
                `拖拽对象：${draggingNode.data.meta.title}, 释放对象：${dropNode.data.meta.title}, 释放对象的位置：${dropType}`
            );
        },
        //增加
        async add(node, data) {
            const newTreeName = "未命名";
            const newTreeData = {
                parentId: data ? data.id : 0,
                name: Math.random().toString(),
                path: Math.random().toString(),
                meta: {
                    title: newTreeName,
                    type: "menu",
                },
            };
            this.loading = true;
            const res = await this.$API["sys_menu"].create.post(newTreeData);
            this.loading = false;
            newTreeData.id = res.data.id;
            newTreeData.sort = res.data.sort;
            this.$refs.tree.append(newTreeData, node);
            this.$refs.tree.setCurrentKey(newTreeData.id);
            const pid = node ? node.data.id : "";
            this.$refs.save.setData(newTreeData, pid);
        },
        //删除菜单
        async delTree() {
            const CheckedNodes = this.$refs.tree.getCheckedNodes();
            if (CheckedNodes.length === 0) {
                this.$message.warning("请选择需要删除的项");
                return false;
            }
            const confirm = await this.$confirm(
                "确认删除已选择的菜单吗？",
                "提示",
                {
                    type: "warning",
                    confirmButtonText: "删除",
                    confirmButtonClass: "el-button--danger",
                }
            ).catch(() => {});
            if (confirm !== "confirm") {
                return false;
            }
            this.loading = true;
            const reqData = {
                items: CheckedNodes.map((item) => {
                    return {
                        id: item.id,
                    };
                }),
            };
            try {
                await this.$API["sys_menu"].bulkDelete.post(reqData);
                CheckedNodes.forEach((item) => {
                    const node = this.$refs.tree.getNode(item);
                    if (node.isCurrent) {
                        this.$refs.save.setData({});
                    }
                    this.$refs.tree.remove(item);
                });
            } catch {}
            this.loading = false;
        },
        handleSuccess() {
            this.$refs.save.setData({});
            this.getTree();
        },
    },
};
</script>
<style scoped>
.tree:deep(.el-tree-node__label) {
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
    height: 100%;
    padding-right: 24px;
}

.custom-tree-node .label {
    display: flex;
    align-items: center;
    height: 100%;
}

.custom-tree-node .do {
    display: none;
}

.custom-tree-node .do i {
    margin-left: 5px;
    color: #999;
}

.custom-tree-node .do i:hover {
    color: #333;
}

.custom-tree-node:hover .do {
    display: inline-block;
}
</style>