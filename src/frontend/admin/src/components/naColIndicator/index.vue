<template>
    <el-table-column v-bind:="$attrs">
        <template #default="{ row }">
            <template v-for="(item, i) in options" :key="i">
                <div v-if="tool.getNestedProperty(row, this.$attrs.prop) === item.value">
                    <sc-status-indicator
                        :pulse="item.pulse"
                        :style="item.type ? '' : `background: #${Math.abs(this.$TOOL.crypto.hashCode(item.value)).toString(16).substring(0, 6)}`"
                        :type="item.type" />
                    <span v-if="!$slots.default">&nbsp;{{ item.text }}</span>
                    <slot :row="row" :text="item.text"></slot>
                </div>
            </template>
            <slot :row="row" name="info"></slot>
        </template>
    </el-table-column>
</template>
<script>
import tool from '@/utils/tool'

export default {
    emits: [],
    props: {
        options: { type: Array },
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