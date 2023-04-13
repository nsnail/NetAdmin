/**
 *  用户-应用映射服务
 *  @module @/api/cst/user.app
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除用户-应用映射
 */
bulkDelete :{
    url: `${config.API_URL}/api/cst/user.app/bulk.delete`,
        name: `批量删除用户-应用映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建用户-应用映射
 */
create :{
    url: `${config.API_URL}/api/cst/user.app/create`,
        name: `创建用户-应用映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除用户-应用映射
 */
delete :{
    url: `${config.API_URL}/api/cst/user.app/delete`,
        name: `删除用户-应用映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询用户-应用映射
 */
pagedQuery :{
    url: `${config.API_URL}/api/cst/user.app/paged.query`,
        name: `分页查询用户-应用映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询用户-应用映射
 */
query :{
    url: `${config.API_URL}/api/cst/user.app/query`,
        name: `查询用户-应用映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新用户-应用映射
 */
update :{
    url: `${config.API_URL}/api/cst/user.app/update`,
        name: `更新用户-应用映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}