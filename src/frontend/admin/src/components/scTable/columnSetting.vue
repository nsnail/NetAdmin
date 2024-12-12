<template>
    <div v-if="usercolumn.length > 0" v-loading="isSave" class="setting-column">
        <div class="setting-column__title">
            <span class="move_b"></span>
            <span class="show_b">{{ $t('显示') }}</span>
            <span class="name_b">{{ $t('名称') }}</span>
            <span class="width_b">{{ $t('宽度') }}</span>
            <span class="sortable_b">{{ $t('排序') }}</span>
            <span class="fixed_b">{{ $t('固定') }}</span>
        </div>
        <div class="setting-column__list" ref="list">
            <ul>
                <li v-for="item in usercolumn" :key="item.prop">
                    <span class="move_b">
                        <el-tag class="move" style="cursor: move"><el-icon-d-caret style="width: 1em; height: 1em" /></el-tag>
                    </span>
                    <span class="show_b">
                        <el-switch v-model="item.hide" :active-value="false" :inactive-value="true"></el-switch>
                    </span>
                    <span :title="item.prop" class="name_b">{{ item.label }}</span>
                    <span class="width_b">
                        <el-input v-model="item.width" placeholder="auto" size="small"></el-input>
                    </span>
                    <span class="sortable_b">
                        <el-switch v-model="item.sortable"></el-switch>
                    </span>
                    <span class="fixed_b">
                        <el-switch v-model="item.fixed"></el-switch>
                    </span>
                </li>
            </ul>
        </div>
        <div class="setting-column__bottom">
            <el-button :disabled="isSave" @click="backDefaul">{{ $t('重置') }}</el-button>
            <el-button @click="save" type="primary">{{ $t('保存') }}</el-button>
        </div>
    </div>
    <el-empty v-else :description="$t('暂无可配置的列')" :image-size="80"></el-empty>
</template>

<script>
import Sortable from 'sortablejs'

export default {
    components: {
        Sortable,
    },
    props: {
        column: {
            type: Object,
            default: () => {},
        },
    },
    data() {
        return {
            isSave: false,
            usercolumn: JSON.parse(JSON.stringify(this.column || [])),
        }
    },
    watch: {
        usercolumn: {
            handler() {
                this.$emit('userChange', this.usercolumn)
            },
            deep: true,
        },
    },
    mounted() {
        this.usercolumn.length > 0 && this.rowDrop()
    },
    methods: {
        rowDrop() {
            const _this = this
            const tbody = this.$refs.list.querySelector('ul')
            Sortable.create(tbody, {
                handle: '.move',
                animation: 300,
                ghostClass: 'ghost',
                onEnd({ newIndex, oldIndex }) {
                    const tableData = _this.usercolumn
                    const currRow = tableData.splice(oldIndex, 1)[0]
                    tableData.splice(newIndex, 0, currRow)
                },
            })
        },
        backDefaul() {
            this.$emit('back', this.usercolumn)
        },
        save() {
            this.$emit('save', this.usercolumn)
        },
    },
}
</script>

<style scoped>
.setting-column__title {
    border-bottom: 1px solid var(--el-border-color-lighter);
    padding-bottom: 1rem;
}

.setting-column__title span {
    display: inline-block;
    font-weight: bold;
    color: var(--el-text-color-secondary);
    font-size: 0.9rem;
}

.setting-column__title span.move_b {
    width: 2.5rem;
    margin-right: 1rem;
}

.setting-column__title span.show_b {
    width: 5rem;
}

.setting-column__title span.name_b {
    width: 10rem;
}

.setting-column__title span.width_b {
    width: 5rem;
    margin-right: 1rem;
}

.setting-column__title span.sortable_b {
    width: 5rem;
}

.setting-column__title span.fixed_b {
    width: 5rem;
}

.setting-column__list {
    max-height: 25rem;
    overflow: auto;
}

.setting-column__list li {
    list-style: none;
    margin: 1rem 0;
    display: flex;
    align-items: center;
}

.setting-column__list li > span {
    display: inline-block;
    font-size: 0.9rem;
}

.setting-column__list li span.move_b {
    width: 2.5rem;
    margin-right: 1rem;
}

.setting-column__list li span.show_b {
    width: 5rem;
}

.setting-column__list li span.name_b {
    width: 10rem;
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
    cursor: default;
}

.setting-column__list li span.width_b {
    width: 5rem;
    margin-right: 1rem;
}

.setting-column__list li span.sortable_b {
    width: 5rem;
}

.setting-column__list li span.fixed_b {
    width: 5rem;
}

.setting-column__list li.ghost {
    opacity: 0.3;
}

.setting-column__bottom {
    border-top: 1px solid var(--el-border-color-lighter);
    padding-top: 1rem;
    text-align: right;
}

.dark .setting-column__title {
    border-color: var(--el-border-color-light);
}

.dark .setting-column__bottom {
    border-color: var(--el-border-color-light);
}
</style>