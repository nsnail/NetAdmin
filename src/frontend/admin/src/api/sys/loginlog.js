/**
 *  登录日志服务
 *  @module @/api/sys/login.log
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除登录日志
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/login.log/bulk.delete`,
        name: `批量删除登录日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 登录日志计数
     */
    count: {
        url: `${config.API_URL}/api/sys/login.log/count`,
        name: `登录日志计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 登录日志分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/sys/login.log/count.by`,
        name: `登录日志分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建登录日志
     */
    create: {
        url: `${config.API_URL}/api/sys/login.log/create`,
        name: `创建登录日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除登录日志
     */
    delete: {
        url: `${config.API_URL}/api/sys/login.log/delete`,
        name: `删除登录日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 导出登录日志
     */
    export: {
        url: `${config.API_URL}/api/sys/login.log/export`,
        name: `导出登录日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个登录日志
     */
    get: {
        url: `${config.API_URL}/api/sys/login.log/get`,
        name: `获取单个登录日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询登录日志
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/login.log/paged.query`,
        name: `分页查询登录日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询登录日志
     */
    query: {
        url: `${config.API_URL}/api/sys/login.log/query`,
        name: `查询登录日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}