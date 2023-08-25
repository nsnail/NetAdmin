/**
 *  文章分类服务
 *  @module @/api/biz/art.category
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除文章分类
 */
bulkDelete :{
    url: `${config.API_URL}/api/biz/art.category/bulk.delete`,
        name: `批量删除文章分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建文章分类
 */
create :{
    url: `${config.API_URL}/api/biz/art.category/create`,
        name: `创建文章分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除文章分类
 */
delete :{
    url: `${config.API_URL}/api/biz/art.category/delete`,
        name: `删除文章分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询文章分类
 */
pagedQuery :{
    url: `${config.API_URL}/api/biz/art.category/paged.query`,
        name: `分页查询文章分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询文章分类
 */
query :{
    url: `${config.API_URL}/api/biz/art.category/query`,
        name: `查询文章分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新文章分类
 */
update :{
    url: `${config.API_URL}/api/biz/art.category/update`,
        name: `更新文章分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

}