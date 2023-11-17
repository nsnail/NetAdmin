/**
 *  缓存服务
 *  @module @/api/sys/cache
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 缓存统计
     */
    cacheStatistics: {
        url: `${config.API_URL}/api/sys/cache/cache.statistics`,
        name: `缓存统计`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取所有缓存项
     */
    getAllEntries: {
        url: `${config.API_URL}/api/sys/cache/get.all.entries`,
        name: `获取所有缓存项`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}