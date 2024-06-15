<template>
    <el-table-column v-bind:="$attrs">
        <template #default="{ row }">
            <na-indicator :data="row" :options="options" :prop="$attrs.prop" />
        </template>
    </el-table-column>
</template>
<script>
import naIndicator from '@/components/naIndicator/index.vue'
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