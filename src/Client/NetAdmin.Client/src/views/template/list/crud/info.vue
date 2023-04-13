<template>
    <el-main style="padding: 0 20px">
        <el-card shadow="never">
            <el-descriptions :column="2" border title="Table row data">
                <el-descriptions-item
                    v-for="(val, key) in data"
                    :key="key"
                    :label="key"
                    width="150px"
                    >{{ val }}
                </el-descriptions-item>
            </el-descriptions>
        </el-card>
    </el-main>
</template>

<script>
export default {
    data() {
        return {
            data: {
                id: "",
            },
        };
    },
    mounted() {},
    methods: {
        //注入数据
        setData(data) {
            this.data = data;
        },
    },
};
</script>

<style></style>