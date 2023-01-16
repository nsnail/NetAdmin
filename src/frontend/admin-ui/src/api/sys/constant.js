/**
 *  常量服务
 *  @module @/api/constant
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
     * 获得公共枚举值
     */
    getEnums :{
        url: `${config.API_URL}/api/constant/get.enums`,
        name: `获得公共枚举值`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 获得本地化字符串
     */
    getLocalizedStrings :{
        url: `${config.API_URL}/api/constant/get.localized.strings`,
        name: `获得本地化字符串`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 获得常量字符串
     */
    getStrings :{
        url: `${config.API_URL}/api/constant/get.strings`,
        name: `获得常量字符串`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


}