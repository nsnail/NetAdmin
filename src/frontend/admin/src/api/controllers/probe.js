/**
 *  探针组件
 *  @module @/api/probe
 */
import config from '@/config'
import http from '@/utils/request'
export default {
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
}