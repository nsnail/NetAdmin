/**
 *  请求日志服务
 *  @module @/api/sys/log
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 分页查询请求日志
 */
pagedQuery :{
    url: `${config.API_URL}/api/sys/log/paged.query`,
        name: `分页查询请求日志`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询请求日志
 */
query :{
    url: `${config.API_URL}/api/sys/log/query`,
        name: `查询请求日志`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}