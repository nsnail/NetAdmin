<template>
    <el-table-column v-bind:="$attrs">
        <template #default="{ row }">
            <na-indicator :data="row" :options="options" :prop="$attrs.prop" />
            <slot :data="row" name="info" />
        </template>
    </el-table-column>
</template>
<script>
import naIndicator from '@/components/na-indicator'
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
    components: { naIndicator },
    computed: {},
    methods: {},
}
</script>