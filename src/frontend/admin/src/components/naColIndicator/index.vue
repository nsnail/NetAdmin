<template>
    <el-table-column :label="label" :min-width="minWidth" :prop="prop" :sortable="customSort ? `custom` : true" align="center">
        <template #default="scope">
            <template v-for="(item, i) in options" :key="i">
                <div v-if="tool.getNestedProperty(scope.row, prop) === item.value" class="indicator">
                    <sc-status-indicator
                        :pulse="item.pulse"
                        :style="item.type ? '' : `background: #${Math.abs(this.$TOOL.crypto.hashCode(item.value)).toString(16).substring(0, 6)}`"
                        :type="item.type" />
                    <span v-if="!$slots.default"> {{ item.text }}</span>
                    <slot :row="scope.row" :text="item.text"></slot>
                </div>
            </template>
            <slot :row="scope.row" name="info"></slot>
        </template>
    </el-table-column>
</template>
<script>
import tool from '@/utils/tool'

export default {
    emits: [],
    props: {
        minWidth: { type: Number, default: 80 },
        options: { type: Array },
        prop: { type: String },
        label: { type: String },
        customSort: { type: Boolean, default: true },
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