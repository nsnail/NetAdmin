/**
 *  角色接口
 *  @module @/api/role
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
     * 批量删除角色
     */
    bulkDelete :{
        url: `${config.API_URL}/api/role/bulk.delete`,
        name: `批量删除角色`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 创建角色
     */
    create :{
        url: `${config.API_URL}/api/role/create`,
        name: `创建角色`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 删除角色
     */
    delete :{
        url: `${config.API_URL}/api/role/delete`,
        name: `删除角色`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 获取角色菜单
     */
    getMenus :{
        url: `${config.API_URL}/api/role/get.menus`,
        name: `获取角色菜单`,
        get:async function(data, config={}) {
            return await http.get(this.url,data, config)
        }
    },


    /**
     * 角色端点映射
     */
    mapEndpoints :{
        url: `${config.API_URL}/api/role/map.endpoints`,
        name: `角色端点映射`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 角色-菜单映射
     */
    mapMenus :{
        url: `${config.API_URL}/api/role/map.menus`,
        name: `角色-菜单映射`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 分页查询角色
     */
    pagedQuery :{
        url: `${config.API_URL}/api/role/paged.query`,
        name: `分页查询角色`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 查询角色
     */
    query :{
        url: `${config.API_URL}/api/role/query`,
        name: `查询角色`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 更新角色
     */
    update :{
        url: `${config.API_URL}/api/role/update`,
        name: `更新角色`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


}