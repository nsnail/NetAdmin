/**
 *  接口服务
 *  @module @/api/sys/api
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 接口计数
     */
    count: {
        url: `${config.API_URL}/api/sys/api/count`,
        name: `接口计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 导出接口
     */
    export: {
        url: `${config.API_URL}/api/sys/api/export`,
        name: `导出接口`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 平面查询接口
     */
    plainQuery: {
        url: `${config.API_URL}/api/sys/api/plain.query`,
        name: `平面查询接口`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询接口
     */
    query: {
        url: `${config.API_URL}/api/sys/api/query`,
        name: `查询接口`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 同步接口
     */
    sync: {
        url: `${config.API_URL}/api/sys/api/sync`,
        name: `同步接口`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}