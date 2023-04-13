/**
 *  人机验证服务
 *  @module @/api/sys/captcha
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 获取人机校验图
 */
getCaptchaImage :{
    url: `${config.API_URL}/api/sys/captcha/get.captcha.image`,
        name: `获取人机校验图`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 完成人机校验
 */
verifyCaptcha :{
    url: `${config.API_URL}/api/sys/captcha/verify.captcha`,
        name: `完成人机校验`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}