/**
 *  任务视图服务
 *  @module @/api/tsk/view
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 分页查询任务视图
 */
pagedQuery :{
    url: `${config.API_URL}/api/tsk/view/paged.query`,
        name: `分页查询任务视图`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询任务视图
 */
query :{
    url: `${config.API_URL}/api/tsk/view/query`,
        name: `查询任务视图`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}