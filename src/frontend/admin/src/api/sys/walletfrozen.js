/**
 *  钱包冻结服务
 *  @module @/api/sys/wallet.frozen
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除钱包冻结
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/wallet.frozen/bulk.delete`,
        name: `批量删除钱包冻结`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 钱包冻结分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/sys/wallet.frozen/count.by`,
        name: `钱包冻结分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建钱包冻结
     */
    create: {
        url: `${config.API_URL}/api/sys/wallet.frozen/create`,
        name: `创建钱包冻结`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除钱包冻结
     */
    delete: {
        url: `${config.API_URL}/api/sys/wallet.frozen/delete`,
        name: `删除钱包冻结`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑钱包冻结
     */
    edit: {
        url: `${config.API_URL}/api/sys/wallet.frozen/edit`,
        name: `编辑钱包冻结`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个钱包冻结
     */
    get: {
        url: `${config.API_URL}/api/sys/wallet.frozen/get`,
        name: `获取单个钱包冻结`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询钱包冻结
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/wallet.frozen/paged.query`,
        name: `分页查询钱包冻结`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 将状态设置为解冻
     */
    setStatusToThawed: {
        url: `${config.API_URL}/api/sys/wallet.frozen/set.status.to.thawed`,
        name: `将状态设置为解冻`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}