<template>
    <el-table-column align="right" fixed="right">
        <template #default="{ row }">
            <el-button-group>
                <template v-for="(item, i) in buttons?.filter((x) => !x.condition || x.condition(row))" :key="i">
                    <el-popconfirm
                        v-if="item.confirm"
                        :title="this.$t(`确定 {title}？`, { title: item.title })"
                        @confirm="click(item, row, vue)"
                        width="20rem">
                        <template #reference>
                            <el-button
                                :disabled="disabled"
                                :icon="item.icon"
                                :title="item.title ? $t(item.title) : ''"
                                :type="item.type"
                                @click.native.stop
                                size="small"></el-button>
                        </template>
                    </el-popconfirm>
                    <el-button
                        v-else
                        :disabled="disabled"
                        :icon="item.icon"
                        :title="item.title ? $t(item.title) : ''"
                        :type="item.type"
                        @click="click(item, row, vue)"
                        size="small">
                    </el-button>
                </template>
            </el-button-group>
        </template>
    </el-table-column>
</template>
<script>
import naColOperation from '@/config/naColOperation'

export default {
    emits: [],
    props: {
        vue: { type: Object },
        buttons: {
            type: Array,
            default: naColOperation.buttons,
        },
        prop: { type: String },
    },
    data() {
        return {
            disabled: false,
        }
    },
    mounted() {},
    created() {},
    components: {},
    computed: {},
    methods: {
        async click(item, row, vue) {
            this.disabled = true
            await item.click(row, vue)
            this.disabled = false
        },
    },
}
</script>
<style scoped></style>