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
     * 导出角色
     */
    export: {
        url: `${config.API_URL}/api/sys/role/export`,
        name: `导出角色`,
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

    /**
     * 设置是否显示仪表板
     */
    setDisplayDashboard: {
        url: `${config.API_URL}/api/sys/role/set.display.dashboard`,
        name: `设置是否显示仪表板`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 启用/禁用角色
     */
    setEnabled: {
        url: `${config.API_URL}/api/sys/role/set.enabled`,
        name: `启用/禁用角色`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置是否忽略权限控制
     */
    setIgnorePermissionControl: {
        url: `${config.API_URL}/api/sys/role/set.ignore.permission.control`,
        name: `设置是否忽略权限控制`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}