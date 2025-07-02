<template>
    <el-table-column :label="label" :prop="`${prop}.${field}`">
        <template #default="{ row }">
            <div class="flex">
                <template
                    v-for="(item, i) in Array.isArray(tool.getNestedProperty(row, prop))
                        ? tool.getNestedProperty(row, prop)
                        : [tool.getNestedProperty(row, prop)]"
                    :key="i">
                    <el-tag
                        v-if="item"
                        :type="['success', 'danger', 'info', 'primary', 'warning'][$TOOL.crypto.hashCode(item[field]) % 5]"
                        @click="$emit('click', item)">
                        {{ item ? item[field] : '' }}
                    </el-tag>
                </template>
            </div>
        </template>
    </el-table-column>
</template>
<script>
import tool from '@/utils/tool'

export default {
    emits: ['click'],
    props: {
        prop: { type: String },
        field: { type: String },
        label: { type: String },
    },
    data() {
        return {}
    },
    mounted() {},
    created() {},
    components: {},
    computed: {
        tool() {
            return tool
        },
    },
    methods: {},
}
</script>
<style scoped>
.el-tag {
    cursor: pointer;
}
</style>