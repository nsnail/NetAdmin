/**
 *  计划作业服务
 *  @module @/api/sys/job
 */
import config from '@/config'
import http from '@/utils/request'
export default {
    /**
     * 批量删除计划作业
     */
    bulkDelete: {
        url: `${config.API_URL}/api/sys/job/bulk.delete`,
        name: `批量删除计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 计划作业计数
     */
    count: {
        url: `${config.API_URL}/api/sys/job/count`,
        name: `计划作业计数`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 创建计划作业
     */
    create: {
        url: `${config.API_URL}/api/sys/job/create`,
        name: `创建计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 删除计划作业
     */
    delete: {
        url: `${config.API_URL}/api/sys/job/delete`,
        name: `删除计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 编辑作业
     */
    edit: {
        url: `${config.API_URL}/api/sys/job/edit`,
        name: `编辑作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 执行作业
     */
    execute: {
        url: `${config.API_URL}/api/sys/job/execute`,
        name: `执行作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 计划作业是否存在
     */
    exist: {
        url: `${config.API_URL}/api/sys/job/exist`,
        name: `计划作业是否存在`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个计划作业
     */
    get: {
        url: `${config.API_URL}/api/sys/job/get`,
        name: `获取单个计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取作业记录条形图数据
     */
    getRecordBarChart: {
        url: `${config.API_URL}/api/sys/job/get.record.bar.chart`,
        name: `获取作业记录条形图数据`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 状态码分组作业记录饼图数据
     */
    getRecordPieChartByHttpStatusCode: {
        url: `${config.API_URL}/api/sys/job/get.record.pie.chart.by.http.status.code`,
        name: `状态码分组作业记录饼图数据`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 名称分组作业记录饼图数据
     */
    getRecordPieChartByName: {
        url: `${config.API_URL}/api/sys/job/get.record.pie.chart.by.name`,
        name: `名称分组作业记录饼图数据`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询计划作业
     */
    pagedQuery: {
        url: `${config.API_URL}/api/sys/job/paged.query`,
        name: `分页查询计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 查询计划作业
     */
    query: {
        url: `${config.API_URL}/api/sys/job/query`,
        name: `查询计划作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 获取单个作业记录
     */
    recordGet: {
        url: `${config.API_URL}/api/sys/job/record.get`,
        name: `获取单个作业记录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 分页查询作业记录
     */
    recordPagedQuery: {
        url: `${config.API_URL}/api/sys/job/record.paged.query`,
        name: `分页查询作业记录`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },

    /**
     * 启用/禁用作业
     */
    setEnabled: {
        url: `${config.API_URL}/api/sys/job/set.enabled`,
        name: `启用/禁用作业`,
        post: async function (data = {}, config = {}) {
            return await http.post(this.url, data, config)
        },
    },
}