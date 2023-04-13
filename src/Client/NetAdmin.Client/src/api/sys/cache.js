/**
 *  缓存服务
 *  @module @/api/sys/cache
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 缓存统计
 */
cacheStatistics :{
    url: `${config.API_URL}/api/sys/cache/cache.statistics`,
        name: `缓存统计`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 清空缓存
 */
clear :{
    url: `${config.API_URL}/api/sys/cache/clear`,
        name: `清空缓存`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}