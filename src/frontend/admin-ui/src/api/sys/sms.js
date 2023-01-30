/**
 *  短信服务
 *  @module @/api/sms
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 分页查询短信
 */
pagedQuery :{
    url: `${config.API_URL}/api/sms/paged.query`,
        name: `分页查询短信`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询短信
 */
query :{
    url: `${config.API_URL}/api/sms/query`,
        name: `查询短信`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 发送短信验证码
 */
sendSmsCode :{
    url: `${config.API_URL}/api/sms/send.sms.code`,
        name: `发送短信验证码`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 完成短信验证
 */
verifySmsCode :{
    url: `${config.API_URL}/api/sms/verify.sms.code`,
        name: `完成短信验证`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}