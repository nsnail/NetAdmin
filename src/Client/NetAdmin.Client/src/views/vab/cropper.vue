<!--
 * @Description: 图像剪裁组件演示文件
 * @version: 1.0
 * @Author: sakuya
 * @Date: 2021年7月24日20:58:51
 * @LastEditors:
 * @LastEditTime:
-->

<template>
    <el-main>
        <el-row :gutter="15">
            <el-col :lg="14">
                <el-card shadow="never">
                    <sc-cropper
                        ref="cropper"
                        :aspectRatio="aspectRatio"
                        :compress="compress"
                        :src="cropperImg"
                    ></sc-cropper>
                </el-card>
                <el-card header="参数和方法" shadow="never">
                    <el-form label-width="100px">
                        <el-form-item label="aspectRatio">
                            <el-select
                                v-model="aspectRatio"
                                placeholder="请选择"
                            >
                                <el-option :value="0" label="自由"></el-option>
                                <el-option :value="1" label="1:1"></el-option>
                                <el-option
                                    :value="4 / 3"
                                    label="4:3"
                                ></el-option>
                                <el-option
                                    :value="16 / 9"
                                    label="16:9"
                                ></el-option>
                            </el-select>
                            <div class="el-form-item-msg">
                                固定选区或者不固定
                            </div>
                        </el-form-item>
                        <el-form-item label="compress">
                            <el-select v-model="compress" placeholder="请选择">
                                <el-option :value="0.1" label="0.1"></el-option>
                                <el-option :value="0.5" label="0.5"></el-option>
                                <el-option :value="1" label="1"></el-option>
                            </el-select>
                            <div class="el-form-item-msg">
                                图像压缩率，值为：0.1-1
                            </div>
                        </el-form-item>
                    </el-form>
                    <el-button plain type="primary" @click="getBase64"
                        >Base64
                    </el-button>
                    <el-button plain type="primary" @click="getBlob"
                        >Blob
                    </el-button>
                    <el-button plain type="primary" @click="getFile"
                        >File
                    </el-button>
                </el-card>
                <el-card header="方法结果" shadow="never">
                    <img :src="imgData" />
                </el-card>
            </el-col>
            <el-col :lg="10">
                <el-card header="已内置剪裁的上传组件" shadow="never">
                    <el-alert
                        style="margin-bottom: 20px"
                        title="设置cropper就可以开启上传前剪裁, 并已封装compress和aspectRatio, 打开F12查看网络请求"
                        type="success"
                    ></el-alert>
                    <sc-upload
                        v-model="uploadImg"
                        :aspectRatio="1"
                        :compress="1"
                        :cropper="true"
                        title="开启剪裁"
                    ></sc-upload>
                </el-card>
            </el-col>
        </el-row>
    </el-main>
</template>

<script>
import scCropper from "@/components/scCropper";

export default {
    name: "cropper",
    components: {
        scCropper,
    },
    data() {
        return {
            cropperImg: "img/avatar.jpg",
            compress: 0.5,
            aspectRatio: 0,
            uploadImg: "",
            imgData: "",
        };
    },
    methods: {
        getBase64() {
            this.$refs.cropper.getCropData((data) => {
                this.imgData = data;
            });
        },
        getBlob() {
            this.$refs.cropper.getCropBlob((blob) => {
                this.imgData = URL.createObjectURL(blob);
            });
        },
        getFile() {
            this.$refs.cropper.getCropFile(
                (file) => {
                    let aTag = document.createElement("a");
                    aTag.download = file.name;
                    aTag.href = URL.createObjectURL(file);
                    aTag.click();
                },
                "fileName.jpg",
                "image/jpeg"
            );
        },
    },
};
</script>

<style></style>