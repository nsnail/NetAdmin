/**
 *  文章服务
 *  @module @/api/biz/article
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除文章
 */
bulkDelete :{
    url: `${config.API_URL}/api/biz/article/bulk.delete`,
        name: `批量删除文章`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建文章
 */
create :{
    url: `${config.API_URL}/api/biz/article/create`,
        name: `创建文章`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除文章
 */
delete :{
    url: `${config.API_URL}/api/biz/article/delete`,
        name: `删除文章`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询文章
 */
pagedQuery :{
    url: `${config.API_URL}/api/biz/article/paged.query`,
        name: `分页查询文章`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询文章
 */
query :{
    url: `${config.API_URL}/api/biz/article/query`,
        name: `查询文章`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新文章
 */
update :{
    url: `${config.API_URL}/api/biz/article/update`,
        name: `更新文章`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

}