/**
 *  机器人分配服务
 *  @module @/api/res/robot.alloc
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除机器人分配
 */
bulkDelete :{
    url: `${config.API_URL}/api/res/robot.alloc/bulk.delete`,
        name: `批量删除机器人分配`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建机器人分配
 */
create :{
    url: `${config.API_URL}/api/res/robot.alloc/create`,
        name: `创建机器人分配`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除机器人分配
 */
delete :{
    url: `${config.API_URL}/api/res/robot.alloc/delete`,
        name: `删除机器人分配`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询机器人分配
 */
pagedQuery :{
    url: `${config.API_URL}/api/res/robot.alloc/paged.query`,
        name: `分页查询机器人分配`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询机器人分配
 */
query :{
    url: `${config.API_URL}/api/res/robot.alloc/query`,
        name: `查询机器人分配`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新机器人分配
 */
update :{
    url: `${config.API_URL}/api/res/robot.alloc/update`,
        name: `更新机器人分配`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}