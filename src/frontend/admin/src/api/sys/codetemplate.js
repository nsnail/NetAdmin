/**
 *  代码模板服务
 *  @module @/api/sys/code.template
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除代码模板
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/code.template/bulk.delete`,
        name: `批量删除代码模板`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 代码模板计数
     */
    count: {
        url: `${config.API_URL}/api/sys/code.template/count`,
        name: `代码模板计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 代码模板分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/sys/code.template/count.by`,
        name: `代码模板分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建代码模板
     */
    create: {
        url: `${config.API_URL}/api/sys/code.template/create`,
        name: `创建代码模板`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除代码模板
     */
    delete: {
        url: `${config.API_URL}/api/sys/code.template/delete`,
        name: `删除代码模板`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑代码模板
     */
    edit: {
        url: `${config.API_URL}/api/sys/code.template/edit`,
        name: `编辑代码模板`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个代码模板
     */
    get: {
        url: `${config.API_URL}/api/sys/code.template/get`,
        name: `获取单个代码模板`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询代码模板
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/code.template/paged.query`,
        name: `分页查询代码模板`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}