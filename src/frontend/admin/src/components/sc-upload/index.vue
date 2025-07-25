<template>
    <div :class="{ 'sc-upload-round': round }" :style="style" class="sc-upload">
        <div v-if="file && file.status !== 'success'" class="sc-upload__uploading">
            <div class="sc-upload__progress">
                <el-progress :percentage="file.percentage" :stroke-width="16" :text-inside="true" />
            </div>
            <el-image :src="file.tempFile" class="image" fit="cover" />
        </div>
        <div v-if="file && file.status === 'success'" class="sc-upload__img">
            <el-image :preview-src-list="[file.url]" :src="file.url" :z-index="9999" append-to-body class="image" fit="cover" hide-on-click-modal>
                <template #placeholder>
                    <div class="sc-upload__img-slot">Loading...</div>
                </template>
            </el-image>
            <div v-if="!disabled" class="sc-upload__img-actions">
                <span @click="handleRemove()" class="del"
                    ><el-icon><el-icon-delete /></el-icon
                ></span>
            </div>
        </div>
        <el-upload
            v-if="!file"
            :accept="accept"
            :action="action"
            :auto-upload="cropper ? false : autoUpload"
            :before-upload="before"
            :data="data"
            :disabled="disabled"
            :http-request="request"
            :limit="1"
            :name="name"
            :on-change="change"
            :on-error="error"
            :on-exceed="handleExceed"
            :on-success="success"
            :show-file-list="showFileList"
            class="uploader"
            ref="uploader">
            <slot>
                <div class="el-upload--picture-card">
                    <div class="file-empty">
                        <el-icon>
                            <component :is="icon" />
                        </el-icon>
                        <h4 v-if="title">{{ title }}</h4>
                    </div>
                </div>
            </slot>
        </el-upload>
        <span style="display: none !important"><el-input v-model="value" /></span>
        <el-dialog v-model="cropperDialogVisible" :title="$t('剪裁')" :width="580" @closed="cropperClosed" destroy-on-close draggable>
            <sc-cropper :aspectRatio="aspectRatio" :compress="compress" :src="cropperFile.tempCropperFile" ref="cropper" />
            <template #footer>
                <el-button @click="cropperDialogVisible = false">{{ $t('取消') }}</el-button>
                <el-button @click="cropperSave" type="primary">{{ $t('确定') }}</el-button>
            </template>
        </el-dialog>
    </div>
</template>

<script>
import { genFileId } from 'element-plus'
import config from '@/config/upload'

export default {
    props: {
        modelValue: { type: String, default: '' },
        height: { type: Number, default: 10 },
        width: { type: Number, default: 10 },
        title: { type: String, default: '' },
        icon: { type: String, default: 'el-icon-plus' },
        action: { type: String, default: '' },
        apiObj: {
            type: Object,
            default: () => {},
        },
        name: { type: String, default: config.filename },
        data: {
            type: Object,
            default: () => {},
        },
        accept: { type: String, default: 'image/gif, image/jpeg, image/png' },
        maxSize: { type: Number, default: config.maxSizeFile },
        limit: { type: Number, default: 1 },
        autoUpload: { type: Boolean, default: true },
        showFileList: { type: Boolean, default: false },
        disabled: { type: Boolean, default: false },
        round: { type: Boolean, default: false },
        onSuccess: {
            type: Function,
            default: () => {
                return true
            },
        },

        cropper: { type: Boolean, default: false },
        compress: { type: Number, default: 1 },
        aspectRatio: { type: Number, default: NaN },
    },
    components: {},
    data() {
        return {
            value: '',
            file: null,
            style: {
                width: this.width + 'rem',
                height: this.height + 'rem',
            },
            cropperDialogVisible: false,
            cropperFile: null,
        }
    },
    watch: {
        modelValue(val) {
            this.value = val
            this.newFile(val)
        },
        value(val) {
            this.$emit('update:modelValue', val)
        },
    },
    mounted() {
        this.value = this.modelValue
        this.newFile(this.modelValue)
    },
    methods: {
        newFile(url) {
            if (url) {
                this.file = {
                    status: 'success',
                    url: url,
                }
            } else {
                this.file = null
            }
        },
        cropperSave() {
            this.$refs.cropper.getCropFile(
                (file) => {
                    file.uid = this.cropperFile.uid
                    this.cropperFile.raw = file

                    this.file = this.cropperFile
                    this.file.tempFile = URL.createObjectURL(this.file.raw)
                    this.$refs.uploader.submit()
                },
                this.cropperFile.name,
                this.cropperFile.type,
            )
            this.cropperDialogVisible = false
        },
        cropperClosed() {
            URL.revokeObjectURL(this.cropperFile.tempCropperFile)
            delete this.cropperFile.tempCropperFile
        },
        handleRemove() {
            this.clearFiles()
        },
        clearFiles() {
            URL.revokeObjectURL(this.file.tempFile)
            this.value = ''
            this.file = null
            this.$nextTick(() => {
                this.$refs.uploader.clearFiles()
            })
        },
        change(file, files) {
            if (files.length > 1) {
                files.splice(0, 1)
            }
            if (this.cropper && file.status === 'ready') {
                const acceptIncludes = ['image/gif', 'image/jpeg', 'image/png'].includes(file.raw.type)
                if (!acceptIncludes) {
                    this.$notify.warning({
                        title: '上传文件警告',
                        message: '选择的文件非图像类文件',
                    })
                    return false
                }
                this.cropperFile = file
                this.cropperFile.tempCropperFile = URL.createObjectURL(file.raw)
                this.cropperDialogVisible = true
                return false
            }
            this.file = file
            if (file.status === 'ready') {
                file.tempFile = URL.createObjectURL(file.raw)
            }
        },
        before(file) {
            const acceptIncludes = this.accept.replace(/\s/g, '').split(',').includes(file.type)
            if (!acceptIncludes) {
                this.$notify.warning({
                    title: '上传文件警告',
                    message: '选择的文件非图像类文件',
                })
                this.clearFiles()
                return false
            }
            const maxSize = file.size / 1024 / 1024 < this.maxSize
            if (!maxSize) {
                this.$message.warning(this.$t(`上传文件大小不能超过 {maxSize}MB`, { maxSize: this.maxSize }))
                this.clearFiles()
                return false
            }
        },
        handleExceed(files) {
            const file = files[0]
            file.uid = genFileId()
            this.$refs.uploader.handleStart(file)
        },
        success(res, file) {
            //释放内存删除blob
            URL.revokeObjectURL(file.tempFile)
            delete file.tempFile
            const os = this.onSuccess(res, file)
            if (os !== undefined && os === false) {
                this.$nextTick(() => {
                    this.file = null
                    this.value = ''
                })
                return false
            }
            const response = config.parseData(res)
            file.url = response.src
            this.value = file.url
        },
        error(err) {
            this.$nextTick(() => {
                this.clearFiles()
            })
            this.$notify.error({
                title: '上传文件未成功',
                message: err,
            })
        },
        request(param) {
            let apiObj = config.apiObj
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
                        param.onError(response.msg || '未知错误')
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
.el-form-item.is-error .sc-upload .el-upload--picture-card {
    border-color: var(--el-color-danger);
}

.sc-upload .el-upload--picture-card {
    border-radius: 0;
}

.sc-upload .uploader,
.sc-upload:deep(.el-upload) {
    width: 100%;
    height: 100%;
}

.sc-upload__img {
    width: 100%;
    height: 100%;
    position: relative;
}

.sc-upload__img .image {
    width: 100%;
    height: 100%;
}

.sc-upload__img-actions {
    position: absolute;
    top: 0;
    right: 0;
    display: none;
}

.sc-upload__img-actions span {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 1.9rem;
    height: 1.9rem;
    cursor: pointer;
    color: var(--el-color-white);
}

.sc-upload__img-actions span i {
    font-size: 0.9rem;
}

.sc-upload__img-actions .del {
    background: var(--el-color-danger);
}

.sc-upload__img:hover .sc-upload__img-actions {
    display: block;
}

.sc-upload__img-slot {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    font-size: 0.9rem;
    background-color: var(--el-fill-color-lighter);
}

.sc-upload__uploading {
    width: 100%;
    height: 100%;
    position: relative;
}

.sc-upload__progress {
    position: absolute;
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: var(--el-overlay-color-lighter);
    z-index: 1;
    padding: 1rem;
}

.sc-upload__progress .el-progress {
    width: 100%;
}

.sc-upload__uploading .image {
    width: 100%;
    height: 100%;
}

.sc-upload .file-empty {
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
}

.sc-upload .file-empty i {
    font-size: 2rem;
}

.sc-upload .file-empty h4 {
    font-size: 0.9rem;
    font-weight: normal;
    color: var(--el-color-info);
    margin-top: 1rem;
}

.sc-upload.sc-upload-round {
    border-radius: 50%;
    overflow: hidden;
}

.sc-upload.sc-upload-round .el-upload--picture-card {
    border-radius: 50%;
}

.sc-upload.sc-upload-round .sc-upload__img-actions {
    top: auto;
    left: 0;
    right: 0;
    bottom: 0;
}

.sc-upload.sc-upload-round .sc-upload__img-actions span {
    width: 100%;
}
</style>