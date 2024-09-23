<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('用户编号 / 用户名'),
                            style: 'width:25rem',
                        }
                    ]"
                    :vue="this"
                    @reset="onReset"
                    @search="onSearch"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-button @click="this.dialog.save = { mode: 'add' }" icon="el-icon-plus" type="primary"></el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :context-menus="['id', 'realName', 'age', 'height', 'createdTime']"
                :context-opers="['view', 'edit']"
                :default-sort="{ prop: 'createdTime', order: 'descending' }"
                :export-api="$API.adm_girlfriends.export"
                :params="query"
                :query-api="$API.adm_girlfriends.pagedQuery"
                :vue="this"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <el-table-column type="selection" />
                <na-col-id :label="$t('编号')" prop="id" sortable="custom" width="170" />
                <na-col-avatar :label="$t('姓名')" prop="realName" width="170" />
                <el-table-column :label="$t('年龄')" align="right" prop="age" sortable="custom" width="120" />
                <el-table-column :label="$t('身高')" align="right" prop="height" sortable="custom" />
                <na-col-operation :vue="this" width="120" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'

const saveDialog = defineAsyncComponent(() => import('./save.vue'))
export default {
    components: {
        saveDialog,
    },
    computed: {
        table() {
            return table
        },
    },
    created() {
        if (this.id) {
            this.query.filter.id = this.id
        }
    },
    data() {
        return {
            dialog: {},
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [
                        
                    ],
                },
                filter: {},
                keywords: this.keywords,
            },
            selection: [],
        }
    },
    inject: ['reload'],
    methods: {
        // async setEnabled(enabled) {
        //     let loading
        //     try {
        //         await this.$confirm(
        //             this.$t('确定要 {operator} 选中的 {count} 项吗？', {
        //                 operator: enabled ? this.$t('启用') : this.$t('禁用'),
        //                 count: this.selection.length,
        //             }),
        //             this.$t('提示'),
        //             {
        //                 type: 'warning',
        //             },
        //         )
        //         loading = this.$loading()
        //         const res = await Promise.all(this.selection.map((x) => this.$API.adm_girlfriends.setEnabled.post(Object.assign(x, { enabled: enabled }))))
        //         this.$message.success(
        //             this.$t('操作成功 {count}/{total} 项', {
        //                 count: res.map((x) => x.data ?? 0).reduce((a, b) => a + b, 0),
        //                 total: this.selection.length,
        //             }),
        //         )
        //     } catch {
        //         //
        //     }
        //     this.$refs.table.refresh()
        //     loading?.close()
        // },
        // async changeSwitch(event, row) {
        //     try {
        //         await this.$API.adm_girlfriends.setEnabled.post(row)
        //         this.$message.success(this.$t('操作成功'))
        //     } catch {
        //         //
        //     }
        //     this.$refs.table.refresh()
        // },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
        //重置
        onReset() {
            // Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))
            // this.$refs.selectFilter.selected['enabled'] = [true]
        },
        //搜索
        onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime,
                })
            }

            this.$refs.table.upData()
        },
    },
    mounted() {
        console.log(this.$API.adm_girlfriends)
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keeps.push({
                field: 'keywords',
                value: this.keywords,
                type: 'root',
            })
        }
        this.onReset()
    },
    props: ['keywords'],
    watch: {},
}
</script>

<style scoped></style>