/**
 *  角色接口
 *  @module @/api/role
 */

import config from "@/config"
import http from "@/utils/request"

export default {

/**
 * 创建角色
 */
create :{
  url: `${config.API_URL}/role/create`,
  name: `创建角色`,
  post:async function(data, config={}) {
    return await http.post(this.url,data, config)
  }
},


/**
 * 删除角色
 */
deleteAsync :{
  url: `${config.API_URL}/role/delete`,
  name: `删除角色`,
  post:async function(data, config={}) {
    return await http.post(this.url,data, config)
  }
},


/**
 * 角色端点映射
 */
mapEndpoints :{
  url: `${config.API_URL}/role/map.endpoints`,
  name: `角色端点映射`,
  post:async function(data, config={}) {
    return await http.post(this.url,data, config)
  }
},


/**
 * 查询角色
 */
query :{
  url: `${config.API_URL}/role/query`,
  name: `查询角色`,
  post:async function(data, config={}) {
    return await http.post(this.url,data, config)
  }
},


/**
 * 更新角色
 */
update :{
  url: `${config.API_URL}/role/update`,
  name: `更新角色`,
  post:async function(data, config={}) {
    return await http.post(this.url,data, config)
  }
},

}