<template>
    <el-table-column :label="$t('操作')" align="right">
        <template #default="scope">
            <el-button-group>
                <template v-for="(item, i) in buttons?.filter((x) => !x.condition || x.condition(scope))" :key="i">
                    <el-popconfirm v-if="item.confirm" :title="`确定${item.title}吗？`" @confirm="item.click(scope.row, vue)">
                        <template #reference>
                            <el-button :icon="item.icon" :title="item.title" :type="item.type" @click.native.stop size="small"></el-button>
                        </template>
                    </el-popconfirm>
                    <el-button v-else :icon="item.icon" :title="item.title" @click="item.click(scope.row, vue)" size="small"
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