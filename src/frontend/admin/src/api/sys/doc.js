/**
 *  文档服务
 *  @module @/api/sys/doc
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除文档分类
     */
    bulkDeleteCatalog: {
        url: `${config.API_URL}/api/sys/doc/bulk.delete.catalog`,
        name: `批量删除文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 批量删除文档内容
     */
    bulkDeleteContent: {
        url: `${config.API_URL}/api/sys/doc/bulk.delete.content`,
        name: `批量删除文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建文档分类
     */
    createCatalog: {
        url: `${config.API_URL}/api/sys/doc/create.catalog`,
        name: `创建文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建文档内容
     */
    createContent: {
        url: `${config.API_URL}/api/sys/doc/create.content`,
        name: `创建文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除文档分类
     */
    deleteCatalog: {
        url: `${config.API_URL}/api/sys/doc/delete.catalog`,
        name: `删除文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除文档内容
     */
    deleteContent: {
        url: `${config.API_URL}/api/sys/doc/delete.content`,
        name: `删除文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑文档分类
     */
    editCatalog: {
        url: `${config.API_URL}/api/sys/doc/edit.catalog`,
        name: `编辑文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑文档内容
     */
    editContent: {
        url: `${config.API_URL}/api/sys/doc/edit.content`,
        name: `编辑文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 导出文档内容
     */
    exportContent: {
        url: `${config.API_URL}/api/sys/doc/export.content`,
        name: `导出文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个文档分类
     */
    getCatalog: {
        url: `${config.API_URL}/api/sys/doc/get.catalog`,
        name: `获取单个文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个文档内容
     */
    getContent: {
        url: `${config.API_URL}/api/sys/doc/get.content`,
        name: `获取单个文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询文档分类
     */
    pagedQueryCatalog: {
        url: `${config.API_URL}/api/sys/doc/paged.query.catalog`,
        name: `分页查询文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询文档内容
     */
    pagedQueryContent: {
        url: `${config.API_URL}/api/sys/doc/paged.query.content`,
        name: `分页查询文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询文档分类
     */
    queryCatalog: {
        url: `${config.API_URL}/api/sys/doc/query.catalog`,
        name: `查询文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询文档内容
     */
    queryContent: {
        url: `${config.API_URL}/api/sys/doc/query.content`,
        name: `查询文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 启用/禁用文档内容
     */
    setEnabled: {
        url: `${config.API_URL}/api/sys/doc/set.enabled`,
        name: `启用/禁用文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 浏览文档内容
     */
    viewContent: {
        url: `${config.API_URL}/api/sys/doc/view.content`,
        name: `浏览文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}