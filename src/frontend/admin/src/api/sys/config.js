/**
 *  配置服务
 *  @module @/api/sys/config
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除配置
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/config/bulk.delete`,
        name: `批量删除配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 配置分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/sys/config/count.by`,
        name: `配置分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建配置
     */
    create: {
        url: `${config.API_URL}/api/sys/config/create`,
        name: `创建配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除配置
     */
    delete: {
        url: `${config.API_URL}/api/sys/config/delete`,
        name: `删除配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑配置
     */
    edit: {
        url: `${config.API_URL}/api/sys/config/edit`,
        name: `编辑配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 导出配置
     */
    export: {
        url: `${config.API_URL}/api/sys/config/export`,
        name: `导出配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个配置
     */
    get: {
        url: `${config.API_URL}/api/sys/config/get`,
        name: `获取单个配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取最新有效配置
     */
    getLatestConfig: {
        url: `${config.API_URL}/api/sys/config/get.latest.config`,
        name: `获取最新有效配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取注册配置
     */
    getRegisterConfig: {
        url: `${config.API_URL}/api/sys/config/get.register.config`,
        name: `获取注册配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询配置
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/config/paged.query`,
        name: `分页查询配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询配置
     */
    query: {
        url: `${config.API_URL}/api/sys/config/query`,
        name: `查询配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置配置启用状态
     */
    setEnabled: {
        url: `${config.API_URL}/api/sys/config/set.enabled`,
        name: `设置配置启用状态`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}