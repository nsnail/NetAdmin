/**
 *  工具服务
 *  @module @/api/sys/tools
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 获取更新日志
     */
    getChangeLog: {
        url: `${config.API_URL}/api/sys/tools/get.change.log`,
        name: `获取更新日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取模块信息
     */
    getModules: {
        url: `${config.API_URL}/api/sys/tools/get.modules`,
        name: `获取模块信息`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取服务器时间
     */
    getServerUtcTime: {
        url: `${config.API_URL}/api/sys/tools/get.server.utc.time`,
        name: `获取服务器时间`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取版本信息
     */
    getVersion: {
        url: `${config.API_URL}/api/sys/tools/get.version`,
        name: `获取版本信息`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}