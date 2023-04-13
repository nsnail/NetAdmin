/**
 *  机器人服务
 *  @module @/api/res/robot
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除机器人
 */
bulkDelete :{
    url: `${config.API_URL}/api/res/robot/bulk.delete`,
        name: `批量删除机器人`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建机器人
 */
create :{
    url: `${config.API_URL}/api/res/robot/create`,
        name: `创建机器人`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除机器人
 */
delete :{
    url: `${config.API_URL}/api/res/robot/delete`,
        name: `删除机器人`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询机器人
 */
pagedQuery :{
    url: `${config.API_URL}/api/res/robot/paged.query`,
        name: `分页查询机器人`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询机器人
 */
query :{
    url: `${config.API_URL}/api/res/robot/query`,
        name: `查询机器人`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新机器人
 */
update :{
    url: `${config.API_URL}/api/res/robot/update`,
        name: `更新机器人`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}