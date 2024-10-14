/**
 *  探针组件
 *  @module @/api/probe
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 退出程序
     */
    exit: {
        url: `${config.API_URL}/api/probe/exit`,
        name: `退出程序`,
        get: async function (data = {}, config = {}) {
            return await http.get(this.url, data, config)
        },
    },

    /**
     * 健康检查
     */
    healthCheck: {
        url: `${config.API_URL}/api/probe/health.check`,
        name: `健康检查`,
        get: async function (data = {}, config = {}) {
            return await http.get(this.url, data, config)
        },
    },

    /**
     * 系统是否已经安全停止
     */
    isSystemSafetyStopped: {
        url: `${config.API_URL}/api/probe/is.system.safety.stopped`,
        name: `系统是否已经安全停止`,
        get: async function (data = {}, config = {}) {
            return await http.get(this.url, data, config)
        },
    },

    /**
     * 实例下线
     */
    offline: {
        url: `${config.API_URL}/api/probe/offline`,
        name: `实例下线`,
        get: async function (data = {}, config = {}) {
            return await http.get(this.url, data, config)
        },
    },

    /**
     * 停止日志计数器
     */
    stopLogCounter: {
        url: `${config.API_URL}/api/probe/stop.log.counter`,
        name: `停止日志计数器`,
        get: async function (data = {}, config = {}) {
            return await http.get(this.url, data, config)
        },
    },
}