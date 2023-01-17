/**
 *  接口服务
 *  @module @/api/api
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 查询接口
 */
query :{
    url: `${config.API_URL}/api/api/query`,
        name: `查询接口`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},


/**
 * 同步接口
 */
sync :{
    url: `${config.API_URL}/api/api/sync`,
        name: `同步接口`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},


}