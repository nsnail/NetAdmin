<!--
 * @Author: Xujianchen
 * @Date: 2023-02-28 14:12:15
 * @LastEditors: Xujianchen
 * @LastEditTime: 2023-03-19 12:01:45
 * @Description:
-->
<template>
    <el-main>
        <el-row :gutter="15">
            <el-col :lg="24">
                <el-card class="aboutTop" shadow="never">
                    <div class="aboutTop-info">
                        <img src="@/assets/img/logo.png" />
                        <h2>{{ data.name }}</h2>
                        <p>{{ data.version }}</p>
                    </div>
                </el-card>
                <el-card header="dependencies" shadow="never">
                    <el-descriptions :column="3" border>
                        <el-descriptions-item v-for="(value, key) in data.dependencies" :key="key" :label="key">{{ value }} </el-descriptions-item>
                    </el-descriptions>
                </el-card>
                <el-card header="devDependencies" shadow="never">
                    <el-descriptions :column="3" border>
                        <el-descriptions-item v-for="(value, key) in data.devDependencies" :key="key" :label="key">{{ value }} </el-descriptions-item>
                    </el-descriptions>
                </el-card>
            </el-col>
        </el-row>
    </el-main>
</template>

<script>
import packageJson from '../../../package.json'

export default {
    name: 'about',
    data() {
        return {
            data: packageJson,
        }
    },
    mounted() {},
    methods: {},
}
</script>

<style scoped>
.aboutTop {
    border: 0;
    background: linear-gradient(to right, #8e54e9, #4776e6);
    color: #fff;
}

.aboutTop-info {
    text-align: center;
}

.aboutTop-info img {
    width: 100px;
}

.aboutTop-info h2 {
    font-size: 26px;
    margin-top: 15px;
}

.aboutTop-info p {
    font-size: 16px;
    margin-top: 10px;
}
</style>