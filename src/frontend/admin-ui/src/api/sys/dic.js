/**
 *  字典服务
 *  @module @/api/dic
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
     * 创建字典目录
     */
    createCatalog: {
        url: `${config.API_URL}/api/dic/create.catalog`,
        name: `创建字典目录`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 创建字典内容
     */
    createContent: {
        url: `${config.API_URL}/api/dic/create.content`,
        name: `创建字典内容`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 删除字典目录
     */
    deleteCatalog: {
        url: `${config.API_URL}/api/dic/delete.catalog`,
        name: `删除字典目录`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 删除字典内容
     */
    deleteContent: {
        url: `${config.API_URL}/api/dic/delete.content`,
        name: `删除字典内容`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 分页查询字典目录
     */
    pagedQueryCatalog: {
        url: `${config.API_URL}/api/dic/paged.query.catalog`,
        name: `分页查询字典目录`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 分页查询字典内容
     */
    pagedQueryContent: {
        url: `${config.API_URL}/api/dic/paged.query.content`,
        name: `分页查询字典内容`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 查询字典目录
     */
    queryCatalog: {
        url: `${config.API_URL}/api/dic/query.catalog`,
        name: `查询字典目录`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 查询字典内容
     */
    queryContent: {
        url: `${config.API_URL}/api/dic/query.content`,
        name: `查询字典内容`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 更新字典目录
     */
    updateCatalog: {
        url: `${config.API_URL}/api/dic/update.catalog`,
        name: `更新字典目录`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


    /**
     * 更新字典内容
     */
    updateContent: {
        url: `${config.API_URL}/api/dic/update.content`,
        name: `更新字典内容`,
        post: async function (data, config = {}) {
            return await http.post(this.url, data, config)
        }
    },


}