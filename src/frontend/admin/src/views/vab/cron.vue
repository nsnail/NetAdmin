<template>
    <el-main>
        <el-alert
            style="margin-bottom: 20px"
            title="NetAdmin独创, 区别于市面上的规则生成器, 对不是很理解Cron的操作员也可以选择设置的快捷录入"
            type="success"></el-alert>
        <el-row :gutter="15">
            <el-col :lg="12">
                <el-card header="默认" shadow="never">
                    <sc-cron v-model="v1" clearable placeholder="请输入Cron定时规则"></sc-cron>
                </el-card>
            </el-col>
            <el-col :lg="12">
                <el-card header="快捷扩展" shadow="never">
                    <sc-cron v-model="v2" :shortcuts="shortcuts" clearable placeholder="请输入Cron定时规则"></sc-cron>
                </el-card>
            </el-col>
        </el-row>
    </el-main>
</template>

<script>
import scCron from '@/components/scCron'

export default {
    name: 'cron',
    components: {
        scCron,
    },
    data() {
        return {
            v1: '',
            v2: '',
            shortcuts: [
                {
                    text: '每天8点和12点 (自定义追加)',
                    value: '0 0 8,12 * * ?',
                },
            ],
        }
    },
    mounted() {},
    methods: {},
}
</script>