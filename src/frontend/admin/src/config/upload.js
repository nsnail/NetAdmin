import API from "@/api";

//上传配置

export default {
    apiObj: API.sys_file.upload, //上传请求API对象
    filename: 'file', //form请求时文件的key
    successCode: 'succeed', //请求完成代码
    maxSize: 10, //最大文件大小 默认10MB
    parseData: function (res) {
        return {
            code: res.code, //分析状态字段结构
            // fileName: null, //分析文件名称
            src: res.data, //分析图片远程地址结构
            msg: res.msg, //分析描述字段结构
        }
    },
    apiObjFile: API.sys_file.upload, //附件上传请求API对象
    maxSizeFile: 10, //最大文件大小 默认10MB
}