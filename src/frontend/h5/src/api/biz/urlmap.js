/**
 *  文章地址服务
 *  @module @/api/biz/url.map
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除文章地址
 */
bulkDelete :{
    url: `${config.API_URL}/api/biz/url.map/bulk.delete`,
        name: `批量删除文章地址`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建文章地址
 */
create :{
    url: `${config.API_URL}/api/biz/url.map/create`,
        name: `创建文章地址`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除文章地址
 */
delete :{
    url: `${config.API_URL}/api/biz/url.map/delete`,
        name: `删除文章地址`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询文章地址
 */
pagedQuery :{
    url: `${config.API_URL}/api/biz/url.map/paged.query`,
        name: `分页查询文章地址`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询文章地址
 */
query :{
    url: `${config.API_URL}/api/biz/url.map/query`,
        name: `查询文章地址`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新文章地址
 */
update :{
    url: `${config.API_URL}/api/biz/url.map/update`,
        name: `更新文章地址`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

}