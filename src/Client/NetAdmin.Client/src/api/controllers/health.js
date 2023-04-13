/**
 *  健康控制器
 *  @module @/api/health
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 健康检查
 */
check :{
    url: `${config.API_URL}/api/health/check`,
        name: `健康检查`,
        get:async function(data, config={}) {
        return await http.get(this.url,data, config)
    }
},

}