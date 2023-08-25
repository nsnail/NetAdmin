/**
 *  会员服务
 *  @module @/api/biz/member
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除会员
 */
bulkDelete :{
    url: `${config.API_URL}/api/biz/member/bulk.delete`,
        name: `批量删除会员`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建会员
 */
create :{
    url: `${config.API_URL}/api/biz/member/create`,
        name: `创建会员`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除会员
 */
delete :{
    url: `${config.API_URL}/api/biz/member/delete`,
        name: `删除会员`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 当前会员信息
 */
memberInfo :{
    url: `${config.API_URL}/api/biz/member/member.info`,
        name: `当前会员信息`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 分页查询会员
 */
pagedQuery :{
    url: `${config.API_URL}/api/biz/member/paged.query`,
        name: `分页查询会员`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询会员
 */
query :{
    url: `${config.API_URL}/api/biz/member/query`,
        name: `查询会员`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 会员注册
 */
register :{
    url: `${config.API_URL}/api/biz/member/register`,
        name: `会员注册`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新会员
 */
update :{
    url: `${config.API_URL}/api/biz/member/update`,
        name: `更新会员`,
        post:async function(data={}, config={}) {
        return await http.post(this.url,data, config)
    }
},

}