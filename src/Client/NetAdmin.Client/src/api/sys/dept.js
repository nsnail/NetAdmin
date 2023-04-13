/**
 *  部门服务
 *  @module @/api/sys/dept
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除部门
 */
bulkDelete :{
    url: `${config.API_URL}/api/sys/dept/bulk.delete`,
        name: `批量删除部门`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建部门
 */
create :{
    url: `${config.API_URL}/api/sys/dept/create`,
        name: `创建部门`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除部门
 */
delete :{
    url: `${config.API_URL}/api/sys/dept/delete`,
        name: `删除部门`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询部门
 */
query :{
    url: `${config.API_URL}/api/sys/dept/query`,
        name: `查询部门`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新部门
 */
update :{
    url: `${config.API_URL}/api/sys/dept/update`,
        name: `更新部门`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}