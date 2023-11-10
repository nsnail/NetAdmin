<template>
    <el-table ref="table" :data="columnData" border row-key="prop" style="width: 100%">
        <el-table-column :label="$t('排序')" prop="" width="60">
            <el-tag class="move" disable-transitions style="cursor: move">
                <el-icon style="cursor: move">
                    <el-icon-d-caret />
                </el-icon>
            </el-tag>
        </el-table-column>
        <el-table-column :label="$t('列名')" prop="label">
            <template #default="scope">
                <el-tag :effect="scope.row.hide ? 'light' : 'dark'" :type="scope.row.hide ? 'info' : ''" disable-transitions round
                    >{{ scope.row.label }}
                </el-tag>
            </template>
        </el-table-column>
        <el-table-column :label="$t('显示')" prop="hide" width="60">
            <template #default="scope">
                <el-switch v-model="scope.row.hide" :active-value="false" :inactive-value="true" size="small" />
            </template>
        </el-table-column>
    </el-table>
</template>

<script>
import Sortable from 'sortablejs'

export default {
    emits: ['success'],
    props: {
        column: { type: Array, default: () => [] },
    },
    data() {
        return {
            columnData: this.column,
        }
    },
    mounted() {
        this.rowDrop()
    },
    methods: {
        rowDrop() {
            const _this = this
            const tbody = this.$refs.table.$el.querySelector('.el-table__body-wrapper tbody')
            Sortable.create(tbody, {
                handle: '.move',
                animation: 200,
                ghostClass: 'ghost',
                onEnd({ newIndex, oldIndex }) {
                    const tableData = _this.columnData
                    const currRow = tableData.splice(oldIndex, 1)[0]
                    tableData.splice(newIndex, 0, currRow)
                },
            })
        },
    },
}
</script>