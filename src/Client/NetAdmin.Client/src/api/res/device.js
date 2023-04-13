/**
 *  设备服务
 *  @module @/api/res/device
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除设备
 */
bulkDelete :{
    url: `${config.API_URL}/api/res/device/bulk.delete`,
        name: `批量删除设备`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建设备
 */
create :{
    url: `${config.API_URL}/api/res/device/create`,
        name: `创建设备`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除设备
 */
delete :{
    url: `${config.API_URL}/api/res/device/delete`,
        name: `删除设备`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询设备
 */
pagedQuery :{
    url: `${config.API_URL}/api/res/device/paged.query`,
        name: `分页查询设备`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询设备
 */
query :{
    url: `${config.API_URL}/api/res/device/query`,
        name: `查询设备`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新设备
 */
update :{
    url: `${config.API_URL}/api/res/device/update`,
        name: `更新设备`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}