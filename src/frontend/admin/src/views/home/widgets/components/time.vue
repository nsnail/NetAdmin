<template>
    <el-card :header="$t('时钟')" class="item-background" shadow="hover">
        <div class="time">
            <h2>{{ time }}</h2>
            <p>{{ day }}</p>
        </div>
    </el-card>
</template>

<script>
export default {
    title: '时钟',
    icon: 'el-icon-clock',
    description: '演示部件效果',
    data() {
        return {
            time: '',
            day: '',
        }
    },
    mounted() {
        this.showTime()
        setInterval(() => {
            this.showTime()
        }, 1000)
    },
    methods: {
        showTime() {
            this.time = this.$TOOL.dateFormat(new Date(), 'hh:mm:ss')
            this.day = this.$TOOL.dateFormat(new Date(), 'yyyy年MM月dd日')
        },
    },
}
</script>

<style scoped>
.item-background {
    background: linear-gradient(to right, rgb(66, 76, 80), #ccc);
    color: #fff;
}

.time h2 {
    font-size: 3rem;
}

.time p {
    font-size: 1.1rem;
    margin-top: 1rem;
    opacity: 0.7;
}
</style>