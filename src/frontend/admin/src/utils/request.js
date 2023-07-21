import axios from "axios";
import jsonBigInt from "json-bigint";
import { ElMessageBox, ElNotification } from "element-plus";
import sysConfig from "@/config";
import config from "@/config";
import tool from "@/utils/tool";
import router from "@/router";

axios.defaults.baseURL = "";

axios.defaults.timeout = sysConfig.TIMEOUT;

// HTTP request 拦截器
axios.interceptors.request.use(
    (config) => {
        let token = tool.cookie.get("TOKEN");
        if (token) {
            config.headers[sysConfig.TOKEN_NAME] =
                sysConfig.TOKEN_PREFIX + token;
            let expires = tool.data.get("TOKEN-EXP");
            //accesstoken 已过期或 5分钟内过期 带上刷新token
            if (!expires || expires - new Date().getTime() < 300000) {
                let refresh_token = tool.data.get("X-TOKEN");
                if (refresh_token) {
                    config.headers[sysConfig.REFRESH_TOKEN_NAME] =
                        sysConfig.TOKEN_PREFIX + refresh_token;
                }
            }
        }

        if (!sysConfig.REQUEST_CACHE && config.method === "get") {
            config.params = config.params || {};
            config.params["_"] = new Date().getTime();
        }
        Object.assign(config.headers, sysConfig.HEADERS);
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

//FIX 多个API同时401时疯狂弹窗BUG
let MessageBox_401_show = false;

// 进行大数字处理
axios.defaults.transformResponse = [
    (data) => {
        try {
            return jsonBigInt.parse(data);
        } catch {
            return data;
        }
    },
];

// HTTP response 拦截器
axios.interceptors.response.use(
    (response) => {
        let token = response.headers[config.TOKEN_RSPNAME];
        let refreshToken = response.headers[config.REFRESH_TOKEN_RSPNAME];
        if (token || refreshToken) {
            let cookieExpires = tool.data.get("REMEMBER_ME")
                ? 24 * 60 * 60 * 7
                : 0;
            if (token) {
                // 保存访问令牌
                tool.cookie.set("TOKEN", token, {
                    expires: cookieExpires,
                });

                // 解析访问令牌，保存令牌的失效时间
                const jwt = tool.crypto.decryptJWT(token);
                const secs = jwt.exp - jwt.iat;
                tool.cookie.set(
                    "TOKEN-EXP",
                    new Date().getTime() + secs * 1000,
                    {
                        expires: cookieExpires,
                    }
                );
            }

            if (refreshToken) {
                // 保存刷新令牌
                tool.cookie.set("X-TOKEN", token, {
                    expires: cookieExpires,
                });
            }
        }
        return response;
    },
    (error) => {
        if (error.response) {
            if (error.response.status === 404) {
                ElNotification.error({
                    title: "请求错误",
                    message: "Status:404，正在请求不存在的服务器记录！",
                });
            } else if (
                error.response.status === 401 ||
                error.response.status === 403
            ) {
                if (!MessageBox_401_show) {
                    MessageBox_401_show = true;
                    ElMessageBox.confirm(
                        "当前用户已被登出或无权限访问当前资源，请尝试重新登录后再操作。",
                        "无权限访问",
                        {
                            type: "error",
                            closeOnClickModal: false,
                            center: true,
                            confirmButtonText: "重新登录",
                            beforeClose: (action, instance, done) => {
                                MessageBox_401_show = false;
                                done();
                            },
                        }
                    )
                        .then(() => {
                            router.replace({ path: "/login" });
                        })
                        .catch(() => {});
                }
            } else if (error.response.data.code) {
                let title =
                    sysConfig.ENUMS.errorCodes[error.response.data.code];
                if (typeof error.response.data.msg == "object") {
                    let i = 0;
                    for (const item in error.response.data.msg) {
                        i += 100;
                        setTimeout(
                            (title, msg) => {
                                ElNotification.error({
                                    title: title,
                                    message: msg,
                                });
                            },
                            i,
                            item,
                            error.response.data.msg[item]
                        );
                    }
                } else {
                    ElNotification.error({
                        title: title,
                        message: error.response.data.msg,
                    });
                }
            } else {
                ElNotification.error({
                    title: "请求错误",
                    message:
                        error.message ||
                        `Status:${error.response.status}，未知错误！`,
                });
            }
        } else {
            ElNotification.error({
                title: "请求错误",
                message: "请求服务器无响应！",
            });
        }

        return Promise.reject(error.response);
    }
);

const http = {
    /** get 请求
     * @param  {接口地址} url
     * @param  {请求参数} params
     * @param  {参数} config
     */
    get: function (url, params = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: "get",
                url: url,
                params: params,
                ...config,
            })
                .then((response) => {
                    resolve(response.data);
                })
                .catch((error) => {
                    reject(error);
                });
        });
    },

    /** post 请求
     * @param  {接口地址} url
     * @param  {请求参数} data
     * @param  {参数} config
     */
    post: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: "post",
                url: url,
                data: data,
                ...config,
            })
                .then((response) => {
                    resolve(response.data);
                })
                .catch((error) => {
                    reject(error);
                });
        });
    },

    /** put 请求
     * @param  {接口地址} url
     * @param  {请求参数} data
     * @param  {参数} config
     */
    put: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: "put",
                url: url,
                data: data,
                ...config,
            })
                .then((response) => {
                    resolve(response.data);
                })
                .catch((error) => {
                    reject(error);
                });
        });
    },

    /** patch 请求
     * @param  {接口地址} url
     * @param  {请求参数} data
     * @param  {参数} config
     */
    patch: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: "patch",
                url: url,
                data: data,
                ...config,
            })
                .then((response) => {
                    resolve(response.data);
                })
                .catch((error) => {
                    reject(error);
                });
        });
    },

    /** delete 请求
     * @param  {接口地址} url
     * @param  {请求参数} data
     * @param  {参数} config
     */
    delete: function (url, data = {}, config = {}) {
        return new Promise((resolve, reject) => {
            axios({
                method: "delete",
                url: url,
                data: data,
                ...config,
            })
                .then((response) => {
                    resolve(response.data);
                })
                .catch((error) => {
                    reject(error);
                });
        });
    },

    /** jsonp 请求
     * @param  {接口地址} url
     * @param  {JSONP回调函数名称} name
     */
    jsonp: function (url, name = "jsonp") {
        return new Promise((resolve) => {
            const script = document.createElement("script");
            script.id = `jsonp${Math.ceil(Math.random() * 1000000)}`;
            script.type = "text/javascript";
            script.src = url;
            window[name] = (response) => {
                resolve(response);
                document.getElementsByTagName("head")[0].removeChild(script);
                try {
                    delete window[name];
                } catch (e) {
                    window[name] = undefined;
                }
            };
            document.getElementsByTagName("head")[0].appendChild(script);
        });
    },
};

export default http;