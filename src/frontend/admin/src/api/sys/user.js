/**
 *  用户服务
 *  @module @/api/sys/user
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 检查邀请码是否正确
     */
    checkInviterAvailable: {
        url: `${config.API_URL}/api/sys/user/check.inviter.available`,
        name: `检查邀请码是否正确`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 检查手机号码是否可用
     */
    checkMobileAvailable: {
        url: `${config.API_URL}/api/sys/user/check.mobile.available`,
        name: `检查手机号码是否可用`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 检查用户名是否可用
     */
    checkUserNameAvailable: {
        url: `${config.API_URL}/api/sys/user/check.user.name.available`,
        name: `检查用户名是否可用`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 用户分组计数
     */
    countBy: {
        url: `${config.API_URL}/api/sys/user/count.by`,
        name: `用户分组计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建用户
     */
    create: {
        url: `${config.API_URL}/api/sys/user/create`,
        name: `创建用户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除用户
     */
    delete: {
        url: `${config.API_URL}/api/sys/user/delete`,
        name: `删除用户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑用户
     */
    edit: {
        url: `${config.API_URL}/api/sys/user/edit`,
        name: `编辑用户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 导出用户
     */
    export: {
        url: `${config.API_URL}/api/sys/user/export`,
        name: `导出用户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个用户
     */
    get: {
        url: `${config.API_URL}/api/sys/user/get`,
        name: `获取单个用户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取当前用户应用配置
     */
    getSessionUserAppConfig: {
        url: `${config.API_URL}/api/sys/user/get.session.user.app.config`,
        name: `获取当前用户应用配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 密码登录
     */
    loginByPwd: {
        url: `${config.API_URL}/api/sys/user/login.by.pwd`,
        name: `密码登录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 短信登录
     */
    loginBySms: {
        url: `${config.API_URL}/api/sys/user/login.by.sms`,
        name: `短信登录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 用户编号登录
     */
    loginByUserId: {
        url: `${config.API_URL}/api/sys/user/login.by.user.id`,
        name: `用户编号登录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询用户
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/user/paged.query`,
        name: `分页查询用户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询用户
     */
    query: {
        url: `${config.API_URL}/api/sys/user/query`,
        name: `查询用户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询用户档案
     */
    queryProfile: {
        url: `${config.API_URL}/api/sys/user/query.profile`,
        name: `查询用户档案`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 注册用户
     */
    register: {
        url: `${config.API_URL}/api/sys/user/register`,
        name: `注册用户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 重设密码
     */
    resetPassword: {
        url: `${config.API_URL}/api/sys/user/reset.password`,
        name: `重设密码`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置用户头像
     */
    setAvatar: {
        url: `${config.API_URL}/api/sys/user/set.avatar`,
        name: `设置用户头像`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置邮箱
     */
    setEmail: {
        url: `${config.API_URL}/api/sys/user/set.email`,
        name: `设置邮箱`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 启用/禁用用户
     */
    setEnabled: {
        url: `${config.API_URL}/api/sys/user/set.enabled`,
        name: `启用/禁用用户`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置手机号码
     */
    setMobile: {
        url: `${config.API_URL}/api/sys/user/set.mobile`,
        name: `设置手机号码`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置密码
     */
    setPassword: {
        url: `${config.API_URL}/api/sys/user/set.password`,
        name: `设置密码`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 设置当前用户应用配置
     */
    setSessionUserAppConfig: {
        url: `${config.API_URL}/api/sys/user/set.session.user.app.config`,
        name: `设置当前用户应用配置`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 当前用户信息
     */
    userInfo: {
        url: `${config.API_URL}/api/sys/user/user.info`,
        name: `当前用户信息`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}