import axios from 'axios'
import jsonBigInt from 'json-bigint'
import sysConfig from '@/config'
import tool from '@/utils/tool'
import {showDialog, showFailToast} from 'vant'
import router from "@/router";

axios.defaults.baseURL = ''

axios.defaults.timeout = sysConfig.TIMEOUT

// HTTP request 拦截器
axios.interceptors.request.use(
    (config) => {
        let token = tool.cookie.get('TOKEN')
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`
            let expires = tool.cookie.get('TOKEN-EXP')
            //accessToken 已过期或 5分钟内过期 带上刷新token
            if (!expires || expires - new Date().getTime() < 300000) {
                let refresh_token = tool.cookie.get('X-TOKEN')
                if (refresh_token) {
                    config.headers['X-Authorization'] = `Bearer ${refresh_token}`
                }
            }
        }

        if (!sysConfig.REQUEST_CACHE && config.method === 'get') {
            config.params = config.params || {}
            config.params['_'] = new Date().getTime()
        }
        Object.assign(config.headers, sysConfig.HEADERS)
        return config
    },
    (error) => {
        return Promise.reject(error)
    }
)


// 进行大数字处理
axios.defaults.transformResponse = [
    (data) => {
        try {
            return jsonBigInt.parse(data)
        } catch {
            return data
        }
    }
]

// HTTP response 拦截器
axios.interceptors.response.use(
    (response) => {
        let token = response.headers['access-token']
        let refreshToken = response.headers['x-access-token']
        if (token || refreshToken) {
            let cookieExpires = tool.data.get('REMEMBER_ME') ? 24 * 60 * 60 * 7 : 0
            if (token) {
                // 保存访问令牌
                tool.cookie.set('TOKEN', token, {
                    expires: cookieExpires,
                    path: '/'
                })

                // 解析访问令牌，保存令牌的失效时间
                const jwt = tool.crypto.decryptJWT(token)
                const secs = jwt.exp - jwt.iat
                tool.cookie.set('TOKEN-EXP', new Date().getTime() + secs * 1000, {
                    expires: cookieExpires,
                    path: '/'
                })
            }

            if (refreshToken) {
                // 保存刷新令牌
                tool.cookie.set('X-TOKEN', token, {
                    expires: cookieExpires,
                    path: '/'
                })
            }
        }
        return response
    },
    (error) => {
        if (error.response) {
            if (error.response.status === 404) {
                showFailToast('Status:404，正在请求不存在的服务器记录！')
            } else if (error.response.status === 401 || error.response.status === 403) {
                showDialog({
                    title: '无权限访问',
                    message:
                        '当前用户已被登出或无权限访问当前资源，请尝试重新登录后再操作。',

                })
                    .then(() => {
                        // on confirm
                        router.push('/account/login-by-pwd')
                    })
            } else if (error.response.data.code) {
                if (typeof error.response.data.msg == 'object') {
                    let i = 0
                    for (const item in error.response.data.msg) {
                        i += 100
                        setTimeout(
                            (title, msg) => {
                                showFailToast(msg)
                            },
                            i,
                            item,
                            error.response.data.msg[item]
                        )
                    }
                } else {
                    showFailToast(error.response.data.msg)
                }
            } else {
                showFailToast(error.message || `Status:${error.response.status}，未知错误！`)
            }
        } else {
            showFailToast('请求服务器无响应！')
        }

        return Promise.reject(error.response)
    }
)

const http = {
    /** get 请求
     * @param  {接口地址} url
     * @param  {请求参数} params
     * @param  {参数} config
     */
    get: function (url, params = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'get',
                url: url,
                params: params,
                ...config
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
     * @param  {接口地址} url
     * @param  {请求参数} data
     * @param  {参数} config
     */
    post: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'post',
                url: url,
                data: data,
                ...config
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
     * @param  {接口地址} url
     * @param  {请求参数} data
     * @param  {参数} config
     */
    put: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'put',
                url: url,
                data: data,
                ...config
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
     * @param  {接口地址} url
     * @param  {请求参数} data
     * @param  {参数} config
     */
    patch: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'patch',
                url: url,
                data: data,
                ...config
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
     * @param  {接口地址} url
     * @param  {请求参数} data
     * @param  {参数} config
     */
    delete: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: 'delete',
                url: url,
                data: data,
                ...config
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
     * @param  {接口地址} url
     * @param  {JSONP回调函数名称} name
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
    }
}

export default http