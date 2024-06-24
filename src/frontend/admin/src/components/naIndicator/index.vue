<template>
    <template v-for="(item, i) in options" :key="i">
        <div v-if="this.$TOOL.getNestedProperty(data, this.prop)?.toLowerCase() === item.value?.toLowerCase()">
            <sc-status-indicator
                :pulse="item.pulse"
                :style="item.type ? '' : `background: #${Math.abs(this.$TOOL.crypto.hashCode(item.value)).toString(16).substring(0, 6)}`"
                :type="item.type" />
            <span v-if="!$slots.default">&nbsp;{{ item.text }}</span>
            <slot :data="data" :text="item.text"></slot>
        </div>
    </template>
</template>
<script>
export default {
    emits: [],
    props: {
        options: { type: Array },
        data: { type: Object },
        prop: { type: String },
    },
    data() {
        return {}
    },
    mounted() {},
    created() {},
    components: {},
    computed: {},
    methods: {},
}
</script>