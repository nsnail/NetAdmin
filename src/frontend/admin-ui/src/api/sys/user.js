/**
 *  用户服务
 *  @module @/api/user
 */

import config from "@/config"
import http from "@/utils/request"

export default {

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
 * 用户登录
 */
login :{
    url: `${config.API_URL}/api/user/login`,
        name: `用户登录`,
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