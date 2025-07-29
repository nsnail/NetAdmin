/**
 *  钱包交易服务
 *  @module @/api/sys/wallet.trade
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除钱包交易
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/wallet.trade/bulk.delete`,
        name: `批量删除钱包交易`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 钱包交易分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/sys/wallet.trade/count.by`,
        name: `钱包交易分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建钱包交易
     */
    create: {
        url: `${config.API_URL}/api/sys/wallet.trade/create`,
        name: `创建钱包交易`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除钱包交易
     */
    delete: {
        url: `${config.API_URL}/api/sys/wallet.trade/delete`,
        name: `删除钱包交易`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑钱包交易
     */
    edit: {
        url: `${config.API_URL}/api/sys/wallet.trade/edit`,
        name: `编辑钱包交易`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个钱包交易
     */
    get: {
        url: `${config.API_URL}/api/sys/wallet.trade/get`,
        name: `获取单个钱包交易`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询钱包交易
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/wallet.trade/paged.query`,
        name: `分页查询钱包交易`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 钱包交易求和
     */
    sum: {
        url: `${config.API_URL}/api/sys/wallet.trade/sum`,
        name: `钱包交易求和`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 从他人账户转账给自己
     */
    transferFromAnotherAccount: {
        url: `${config.API_URL}/api/sys/wallet.trade/transfer.from.another.account`,
        name: `从他人账户转账给自己`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 转账到他人账户
     */
    transferToAnotherAccount: {
        url: `${config.API_URL}/api/sys/wallet.trade/transfer.to.another.account`,
        name: `转账到他人账户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}