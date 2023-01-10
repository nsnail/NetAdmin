/**
 *  菜单接口
 *  @module @/api/menu
 */

import config from "@/config"
import http from "@/utils/request"

export default {

	/**
	 * 批量删除菜单
	 */
	bulkDelete :{
		url: `${config.API_URL}/menu/bulk.delete`,
		name: `批量删除菜单`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},


	/**
	 * 创建菜单
	 */
	create :{
		url: `${config.API_URL}/menu/create`,
		name: `创建菜单`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},


	/**
	 * 删除菜单
	 */
	deleteAsync :{
		url: `${config.API_URL}/menu/delete`,
		name: `删除菜单`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},


	/**
	 * 查询菜单
	 */
	query :{
		url: `${config.API_URL}/menu/query`,
		name: `查询菜单`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},


	/**
	 * 更新菜单
	 */
	update :{
		url: `${config.API_URL}/menu/update`,
		name: `更新菜单`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},

}