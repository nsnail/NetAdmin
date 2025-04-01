/**
 *  女朋友服务
 *  @module @/api/adm/girl.friends
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除女朋友
     */
    bulkDelete: {
        url: `${config.API_URL}/api/adm/girl.friends/bulk.delete`,
        name: `批量删除女朋友`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 女朋友计数
     */
    count: {
        url: `${config.API_URL}/api/adm/girl.friends/count`,
        name: `女朋友计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 女朋友分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/adm/girl.friends/count.by`,
        name: `女朋友分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建女朋友
     */
    create: {
        url: `${config.API_URL}/api/adm/girl.friends/create`,
        name: `创建女朋友`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除女朋友
     */
    delete: {
        url: `${config.API_URL}/api/adm/girl.friends/delete`,
        name: `删除女朋友`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑女朋友
     */
    edit: {
        url: `${config.API_URL}/api/adm/girl.friends/edit`,
        name: `编辑女朋友`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个女朋友
     */
    get: {
        url: `${config.API_URL}/api/adm/girl.friends/get`,
        name: `获取单个女朋友`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询女朋友
     */
    pagedQuery: {
        url: `${config.API_URL}/api/adm/girl.friends/paged.query`,
        name: `分页查询女朋友`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}