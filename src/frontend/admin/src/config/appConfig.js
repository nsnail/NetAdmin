// 此文件非必要，在生产环境下此文件配置可覆盖运行配置，开发环境下不起效
// 详情见 src/config/index.js

export default {
    //标题
    //APP_NAME: "标题",
    //接口地址，如遇跨域需使用nginx代理
    API_URL: import.meta.env.VITE_API_URL,
}