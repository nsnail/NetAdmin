<template>
    <el-container>
        <el-aside v-loading="loading" width="40rem">
            <el-container>
                <el-header>
                    <el-input v-model="filterText" :placeholder="$t('输入关键字进行过滤')" clearable></el-input>
                </el-header>
                <el-main class="nopadding">
                    <el-tree
                        :data="data"
                        :expand-on-click-node="false"
                        :filter-node-method="filterNode"
                        :highlight-current="true"
                        :props="{
                            label: 'name',
                        }"
                        @node-click="click"
                        default-expand-all
                        node-key="id"
                        ref="dic">
                        <template #default="{ _, data }">
                            <div class="custom-tree-node">
                                <span>{{ data.name }} {{ data.code }}</span>
                                <span class="btn">
                                    <el-button-group size="small">
                                        <el-button @click.stop="add(data.id, data.code)" icon="el-icon-plus"></el-button>
                                        <el-button @click.stop="edit(data)" icon="el-icon-edit"></el-button>
                                        <el-popconfirm :title="$t('确定删除 {item} 吗？', { item: data.name })" @confirm="del(data)" width="20rem">
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
                    <el-button @click="add(form.catalogId)" icon="el-icon-plus" size="small" style="width: 100%" type="primary">{{
                        $t('字典分类')
                    }}</el-button>
                </el-footer>
            </el-container>
        </el-aside>
        <list :catalogId="form.catalogId" />
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => getData()"
        ref="saveDialog"></save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import list from './list'

const saveDialog = defineAsyncComponent(() => import('./save.vue'))
export default {
    components: {
        list,
        saveDialog,
    },
    computed: {},
    created() {
        if (this.keywords) {
            this.query.keywords = this.keywords
        }
    },
    data() {
        return {
            data: [],
            dialog: {},
            filterText: '',
            form: {},
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [],
                },
                filter: {},
            },
        }
    },
    inject: ['reload'],
    methods: {
        // 获取字典目录
        async getData() {
            this.loading = true
            const res = await this.$API.sys_dic.queryCatalog.post()
            this.loading = false
            this.data = res.data

            //获取第一个节点,设置选中 & 加载明细列表
            if (this.data.length > 0) {
                await this.$nextTick()
                this.$refs.dic.setCurrentKey(this.data[0].id)
                this.form.catalogId = this.data[0].id
            }
        },
        //字典目录过滤
        filterNode(value, data) {
            if (!value) return true
            const targetText = data.name + data.code
            return targetText.indexOf(value) !== -1
        },
        //字典目录增加
        async add(id, code) {
            this.dialog.save = { mode: 'add', data: { catalogId: id, code: code + '>' } }
        },
        //字典目录编辑
        async edit(data) {
            this.dialog.save = { mode: 'edit', row: data }
        },
        //字典目录点击
        click(data) {
            this.form.catalogId = data.id
        },
        //字典目录删除
        async del(data) {
            this.loading = true
            try {
                await this.$API.sys_dic.deleteCatalog.post({
                    id: data.id,
                })
                this.$message.success(this.$t('操作成功'))
            } catch {
                //
            }
            this.loading = false
            await this.getData()
        },
    },
    mounted() {
        this.getData()
    },
    props: ['keywords'],
    watch: {
        filterText(val) {
            this.$refs.dic.filter(val)
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

::v-deep .el-tree-node__content {
    height: 3rem;
}
</style>