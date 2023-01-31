/**
 *  示例服务
 *  @module @/api/example
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除示例
 */
bulkDelete :{
    url: `${config.API_URL}/api/example/bulk.delete`,
        name: `批量删除示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建示例
 */
create :{
    url: `${config.API_URL}/api/example/create`,
        name: `创建示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除示例
 */
delete :{
    url: `${config.API_URL}/api/example/delete`,
        name: `删除示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询示例
 */
pagedQuery :{
    url: `${config.API_URL}/api/example/paged.query`,
        name: `分页查询示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询示例
 */
query :{
    url: `${config.API_URL}/api/example/query`,
        name: `查询示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新示例
 */
update :{
    url: `${config.API_URL}/api/example/update`,
        name: `更新示例`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}