/**
 *  字典服务
 *  @module @/api/dic
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
     * 创建字典
     */
    create :{
        url: `${config.API_URL}/api/dic/create`,
        name: `创建字典`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 删除字典
     */
    delete :{
        url: `${config.API_URL}/api/dic/delete`,
        name: `删除字典`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 分页查询字典
     */
    pagedQuery :{
        url: `${config.API_URL}/api/dic/paged.query`,
        name: `分页查询字典`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 查询字典
     */
    query :{
        url: `${config.API_URL}/api/dic/query`,
        name: `查询字典`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


    /**
     * 更新字典
     */
    update :{
        url: `${config.API_URL}/api/dic/update`,
        name: `更新字典`,
        post:async function(data, config={}) {
            return await http.post(this.url,data, config)
        }
    },


}