/**
 *  字典服务
 *  @module @/api/sys/dic
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除字典目录
     */
    bulkDeleteCatalog: {
        url: `${config.API_URL}/api/sys/dic/bulk.delete.catalog`,
        name: `批量删除字典目录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 批量删除字典内容
     */
    bulkDeleteContent: {
        url: `${config.API_URL}/api/sys/dic/bulk.delete.content`,
        name: `批量删除字典内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建字典目录
     */
    createCatalog: {
        url: `${config.API_URL}/api/sys/dic/create.catalog`,
        name: `创建字典目录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建字典内容
     */
    createContent: {
        url: `${config.API_URL}/api/sys/dic/create.content`,
        name: `创建字典内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除字典目录
     */
    deleteCatalog: {
        url: `${config.API_URL}/api/sys/dic/delete.catalog`,
        name: `删除字典目录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除字典内容
     */
    deleteContent: {
        url: `${config.API_URL}/api/sys/dic/delete.content`,
        name: `删除字典内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑字典目录
     */
    editCatalog: {
        url: `${config.API_URL}/api/sys/dic/edit.catalog`,
        name: `编辑字典目录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑字典内容
     */
    editContent: {
        url: `${config.API_URL}/api/sys/dic/edit.content`,
        name: `编辑字典内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 导出字典内容
     */
    exportContent: {
        url: `${config.API_URL}/api/sys/dic/export.content`,
        name: `导出字典内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个字典目录
     */
    getCatalog: {
        url: `${config.API_URL}/api/sys/dic/get.catalog`,
        name: `获取单个字典目录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个字典内容
     */
    getContent: {
        url: `${config.API_URL}/api/sys/dic/get.content`,
        name: `获取单个字典内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取字典值
     */
    getDicValue: {
        url: `${config.API_URL}/api/sys/dic/get.dic.value`,
        name: `获取字典值`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询字典目录
     */
    pagedQueryCatalog: {
        url: `${config.API_URL}/api/sys/dic/paged.query.catalog`,
        name: `分页查询字典目录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询字典内容
     */
    pagedQueryContent: {
        url: `${config.API_URL}/api/sys/dic/paged.query.content`,
        name: `分页查询字典内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询字典目录
     */
    queryCatalog: {
        url: `${config.API_URL}/api/sys/dic/query.catalog`,
        name: `查询字典目录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询字典内容
     */
    queryContent: {
        url: `${config.API_URL}/api/sys/dic/query.content`,
        name: `查询字典内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 启用/禁用字典内容
     */
    setEnabled: {
        url: `${config.API_URL}/api/sys/dic/set.enabled`,
        name: `启用/禁用字典内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}