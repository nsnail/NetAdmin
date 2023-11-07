import Request from '/@/utils/Request';

export default {
    /**
     * 检查手机号是否可用
     */
    checkMobileAvailable: {
        url: `/sys/user/check.mobile.available`,
        name: `检查手机号是否可用`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 检查用户名是否可用
     */
    checkUserNameAvailable: {
        url: `/sys/user/check.user.name.available`,
        name: `检查用户名是否可用`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 创建用户
     */
    create: {
        url: `/sys/user/create`,
        name: `创建用户`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 删除用户
     */
    delete: {
        url: `/sys/user/delete`,
        name: `删除用户`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 获取单个用户
     */
    get: {
        url: `/sys/user/get`,
        name: `获取单个用户`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 密码登录
     */
    loginByPwd: {
        url: `/sys/user/login.by.pwd`,
        name: `密码登录`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 短信登录
     */
    loginBySms: {
        url: `/sys/user/login.by.sms`,
        name: `短信登录`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 分页查询用户
     */
    pagedQuery: {
        url: `/sys/user/paged.query`,
        name: `分页查询用户`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 查询用户
     */
    query: {
        url: `/sys/user/query`,
        name: `查询用户`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 查询用户档案
     */
    queryProfile: {
        url: `/sys/user/query.profile`,
        name: `查询用户档案`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 注册用户
     */
    register: {
        url: `/sys/user/register`,
        name: `注册用户`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 重设密码
     */
    resetPassword: {
        url: `/sys/user/reset.password`,
        name: `重设密码`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 更新用户头像
     */
    setAvatar: {
        url: `/sys/user/set.avatar`,
        name: `更新用户头像`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 设置邮箱
     */
    setEmail: {
        url: `/sys/user/set.email`,
        name: `设置邮箱`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 设置手机号
     */
    setMobile: {
        url: `/sys/user/set.mobile`,
        name: `设置手机号`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 设置密码
     */
    setPassword: {
        url: `/sys/user/set.password`,
        name: `设置密码`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 更新用户
     */
    update: {
        url: `/sys/user/update`,
        name: `更新用户`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },

    /**
     * 当前用户信息
     */
    userInfo: {
        url: `/sys/user/user.info`,
        name: `当前用户信息`,
        post: async function (data = {}, config = {}) {
            return await Request.post(this.url, data, config);
        },
    },
};