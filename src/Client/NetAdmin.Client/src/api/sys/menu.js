/**
 *  菜单服务
 *  @module @/api/sys/menu
 */

import config from "@/config"
import http from "@/utils/request"

export default {

    /**
 * 批量删除菜单
 */
bulkDelete :{
    url: `${config.API_URL}/api/sys/menu/bulk.delete`,
        name: `批量删除菜单`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 创建菜单
 */
create :{
    url: `${config.API_URL}/api/sys/menu/create`,
        name: `创建菜单`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 删除菜单
 */
delete :{
    url: `${config.API_URL}/api/sys/menu/delete`,
        name: `删除菜单`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 查询菜单
 */
query :{
    url: `${config.API_URL}/api/sys/menu/query`,
        name: `查询菜单`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 更新菜单
 */
update :{
    url: `${config.API_URL}/api/sys/menu/update`,
        name: `更新菜单`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

/**
 * 当前用户菜单
 */
userMenus :{
    url: `${config.API_URL}/api/sys/menu/user.menus`,
        name: `当前用户菜单`,
        post:async function(data, config={}) {
        return await http.post(this.url,data, config)
    }
},

}