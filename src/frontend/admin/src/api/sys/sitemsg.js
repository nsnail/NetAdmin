/**
 *  站内信服务
 *  @module @/api/sys/site.msg
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除站内信
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/site.msg/bulk.delete`,
        name: `批量删除站内信`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建站内信
     */
    create: {
        url: `${config.API_URL}/api/sys/site.msg/create`,
        name: `创建站内信`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除站内信
     */
    delete: {
        url: `${config.API_URL}/api/sys/site.msg/delete`,
        name: `删除站内信`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 站内信是否存在
     */
    exist: {
        url: `${config.API_URL}/api/sys/site.msg/exist`,
        name: `站内信是否存在`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个站内信
     */
    get: {
        url: `${config.API_URL}/api/sys/site.msg/get`,
        name: `获取单个站内信`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询站内信
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/site.msg/paged.query`,
        name: `分页查询站内信`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询站内信
     */
    query: {
        url: `${config.API_URL}/api/sys/site.msg/query`,
        name: `查询站内信`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 更新站内信
     */
    update: {
        url: `${config.API_URL}/api/sys/site.msg/update`,
        name: `更新站内信`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}