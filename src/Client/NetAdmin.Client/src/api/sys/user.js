/**
 *  用户服务
 *  @module @/api/sys/user
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 检查手机号是否可用
 */
checkMobileAvailable :{
    url: `${config.API_URL}/api/sys/user/check.mobile.available`,
        name: `检查手机号是否可用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 检查用户名是否可用
 */
checkUserNameAvailable :{
    url: `${config.API_URL}/api/sys/user/check.user.name.available`,
        name: `检查用户名是否可用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建用户
 */
create :{
    url: `${config.API_URL}/api/sys/user/create`,
        name: `创建用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询用户
 */
pagedQuery :{
    url: `${config.API_URL}/api/sys/user/paged.query`,
        name: `分页查询用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 密码登录
 */
pwdLogin :{
    url: `${config.API_URL}/api/sys/user/pwd.login`,
        name: `密码登录`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询用户
 */
query :{
    url: `${config.API_URL}/api/sys/user/query`,
        name: `查询用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询用户档案
 */
queryProfile :{
    url: `${config.API_URL}/api/sys/user/query.profile`,
        name: `查询用户档案`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 注册用户
 */
register :{
    url: `${config.API_URL}/api/sys/user/register`,
        name: `注册用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 重设密码
 */
resetPassword :{
    url: `${config.API_URL}/api/sys/user/reset.password`,
        name: `重设密码`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 短信登录
 */
smsLogin :{
    url: `${config.API_URL}/api/sys/user/sms.login`,
        name: `短信登录`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新用户
 */
update :{
    url: `${config.API_URL}/api/sys/user/update`,
        name: `更新用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 当前用户信息
 */
userInfo :{
    url: `${config.API_URL}/api/sys/user/user.info`,
        name: `当前用户信息`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}