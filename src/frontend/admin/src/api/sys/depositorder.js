/**
 *  充值订单服务
 *  @module @/api/sys/deposit.order
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除充值订单
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/deposit.order/bulk.delete`,
        name: `批量删除充值订单`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 充值订单计数
     */
    count: {
        url: `${config.API_URL}/api/sys/deposit.order/count`,
        name: `充值订单计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 充值订单分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/sys/deposit.order/count.by`,
        name: `充值订单分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建充值订单
     */
    create: {
        url: `${config.API_URL}/api/sys/deposit.order/create`,
        name: `创建充值订单`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除充值订单
     */
    delete: {
        url: `${config.API_URL}/api/sys/deposit.order/delete`,
        name: `删除充值订单`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑充值订单
     */
    edit: {
        url: `${config.API_URL}/api/sys/deposit.order/edit`,
        name: `编辑充值订单`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个充值订单
     */
    get: {
        url: `${config.API_URL}/api/sys/deposit.order/get`,
        name: `获取单个充值订单`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取充值配置
     */
    getDepositConfig: {
        url: `${config.API_URL}/api/sys/deposit.order/get.deposit.config`,
        name: `获取充值配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询充值订单
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/deposit.order/paged.query`,
        name: `分页查询充值订单`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 支付确认
     */
    payConfirm: {
        url: `${config.API_URL}/api/sys/deposit.order/pay.confirm`,
        name: `支付确认`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 到账确认
     */
    receivedConfirmation: {
        url: `${config.API_URL}/api/sys/deposit.order/received.confirmation`,
        name: `到账确认`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}