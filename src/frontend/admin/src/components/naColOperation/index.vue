<template>
    <el-table-column align="right">
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
                                v-loading="loading"
                                :icon="item.icon"
                                :title="item.title"
                                :type="item.type"
                                @click.native.stop
                                size="small"></el-button>
                        </template>
                    </el-popconfirm>
                    <el-button v-else v-loading="loading" :icon="item.icon" :title="item.title" @click="click(item, row, vue)" size="small"
                        >{{ item.title }}
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
            loading: false,
        }
    },
    mounted() {},
    created() {},
    components: {},
    computed: {},
    methods: {
        async click(item, row, vue) {
            this.loading = true
            await item.click(row, vue)
            this.loading = false
        },
    },
}
</script>
<style scoped></style>