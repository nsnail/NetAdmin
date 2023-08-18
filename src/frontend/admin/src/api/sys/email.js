/**
 *  邮件服务
 *  @module @/api/sys/email
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 发送邮箱验证码
 */
sendEmailCode :{
    url: `${config.API_URL}/api/sys/email/send.email.code`,
        name: `发送邮箱验证码`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 完成邮箱验证
 */
verifyEmailCode :{
    url: `${config.API_URL}/api/sys/email/verify.email.code`,
        name: `完成邮箱验证`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

}