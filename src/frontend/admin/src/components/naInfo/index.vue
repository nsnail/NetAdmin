<template>
    <el-drawer v-model="visible" :size="size" :title="title" @closed="$emit('closed')" destroy-on-close>
        <el-main>
            <el-descriptions :column="1" border size="small">
                <el-descriptions-item v-for="(item, i) in data" :key="i" :label="i" label-class-name="w15">
                    {{ item }}
                </el-descriptions-item>
            </el-descriptions>
        </el-main>
    </el-drawer>
</template>

<script>
export default {
    emits: ['success', 'closed'],
    props: {
        size: { type: String, default: '80%' },
    },
    data() {
        return {
            title: '',
            visible: false,
            data: {},
        }
    },
    methods: {
        open(data, title) {
            this.title = title
            this.data = data
            this.visible = true
            return this
        },
    },
}
</script>
<style scoped></style>