/* eslint-disable no-undef */
const DEFAULT_CONFIG = {
    //标题
    APP_NAME: process.env.VUE_APP_TITLE,

    //首页地址
    DASHBOARD_URL: "/home/dashboard",

    //版本号
    APP_VER: "1.6.9",

    //内核版本号
    CORE_VER: "1.6.9",

    //接口地址
    API_URL: process.env.NODE_ENV === 'development' && process.env.VUE_APP_PROXY === 'true' ? "/api" : process.env.VUE_APP_API_BASEURL,

    //请求超时
    TIMEOUT: 100000,

    //TokenName
    TOKEN_NAME: "Authorization",

    //Token前缀，注意最后有个空格，如不需要需设置空字符串
    TOKEN_PREFIX: "Bearer ",

    //Refresh TokenName
    REFRESH_TOKEN_NAME: "X-Authorization",

    TOKEN_RSPNAME: 'access-token',
    REFRESH_TOKEN_RSPNAME: 'x-access-token',

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
    COLOR: '#06c755',

    //是否加密localStorage, 为空不加密，可填写AES(模式ECB,移位Pkcs7)加密
    LS_ENCRYPTION: '',

    //localStorageAES加密秘钥，位数建议填写8的倍数
    LS_ENCRYPTION_key: '2XNN4K8LC0ELVWN4',

    //控制台首页默认布局
    DEFAULT_GRID: {
        //默认分栏数量和宽度 例如 [24] [18,6] [8,8,8] [6,12,6]
        layout: [12, 12],
        //小组件分布，com取值:views/home/components 文件名
        copmsList: [
            ['welcome'],
            ['time', 'ver']
        ]
    },
    //常量字符串
    STRINGS: null,
    //本地化常量字符串
    LOC_STRINGS: null,
    //常量枚举
    ENUMS: null,
    //数字常量表,
    NUMBERS: null,
    //默认头像
    DEF_AVATAR: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAIAAAC0Ujn1AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAflJREFUeNqs1u1P2kAYAHB6B+21UmZbatEAki2+MKIR42TGaHRLjBr/Yj/7wWRb3If5gjHbfIlBjIqCUCq0PmaJLqG9IwdP+qV3vV+v1/Z5Ttg/Ke39tm+q7VD/wlBx/j0J7xzWbccL9TVgosCivrv/Atgw8yI9JuZGVXNQjCkROL2tOpc39q8/D7bj0gcy6NSQvDoTR4LwdidVhOPDyMD29/Jd9YkyFlH6pAhanjb+d19DFvHSlOHX0x09kYpGcOAFMPf0kMxJJ02ZvlyWRjhpWcJ0moiIk240Gf9Rw2lz0o92i04/Nnhpl/Hhhtqux0mnLcZrzCQUTrrV9lh/M++CFM9rtOXyvL+lOif98+T++r4Z1Ht0Vju9anDSEKXbQBqSFH0sgz6+qMGDd7Y/1J96pav1FuTPzvZvxYrrej3REOWK09l4XWkyB7Jp30RBRNwH2jf/MZMimzZi/kk5l1HFMOKnFYK/zMaDqsxqPo6QwENDtd2YtwakwOI5rJO1OZOS0/3p8VR0s2CpMqMoJzSytWAldKmrig7cQk4fMUiX+w14rPVPVvGi9qNYcVpuIP1xVM2PvaOU2sACnYwmTbJ7cHdefssqSJFeXkUYCysz8flJjcN9nf7XvFnIahi/gMCiwphMROFzVstYSu/7sWxaXczpAAL7LMAA6FWV/DBrhrIAAAAASUVORK5CYII='
}

//合并业务配置
import MY_CONFIG from "./myConfig"

Object.assign(DEFAULT_CONFIG, MY_CONFIG)

// 如果生产模式，就合并动态的APP_CONFIG
// public/config.js
if (process.env.NODE_ENV === 'production') {
    Object.assign(DEFAULT_CONFIG, APP_CONFIG)
}

export default DEFAULT_CONFIG