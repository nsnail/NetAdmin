<template>
    <div class="sc-upload-file">
        <el-upload
            v-model:file-list="defaultFileList"
            :accept="accept"
            :action="action"
            :auto-upload="autoUpload"
            :before-upload="before"
            :data="data"
            :disabled="disabled"
            :drag="drag"
            :http-request="request"
            :limit="limit"
            :multiple="multiple"
            :name="name"
            :on-error="error"
            :on-exceed="handleExceed"
            :on-preview="handlePreview"
            :on-success="success"
            :show-file-list="showFileList">
            <slot>
                <el-button :disabled="disabled" type="primary">{{ $t(`点击上传`) }}</el-button>
            </slot>
            <template #tip>
                <div v-if="tip" class="el-upload__tip">{{ tip }}</div>
            </template>
        </el-upload>
        <span style="display: none !important"><el-input v-model="value" /></span>
    </div>
</template>

<script>
import config from '@/config/upload'

export default {
    props: {
        modelValue: { type: [String, Array], default: `` },
        tip: { type: String, default: `` },
        action: { type: String, default: `` },
        apiObj: {
            type: Object,
            default: () => {},
        },
        name: { type: String, default: config.filename },
        data: {
            type: Object,
            default: () => {},
        },
        accept: { type: String, default: `` },
        maxSize: { type: Number, default: config.maxSizeFile },
        limit: { type: Number, default: 0 },
        autoUpload: { type: Boolean, default: true },
        showFileList: { type: Boolean, default: true },
        drag: { type: Boolean, default: false },
        multiple: { type: Boolean, default: true },
        disabled: { type: Boolean, default: false },
        onSuccess: {
            type: Function,
            default: () => {
                return true
            },
        },
    },
    data() {
        return {
            value: ``,
            defaultFileList: [],
        }
    },
    watch: {
        modelValue(val) {
            if (Array.isArray(val)) {
                if (JSON.stringify(val) !== JSON.stringify(this.formatArr(this.defaultFileList))) {
                    this.defaultFileList = val
                    this.value = val
                }
            } else {
                if (val !== this.toStr(this.defaultFileList)) {
                    this.defaultFileList = this.toArr(val)
                    this.value = val
                }
            }
        },
        defaultFileList: {
            handler(val) {
                this.$emit(`update:modelValue`, Array.isArray(this.modelValue) ? this.formatArr(val) : this.toStr(val))
                this.value = this.toStr(val)
            },
            deep: true,
        },
    },
    mounted() {
        this.defaultFileList = Array.isArray(this.modelValue) ? this.modelValue : this.toArr(this.modelValue)
        this.value = this.modelValue
    },
    methods: {
        //默认值转换为数组
        toArr(str) {
            const _arr = []
            const arr = str.split(`,`)
            arr.forEach((item) => {
                if (item) {
                    const urlArr = item.split(`/`)
                    const fileName = urlArr[urlArr.length - 1]
                    _arr.push({
                        name: fileName,
                        url: item,
                    })
                }
            })
            return _arr
        },
        //数组转换为原始值
        toStr(arr) {
            return arr.map((v) => v.url).join(`,`)
        },
        //格式化数组值
        formatArr(arr) {
            const _arr = []
            arr.forEach((item) => {
                if (item) {
                    _arr.push({
                        name: item.name,
                        url: item.url,
                    })
                }
            })
            return _arr
        },
        before(file) {
            const maxSize = file.size / 1024 / 1024 < this.maxSize
            if (!maxSize) {
                this.$message.warning(this.$t(`上传文件大小不能超过 {maxSize}MB`, { maxSize: this.maxSize }))
                return false
            }
        },
        success(res, file) {
            const os = this.onSuccess(res, file)
            if (os !== undefined && os === false) {
                return false
            }
            const response = config.parseData(res)
            file.name = response.fileName
            file.url = response.src
        },
        error(err) {
            this.$notify.error({
                title: this.$t(`上传文件未成功`),
                message: err,
            })
        },
        beforeRemove(uploadFile) {
            return this.$confirm(this.$t(`是否移除 {name} ?`, { name: uploadFile.name }), `提示`, {
                type: `warning`,
            })
                .then(() => {
                    return true
                })
                .catch(() => {
                    return false
                })
        },
        handleExceed() {
            this.$message.warning(this.$t(`当前设置最多上传 {limit} 个文件，请移除后上传!`, { limit: this.limit }))
        },
        handlePreview(uploadFile) {
            window.open(uploadFile.url)
        },
        request(param) {
            let apiObj = config.apiObjFile
            if (this.apiObj) {
                apiObj = this.apiObj
            }
            const data = new FormData()
            data.append(param.filename, param.file)
            for (const key in param.data) {
                data.append(key, param.data[key])
            }
            apiObj
                .post(data, {
                    onUploadProgress: (e) => {
                        const complete = parseInt(((e.loaded / e.total) * 100) | 0, 10)
                        param.onProgress({ percent: complete })
                    },
                })
                .then((res) => {
                    const response = config.parseData(res)
                    if (response.code === config.successCode) {
                        param.onSuccess(res)
                    } else {
                        param.onError(response.msg || this.$t(`未知错误`))
                    }
                })
                .catch((err) => {
                    param.onError(err)
                })
        },
    },
}
</script>

<style scoped>
.el-form-item.is-error .sc-upload-file:deep(.el-upload-dragger) {
    border-color: var(--el-color-danger);
}

.sc-upload-file {
    width: 100%;
}

.sc-upload-file:deep(.el-upload-list__item) {
    transition: none !important;
}
</style>