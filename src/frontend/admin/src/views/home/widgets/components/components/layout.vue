<template>
    <div :class="[{ active: active }]" @click="$emit('onSetLayout', realLayout)" class="selectLayout-item">
        <el-row v-for="l in layout.split('-')" :gutter="2">
            <el-col v-for="span in l.split(',')" :span="Number(span) > 24 ? 24 : Number(span)"><span></span></el-col>
        </el-row>
    </div>
</template>

<script>
export default {
    components: {},
    props: ['active', 'layout'],
    data() {
        return {}
    },
    computed: {
        realLayout() {
            return this.layout
                .replaceAll('-', ',')
                .split(',')
                .map((x) => Number(x))
        },
    },
    async created() {},
    mounted() {},
}
</script>
<style lang="scss" scoped>
.selectLayout-item {
    width: 5rem;
    height: 5rem;
    border: 0.2rem solid var(--el-border-color-light);
    padding: 0.4rem;
    cursor: pointer;
    margin-right: 1rem;
    display: flex;
    flex-direction: column;
}

.selectLayout-item .el-row {
    height: 3.6rem;
    margin-bottom: 0.2rem;
}

.selectLayout-item span {
    display: block;
    background: var(--el-border-color-light);
}

.selectLayout-item:has(:only-child) span {
    height: 100%;
}

.selectLayout-item:has(:nth-child(2)):not(:has(:nth-child(3))) > span {
    height: 50%;
}

.selectLayout-item:has(:nth-child(3)):not(:has(:nth-child(4))) > span {
    height: 33.3%;
}

.selectLayout-item:has(:nth-child(4)):not(:has(:nth-child(5))) > span {
    height: 25%;
}

.selectLayout-item:has(:nth-child(5)):not(:has(:nth-child(6))) > span {
    height: 20%;
}

.selectLayout-item:has(:nth-child(6)):not(:has(:nth-child(7))) > span {
    height: 16.7%;
}

.selectLayout-item:has(:nth-child(7)):not(:has(:nth-child(8))) > span {
    height: 14.3%;
}

.selectLayout-item:has(:nth-child(8)):not(:has(:nth-child(9))) > span {
    height: 12.5%;
}

.selectLayout-item:has(:nth-child(9)):not(:has(:nth-child(10))) > span {
    height: 11.1%;
}

.selectLayout-item:hover {
    border-color: var(--na-color-primary);
}

.selectLayout-item.active {
    border-color: var(--na-color-primary);
}

.selectLayout-item.active span {
    background: var(--na-color-primary);
}
</style>