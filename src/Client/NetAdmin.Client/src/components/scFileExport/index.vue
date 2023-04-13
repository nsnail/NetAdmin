<!--
 * @Description: 文件导出
 * @version: 1.1
 * @Author: sakuya
 * @Date: 2022年5月24日16:20:12
 * @LastEditors: sakuya
 * @LastEditTime: 2022年6月13日17:32:05
-->

<template>
    <slot :open="open">
        <el-button plain type="primary" @click="open">导出</el-button>
    </slot>
    <el-drawer
        v-model="dialog"
        :size="400"
        append-to-body
        destroy-on-close
        direction="rtl"
        title="导出"
    >
        <el-main style="padding: 0 20px 20px 20px">
            <div v-loading="downLoading" element-loading-text="正在处理中...">
                <div
                    v-if="downLoading && progress"
                    style="
                        position: absolute;
                        width: 100%;
                        height: 100%;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        z-index: 3000;
                    "
                >
                    <el-progress
                        :percentage="downLoadProgress"
                        :stroke-width="20"
                        :text-inside="true"
                        style="width: 100%; margin-bottom: 120px"
                    />
                </div>
                <el-tabs>
                    <el-tab-pane label="常规" lazy>
                        <el-form
                            label-position="left"
                            label-width="100px"
                            style="margin: 10px 0 20px 0"
                        >
                            <el-form-item label="文件名">
                                <el-input
                                    v-model="formData.fileName"
                                    placeholder="请输入文件名"
                                />
                            </el-form-item>
                            <el-form-item label="文件类型">
                                <el-select
                                    v-model="formData.fileType"
                                    placeholder="请选择文件类型"
                                >
                                    <el-option
                                        v-for="item in fileTypes"
                                        :key="item"
                                        :label="'*.' + item"
                                        :value="item"
                                    />
                                </el-select>
                            </el-form-item>
                            <slot :formData="formData" name="form"></slot>
                        </el-form>
                        <el-button
                            v-if="async"
                            :loading="asyncLoading"
                            icon="el-icon-plus"
                            size="large"
                            style="width: 100%"
                            type="primary"
                            @click="download"
                            >发起导出任务
                        </el-button>
                        <el-button
                            v-else
                            icon="el-icon-download"
                            size="large"
                            style="width: 100%"
                            type="primary"
                            @click="download"
                            >下 载
                        </el-button>
                    </el-tab-pane>
                    <el-tab-pane
                        v-if="columnData.length > 0"
                        label="列设置"
                        lazy
                    >
                        <columnSet :column="columnData"></columnSet>
                    </el-tab-pane>
                    <el-tab-pane v-if="data && showData" label="其他参数" lazy>
                        <el-descriptions :column="1" border size="small">
                            <el-descriptions-item
                                v-for="(val, key) in data"
                                :key="key"
                                :label="key"
                                >{{ val }}
                            </el-descriptions-item>
                        </el-descriptions>
                    </el-tab-pane>
                </el-tabs>
            </div>
        </el-main>
    </el-drawer>
</template>

<script>
import columnSet from "./column";

export default {
    components: {
        columnSet,
    },
    props: {
        apiObj: {
            type: Object,
            default: () => {},
        },
        fileName: { type: String, default: "" },
        fileTypes: { type: Array, default: () => ["xlsx"] },
        data: {
            type: Object,
            default: () => {},
        },
        showData: { type: Boolean, default: false },
        async: { type: Boolean, default: false },
        column: { type: Array, default: () => [] },
        blob: { type: Boolean, default: false },
        progress: { type: Boolean, default: true },
    },
    data() {
        return {
            dialog: false,
            formData: {
                fileName: this.fileName,
                fileType: this.fileTypes[0],
            },
            columnData: [],
            downLoading: false,
            downLoadProgress: 0,
            asyncLoading: false,
        };
    },
    watch: {
        "formData.fileType"(val) {
            if (this.formData.fileName.includes(".")) {
                this.formData.fileName =
                    this.formData.fileName.substring(
                        0,
                        this.formData.fileName.lastIndexOf(".")
                    ) +
                    "." +
                    val;
            } else {
                this.formData.fileName = this.formData.fileName + "." + val;
            }
        },
    },
    mounted() {},
    methods: {
        open() {
            this.dialog = true;
            this.formData = {
                fileName:
                    (this.fileName
                        ? this.fileName
                        : new Date().getTime() + "") +
                    "." +
                    this.fileTypes[0],
                fileType: this.fileTypes[0],
            };
            this.columnData = JSON.parse(JSON.stringify(this.column));
        },
        close() {
            this.dialog = false;
        },
        download() {
            let columnArr = {
                column: this.columnData
                    .filter((n) => !n.hide)
                    .map((n) => n.prop)
                    .join(","),
            };
            let assignData = { ...this.data, ...this.formData, ...columnArr };
            if (this.async) {
                this.asyncDownload(
                    this.apiObj,
                    this.formData.fileName,
                    assignData
                );
            } else if (this.blob) {
                this.downloadFile(
                    this.apiObj,
                    this.formData.fileName,
                    assignData
                );
            } else {
                this.linkFile(
                    this.apiObj.url,
                    this.formData.fileName,
                    assignData
                );
            }
        },
        linkFile(url, fileName, data = {}) {
            let a = document.createElement("a");
            a.style = "display: none";
            a.target = "_blank";
            //a.download = fileName
            a.href = url + this.toQueryString(data);
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
        },
        downloadFile(apiObj, fileName, data = {}) {
            this.downLoading = true;
            const _this = this;
            apiObj
                .get(data, {
                    responseType: "blob",
                    onDownloadProgress(e) {
                        if (e.lengthComputable) {
                            _this.downLoadProgress = parseInt(
                                (e.loaded / e.total) * 100
                            );
                        }
                    },
                })
                .then((res) => {
                    this.downLoading = false;
                    this.downLoadProgress = 0;
                    let url = URL.createObjectURL(res);
                    let a = document.createElement("a");
                    a.style = "display: none";
                    a.target = "_blank";
                    a.download = fileName;
                    a.href = url;
                    document.body.appendChild(a);
                    a.click();
                    document.body.removeChild(a);
                    URL.revokeObjectURL(url);
                })
                .catch((err) => {
                    this.downLoading = false;
                    this.downLoadProgress = 0;
                    this.$notify.error({
                        title: "下载文件失败",
                        message: err,
                    });
                });
        },
        asyncDownload(apiObj, fileName, data = {}) {
            this.asyncLoading = true;
            apiObj
                .get(data)
                .then((res) => {
                    this.asyncLoading = false;
                    if (res.code === "succeed") {
                        this.dialog = false;
                        this.$msgbox({
                            title: "成功发起任务",
                            message: `<div><img style="height:200px" src="img/tasks-example.png"/></div><p>已成功发起导出任务，您可以操作其他事务</p><p>稍后可在 <b>任务中心</b> 查看执行结果</p>`,
                            type: "success",
                            confirmButtonText: "知道了",
                            dangerouslyUseHTMLString: true,
                            center: true,
                        }).catch(() => {});
                    } else {
                        this.$alert(res.message || "未知错误", "发起任务失败", {
                            type: "error",
                            center: true,
                        }).catch(() => {});
                    }
                })
                .catch(() => {
                    this.asyncLoading = false;
                });
        },
        toQueryString(obj) {
            let arr = [];
            for (let k in obj) {
                arr.push(`${k}=${obj[k]}`);
            }
            return (arr.length > 0 ? "?" : "") + arr.join("&");
        },
    },
};
</script>

<style></style>