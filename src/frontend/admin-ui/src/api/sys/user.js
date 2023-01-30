/**
 *  用户服务
 *  @module @/api/user
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 检查手机号是否可用
 */
checkMobileAvaliable :{
    url: `${config.API_URL}/api/user/check.mobile.avaliable`,
        name: `检查手机号是否可用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 检查用户名是否可用
 */
checkUserNameAvaliable :{
    url: `${config.API_URL}/api/user/check.user.name.avaliable`,
        name: `检查用户名是否可用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建用户
 */
create :{
    url: `${config.API_URL}/api/user/create`,
        name: `创建用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询用户
 */
pagedQuery :{
    url: `${config.API_URL}/api/user/paged.query`,
        name: `分页查询用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 密码登录
 */
pwdLogin :{
    url: `${config.API_URL}/api/user/pwd.login`,
        name: `密码登录`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询用户
 */
query :{
    url: `${config.API_URL}/api/user/query`,
        name: `查询用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询用户档案
 */
queryProfile :{
    url: `${config.API_URL}/api/user/query.profile`,
        name: `查询用户档案`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 注册用户
 */
register :{
    url: `${config.API_URL}/api/user/register`,
        name: `注册用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 重设密码
 */
resetPassword :{
    url: `${config.API_URL}/api/user/reset.password`,
        name: `重设密码`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 短信登录
 */
smsLogin :{
    url: `${config.API_URL}/api/user/sms.login`,
        name: `短信登录`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新用户
 */
update :{
    url: `${config.API_URL}/api/user/update`,
        name: `更新用户`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 当前用户信息
 */
userInfo :{
    url: `${config.API_URL}/api/user/user.info`,
        name: `当前用户信息`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}