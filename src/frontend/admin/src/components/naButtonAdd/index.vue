<template>
    <el-button @click="add" icon="el-icon-plus" type="primary"></el-button>
</template>
<style scoped></style>
<script>
export default {
    emits: [],
    props: { vue: { type: Object }, data: { type: Object } },
    data() {
        return {}
    },
    mounted() {},
    created() {},
    components: {},
    computed: {},
    methods: {
        //添加
        async add() {
            this.vue.dialog.save = true
            await this.vue.$nextTick()
            this.vue.$refs.saveDialog.open('add', this.data)
        },
    },
}
</script>