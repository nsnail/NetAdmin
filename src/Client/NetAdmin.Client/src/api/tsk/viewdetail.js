/**
 *  任务明细视图服务
 *  @module @/api/tsk/view.detail
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 分页查询任务明细视图
 */
pagedQuery :{
    url: `${config.API_URL}/api/tsk/view.detail/paged.query`,
        name: `分页查询任务明细视图`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询任务明细视图
 */
query :{
    url: `${config.API_URL}/api/tsk/view.detail/query`,
        name: `查询任务明细视图`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}