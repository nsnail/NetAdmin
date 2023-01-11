/**
 *  端点接口
 *  @module @/api/end.point
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
     * 生成前端代码
     */
    generateJsCode :{
        url: `${config.API_URL}/api/end.point/generate.js.code`,
        name: `生成前端代码`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 端点列表
     */
    list :{
        url: `${config.API_URL}/api/end.point/list`,
        name: `端点列表`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


}