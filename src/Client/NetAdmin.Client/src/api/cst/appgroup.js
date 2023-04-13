/**
 *  应用分组服务
 *  @module @/api/cst/app.group
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除应用分组
 */
bulkDelete :{
    url: `${config.API_URL}/api/cst/app.group/bulk.delete`,
        name: `批量删除应用分组`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建应用分组
 */
create :{
    url: `${config.API_URL}/api/cst/app.group/create`,
        name: `创建应用分组`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除应用分组
 */
delete :{
    url: `${config.API_URL}/api/cst/app.group/delete`,
        name: `删除应用分组`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询应用分组
 */
pagedQuery :{
    url: `${config.API_URL}/api/cst/app.group/paged.query`,
        name: `分页查询应用分组`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询应用分组
 */
query :{
    url: `${config.API_URL}/api/cst/app.group/query`,
        name: `查询应用分组`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新应用分组
 */
update :{
    url: `${config.API_URL}/api/cst/app.group/update`,
        name: `更新应用分组`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}