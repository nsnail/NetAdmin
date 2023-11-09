<template>
    <el-table-column align="right" :label="$t('操作')">
        <template #default="scope">
            <el-button-group>
                <template v-for="(item, i) in buttons?.filter((x) => !x.condition || x.condition(scope))" :key="i">
                    <el-popconfirm v-if="item.confirm" :title="`确定${item.title}吗？`" @confirm="item.click(scope.row, vue)">
                        <template #reference>
                            <el-button :icon="item.icon" :title="item.title" :type="item.type" size="small" @click.native.stop></el-button>
                        </template>
                    </el-popconfirm>
                    <el-button v-else :icon="item.icon" :title="item.title" size="small" @click="item.click(scope.row, vue)"
                        >{{ item.title }}
                    </el-button>
                </template>
            </el-button-group>
        </template>
    </el-table-column>
</template>
<style scoped>
.avatar {
    display: flex;
    gap: 0.5rem;
}
</style>
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
        return {}
    },
    mounted() {},
    created() {},
    components: {},
    computed: {},
    methods: {},
}
</script>