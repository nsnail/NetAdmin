/**
 *  计划作业服务
 *  @module @/api/sys/job
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除计划作业
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/job/bulk.delete`,
        name: `批量删除计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建计划作业
     */
    create: {
        url: `${config.API_URL}/api/sys/job/create`,
        name: `创建计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除计划作业
     */
    delete: {
        url: `${config.API_URL}/api/sys/job/delete`,
        name: `删除计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 计划作业是否存在
     */
    exist: {
        url: `${config.API_URL}/api/sys/job/exist`,
        name: `计划作业是否存在`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个计划作业
     */
    get: {
        url: `${config.API_URL}/api/sys/job/get`,
        name: `获取单个计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询计划作业
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/job/paged.query`,
        name: `分页查询计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询计划作业
     */
    query: {
        url: `${config.API_URL}/api/sys/job/query`,
        name: `查询计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 启用/禁用作业
     */
    setEnabled: {
        url: `${config.API_URL}/api/sys/job/set.enabled`,
        name: `启用/禁用作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 更新计划作业
     */
    update: {
        url: `${config.API_URL}/api/sys/job/update`,
        name: `更新计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}