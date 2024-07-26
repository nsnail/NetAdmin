/**
 *  请求日志服务
 *  @module @/api/sys/request.log
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 请求日志计数
     */
    count: {
        url: `${config.API_URL}/api/sys/request.log/count`,
        name: `请求日志计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 导出请求日志
     */
    export: {
        url: `${config.API_URL}/api/sys/request.log/export`,
        name: `导出请求日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个请求日志
     */
    get: {
        url: `${config.API_URL}/api/sys/request.log/get`,
        name: `获取单个请求日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取条形图数据
     */
    getBarChart: {
        url: `${config.API_URL}/api/sys/request.log/get.bar.chart`,
        name: `获取条形图数据`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 描述分组饼图数据
     */
    getPieChartByApiSummary: {
        url: `${config.API_URL}/api/sys/request.log/get.pie.chart.by.api.summary`,
        name: `描述分组饼图数据`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 状态码分组饼图数据
     */
    getPieChartByHttpStatusCode: {
        url: `${config.API_URL}/api/sys/request.log/get.pie.chart.by.http.status.code`,
        name: `状态码分组饼图数据`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询请求日志
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/request.log/paged.query`,
        name: `分页查询请求日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询请求日志
     */
    query: {
        url: `${config.API_URL}/api/sys/request.log/query`,
        name: `查询请求日志`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}