/**
 *  接口服务
 *  @module @/api/sys/api
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 查询接口
 */
query :{
    url: `${config.API_URL}/api/sys/api/query`,
        name: `查询接口`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 同步接口
 */
sync :{
    url: `${config.API_URL}/api/sys/api/sync`,
        name: `同步接口`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}