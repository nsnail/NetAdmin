/**
 *  文件服务
 *  @module @/api/file
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 文件上传
 */
upload :{
    url: `${config.API_URL}/api/file/upload`,
        name: `文件上传`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},


}