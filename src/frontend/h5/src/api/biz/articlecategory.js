/**
 *  文章分类映射服务
 *  @module @/api/biz/article.category
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除文章分类映射
 */
bulkDelete :{
    url: `${config.API_URL}/api/biz/article.category/bulk.delete`,
        name: `批量删除文章分类映射`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建文章分类映射
 */
create :{
    url: `${config.API_URL}/api/biz/article.category/create`,
        name: `创建文章分类映射`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除文章分类映射
 */
delete :{
    url: `${config.API_URL}/api/biz/article.category/delete`,
        name: `删除文章分类映射`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询文章分类映射
 */
pagedQuery :{
    url: `${config.API_URL}/api/biz/article.category/paged.query`,
        name: `分页查询文章分类映射`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询文章分类映射
 */
query :{
    url: `${config.API_URL}/api/biz/article.category/query`,
        name: `查询文章分类映射`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新文章分类映射
 */
update :{
    url: `${config.API_URL}/api/biz/article.category/update`,
        name: `更新文章分类映射`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

}