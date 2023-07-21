/**
 *  文章详情服务
 *  @module @/api/biz/article.detail
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除文章详情
 */
bulkDelete :{
    url: `${config.API_URL}/api/biz/article.detail/bulk.delete`,
        name: `批量删除文章详情`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建文章详情
 */
create :{
    url: `${config.API_URL}/api/biz/article.detail/create`,
        name: `创建文章详情`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除文章详情
 */
delete :{
    url: `${config.API_URL}/api/biz/article.detail/delete`,
        name: `删除文章详情`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询文章详情
 */
pagedQuery :{
    url: `${config.API_URL}/api/biz/article.detail/paged.query`,
        name: `分页查询文章详情`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询文章详情
 */
query :{
    url: `${config.API_URL}/api/biz/article.detail/query`,
        name: `查询文章详情`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新文章详情
 */
update :{
    url: `${config.API_URL}/api/biz/article.detail/update`,
        name: `更新文章详情`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

}