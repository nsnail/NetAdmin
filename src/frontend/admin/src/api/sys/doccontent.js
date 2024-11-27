/**
 *  文档内容服务
 *  @module @/api/sys/doc.content
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除文档内容
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/doc.content/bulk.delete`,
        name: `批量删除文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 文档内容计数
     */
    count: {
        url: `${config.API_URL}/api/sys/doc.content/count`,
        name: `文档内容计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建文档内容
     */
    create: {
        url: `${config.API_URL}/api/sys/doc.content/create`,
        name: `创建文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除文档内容
     */
    delete: {
        url: `${config.API_URL}/api/sys/doc.content/delete`,
        name: `删除文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个文档内容
     */
    get: {
        url: `${config.API_URL}/api/sys/doc.content/get`,
        name: `获取单个文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询文档内容
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/doc.content/paged.query`,
        name: `分页查询文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 启用/禁用文档内容
     */
    setEnabled: {
        url: `${config.API_URL}/api/sys/doc.content/set.enabled`,
        name: `启用/禁用文档内容`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}