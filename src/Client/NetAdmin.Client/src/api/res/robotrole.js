/**
 *  机器人角色服务
 *  @module @/api/res/robot.role
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除机器人角色
 */
bulkDelete :{
    url: `${config.API_URL}/api/res/robot.role/bulk.delete`,
        name: `批量删除机器人角色`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建机器人角色
 */
create :{
    url: `${config.API_URL}/api/res/robot.role/create`,
        name: `创建机器人角色`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除机器人角色
 */
delete :{
    url: `${config.API_URL}/api/res/robot.role/delete`,
        name: `删除机器人角色`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询机器人角色
 */
pagedQuery :{
    url: `${config.API_URL}/api/res/robot.role/paged.query`,
        name: `分页查询机器人角色`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询机器人角色
 */
query :{
    url: `${config.API_URL}/api/res/robot.role/query`,
        name: `查询机器人角色`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新机器人角色
 */
update :{
    url: `${config.API_URL}/api/res/robot.role/update`,
        name: `更新机器人角色`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}