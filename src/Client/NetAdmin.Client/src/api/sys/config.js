/**
 *  配置服务
 *  @module @/api/sys/config
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除配置
 */
bulkDelete :{
    url: `${config.API_URL}/api/sys/config/bulk.delete`,
        name: `批量删除配置`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建配置
 */
create :{
    url: `${config.API_URL}/api/sys/config/create`,
        name: `创建配置`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除配置
 */
delete :{
    url: `${config.API_URL}/api/sys/config/delete`,
        name: `删除配置`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 获取最新有效配置
 */
getLatestConfig :{
    url: `${config.API_URL}/api/sys/config/get.latest.config`,
        name: `获取最新有效配置`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询配置
 */
pagedQuery :{
    url: `${config.API_URL}/api/sys/config/paged.query`,
        name: `分页查询配置`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询配置
 */
query :{
    url: `${config.API_URL}/api/sys/config/query`,
        name: `查询配置`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新配置
 */
update :{
    url: `${config.API_URL}/api/sys/config/update`,
        name: `更新配置`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}