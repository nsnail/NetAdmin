<template>
    <slot :open="open">
        <el-button @click="open" plain type="primary">{{ $t(`导入`) }}</el-button>
    </slot>
    <el-dialog v-model="dialog" :close-on-click-modal="false" :title="$t(`导入`)" :width="550" append-to-body destroy-on-close>
        <el-progress v-if="loading" :percentage="percentage" :stroke-width="20" :text-inside="true" style="margin-bottom: 1rem" />
        <div v-loading="loading">
            <el-upload
                :accept="accept"
                :before-upload="before"
                :data="data"
                :http-request="request"
                :limit="1"
                :maxSize="maxSize"
                :on-error="error"
                :on-progress="progress"
                :on-success="success"
                :show-file-list="false"
                drag
                ref="uploader">
                <slot name="uploader">
                    <el-icon class="el-icon--upload">
                        <el-icon-upload-filled />
                    </el-icon>
                    <div class="el-upload__text">
                        {{ $t(`将文件拖到此处或 `) }}<em>{{ $t(`点击选择文件上传`) }}</em>
                    </div>
                </slot>
                <template #tip>
                    <div class="el-upload__tip">
                        <template v-if="tip">{{ tip }}</template>
                        <template v-else>{{ $t(`请上传小于或等于 {maxSize}M 的 {accept} 格式文件`) }}</template>
                        <p v-if="templateUrl" style="margin-top: 7px">
                            <el-link :href="templateUrl" :underline="false" target="_blank" type="primary">{{ $t(`下载导入模板`) }}</el-link>
                        </p>
                    </div>
                </template>
            </el-upload>
            <el-form v-if="$slots.form" inline label-position="left" label-width="10rem" style="margin-top: 18px">
                <slot :formData="formData" name="form" />
            </el-form>
        </div>
    </el-dialog>
</template>

<script>
export default {
    emits: [`success`],
    props: {
        apiObj: {
            type: Object,
            default: () => {},
        },
        data: {
            type: Object,
            default: () => {},
        },
        accept: { type: String, default: `.xls, .xlsx` },
        maxSize: { type: Number, default: 10 },
        tip: { type: String, default: `` },
        templateUrl: { type: String, default: `` },
    },
    data() {
        return {
            dialog: false,
            loading: false,
            percentage: 0,
            formData: {},
        }
    },
    mounted() {},
    methods: {
        open() {
            this.dialog = true
            this.formData = {}
        },
        close() {
            this.dialog = false
        },
        before(file) {
            const maxSize = file.size / 1024 / 1024 < this.maxSize
            if (!maxSize) {
                this.$message.warning(this.$t(`上传文件大小不能超过 {maxSize}MB`, { maxSize: this.maxSize }))
                return false
            }
            this.loading = true
        },
        progress(e) {
            this.percentage = e.percent
        },
        success(res, file) {
            this.$refs.uploader.handleRemove(file)
            this.$refs.uploader.clearFiles()
            this.loading = false
            this.percentage = 0
            this.$emit(`success`, res, this.close)
        },
        error(err) {
            this.loading = false
            this.percentage = 0
            this.$notify.error({
                title: `上传文件未成功`,
                message: err,
            })
        },
        request(param) {
            Object.assign(param.data, this.formData)
            const data = new FormData()
            data.append(param.filename, param.file)
            for (const key in param.data) {
                data.append(key, param.data[key])
            }
            this.apiObj
                .post(data, {
                    onUploadProgress: (e) => {
                        const complete = parseInt(((e.loaded / e.total) * 100) | 0, 10)
                        param.onProgress({ percent: complete })
                    },
                })
                .then((res) => {
                    param.onSuccess(res)
                })
                .catch((err) => {
                    param.onError(err)
                })
        },
    },
}
</script>

<style />