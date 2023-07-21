/**
 *  商品服务
 *  @module @/api/biz/product
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除商品
 */
bulkDelete :{
    url: `${config.API_URL}/api/biz/product/bulk.delete`,
        name: `批量删除商品`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建商品
 */
create :{
    url: `${config.API_URL}/api/biz/product/create`,
        name: `创建商品`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除商品
 */
delete :{
    url: `${config.API_URL}/api/biz/product/delete`,
        name: `删除商品`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 获取单个商品
 */
get :{
    url: `${config.API_URL}/api/biz/product/get`,
        name: `获取单个商品`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询商品
 */
pagedQuery :{
    url: `${config.API_URL}/api/biz/product/paged.query`,
        name: `分页查询商品`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询商品
 */
query :{
    url: `${config.API_URL}/api/biz/product/query`,
        name: `查询商品`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新商品
 */
update :{
    url: `${config.API_URL}/api/biz/product/update`,
        name: `更新商品`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

}