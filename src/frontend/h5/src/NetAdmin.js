import config from './config'
import api from './api'
import tool from './utils/tool'
import http from './utils/request'

export default {
  async install(app) {
    //挂载全局对象
    app.config.globalProperties.$CONFIG = config
    app.config.globalProperties.$TOOL = tool
    app.config.globalProperties.$HTTP = http
    app.config.globalProperties.$API = api
    const res = await Promise.all([
      api['sys/constant'].getChars.post(),
      api['sys/constant'].getEnums.post(),
      api['sys/constant'].getLocalizedStrings.post(),
      api['sys/constant'].getNumbers.post()
    ])

    config.STRINGS = res[0].data
    config.ENUMS = res[1].data
    config.LOC_STRINGS = res[2].data
    config.NUMBERS = res[3].data
  }
}