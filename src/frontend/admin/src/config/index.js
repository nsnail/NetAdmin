import MY_CONFIG from './myConfig'
import APP_CONFIG from './appConfig'
import avatar from '@/utils/avatar'
import tool from '@/utils/tool'

const DEFAULT_CONFIG = {
    //标题
    APP_NAME: 'NetAdmin',

    //首页地址
    DASHBOARD_URL: '/home',

    //版本号
    APP_VER: '1.0.0',

    //内核版本号
    CORE_VER: '1.6.9',

    //接口地址
    API_URL: '',

    //请求超时
    TIMEOUT: 10000,

    //TokenName
    TOKEN_NAME: 'Authorization',

    //Token前缀，注意最后有个空格，如不需要需设置空字符串
    TOKEN_PREFIX: 'Bearer ',

    //追加其他头
    HEADERS: {},

    //请求是否开启缓存
    REQUEST_CACHE: false,

    //布局 默认：default | 通栏：header | 经典：menu | 功能坞：dock
    //dock将关闭标签和面包屑栏
    LAYOUT: 'menu',

    //菜单是否折叠
    MENU_IS_COLLAPSE: false,

    //菜单是否启用手风琴效果
    MENU_UNIQUE_OPENED: false,

    //是否开启多标签
    LAYOUT_TAGS: true,

    //语言
    LANG: 'zh-cn',

    //主题颜色
    COLOR: '#21A675',

    //是否加密localStorage, 为空不加密，可填写AES(模式ECB,移位Pkcs7)加密
    LS_ENCRYPTION: '',

    //localStorageAES加密秘钥，位数建议填写8的倍数
    LS_ENCRYPTION_key: '2XNN4K8LC0ELVWN4',

    //控制台首页默认布局
    DEFAULT_GRID: {
        //默认分栏数量和宽度 例如 [24] [18,6] [8,8,8] [6,12,6]
        layout: [8, 8, 8, 12, 12, 12, 12],
        //小组件分布，com取值:views/home/components 文件名
        compsList: [
            ['chart-bar-request'],
            ['ver'],
            ['chart-bar-jobrecord'],
            ['chart-pie-request'],
            ['chart-pie-jobrecord'],
            ['modules'],
            ['change-log'],
        ],
    },

    //默认头像
    DEFAULT_AVATAR(name) {
        return (
            'data:image/svg+xml,' +
            encodeURIComponent(
                avatar.createSVG(`#${Math.abs(tool.crypto.hashCode(name)).toString(16).substring(0, 6)}`, name.slice(0, 1).toUpperCase()).outerHTML,
            )
        )
    },
}

//合并业务配置
Object.assign(DEFAULT_CONFIG, MY_CONFIG)

// 如果生产模式，就合并动态的APP_CONFIG
// public/config.js
if (import.meta.env.MODE === 'production' || import.meta.env.MODE === 'test') {
    Object.assign(DEFAULT_CONFIG, APP_CONFIG)
}

export default DEFAULT_CONFIG