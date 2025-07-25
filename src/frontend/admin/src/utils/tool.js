import cryptoJS from 'crypto-js'
import sysConfig from '@/config'

import dayjs from 'dayjs'
import relativeTime from 'dayjs/plugin/relativeTime'

// 扩展插件和语言设置
dayjs.extend(relativeTime)

const tool = {}

tool.time = {
    //获取当前时间戳
    getUnix: function () {
        const date = new Date()
        return date.getTime()
    },
    //获取今天0点0分0秒的时间戳
    getTodayUnix: function () {
        const date = new Date()
        date.setHours(0)
        date.setMinutes(0)
        date.setSeconds(0)
        date.setMilliseconds(0)
        return date.getTime()
    },
    //获取今年1月1日0点0秒的时间戳
    getYearUnix: function () {
        const date = new Date()
        date.setMonth(0)
        date.setDate(1)
        date.setHours(0)
        date.setMinutes(0)
        date.setSeconds(0)
        date.setMilliseconds(0)
        return date.getTime()
    },
    //获取标准年月日
    getLastDate: function (time) {
        const date = new Date(time)
        const month = date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1
        const day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate()
        return date.getFullYear() + '-' + month + '-' + day
    },
    //转换时间
    getFormatTime: function (_this, timestamp) {
        if (!timestamp) return '--'

        const now = dayjs()
        const target = dayjs(timestamp)
        const diffSeconds = now.diff(target, 'second', true)
        const isFuture = diffSeconds < 0 // 是否未来时间
        const absDiff = Math.abs(diffSeconds) // 时间差绝对值

        // 超过10天（10 * 24 * 60 * 60 秒）显示具体日期
        if (absDiff > 10 * 86400) {
            return target.format('YYYY-MM-DD')
        }

        let result
        if (absDiff < 60) {
            // 1分钟内
            result = _this.$t(`{n} 秒`, { n: Math.floor(absDiff) })
        } else if (absDiff < 3600) {
            // 1小时内
            result = _this.$t(`{n} 分钟`, { n: Math.floor(absDiff / 60) })
        } else if (absDiff < 86400) {
            // 24小时内
            result = _this.$t(`{n} 小时`, { n: Math.floor(absDiff / 3600) })
        } else {
            // 10天内
            result = _this.$t(`{n} 天`, { n: Math.floor(absDiff / 86400) })
        }
        // 未来时间加“后”，过去时间加“前”
        return isFuture ? `${result}${_this.$t('后')}` : `${result}${_this.$t('前')}`
    },
}

/* localStorage */
tool.data = {
    configJson: null,
    async uploadConfig() {
        try {
            const json = JSON.stringify(Object.entries(localStorage).filter((x) => x[0].indexOf('APP_SET_') === 0))
            if (this.configJson !== json) {
                this.configJson = json
                const userApi = await import('@/api/sys/user')
                await userApi.default.setSessionUserAppConfig.post({
                    appConfig: this.configJson,
                })
            }
        } catch {}
    },
    clearAppSet() {
        Object.entries(localStorage)
            .filter((x) => x[0].indexOf('APP_SET_') === 0)
            .map((x) => localStorage.removeItem(x[0]))
    },
    async downloadConfig() {
        try {
            const userApi = await import('@/api/sys/user')
            const res = await userApi.default.getSessionUserAppConfig.post({})
            this.clearAppSet()
            for (const item of JSON.parse(res.data.appConfig)) {
                localStorage.setItem(item[0], item[1])
            }
        } catch {}
    },
    async set(key, data, datetime = 0) {
        //加密
        if (sysConfig.LS_ENCRYPTION === 'AES') {
            data = tool.crypto.AES.encrypt(JSON.stringify(data), sysConfig.LS_ENCRYPTION_key)
        }
        let cacheValue = {
            content: data,
            datetime: parseInt(datetime) === 0 ? 0 : new Date().getTime() + parseInt(datetime) * 1000,
        }
        localStorage.setItem(key, JSON.stringify(cacheValue))
        await this.uploadConfig()
    },
    get(key) {
        try {
            const value = JSON.parse(localStorage.getItem(key))
            if (value) {
                let nowTime = new Date().getTime()
                if (nowTime > value.datetime && value.datetime !== 0) {
                    localStorage.removeItem(key)
                    return null
                }
                //解密
                if (sysConfig.LS_ENCRYPTION === 'AES') {
                    value.content = JSON.parse(tool.crypto.AES.decrypt(value.content, sysConfig.LS_ENCRYPTION_key))
                }
                return value.content
            }
            return null
        } catch (err) {
            return null
        }
    },
    async remove(key) {
        localStorage.removeItem(key)
        await this.uploadConfig()
    },
    async clear() {
        localStorage.clear()
        await this.uploadConfig()
    },
}

/*sessionStorage*/
tool.session = {
    set(table, settings) {
        const _set = JSON.stringify(settings)
        return sessionStorage.setItem(table, _set)
    },
    get(table) {
        let data = sessionStorage.getItem(table)
        try {
            data = JSON.parse(data)
        } catch (err) {
            return null
        }
        return data
    },
    remove(table) {
        return sessionStorage.removeItem(table)
    },
    clear() {
        return sessionStorage.clear()
    },
}

/*cookie*/
tool.cookie = {
    set(name, value, config = {}) {
        const cfg = {
            expires: null,
            path: null,
            domain: null,
            secure: false,
            httpOnly: false,
            ...config,
        }
        let cookieStr = `${name}=${escape(value)}`
        if (cfg.expires) {
            const exp = new Date()
            exp.setTime(exp.getTime() + parseInt(cfg.expires) * 1000)
            cookieStr += `;expires=${exp.toGMTString()}`
        }
        if (cfg.path) {
            cookieStr += `;path=${cfg.path}`
        }
        if (cfg.domain) {
            cookieStr += `;domain=${cfg.domain}`
        }
        document.cookie = cookieStr
    },
    get(name) {
        const arr = document.cookie.match(new RegExp('(^| )' + name + '=([^;]*)(;|$)'))
        if (arr !== null) {
            return unescape(arr[2])
        } else {
            return null
        }
    },
    remove(name) {
        const exp = new Date()
        exp.setTime(exp.getTime() - 1)
        document.cookie = `${name}=;expires=${exp.toGMTString()}`
    },
    clear() {
        const cookies = document.cookie.split(';')
        const pastDate = new Date(0).toUTCString()

        cookies.forEach((cookie) => {
            const [name, value] = cookie.split('=')
            document.cookie = `${name.trim()}=${encodeURIComponent(value.trim())}; expires=${pastDate}; path=/`
        })
    },
}

/* Fullscreen */
tool.screen = async function (element) {
    const isFull = !!(document.webkitIsFullScreen || document.mozFullScreen || document.msFullscreenElement || document.fullscreenElement)
    if (isFull) {
        if (document.exitFullscreen) {
            await document.exitFullscreen()
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen()
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen()
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen()
        }
    } else {
        if (element.requestFullscreen) {
            element.requestFullscreen()
        } else if (element.msRequestFullscreen) {
            element.msRequestFullscreen()
        } else if (element.mozRequestFullScreen) {
            element.mozRequestFullScreen()
        } else if (element.webkitRequestFullscreen) {
            element.webkitRequestFullscreen()
        }
    }
}

/* 复制对象 */
tool.objCopy = function (obj) {
    return JSON.parse(JSON.stringify(obj))
}

/* 带点属性转嵌套对象 */
tool.dotNotationToNested = function (obj) {
    const result = {}

    for (const key in obj) {
        if (key.includes('.')) {
            // 处理带点的键
            const keys = key.split('.')
            let current = result

            for (let i = 0; i < keys.length; i++) {
                const part = keys[i]

                // 如果是最后一个部分，设置值
                if (i === keys.length - 1) {
                    current[part] = obj[key]
                } else {
                    // 如果不是最后一个部分，确保对象存在
                    current[part] = current[part] || {}
                    current = current[part]
                }
            }
        } else {
            // 直接复制不带点的键
            result[key] = obj[key]
        }
    }
    return result
}
/* 将嵌套对象转带点属性 */
tool.nestedToDotNotation = function (obj, prefix = '', result = {}) {
    // 处理 null 或 undefined 的情况
    if (obj === null || obj === undefined) {
        return result
    }

    for (const key in obj) {
        // 更安全的属性检查方式
        if (Object.prototype.hasOwnProperty.call(obj, key)) {
            const newKey = prefix ? `${prefix}.${key}` : key

            if (typeof obj[key] === 'object' && obj[key] !== null && !Array.isArray(obj[key])) {
                // 递归处理嵌套对象
                this.nestedToDotNotation(obj[key], newKey, result)
            } else {
                // 基本类型或数组，直接赋值
                result[newKey] = obj[key]
            }
        }
    }
    return result
}

/* 获取嵌套属性 */
tool.getNestedProperty = function (obj, path) {
    if (!path) return null
    const keys = path.split('.') // 将属性路径分割为键的数组
    let current = obj

    for (let key of keys) {
        if (current && key in current) {
            current = current[key]
        } else {
            return undefined // 如果任何一个键不存在，返回 undefined
        }
    }

    return current
}
/* 日期格式化 */
tool.dateFormat = function (date, fmt = 'yyyy-MM-dd hh:mm:ss') {
    date = new Date(date)
    const o = {
        'M+': date.getMonth() + 1, //月份
        'd+': date.getDate(), //日
        'h+': date.getHours(), //小时
        'm+': date.getMinutes(), //分
        's+': date.getSeconds(), //秒
        'q+': Math.floor((date.getMonth() + 3) / 3), //季度
        S: date.getMilliseconds(), //毫秒
    }
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (date.getFullYear() + '').substr(4 - RegExp.$1.length))
    }
    for (const k in o) {
        if (new RegExp('(' + k + ')').test(fmt)) {
            fmt = fmt.replace(RegExp.$1, RegExp.$1.length === 1 ? o[k] : ('00' + o[k]).substr(('' + o[k]).length))
        }
    }
    return fmt
}

// json字符串转对象
tool.tryJson2Obj = (json) => {
    try {
        return JSON.parse(json)
    } catch {
        return json
    }
}

/* 千分符 */
tool.groupSeparator = function (num) {
    num = num + ''
    if (!num.includes('.')) {
        num += '.'
    }
    return num
        .replace(/(\d)(?=(\d{3})+\.)/g, function ($0, $1) {
            return $1 + ','
        })
        .replace(/\.$/, '')
}
// unicode解码
tool.unicodeDecode = function (str) {
    return str.replace(/\\u([0-9a-fA-F]{4})/g, (match, grp) => String.fromCharCode(parseInt(grp, 16)))
}
// 高亮关键词
tool.highLightKeywords = function (str) {
    return str
        .replace(new RegExp('(Body)', 'gi'), '<span class=keywords-highlight>$1</span>')
        .replace(new RegExp('(http(s?)://.*?),', 'gi'), '<span class=keywords-highlight>$1</span>')
}
// 属性排序
tool.sortProperties = function (obj) {
    const sortedKeys = Object.keys(obj).sort()
    const sortedObject = {}

    for (const key of sortedKeys) {
        sortedObject[key] = obj[key]
    }

    return sortedObject
}

tool.recursiveFindProperty = function (obj, propName, propValue, result = [], visited = new Set()) {
    if (visited.has(obj)) {
        return result
    }

    visited.add(obj)

    if (Array.isArray(obj)) {
        // 遍历数组
        for (const item of obj) {
            this.recursiveFindProperty(item, propName, propValue, result, visited)
        }
    } else if (typeof obj === 'object') {
        // 遍历对象的属性
        for (const key in obj) {
            if (key === propName && obj[key] === propValue) {
                // 找到匹配的对象，将其添加到结果数组中
                result.push(obj)
            } else {
                // 继续递归遍历子对象或数组
                this.recursiveFindProperty(obj[key], propName, propValue, result, visited)
            }
        }
    }

    return result
}

//TAB 刷新
tool.refreshTab = function (_this) {
    _this.$parent.keepAliveList = []
    _this.contextMenuVisible = false
    const tag = _this.$store.state['view-tags']['view-tags'].find((x) => x.fullPath === _this.$route.fullPath)
    //判断是否当前路由，否的话跳转
    if (_this.$route.fullPath !== tag.fullPath) {
        _this.$router.push({
            path: tag.fullPath,
            query: tag.query,
        })
    }
    _this.$store.commit('refreshIframe', tag)
    setTimeout(() => {
        _this.$store.commit('removeKeepLive', tag.name)
        _this.$store.commit('setRouteShow', false)
        _this.$nextTick(() => {
            _this.$store.commit('pushKeepLive', tag.name)
            _this.$store.commit('setRouteShow', true)

            setTimeout(() => {
                _this.$parent.keepAliveList = null
            }, 100)
        })
    }, 0)
}

// 定时器
tool.timeout = (time) => {
    return new Promise(function (resolve, reject) {
        setTimeout(function () {
            resolve()
        }, time)
    })
}

/* 常用加解密 */
tool.crypto = {
    hashCode(str) {
        let hash = 0
        if (str.length === 0) return hash
        for (let i = 0; i < str.length; i++) {
            const char = str.charCodeAt(i)
            hash = (hash << 5) - hash + char
            hash = hash & hash // Convert to 32bit integer
        }
        return hash
    },

    stringToInt32(inputString) {
        let int32Value = 0
        for (let i = 0; i < 4; i++) {
            const charCode = inputString.charCodeAt(i)
            int32Value <<= 8 // 左移8位
            int32Value += charCode
        }
        return int32Value
    },
    generateDerivedKey(seed) {
        const chars = '!"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~'
        let key = ''
        for (let i = 0; i < 32; i++) {
            key += chars[Math.floor(Math.abs(Math.sin(this.stringToInt32(seed) * (i + 1))) * chars.length)]
        }
        return key
    },

    //MD5加密
    MD5(data) {
        return cryptoJS.MD5(data).toString()
    },
    //BASE64加解密
    BASE64: {
        encrypt(data) {
            return cryptoJS.enc.Base64.stringify(cryptoJS.enc.Utf8.parse(data))
        },
        decrypt(cipher) {
            return cryptoJS.enc.Base64.parse(cipher).toString(cryptoJS.enc.Utf8)
        },
    },
    //AES加解密
    AES: {
        encrypt(data, secretKey, config = {}) {
            if (secretKey.length % 8 !== 0) {
                console.warn('[error]: 秘钥长度需为8的倍数，否则解密将会失败。')
            }
            const result = cryptoJS.AES.encrypt(data, cryptoJS.enc.Utf8.parse(secretKey), {
                iv: cryptoJS.enc.Utf8.parse(config.iv || ''),
                mode: cryptoJS.mode[config.mode || 'ECB'],
                padding: cryptoJS.pad[config.padding || 'Pkcs7'],
            })
            return result.toString()
        },
        decrypt(cipher, secretKey, config = {}) {
            const result = cryptoJS.AES.decrypt(cipher, cryptoJS.enc.Utf8.parse(secretKey), {
                iv: cryptoJS.enc.Utf8.parse(config.iv || ''),
                mode: cryptoJS.mode[config.mode || 'ECB'],
                padding: cryptoJS.pad[config.padding || 'Pkcs7'],
            })
            return cryptoJS.enc.Utf8.stringify(result)
        },
    },
}

export default tool