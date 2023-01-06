/**
 *  用户接口
 *  @module @/api/user
 */

import config from "@/config"
import http from "@/utils/request"

export default {

	/**
	 * 创建用户
	 */
	create :{
		url: `${config.API_URL}/user/create`,
		name: `创建用户`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},


	/**
	 * 用户登录
	 */
	login :{
		url: `${config.API_URL}/user/login`,
		name: `用户登录`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},


	/**
	 * 查询用户
	 */
	query :{
		url: `${config.API_URL}/user/query`,
		name: `查询用户`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},


	/**
	 * 当前用户信息
	 */
	userInfo :{
		url: `${config.API_URL}/user/user.info`,
		name: `当前用户信息`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},

}