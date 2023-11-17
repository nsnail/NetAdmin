import axios from 'axios'
import { ElMessageBox, ElNotification } from 'element-plus'
import sysConfig from '@/config'
import tool from '@/utils/tool'
import router from '@/router'
import { h } from 'vue'
import jsonBigInt from 'json-bigint'

axios.defaults.baseURL = ''

axios.defaults.timeout = sysConfig.TIMEOUT

// HTTP request 拦截器
axios.interceptors.request.use(
    (config) => {
        let accessToken = tool.cookie.get('ACCESS-TOKEN')
        let refreshToken = tool.cookie.get('X-ACCESS-TOKEN')
        if (accessToken) {
            config.headers['Authorization'] = accessToken
        }
        if (refreshToken) {
            config.headers['X-Authorization'] = refreshToken
        }
        if (!sysConfig.REQUEST_CACHE && config.method === 'get') {
            config.params = config.params || {}
            config.params['_'] = new Date().getTime()
        }
        Object.assign(config.headers, sysConfig.HEADERS)

        function removeEmpty(reqData, level) {
            let deleted = false
            Object.keys(reqData).forEach((x) => {
                if (!reqData[x] && reqData[x] !== 0 && reqData[x] !== false) {
                    delete reqData[x]
                    deleted = true
                } else if (typeof reqData[x] === 'object') {
                    if (Object.keys(reqData[x]).length === 0) {
                        delete reqData[x]
                        deleted = true
                    } else return removeEmpty(reqData[x], ++level)
                }
            })
            return deleted
        }

        for (let i = 0; i !== 2; ++i)
            while (removeEmpty(config.data ? config.data : {}, 0)) {
                //
            }

        return config
    },
    (error) => {
        return Promise.reject(error)
    },
)

//FIX 多个API同时401时疯狂弹窗BUG
let MessageBox_401_show = false

// 进行大数字处理
axios.defaults.transformResponse = [
    (data) => {
        try {
            return jsonBigInt.parse(data)
        } catch {
            return data
        }
    },
]

// HTTP response 拦截器
axios.interceptors.response.use(
    (response) => {
        function setCookie(name, value) {
            tool.cookie.set(name, 'Bearer ' + value, {
                expires: tool.data.get('AUTO_LOGIN') ? 24 * 60 * 60 : 0,
                path: '/',
            })
        }

        if (response.headers['access-token']) {
            setCookie('ACCESS-TOKEN', response.headers['access-token'])
        }
        if (response.headers['x-access-token']) {
            setCookie('X-ACCESS-TOKEN', response.headers['x-access-token'])
        }
        return response
    },
    async (error) => {
        if (error.response) {
            if (error.response.status === 404) {
                ElNotification.error({
                    title: '请求错误',
                    message: 'Status:404，正在请求不存在的服务器记录！',
                })
            } else if (error.response.status === 500) {
                ElNotification.error({
                    title: '请求错误',
                    message: error.response.data.message || 'Status:500，服务器发生错误！',
                })
            } else if ([401, 403].includes(error.response.status)) {
                // 如果token不存在，说明用户是第一次访问，直接跳转到登录页面
                if (!tool.cookie.get('ACCESS-TOKEN') && window.location.href.indexOf('guest') < 0) {
                    await router.replace({ path: '/guest/login' })
                    return
                }
                if (!MessageBox_401_show && window.location.href.indexOf('guest') < 0) {
                    MessageBox_401_show = true
                    await ElMessageBox.confirm('当前用户已被登出或无权限访问当前资源，请尝试重新登录后再操作。', '无权限访问', {
                        type: 'error',
                        closeOnClickModal: false,
                        center: true,
                        confirmButtonText: '重新登录',
                        beforeClose: (action, instance, done) => {
                            MessageBox_401_show = false
                            done()
                        },
                    })
                    await router.replace({ path: '/guest/login' })
                }
            } else if (error.response.status === 900) {
                function showErr(msg) {
                    const title = axios.defaults.app().config.globalProperties.$GLOBAL.enums.errorCodes[error.response.data.code][1]

                    ElNotification.error({
                        title: title,
                        message: h('p', msg),
                    })
                }

                //业务错误
                if (typeof error.response.data.msg === 'object') {
                    Object.keys(error.response.data.msg).forEach((x) => {
                        showErr(error.response.data.msg[x])
                    })
                } else {
                    showErr(error.response.data.msg)
                }
            } else {
                ElNotification.error({
                    title: '请求错误',
                    message: error.message || `Status:${error.response.status}，未知错误！`,
                })
            }
        } else {
            ElNotification.error({
                title: '请求错误',
                message: '请求服务器无响应！',
            })
        }

        return Promise.reject(error.response)
    },
)

export default {
    // axios对象
    axios: axios.defaults,

    /** get 请求
     * @param  {string} url 接口地址
     * @param  {object} params 请求参数
     * @param  {object} config 参数
     */
    get: function (url, params = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'get',
                url: url,
                params: params,
                ...config,
            })
                .then((response) => {
                    resolve(response.data)
                })
                .catch((error) => {
                    reject(error)
                })
        })
    },

    /** post 请求
     * @param  {string} url 接口地址
     * @param  {object} data 请求参数
     * @param  {object} config 参数
     */
    post: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'post',
                url: url,
                data: data,
                ...config,
            })
                .then((response) => {
                    resolve(response.data)
                })
                .catch((error) => {
                    reject(error)
                })
        })
    },

    /** put 请求
     * @param  {string} url 接口地址
     * @param  {object} data 请求参数
     * @param  {object} config 参数
     */
    put: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'put',
                url: url,
                data: data,
                ...config,
            })
                .then((response) => {
                    resolve(response.data)
                })
                .catch((error) => {
                    reject(error)
                })
        })
    },

    /** patch 请求
     * @param  {string} url 接口地址
     * @param  {object} data 请求参数
     * @param  {object} config 参数
     */
    patch: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'patch',
                url: url,
                data: data,
                ...config,
            })
                .then((response) => {
                    resolve(response.data)
                })
                .catch((error) => {
                    reject(error)
                })
        })
    },

    /** delete 请求
     * @param  {string} url 接口地址
     * @param  {object} data 请求参数
     * @param  {object} config 参数
     */
    delete: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'delete',
                url: url,
                data: data,
                ...config,
            })
                .then((response) => {
                    resolve(response.data)
                })
                .catch((error) => {
                    reject(error)
                })
        })
    },

    /** jsonp 请求
     * @param  {string} url 接口地址
     * @param  {string} name JSONP回调函数名称
     */
    jsonp: function (url, name = 'jsonp') {
        return new Promise((resolve) => {
            const script = document.createElement('script')
            script.id = `jsonp${Math.ceil(Math.random() * 1000000)}`
            script.type = 'text/javascript'
            script.src = url
            window[name] = (response) => {
                resolve(response)
                document.getElementsByTagName('head')[0].removeChild(script)
                try {
                    delete window[name]
                } catch (e) {
                    window[name] = undefined
                }
            }
            document.getElementsByTagName('head')[0].appendChild(script)
        })
    },
}