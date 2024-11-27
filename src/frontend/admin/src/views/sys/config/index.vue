<template>
    <el-container>
        <el-header v-loading="total === '...'" style="height: auto; padding: 1rem 1rem 0 1rem; display: block">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <sc-statistic :value="total" group-separator title="总数"></sc-statistic>
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
        <el-header style="height: auto; padding: 0 1rem">
            <sc-select-filter
                :data="[
                    {
                        title: $t('启用状态'),
                        key: 'enabled',
                        options: [
                            { label: $t('全部'), value: '' },
                            { label: $t('启用'), value: true },
                            { label: $t('禁用'), value: false },
                        ],
                    },
                ]"
                :label-width="10"
                @on-change="filterChange"
                ref="selectFilter"></sc-select-filter>
        </el-header>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[]"
                    :vue="this"
                    @reset="onReset"
                    @search="onSearch"
                    dateFormat="YYYY-MM-DD HH:mm:ss"
                    dateType="datetimerange"
                    dateValueFormat="YYYY-MM-DD HH:mm:ss"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-button @click="(this.dialog.save = { mode: 'add' })" icon="el-icon-plus" type="primary"></el-button>
                <na-button-bulk-del :api="$API.sys_config.bulkDelete" :vue="this" />
                <el-dropdown v-show="this.selection.length > 0">
                    <el-button type="primary">
                        {{ $t('批量操作') }}
                        <el-icon>
                            <el-icon-arrow-down />
                        </el-icon>
                    </el-button>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item @click="setEnabled(true)">{{ $t('启用配置') }}</el-dropdown-item>
                            <el-dropdown-item @click="setEnabled(false)">{{ $t('禁用配置') }}</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-header>
        <el-main class="nopadding">
            <sc-table
                :context-menus="['id', 'userRegisterConfirm', 'userRegisterDept.name', 'userRegisterRole.name', 'enabled', 'createdTime']"
                :export-api="$API.sys_config.export"
                :on-command="this.getStatistics"
                :params="query"
                :query-api="$API.sys_config.pagedQuery"
                :vue="this"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                ref="table"
                row-key="id"
                stripe>
                <el-table-column type="selection" />
                <na-col-id :label="$t('配置编号')" prop="id" width="170" />
                <el-table-column :label="$t('用户注册')" align="center">
                    <el-table-column :label="$t('默认部门')" align="center" prop="userRegisterDept.name" width="150" />
                    <el-table-column :label="$t('默认角色')" align="center" prop="userRegisterRole.name" width="150" />
                    <el-table-column :label="$t('人工审核')" align="center" prop="userRegisterConfirm" width="120">
                        <template #default="{ row }">
                            <el-switch v-model="row.userRegisterConfirm" @change="changeSwitch($event, row)"></el-switch>
                        </template>
                    </el-table-column>
                </el-table-column>
                <el-table-column :label="$t('启用')" align="center" prop="enabled" sortable="custom" width="100">
                    <template #default="{ row }">
                        <el-switch v-model="row.enabled" @change="changeSwitch($event, row)"></el-switch>
                    </template>
                </el-table-column>
                <na-col-operation
                    :buttons="
                        naColOperation.buttons.concat({
                            icon: 'el-icon-delete',
                            confirm: true,
                            title: '删除部门',
                            click: rowDel,
                            type: 'danger',
                        })
                    "
                    :vue="this" />
            </sc-table>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="(dialog.save = null)"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => $refs.table.upData()"
        ref="saveDialog"></save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'

const saveDialog = defineAsyncComponent(() => import('./save.vue'))
export default {
    components: {
        saveDialog,
    },
    computed: {
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    created() {},
    data() {
        return {
            total: '...',
            dialog: {},
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [
                        {
                            field: 'enabled',
                            operator: 'eq',
                            value: true,
                        },
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
        async getStatistics() {
            const res = await this.$API.sys_config.count.post(this.query)
            this.total = res.data
        },
        async setEnabled(enabled) {
            let loading
            try {
                await this.$confirm(
                    this.$t('确定要 {operator} 选中的 {count} 项吗？', {
                        operator: enabled ? this.$t('启用') : this.$t('禁用'),
                        count: this.selection.length,
                    }),
                    this.$t('提示'),
                    {
                        type: 'warning',
                    },
                )
                loading = this.$loading()
                const res = await Promise.all(this.selection.map((x) => this.$API.sys_config.setEnabled.post(Object.assign(x, { enabled: enabled }))))
                this.$message.success(
                    this.$t('操作成功 {count}/{total} 项', {
                        count: res.map((x) => x.data ?? 0).reduce((a, b) => a + b, 0),
                        total: this.selection.length,
                    }),
                )
            } catch {
                //
            }
            this.$refs.table.refresh()
            loading?.close()
        },
        async changeSwitch(event, row) {
            try {
                await this.$API.sys_config.edit.post(row)
                this.$message.success(this.$t('操作成功'))
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        filterChange(data) {
            Object.entries(data).forEach(([key, value]) => {
                this.$refs.search.form.dy[key] = value === 'true' ? true : value === 'false' ? false : value
            })
            this.$refs.search.search()
        },
        async rowDel(row) {
            try {
                const res = await this.$API.sys_config.delete.post({ id: row.id })
                this.$message.success(this.$t('删除 {count} 项', { count: res.data }))
            } catch {
                //
            }
            this.$refs.table.refresh()
        },
        //重置
        onReset() {
            Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))
            this.$refs.selectFilter.selected['enabled'] = [true]
        },
        //搜索
        async onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime.map((x) => x.replace(/ 00:00:00$/, '')),
                })
            }

            if (typeof form.dy.enabled === 'boolean') {
                this.query.dynamicFilter.filters.push({
                    field: 'enabled',
                    operator: 'eq',
                    value: form.dy.enabled,
                })
            }

            this.$refs.table.upData()
            await this.getStatistics()
        },
    },
    async mounted() {
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keeps.push({
                field: 'keywords',
                value: this.keywords,
                type: 'root',
            })
        }

        this.$refs.search.form.dy.enabled = true
        this.$refs.search.keeps.push({
            field: 'enabled',
            value: true,
            type: 'dy',
        })
        this.onReset()
        await this.getStatistics()
    },
    props: ['keywords'],
    watch: {},
}
</script>

<style scoped></style>