/* eslint-disable no-undef */
const DEFAULT_CONFIG = {
  //接口地址
  API_URL: '',

  //请求超时
  TIMEOUT: 100000,

  //追加其他头
  HEADERS: {},

  //请求是否开启缓存
  REQUEST_CACHE: false,

  //是否加密localStorage, 为空不加密，可填写AES(模式ECB,移位Pkcs7)加密
  LS_ENCRYPTION: '',

  //localStorageAES加密秘钥，位数建议填写8的倍数
  LS_ENCRYPTION_key: '2XNN4K8LC0ELVWN4',

  //常量字符串
  STRINGS: null,

  //本地化常量字符串
  LOC_STRINGS: null,

  //常量枚举
  ENUMS: null,

  //数字常量表,
  NUMBERS: null
}

export default DEFAULT_CONFIG