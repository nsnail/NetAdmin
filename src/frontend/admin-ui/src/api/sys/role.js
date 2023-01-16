/**
 *  角色服务
 *  @module @/api/role
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
     * 批量删除角色
     */
    bulkDelete: {
        url: `${config.API_URL}/api/role/bulk.delete`,
        name: `批量删除角色`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 创建角色
     */
    create: {
        url: `${config.API_URL}/api/role/create`,
        name: `创建角色`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 删除角色
     */
    delete: {
        url: `${config.API_URL}/api/role/delete`,
        name: `删除角色`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 分页查询角色
     */
    pagedQuery: {
        url: `${config.API_URL}/api/role/paged.query`,
        name: `分页查询角色`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 查询角色
     */
    query: {
        url: `${config.API_URL}/api/role/query`,
        name: `查询角色`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 更新角色
     */
    update: {
        url: `${config.API_URL}/api/role/update`,
        name: `更新角色`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


}