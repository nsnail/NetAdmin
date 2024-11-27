/**
 *  文档分类服务
 *  @module @/api/sys/doc.catalog
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除文档分类
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/doc.catalog/bulk.delete`,
        name: `批量删除文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 文档分类计数
     */
    count: {
        url: `${config.API_URL}/api/sys/doc.catalog/count`,
        name: `文档分类计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建文档分类
     */
    create: {
        url: `${config.API_URL}/api/sys/doc.catalog/create`,
        name: `创建文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除文档分类
     */
    delete: {
        url: `${config.API_URL}/api/sys/doc.catalog/delete`,
        name: `删除文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个文档分类
     */
    get: {
        url: `${config.API_URL}/api/sys/doc.catalog/get`,
        name: `获取单个文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询文档分类
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/doc.catalog/paged.query`,
        name: `分页查询文档分类`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}