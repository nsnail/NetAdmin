/**
 *  部门服务
 *  @module @/api/sys/dept
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除部门
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/dept/bulk.delete`,
        name: `批量删除部门`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 部门计数
     */
    count: {
        url: `${config.API_URL}/api/sys/dept/count`,
        name: `部门计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建部门
     */
    create: {
        url: `${config.API_URL}/api/sys/dept/create`,
        name: `创建部门`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除部门
     */
    delete: {
        url: `${config.API_URL}/api/sys/dept/delete`,
        name: `删除部门`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑部门
     */
    edit: {
        url: `${config.API_URL}/api/sys/dept/edit`,
        name: `编辑部门`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个部门
     */
    get: {
        url: `${config.API_URL}/api/sys/dept/get`,
        name: `获取单个部门`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询部门
     */
    query: {
        url: `${config.API_URL}/api/sys/dept/query`,
        name: `查询部门`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}