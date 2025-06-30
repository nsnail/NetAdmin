/**
 *  用户邀请服务
 *  @module @/api/sys/user.invite
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除用户邀请
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/user.invite/bulk.delete`,
        name: `批量删除用户邀请`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 用户邀请计数
     */
    count: {
        url: `${config.API_URL}/api/sys/user.invite/count`,
        name: `用户邀请计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 用户邀请分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/sys/user.invite/count.by`,
        name: `用户邀请分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建用户邀请
     */
    create: {
        url: `${config.API_URL}/api/sys/user.invite/create`,
        name: `创建用户邀请`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除用户邀请
     */
    delete: {
        url: `${config.API_URL}/api/sys/user.invite/delete`,
        name: `删除用户邀请`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑用户邀请
     */
    edit: {
        url: `${config.API_URL}/api/sys/user.invite/edit`,
        name: `编辑用户邀请`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个用户邀请
     */
    get: {
        url: `${config.API_URL}/api/sys/user.invite/get`,
        name: `获取单个用户邀请`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询用户邀请
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/user.invite/paged.query`,
        name: `分页查询用户邀请`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询用户邀请
     */
    query: {
        url: `${config.API_URL}/api/sys/user.invite/query`,
        name: `查询用户邀请`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置返佣比率
     */
    setCommissionRatio: {
        url: `${config.API_URL}/api/sys/user.invite/set.commission.ratio`,
        name: `设置返佣比率`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}