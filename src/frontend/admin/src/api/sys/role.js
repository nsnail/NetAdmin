/**
 *  角色服务
 *  @module @/api/sys/role
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除角色
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/role/bulk.delete`,
        name: `批量删除角色`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 角色计数
     */
    count: {
        url: `${config.API_URL}/api/sys/role/count`,
        name: `角色计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建角色
     */
    create: {
        url: `${config.API_URL}/api/sys/role/create`,
        name: `创建角色`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除角色
     */
    delete: {
        url: `${config.API_URL}/api/sys/role/delete`,
        name: `删除角色`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑角色
     */
    edit: {
        url: `${config.API_URL}/api/sys/role/edit`,
        name: `编辑角色`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个角色
     */
    get: {
        url: `${config.API_URL}/api/sys/role/get`,
        name: `获取单个角色`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询角色
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/role/paged.query`,
        name: `分页查询角色`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询角色
     */
    query: {
        url: `${config.API_URL}/api/sys/role/query`,
        name: `查询角色`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}