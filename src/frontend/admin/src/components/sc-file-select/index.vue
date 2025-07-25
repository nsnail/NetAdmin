<template>
    <div class="sc-file-select">
        <div v-loading="menuLoading" class="sc-file-select__side">
            <div class="sc-file-select__side-menu">
                <el-tree
                    :current-node-key="menu.length > 0 ? menu[0][treeProps.key] : ``"
                    :data="menu"
                    :node-key="treeProps.key"
                    :props="treeProps"
                    @node-click="groupClick"
                    class="menu"
                    highlight-current
                    ref="group">
                    <template #default="{ node }">
                        <span class="el-tree-node__label">
                            <el-icon class="icon"><el-icon-folder />{{ node.label }}</el-icon>
                        </span>
                    </template>
                </el-tree>
            </div>
            <div v-if="multiple" class="sc-file-select__side-msg">
                已选择 <b>{{ value.length }}</b> / <b>{{ max }}</b> 项
            </div>
        </div>
        <div v-loading="listLoading" class="sc-file-select__files">
            <div class="sc-file-select__top">
                <div v-if="!hideUpload" class="upload">
                    <el-upload
                        :accept="accept"
                        :before-upload="uploadBefore"
                        :http-request="uploadRequest"
                        :on-change="uploadChange"
                        :on-error="uploadError"
                        :on-progress="uploadProcess"
                        :on-success="uploadSuccess"
                        :show-file-list="false"
                        action=""
                        class="sc-file-select__upload"
                        multiple>
                        <el-button icon="el-icon-upload" type="primary">{{ $t(`本地上传`) }}</el-button>
                    </el-upload>
                    <span class="tips"
                        ><el-icon><el-icon-warning />{{ $t(`大小不超过 {maxSize} MB`) }} </el-icon></span
                    >
                </div>
                <div class="keyword">
                    <el-input
                        v-model="keyword"
                        :placeholder="$t(`文件名搜索`)"
                        @clear="search"
                        @keyup.enter="search"
                        clearable
                        prefix-icon="el-icon-search" />
                </div>
            </div>
            <div class="sc-file-select__list">
                <el-scrollbar ref="scrollbar">
                    <el-empty v-if="fileList.length === 0 && data.length === 0" :description="$t(`无数据`)" :image-size="80" />
                    <div v-for="(file, index) in fileList" :key="index" class="sc-file-select__item">
                        <div class="sc-file-select__item__file">
                            <div class="sc-file-select__item__upload">
                                <el-progress :percentage="file.progress" :width="70" type="circle" />
                            </div>
                            <el-image :src="file.tempImg" fit="contain" />
                        </div>
                        <p>{{ file.name }}</p>
                    </div>
                    <div
                        v-for="item in data"
                        :class="{ active: value.includes(item[fileProps.url]) }"
                        :key="item[fileProps.key]"
                        @click="select(item)"
                        class="sc-file-select__item">
                        <div class="sc-file-select__item__file">
                            <div v-if="multiple" class="sc-file-select__item__checkbox">
                                <el-icon>
                                    <el-icon-check />
                                </el-icon>
                            </div>
                            <div v-else class="sc-file-select__item__select">
                                <el-icon>
                                    <el-icon-check />
                                </el-icon>
                            </div>
                            <div class="sc-file-select__item__box"></div>
                            <el-image v-if="_isImg(item[fileProps.url])" :src="item[fileProps.url]" fit="contain" lazy />
                            <div v-else class="item-file item-file-doc">
                                <i
                                    v-if="files[_getExt(item[fileProps.url])]"
                                    :class="files[_getExt(item[fileProps.url])].icon"
                                    :style="{ color: files[_getExt(item[fileProps.url])].color }"></i>
                                <i v-else class="sc-icon-file-list-fill" style="color: var(--el-color-info)"></i>
                            </div>
                        </div>
                        <p :title="item[fileProps.fileName]">{{ item[fileProps.fileName] }}</p>
                    </div>
                </el-scrollbar>
            </div>
            <div class="sc-file-select__pagination">
                <el-pagination
                    v-model:currentPage="currentPage"
                    :page-size="pageSize"
                    :total="total"
                    @current-change="reload"
                    background
                    layout="prev, pager, next"
                    small />
            </div>
            <div class="sc-file-select__do">
                <slot name="do" />
                <el-button :disabled="value.length <= 0" @click="submit" type="primary">{{ $t(`确定`) }}</el-button>
            </div>
        </div>
    </div>
</template>

<script>
import config from '@/config/file-select'

export default {
    props: {
        modelValue: null,
        hideUpload: { type: Boolean, default: false },
        multiple: { type: Boolean, default: false },
        max: { type: Number, default: config.max },
        onlyImage: { type: Boolean, default: false },
        maxSize: { type: Number, default: config.maxSize },
    },
    data() {
        return {
            keyword: null,
            pageSize: 20,
            total: 0,
            currentPage: 1,
            data: [],
            menu: [],
            menuId: ``,
            value: this.multiple ? [] : ``,
            fileList: [],
            accept: this.onlyImage ? `image/gif, image/jpeg, image/png` : ``,
            listLoading: false,
            menuLoading: false,
            treeProps: config.menuProps,
            fileProps: config.fileProps,
            files: config.files,
        }
    },
    watch: {
        multiple() {
            this.value = this.multiple ? [] : ``
            this.$emit(`update:modelValue`, JSON.parse(JSON.stringify(this.value)))
        },
    },
    mounted() {
        this.getMenu()
        this.getData()
    },
    methods: {
        //获取分类数据
        async getMenu() {
            this.menuLoading = true
            const res = await config.menuApiObj.get()
            this.menu = res.data
            this.menuLoading = false
        },
        //获取列表数据
        async getData() {
            this.listLoading = true
            const reqData = {
                [config.request.menuKey]: this.menuId,
                [config.request.page]: this.currentPage,
                [config.request.pageSize]: this.pageSize,
                [config.request.keyword]: this.keyword,
            }
            if (this.onlyImage) {
                reqData.type = `image`
            }
            const res = await config.listApiObj.get(reqData)
            const parseData = config.listParseData(res)
            this.data = parseData.rows
            this.total = parseData.total
            this.listLoading = false
            this.$refs.scrollbar.setScrollTop(0)
        },
        //树点击事件
        groupClick(data) {
            this.menuId = data.id
            this.currentPage = 1
            this.keyword = null
            this.getData()
        },
        //分页刷新表格
        reload() {
            this.getData()
        },
        search() {
            this.currentPage = 1
            this.getData()
        },
        select(item) {
            const itemUrl = item[this.fileProps.url]
            if (this.multiple) {
                if (this.value.includes(itemUrl)) {
                    this.value.splice(
                        this.value.findIndex((f) => f === itemUrl),
                        1,
                    )
                } else {
                    this.value.push(itemUrl)
                }
            } else {
                if (this.value.includes(itemUrl)) {
                    this.value = ``
                } else {
                    this.value = itemUrl
                }
            }
        },
        submit() {
            const value = JSON.parse(JSON.stringify(this.value))
            this.$emit(`update:modelValue`, value)
            this.$emit(`submit`, value)
        },
        //上传处理
        uploadChange(file, fileList) {
            file.tempImg = URL.createObjectURL(file.raw)
            this.fileList = fileList
        },
        uploadBefore(file) {
            const maxSize = file.size / 1024 / 1024 < this.maxSize
            if (!maxSize) {
                this.$message.warning(this.$t(`上传文件大小不能超过 {maxSize}MB`, { maxSize: this.maxSize }))
                return false
            }
        },
        uploadRequest(param) {
            const apiObj = config.apiObj
            const data = new FormData()
            data.append(`file`, param.file)
            data.append([config.request.menuKey], this.menuId)
            apiObj
                .post(data, {
                    onUploadProgress: (e) => {
                        param.onProgress(e)
                    },
                })
                .then((res) => {
                    param.onSuccess(res)
                })
                .catch((err) => {
                    param.onError(err)
                })
        },
        uploadProcess(event, file) {
            file.progress = Number(((event.loaded / event.total) * 100).toFixed(2))
        },
        uploadSuccess(res, file) {
            this.fileList.splice(
                this.fileList.findIndex((f) => f.uid === file.uid),
                1,
            )
            const response = config.uploadParseData(res)
            this.data.unshift({
                [this.fileProps.key]: response.id,
                [this.fileProps.fileName]: response.fileName,
                [this.fileProps.url]: response.url,
            })
            if (!this.multiple) {
                this.value = response.url
            }
        },
        uploadError(err) {
            this.$notify.error({
                title: this.$t(`上传文件错误`),
                message: err,
            })
        },
        //内置函数
        _isImg(fileUrl) {
            const imgExt = [`.jpg`, `.jpeg`, `.png`, `.gif`, `.bmp`]
            const fileExt = fileUrl.substring(fileUrl.lastIndexOf(`.`))
            return imgExt.indexOf(fileExt) !== -1
        },
        _getExt(fileUrl) {
            return fileUrl.substring(fileUrl.lastIndexOf(`.`) + 1)
        },
    },
}
</script>

<style scoped>
.sc-file-select {
    display: flex;
}

.sc-file-select__files {
    flex: 1;
}

.sc-file-select__list {
    height: 400px;
}

.sc-file-select__item {
    display: inline-block;
    margin: 0 1rem 25px 0;
    width: 110px;
    cursor: pointer;
}

.sc-file-select__item__file {
    width: 110px;
    height: 110px;
    position: relative;
}

.sc-file-select__item__file .el-image {
    width: 110px;
    height: 110px;
}

.sc-file-select__item__box {
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    border: 2px solid var(--el-color-success);
    z-index: 1;
    display: none;
}

.sc-file-select__item__box::before {
    content: '';
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    background: var(--el-color-success);
    opacity: 0.2;
    display: none;
}

.sc-file-select__item:hover .sc-file-select__item__box {
    display: block;
}

.sc-file-select__item.active .sc-file-select__item__box {
    display: block;
}

.sc-file-select__item.active .sc-file-select__item__box::before {
    display: block;
}

.sc-file-select__item p {
    margin-top: 10px;
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
    text-align: center;
}

.sc-file-select__item__checkbox {
    position: absolute;
    width: 20px;
    height: 20px;
    top: 7px;
    right: 7px;
    z-index: 2;
    background: rgba(0, 0, 0, 0.2);
    border: 1px solid var(--el-color-white);
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.sc-file-select__item__checkbox i {
    font-size: 1.1rem;
    color: var(--el-color-white);
    font-weight: bold;
    display: none;
}

.sc-file-select__item__select {
    position: absolute;
    width: 20px;
    height: 20px;
    top: 0;
    right: 0;
    z-index: 2;
    background: var(--el-color-success);
    display: none;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.sc-file-select__item__select i {
    font-size: 1.1rem;
    color: var(--el-color-white);
    font-weight: bold;
}

.sc-file-select__item.active .sc-file-select__item__checkbox {
    background: var(--el-color-success);
}

.sc-file-select__item.active .sc-file-select__item__checkbox i {
    display: block;
}

.sc-file-select__item.active .sc-file-select__item__select {
    display: flex;
}

.sc-file-select__item__file .item-file {
    width: 110px;
    height: 110px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.sc-file-select__item__file .item-file i {
    font-size: 3rem;
}

.sc-file-select__item__file .item-file.item-file-doc {
    color: var(--el-color-primary);
}

.sc-file-select__item__upload {
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 1;
    background: rgba(255, 255, 255, 0.7);
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.sc-file-select__side {
    width: 200px;
    margin-right: 1rem;
    border-right: 1px solid rgba(128, 128, 128, 0.2);
    display: flex;
    flex-flow: column;
}

.sc-file-select__side-menu {
    flex: 1;
}

.sc-file-select__side-msg {
    height: 32px;
    line-height: 32px;
}

.sc-file-select__top {
    margin-bottom: 1rem;
    display: flex;
    justify-content: space-between;
}

.sc-file-select__upload {
    display: inline-block;
}

.sc-file-select__top .tips {
    font-size: 0.9rem;
    margin-left: 10px;
    color: var(--el-color-info);
}

.sc-file-select__top .tips i {
    font-size: 1.1rem;
    margin-right: 0.4rem;
    position: relative;
    bottom: -0.125em;
}

.sc-file-select__pagination {
    margin: 1rem 0;
}

.sc-file-select__do {
    text-align: right;
}
</style>