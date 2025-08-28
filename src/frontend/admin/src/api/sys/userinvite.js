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
     * 创建粉丝账号
     */
    createFansAccount: {
        url: `${config.API_URL}/api/sys/user.invite/create.fans.account`,
        name: `创建粉丝账号`,
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
     * 获取自己是否允许自助充值
     */
    getSelfDepositAllowed: {
        url: `${config.API_URL}/api/sys/user.invite/get.self.deposit.allowed`,
        name: `获取自己是否允许自助充值`,
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
     * 查询可分配的角色
     */
    queryRolesAllowApply: {
        url: `${config.API_URL}/api/sys/user.invite/query.roles.allow.apply`,
        name: `查询可分配的角色`,
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

    /**
     * 设置粉丝是否启用
     */
    setEnabled: {
        url: `${config.API_URL}/api/sys/user.invite/set.enabled`,
        name: `设置粉丝是否启用`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 修改粉丝角色
     */
    setFansRole: {
        url: `${config.API_URL}/api/sys/user.invite/set.fans.role`,
        name: `修改粉丝角色`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置上级
     */
    setInviter: {
        url: `${config.API_URL}/api/sys/user.invite/set.inviter`,
        name: `设置上级`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置允许自助充值
     */
    setSelfDepositAllowed: {
        url: `${config.API_URL}/api/sys/user.invite/set.self.deposit.allowed`,
        name: `设置允许自助充值`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}