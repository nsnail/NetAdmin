/**
 *  商品分类服务
 *  @module @/api/biz/product.category
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除商品分类
 */
bulkDelete :{
    url: `${config.API_URL}/api/biz/product.category/bulk.delete`,
        name: `批量删除商品分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建商品分类
 */
create :{
    url: `${config.API_URL}/api/biz/product.category/create`,
        name: `创建商品分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除商品分类
 */
delete :{
    url: `${config.API_URL}/api/biz/product.category/delete`,
        name: `删除商品分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询商品分类
 */
pagedQuery :{
    url: `${config.API_URL}/api/biz/product.category/paged.query`,
        name: `分页查询商品分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询商品分类
 */
query :{
    url: `${config.API_URL}/api/biz/product.category/query`,
        name: `查询商品分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新商品分类
 */
update :{
    url: `${config.API_URL}/api/biz/product.category/update`,
        name: `更新商品分类`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

}