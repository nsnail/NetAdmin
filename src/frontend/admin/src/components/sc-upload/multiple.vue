<template>
    <div class="sc-upload-multiple">
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
            :show-file-list="showFileList"
            list-type="picture-card"
            ref="uploader">
            <slot>
                <el-icon>
                    <el-icon-plus />
                </el-icon>
            </slot>
            <template #tip>
                <div v-if="tip" class="el-upload__tip">{{ tip }}</div>
            </template>
            <template #file="{ file }">
                <div class="sc-upload-list-item">
                    <el-image
                        :initial-index="preview.findIndex((n) => n === file.url)"
                        :preview-src-list="preview"
                        :src="file.url"
                        :z-index="9999"
                        append-to-body
                        class="el-upload-list__item-thumbnail"
                        fit="cover"
                        hide-on-click-modal>
                        <template #placeholder>
                            <div class="sc-upload-multiple-image-slot">Loading...</div>
                        </template>
                    </el-image>
                    <div v-if="!disabled && file.status === 'success'" class="sc-upload__item-actions">
                        <span @click="handleRemove(file)" class="del"
                            ><el-icon><el-icon-delete /></el-icon
                        ></span>
                    </div>
                    <div v-if="file.status === 'ready' || file.status === 'uploading'" class="sc-upload__item-progress">
                        <el-progress :percentage="file.percentage" :stroke-width="16" :text-inside="true" />
                    </div>
                </div>
            </template>
        </el-upload>
        <span style="display: none !important"><el-input v-model="value" /></span>
    </div>
</template>

<script>
import config from '@/config/upload'
import sortable from 'sortablejs'

export default {
    props: {
        modelValue: { type: [String, Array], default: '' },
        tip: { type: String, default: '' },
        action: { type: String, default: '' },
        apiObj: {
            type: Object,
            default: () => {},
        },
        drag: { type: Boolean, default: false },
        name: { type: String, default: config.filename },
        data: {
            type: Object,
            default: () => {},
        },
        accept: { type: String, default: 'image/gif,image/jpeg,image/jpg,image/png' },
        maxSize: { type: Number, default: config.maxSizeFile },
        limit: { type: Number, default: 0 },
        autoUpload: { type: Boolean, default: true },
        showFileList: { type: Boolean, default: true },
        multiple: { type: Boolean, default: true },
        disabled: { type: Boolean, default: false },
        draggable: { type: Boolean, default: false },
        onSuccess: {
            type: Function,
            default: () => {
                return true
            },
        },
    },
    data() {
        return {
            value: '',
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
                this.$emit('update:modelValue', Array.isArray(this.modelValue) ? this.formatArr(val) : this.toStr(val))
                this.value = this.toStr(val)
            },
            deep: true,
        },
    },
    computed: {
        preview() {
            return this.defaultFileList.map((v) => v.url)
        },
    },
    mounted() {
        this.defaultFileList = Array.isArray(this.modelValue) ? this.modelValue : this.toArr(this.modelValue)
        this.value = this.modelValue
        if (!this.disabled && this.draggable) {
            this.rowDrop()
        }
    },
    methods: {
        //默认值转换为数组
        toArr(str) {
            const _arr = []
            const arr = str?.split(',')
            arr?.forEach((item) => {
                if (item) {
                    const urlArr = item.split('/')
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
            return arr.map((v) => v.url).join(',')
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
        //拖拽
        rowDrop() {
            const _this = this
            const itemBox = this.$refs.uploader.$el.querySelector('.el-upload-list')
            sortable.create(itemBox, {
                handle: '.el-upload-list__item',
                animation: 200,
                ghostClass: 'ghost',
                onEnd({ newIndex, oldIndex }) {
                    const tableData = _this.defaultFileList
                    const currRow = tableData.splice(oldIndex, 1)[0]
                    tableData.splice(newIndex, 0, currRow)
                },
            })
        },
        before(file) {
            if (!this.accept.split(',').includes(file.type)) {
                this.$message.warning(`选择的文件类型 ${file.type} 非图像类文件`)
                return false
            }
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
                title: '上传文件未成功',
                message: err,
            })
        },
        beforeRemove(uploadFile) {
            return this.$confirm(`是否移除 ${uploadFile.name} ?`, '提示', {
                type: 'warning',
            })
                .then(() => {
                    return true
                })
                .catch(() => {
                    return false
                })
        },
        handleRemove(file) {
            this.$refs.uploader.handleRemove(file)
            //this.defaultFileList.splice(this.defaultFileList.findIndex(item => item.uid===file.uid), 1)
        },
        handleExceed() {
            this.$message.warning(`当前设置最多上传 ${this.limit} 个文件，请移除后上传!`)
        },
        handlePreview(uploadFile) {
            window.open(uploadFile.url)
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
.el-form-item.is-error .sc-upload-multiple:deep(.el-upload--picture-card) {
    border-color: var(--el-color-danger);
}

:deep(.el-upload-list__item) {
    transition: none;
    border-radius: 0;
}

.sc-upload-multiple:deep(.el-upload-list__item.el-list-leave-active) {
    position: static !important;
}

.sc-upload-multiple:deep(.el-upload--picture-card) {
    border-radius: 0;
}

.sc-upload-list-item {
    width: 100%;
    height: 100%;
    position: relative;
}

.sc-upload-multiple .el-image {
    display: block;
}

.sc-upload-multiple .el-image:deep(img) {
    -webkit-user-drag: none;
}

.sc-upload-multiple-image-slot {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    font-size: 0.9rem;
}

.sc-upload-multiple .el-upload-list__item:hover .sc-upload__item-actions {
    display: block;
}

.sc-upload__item-actions {
    position: absolute;
    top: 0;
    right: 0;
    display: none;
}

.sc-upload__item-actions span {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 25px;
    height: 25px;
    cursor: pointer;
    color: var(--el-color-white);
}

.sc-upload__item-actions span i {
    font-size: 0.9rem;
}

.sc-upload__item-actions .del {
    background: var(--el-color-danger);
}

.sc-upload__item-progress {
    position: absolute;
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
    background-color: var(--el-overlay-color-lighter);
}
</style>