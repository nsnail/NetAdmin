/**
 *  日志服务
 *  @module @/api/log
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
     * 分页查询登录日志
     */
    pagedQueryLoginLog :{
        url: `${config.API_URL}/api/log/paged.query.login.log`,
        name: `分页查询登录日志`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 分页查询操作日志
     */
    pagedQueryOperationLog :{
        url: `${config.API_URL}/api/log/paged.query.operation.log`,
        name: `分页查询操作日志`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


}