/**
 *  应用服务
 *  @module @/api/cst/app
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除应用
 */
bulkDelete :{
    url: `${config.API_URL}/api/cst/app/bulk.delete`,
        name: `批量删除应用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 批量更新设置位
 */
bulkUpdateEnabled :{
    url: `${config.API_URL}/api/cst/app/bulk.update.enabled`,
        name: `批量更新设置位`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建应用
 */
create :{
    url: `${config.API_URL}/api/cst/app/create`,
        name: `创建应用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除应用
 */
delete :{
    url: `${config.API_URL}/api/cst/app/delete`,
        name: `删除应用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询应用
 */
pagedQuery :{
    url: `${config.API_URL}/api/cst/app/paged.query`,
        name: `分页查询应用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询当前用户的应用
 */
pagedQueryCurrentUser :{
    url: `${config.API_URL}/api/cst/app/paged.query.current.user`,
        name: `分页查询当前用户的应用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询应用
 */
query :{
    url: `${config.API_URL}/api/cst/app/query`,
        name: `查询应用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 注册新应用
 */
registerNewApp :{
    url: `${config.API_URL}/api/cst/app/register.new.app`,
        name: `注册新应用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新应用
 */
update :{
    url: `${config.API_URL}/api/cst/app/update`,
        name: `更新应用`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新设置位
 */
updateEnabled :{
    url: `${config.API_URL}/api/cst/app/update.enabled`,
        name: `更新设置位`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}