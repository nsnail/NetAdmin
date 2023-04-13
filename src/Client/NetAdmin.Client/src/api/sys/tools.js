/**
 *  工具服务
 *  @module @/api/sys/tools
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 服务器时间
 */
getServerUtcTime :{
    url: `${config.API_URL}/api/sys/tools/get.server.utc.time`,
        name: `服务器时间`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 版本信息
 */
version :{
    url: `${config.API_URL}/api/sys/tools/version`,
        name: `版本信息`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}