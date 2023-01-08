/**
 *  常量接口
 *  @module @/api/constant
 */

import config from "@/config"
import http from "@/utils/request"

export default {

	/**
	 * 获得枚举常量
	 */
	getEnums :{
		url: `${config.API_URL}/constant/get.enums`,
		name: `获得枚举常量`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},


	/**
	 * 获得本地化字符串常量
	 */
	getLocalizedStrings :{
		url: `${config.API_URL}/constant/get.localized.strings`,
		name: `获得本地化字符串常量`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},


	/**
	 * 获得字符串常量
	 */
	getStrings :{
		url: `${config.API_URL}/constant/get.strings`,
		name: `获得字符串常量`,
		post:async function(data, config={}) {
			return await http.post(this.url,data, config)
		}
	},

}