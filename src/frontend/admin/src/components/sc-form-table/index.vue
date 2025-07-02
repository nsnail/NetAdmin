<template>
    <div class="sc-form-table" ref="scFormTable">
        <el-table :data="data" border ref="table" stripe>
            <el-table-column fixed="left" type="index" width="50">
                <template #header>
                    <el-button v-if="!hideAdd" @click="rowAdd" circle icon="el-icon-plus" size="small" type="primary" />
                </template>
                <template #default="scope">
                    <div :class="['sc-form-table-handle', { 'sc-form-table-handle-delete': !hideDelete }]">
                        <span>{{ scope.$index + 1 }}</span>
                        <el-button
                            v-if="!hideDelete"
                            @click="rowDel(scope.row, scope.$index)"
                            circle
                            icon="el-icon-delete"
                            plain
                            size="small"
                            type="danger" />
                    </div>
                </template>
            </el-table-column>
            <el-table-column v-if="dragSort" label="" width="50">
                <template #default>
                    <div class="move" style="cursor: move">
                        <el-icon-d-caret style="width: 1em; height: 1em" />
                    </div>
                </template>
            </el-table-column>
            <slot />
            <template #empty>
                {{ placeholder }}
            </template>
        </el-table>
    </div>
</template>

<script>
import Sortable from 'sortablejs'

export default {
    props: {
        modelValue: { type: Array, default: () => [] },
        addTemplate: {
            type: Object,
            default: () => {},
        },
        placeholder: { type: String, default: '暂无数据' },
        dragSort: { type: Boolean, default: false },
        hideAdd: { type: Boolean, default: false },
        hideDelete: { type: Boolean, default: false },
    },
    data() {
        return {
            data: [],
        }
    },
    mounted() {
        this.data = this.modelValue
        if (this.dragSort) {
            this.rowDrop()
        }
    },
    watch: {
        modelValue() {
            this.data = this.modelValue
        },
        data: {
            handler() {
                this.$emit('update:modelValue', this.data)
            },
            deep: true,
        },
    },
    methods: {
        rowDrop() {
            const _this = this
            const tbody = this.$refs.table.$el.querySelector('.el-table__body-wrapper tbody')
            Sortable.create(tbody, {
                handle: '.move',
                animation: 300,
                ghostClass: 'ghost',
                onEnd({ newIndex, oldIndex }) {
                    _this.data.splice(newIndex, 0, _this.data.splice(oldIndex, 1)[0])
                    const newArray = _this.data.slice(0)
                    const tmpHeight = _this.$refs.scFormTable.offsetHeight
                    _this.$refs.scFormTable.style.setProperty('height', tmpHeight + 'px')
                    _this.data = []
                    _this.$nextTick(() => {
                        _this.data = newArray
                        _this.$nextTick(() => {
                            _this.$refs.scFormTable.style.removeProperty('height')
                        })
                    })
                },
            })
        },
        rowAdd() {
            const temp = JSON.parse(JSON.stringify(this.addTemplate))
            this.data.push(temp)
        },
        rowDel(row, index) {
            this.data.splice(index, 1)
        },
        //插入行
        pushRow(row) {
            const temp = row || JSON.parse(JSON.stringify(this.addTemplate))
            this.data.push(temp)
        },
        //根据index删除
        deleteRow(index) {
            this.data.splice(index, 1)
        },
    },
}
</script>

<style scoped>
.sc-form-table {
    width: 100%;
}

.sc-form-table .sc-form-table-handle {
    text-align: center;
}

.sc-form-table .sc-form-table-handle span {
    display: inline-block;
}

.sc-form-table .sc-form-table-handle button {
    display: none;
}

.sc-form-table .hover-row .sc-form-table-handle-delete span {
    display: none;
}

.sc-form-table .hover-row .sc-form-table-handle-delete button {
    display: inline-block;
}

.sc-form-table .move {
    text-align: center;
    font-size: 1.1rem;
    margin-top: 0.2rem;
}
</style>