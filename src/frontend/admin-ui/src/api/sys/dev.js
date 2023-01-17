/**
 *  开发服务
 *  @module @/api/dev
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 生成后端代码
 */
generateCsCode :{
    url: `${config.API_URL}/api/dev/generate.cs.code`,
        name: `生成后端代码`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},


/**
 * 生成图标代码
 */
generateIconCode :{
    url: `${config.API_URL}/api/dev/generate.icon.code`,
        name: `生成图标代码`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},


/**
 * 生成接口代码
 */
generateJsCode :{
    url: `${config.API_URL}/api/dev/generate.js.code`,
        name: `生成接口代码`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},


}