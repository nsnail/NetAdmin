/**
 *  常量服务
 *  @module @/api/sys/constant
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 获得常量字符串
 */
getChars :{
    url: `${config.API_URL}/api/sys/constant/get.chars`,
        name: `获得常量字符串`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 获得公共枚举值
 */
getEnums :{
    url: `${config.API_URL}/api/sys/constant/get.enums`,
        name: `获得公共枚举值`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 获得本地化字符串
 */
getLocalizedStrings :{
    url: `${config.API_URL}/api/sys/constant/get.localized.strings`,
        name: `获得本地化字符串`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 获得数字常量表
 */
getNumbers :{
    url: `${config.API_URL}/api/sys/constant/get.numbers`,
        name: `获得数字常量表`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}