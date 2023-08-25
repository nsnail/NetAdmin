<template>
    <el-table-column :label="label" :min-width="minWidth" :prop="prop" align="center" sortable="custom">
        <template #default="scope">
            <template v-for="(item, i) in options" :key="i">
                <div v-if="scope.row[prop] === item.value" class="indicator">
                    <sc-status-indicator :pulse="item.pulse" :type="item.type" />
                    <span> {{ item.text }}</span>
                </div>
            </template>
        </template>
    </el-table-column>
</template>
<style scoped>
.indicator {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 0.5rem;
}
</style>
<script>
export default {
    emits: [],
    props: {
        minWidth: { type: Number, default: 80 },
        options: { type: Array },
        prop: { type: String },
        label: { type: String },
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