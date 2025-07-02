import api from '@/api'
import defaultConfig from '@/config'
//文件选择器配置

export default {
    apiObj: api.common.upload,
    menuApiObj: api.common.file.menu,
    listApiObj: api.common.file.list,
    successCode: 200,
    maxSize: 30,
    max: 99,
    uploadParseData: function (res) {
        return {
            id: res.data.id,
            fileName: res.data.fileName,
            url: res.data.src,
        }
    },
    listParseData: function (res) {
        return {
            rows: res.data.rows,
            total: res.data.total,
            msg: res.message,
            code: res.code,
        }
    },
    request: {
        page: 'page',
        pageSize: 'pageSize',
        keyword: 'keyword',
        menuKey: 'groupId',
    },
    menuProps: {
        key: 'id',
        label: 'label',
        children: 'children',
    },
    fileProps: {
        key: 'id',
        fileName: 'fileName',
        url: 'url',
    },
    files: {
        doc: {
            icon: 'sc-icon-file-word-2-fill',
            color: defaultConfig.APP_SET_COLOR,
        },
        docx: {
            icon: 'sc-icon-file-word-2-fill',
            color: defaultConfig.APP_SET_COLOR,
        },
        xls: {
            icon: 'sc-icon-file-excel-2-fill',
            color: 'var(--el-color-success)',
        },
        xlsx: {
            icon: 'sc-icon-file-excel-2-fill',
            color: 'var(--el-color-success)',
        },
        ppt: {
            icon: 'sc-icon-file-ppt-2-fill',
            color: 'var(--el-color-danger)',
        },
        pptx: {
            icon: 'sc-icon-file-ppt-2-fill',
            color: 'var(--el-color-danger)',
        },
    },
}