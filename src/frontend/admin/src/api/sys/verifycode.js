/**
 *  验证码服务
 *  @module @/api/sys/verify.code
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 验证码计数
     */
    count: {
        url: `${config.API_URL}/api/sys/verify.code/count`,
        name: `验证码计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 发送验证码
     */
    sendVerifyCode: {
        url: `${config.API_URL}/api/sys/verify.code/send.verify.code`,
        name: `发送验证码`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 完成验证
     */
    verify: {
        url: `${config.API_URL}/api/sys/verify.code/verify`,
        name: `完成验证`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}