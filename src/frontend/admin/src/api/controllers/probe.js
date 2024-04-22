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
}