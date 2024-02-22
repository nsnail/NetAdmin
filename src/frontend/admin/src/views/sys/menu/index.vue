<template>
    <el-container>
        <el-aside v-loading="loading" width="300px">
            <el-container>
                <el-header>
                    <el-input v-model="filterText" :placeholder="$t('输入关键字进行过滤')" clearable></el-input>
                </el-header>
                <el-main class="nopadding">
                    <el-tree
                        :check-strictly="true"
                        :data="treeList"
                        :default-expand-all="true"
                        :expand-on-click-node="false"
                        :filter-node-method="treeFilterNode"
                        :props="{
                            label: (data) => {
                                return data.meta.title + ' - ' + data.sort
                            },
                        }"
                        @node-click="treeClick"
                        draggable
                        highlight-current
                        node-key="id"
                        ref="tree"
                        show-checkbox>
                        <template #default="{ node, data }">
                            <div class="custom-tree-node">
                                <span>{{ node.label }}</span>
                                <span class="btn">
                                    <el-button-group size="small">
                                        <el-button @click.stop="add(node, data)" icon="el-icon-plus"></el-button>
                                        <el-popconfirm :title="`确定删除 ${data.meta.title} 吗？`" @confirm="del(node, data)">
                                            <template #reference>
                                                <el-button @click.stop="() => {}" icon="el-icon-delete"></el-button>
                                            </template>
                                        </el-popconfirm>
                                    </el-button-group>
                                </span>
                            </div>
                        </template>
                    </el-tree>
                </el-main>
                <el-footer>
                    <el-button @click="add()" icon="el-icon-plus" size="small" type="primary"></el-button>
                    <el-button @click="batchDel" icon="el-icon-delete" plain size="small" type="danger"></el-button>
                </el-footer>
            </el-container>
        </el-aside>
        <el-container>
            <el-main class="main" ref="main">
                <save :tree="treeList" @success="handleSuccess" ref="save"></save>
            </el-main>
        </el-container>
    </el-container>
</template>
<script>
import save from './save'

export default {
    components: {
        save,
    },
    data() {
        return {
            loading: false,
            treeList: [],
            filterText: '',
        }
    },
    watch: {
        filterText(val) {
            this.$refs.tree.filter(val)
        },
    },
    mounted() {
        this.getTree()
    },
    methods: {
        //加载树数据
        async getTree() {
            this.loading = true
            const res = await this.$API.sys_menu.query.post()
            this.loading = false
            this.treeList = res.data
        },
        //树点击
        treeClick(data, node) {
            this.$refs.save.setData(data, node.level === 1 ? 0 : node.parent.data.id)
            this.$refs.main.$el.scrollTop = 0
        },
        //树过滤
        treeFilterNode(value, data) {
            if (!value) return true
            const targetText = data.meta.title
            return targetText.indexOf(value) !== -1
        },
        //增加
        async add(node, data) {
            const newData = {
                parentId: data ? data.id : 0,
                name: Math.random().toString(),
                path: Math.random().toString(),
                meta: {
                    title: '未命名',
                    type: 'menu',
                },
            }
            this.loading = true
            const res = await this.$API.sys_menu.create.post(newData)
            this.loading = false
            Object.assign(newData, res.data)
            this.$refs.tree.append(newData, node)
            this.$refs.tree.setCurrentKey(newData.id)
            this.$refs.save.setData(newData, node ? node.data.id : '')
        }, //删除
        async del(node, data) {
            this.loading = true
            try {
                const res = await this.$API.sys_menu.delete.post({ id: data.id })
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
            this.handleSuccess()
            this.loading = false
        },
        //批量删除
        async batchDel() {
            const checkedNodes = this.$refs.tree.getCheckedNodes()
            if (checkedNodes.length === 0) {
                this.$message.warning('请选择需要删除的项')
                return false
            }
            try {
                await this.$confirm('确认删除已选择的菜单吗？', '提示', {
                    type: 'warning',
                    confirmButtonText: '删除',
                    confirmButtonClass: 'el-button--danger',
                })
            } catch {
                return false
            }

            this.loading = true
            try {
                await this.$API.sys_menu.bulkDelete.post({
                    items: checkedNodes.map((item) => ({
                        id: item.id,
                    })),
                })
                checkedNodes.forEach((item) => {
                    if (this.$refs.tree.getNode(item).isCurrent) {
                        this.$refs.save.setData({})
                    }
                    this.$refs.tree.remove(item)
                })
            } catch {
                //
            }
            this.loading = false
        },
        async handleSuccess() {
            this.$TOOL.refreshTab(this)
        },
    },
}
</script>
<style lang="scss" scoped>
.custom-tree-node {
    display: flex;
    flex-grow: 1;
    align-items: center;
    justify-content: space-between;
    margin-right: 2rem;

    .btn {
        display: none;
        gap: 1rem;
    }

    &:hover .btn {
        display: flex;
    }
}

.main {
    background: var(--el-bg-color);
}

::v-deep .el-tree-node__content {
    height: 3rem;
}
</style>