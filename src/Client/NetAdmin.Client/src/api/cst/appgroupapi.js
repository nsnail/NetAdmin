/**
 *  应用分组-接口映射服务
 *  @module @/api/cst/app.group.api
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除应用分组-接口映射
 */
bulkDelete :{
    url: `${config.API_URL}/api/cst/app.group.api/bulk.delete`,
        name: `批量删除应用分组-接口映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建应用分组-接口映射
 */
create :{
    url: `${config.API_URL}/api/cst/app.group.api/create`,
        name: `创建应用分组-接口映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除应用分组-接口映射
 */
delete :{
    url: `${config.API_URL}/api/cst/app.group.api/delete`,
        name: `删除应用分组-接口映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询应用分组-接口映射
 */
pagedQuery :{
    url: `${config.API_URL}/api/cst/app.group.api/paged.query`,
        name: `分页查询应用分组-接口映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询应用分组-接口映射
 */
query :{
    url: `${config.API_URL}/api/cst/app.group.api/query`,
        name: `查询应用分组-接口映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新应用分组-接口映射
 */
update :{
    url: `${config.API_URL}/api/cst/app.group.api/update`,
        name: `更新应用分组-接口映射`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}