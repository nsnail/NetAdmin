/**
 *  示例服务
 *  @module @/api/tpl/example
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除示例
 */
bulkDelete :{
    url: `${config.API_URL}/api/tpl/example/bulk.delete`,
        name: `批量删除示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建示例
 */
create :{
    url: `${config.API_URL}/api/tpl/example/create`,
        name: `创建示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除示例
 */
delete :{
    url: `${config.API_URL}/api/tpl/example/delete`,
        name: `删除示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询示例
 */
pagedQuery :{
    url: `${config.API_URL}/api/tpl/example/paged.query`,
        name: `分页查询示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询示例
 */
query :{
    url: `${config.API_URL}/api/tpl/example/query`,
        name: `查询示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新示例
 */
update :{
    url: `${config.API_URL}/api/tpl/example/update`,
        name: `更新示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}