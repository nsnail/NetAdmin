<!--
 * @Description: scDialog 弹窗扩展演示文件
 * @version: 1.0
 * @Author: sakuya
 * @Date: 2021年8月27日08:56:30
 * @LastEditors:
 * @LastEditTime:
-->

<template>
    <el-main>
        <el-alert
            style="margin: 0 0 20px 0"
            title="二次封装el-dialog,加入加载中/最大化."
            type="success"
        ></el-alert>
        <el-card header="内置" shadow="never">
            <el-button type="primary" @click="open1">默认</el-button>
            <el-button type="primary" @click="open2">加载</el-button>
            <el-button type="primary" @click="open3"
                >禁止拖拽最大化和关闭</el-button
            >
        </el-card>
        <el-card header="异步" shadow="never" style="margin-top: 15px">
            <el-button type="primary" @click="asyn1">异步加载1</el-button>
            <el-button type="primary" @click="asyn2">异步加载2</el-button>
            <el-alert
                style="margin-top: 20px"
                title="适用于页面有很多弹窗操作,利用异步组件按需加载,加快首屏的加载速度和打包体积"
            ></el-alert>
        </el-card>
    </el-main>

    <sc-dialog v-model="dialog1" draggable title="提示">
        内容
        <template #footer>
            <el-button @click="dialog1 = false">取 消</el-button>
            <el-button type="primary" @click="dialog1 = false">确 定</el-button>
        </template>
    </sc-dialog>

    <sc-dialog
        v-model="dialog2"
        :loading="dialog2Loading"
        :width="400"
        draggable
        title="模拟加载"
    >
        <el-empty :image-size="80" description="NO Data"></el-empty>
        <template #footer>
            <el-button @click="dialog2 = false">取 消</el-button>
            <el-button
                :loading="dialog2Loading"
                type="primary"
                @click="dialog2 = false"
                >确 定</el-button
            >
        </template>
    </sc-dialog>

    <sc-dialog
        v-model="dialog3"
        :show-close="false"
        :show-fullscreen="false"
        title="禁用拖拽"
    >
        内容
        <template #footer>
            <el-button @click="dialog3 = false">取 消</el-button>
            <el-button type="primary" @click="dialog3 = false">确 定</el-button>
        </template>
    </sc-dialog>

    <dialog1
        v-if="asynDialog1"
        draggable
        @closed="asynDialog1 = false"
    ></dialog1>
    <dialog2
        v-if="asynDialog2"
        draggable
        @closed="asynDialog2 = false"
    ></dialog2>
</template>

<script>
import { defineAsyncComponent } from "vue";

export default {
    name: "dialogExtend",
    components: {
        dialog1: defineAsyncComponent(() => import("./dialog1")),
        dialog2: defineAsyncComponent(() => import("./dialog2")),
    },
    data() {
        return {
            dialog1: false,
            dialog2: false,
            dialog3: false,
            dialog2Loading: false,
            asynDialog1: false,
            asynDialog2: false,
        };
    },
    mounted() {},
    methods: {
        open1() {
            this.dialog1 = true;
        },
        open2() {
            this.dialog2 = true;
            this.dialog2Loading = true;
            setTimeout(() => {
                this.dialog2Loading = false;
            }, 1000);
        },
        open3() {
            this.dialog3 = true;
        },
        asyn1() {
            this.asynDialog1 = true;
        },
        asyn2() {
            this.asynDialog2 = true;
        },
    },
};
</script>

<style></style>