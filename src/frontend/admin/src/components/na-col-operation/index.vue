<template>
    <el-table-column align="right" fixed="right">
        <template #default="{ row }">
            <template v-if="showMenu">
                <el-dropdown>
                    <el-button icon="el-icon-setting" size="small"> {{ $t('操作') }} </el-button>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <template v-for="(item, i) in buttons?.filter((x) => !x.condition || x.condition(row))" :key="i">
                                <el-dropdown-item
                                    :disabled="disabled"
                                    :icon="item.icon"
                                    :title="item.title ? $t(item.title) : ''"
                                    :type="item.type"
                                    @click="click(item, row, vue)"
                                    size="small"
                                    >{{ item.title }}
                                </el-dropdown-item>
                            </template>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </template>
            <template v-else>
                <el-button-group>
                    <template v-for="(item, i) in buttons?.filter((x) => !x.condition || x.condition(row))" :key="i">
                        <el-button
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
        </template>
    </el-table-column>
</template>
<script>
import naColOperation from '@/config/na-col-operation'

export default {
    emits: [],
    props: {
        vue: { type: Object },
        buttons: {
            type: Array,
            default: naColOperation.buttons,
        },
        prop: { type: String },
        showMenu: { type: Boolean },
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

            if (item.confirm) {
                try {
                    await this.$confirm(this.$t(`确定 {title}？`, { title: item.title }), this.$t('提示'), {
                        type: 'warning',
                    })
                } catch {
                    //

                    this.disabled = false
                    return
                }
            }
            await item.click(row, vue)
            this.disabled = false
        },
    },
}
</script>
<style scoped />