/**
 *  岗位服务
 *  @module @/api/position
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除岗位
 */
bulkDelete :{
    url: `${config.API_URL}/api/position/bulk.delete`,
        name: `批量删除岗位`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建岗位
 */
create :{
    url: `${config.API_URL}/api/position/create`,
        name: `创建岗位`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除岗位
 */
delete :{
    url: `${config.API_URL}/api/position/delete`,
        name: `删除岗位`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询岗位
 */
pagedQuery :{
    url: `${config.API_URL}/api/position/paged.query`,
        name: `分页查询岗位`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询岗位
 */
query :{
    url: `${config.API_URL}/api/position/query`,
        name: `查询岗位`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新岗位
 */
update :{
    url: `${config.API_URL}/api/position/update`,
        name: `更新岗位`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}