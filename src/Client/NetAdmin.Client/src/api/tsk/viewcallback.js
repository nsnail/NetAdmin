/**
 *  任务回调视图服务
 *  @module @/api/tsk/view.callback
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 删除任务回调视图
 */
delete :{
    url: `${config.API_URL}/api/tsk/view.callback/delete`,
        name: `删除任务回调视图`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询任务回调视图
 */
pagedQuery :{
    url: `${config.API_URL}/api/tsk/view.callback/paged.query`,
        name: `分页查询任务回调视图`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询任务回调视图
 */
query :{
    url: `${config.API_URL}/api/tsk/view.callback/query`,
        name: `查询任务回调视图`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}