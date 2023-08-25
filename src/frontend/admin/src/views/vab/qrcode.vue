<!--
 * @Author: Xujianchen
 * @Date: 2023-02-28 14:12:15
 * @LastEditors: Xujianchen
 * @LastEditTime: 2023-03-19 12:02:11
 * @Description:
-->
<template>
    <el-main>
        <el-row :gutter="15">
            <el-col :lg="8">
                <el-card header="常用" shadow="never">
                    <sc-qr-code text="scui"></sc-qr-code>
                </el-card>
            </el-col>
            <el-col :lg="8">
                <el-card header="带Logo" shadow="never">
                    <sc-qr-code logo="@/assets/img/logo.png" text="scui"></sc-qr-code>
                </el-card>
            </el-col>
            <el-col :lg="8">
                <el-card header="自定义颜色大小" shadow="never">
                    <sc-qr-code :size="100" colorDark="#088200" colorLight="#fff" text="scui"></sc-qr-code>
                </el-card>
            </el-col>
            <el-col :lg="8">
                <el-card header="动态" shadow="never">
                    <sc-qr-code :text="qrcode"></sc-qr-code>
                    <el-input v-model="qrcode" placeholder="Please input" style="margin-top: 20px" />
                </el-card>
            </el-col>
        </el-row>
    </el-main>
</template>

<script>
export default {
    name: 'qrcode',
    data() {
        return {
            qrcode: 'scui',
        }
    },
    mounted() {},
    methods: {},
}
</script>

<style></style>