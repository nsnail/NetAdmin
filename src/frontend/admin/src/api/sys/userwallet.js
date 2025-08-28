/**
 *  用户钱包服务
 *  @module @/api/sys/user.wallet
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除用户钱包
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/user.wallet/bulk.delete`,
        name: `批量删除用户钱包`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 用户钱包分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/sys/user.wallet/count.by`,
        name: `用户钱包分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建用户钱包
     */
    create: {
        url: `${config.API_URL}/api/sys/user.wallet/create`,
        name: `创建用户钱包`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除用户钱包
     */
    delete: {
        url: `${config.API_URL}/api/sys/user.wallet/delete`,
        name: `删除用户钱包`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑用户钱包
     */
    edit: {
        url: `${config.API_URL}/api/sys/user.wallet/edit`,
        name: `编辑用户钱包`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 导出用户钱包
     */
    export: {
        url: `${config.API_URL}/api/sys/user.wallet/export`,
        name: `导出用户钱包`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个用户钱包
     */
    get: {
        url: `${config.API_URL}/api/sys/user.wallet/get`,
        name: `获取单个用户钱包`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询用户钱包
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/user.wallet/paged.query`,
        name: `分页查询用户钱包`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 用户钱包求和
     */
    sum: {
        url: `${config.API_URL}/api/sys/user.wallet/sum`,
        name: `用户钱包求和`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}