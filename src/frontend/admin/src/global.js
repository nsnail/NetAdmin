import * as elIcons from '@element-plus/icons-vue'
import * as scIcons from '@/assets/icons'
import api from '@/api'
import auth from '@/directives/auth'
import config from '@/config'
import copy from '@/directives/copy'
import errorHandler from '@/utils/errorHandler'
import http from '@/utils/request'
import naArea from '@/components/naArea/index.vue'
import naUserSelect from '@/components/naUserSelect/index.vue'
import naButtonAdd from '@/components/naButtonAdd/index.vue'
import naButtonBatchDel from '@/components/naButtonBatchDel/index.vue'
import naColAvatar from '@/components/naColAvatar'
import naColIndicator from '@/components/naColIndicator/index.vue'
import naColOperation from '@/components/naColOperation'
import naColTags from '@/components/naColTags/index.vue'
import naDept from '@/components/naDept/index.vue'
import naDicCatalog from '@/components/naDicCatalog/index.vue'
import naFormEmail from '@/components/naFormEmail/index.vue'
import naSearch from '@/components/naSearch'
import role from '@/directives/role'
import scDialog from '@/components/scDialog'
import scFilterBar from '@/components/scFilterBar'
import scForm from '@/components/scForm'
import scFormTable from '@/components/scFormTable'
import scPageHeader from '@/components/scPageHeader'
import scQrCode from '@/components/scQrCode'
import scSelect from '@/components/scSelect'
import scStatusIndicator from '@/components/scMini/scStatusIndicator'
import scTable from '@/components/scTable'
import scTableColumn from '@/components/scTable/column.js'
import scTableSelect from '@/components/scTableSelect'
import scTitle from '@/components/scTitle'
import scTrend from '@/components/scMini/scTrend'
import scUpload from '@/components/scUpload'
import scUploadFile from '@/components/scUpload/file'
import scUploadMultiple from '@/components/scUpload/multiple'
import scWaterMark from '@/components/scWaterMark'
import time from '@/directives/time'
import tool from '@/utils/tool'
import JsonViewer from 'vue3-json-viewer'
import 'vue3-json-viewer/dist/index.css'
import naColTime from '@/components/naColTime/index.vue'

export default {
    install(app) {
        //挂载全局对象
        app.config.globalProperties.$CONFIG = config
        app.config.globalProperties.$TOOL = tool

        // http
        http.axios.app = () => app
        app.config.globalProperties.$HTTP = http

        app.config.globalProperties.$API = api
        app.config.globalProperties.$GLOBAL = {
            enums: null,
            menu: null,
            user: null,
            numbers: null,
            chars: null,
        }

        //注册全局组件
        app.use(JsonViewer)
        app.component('scTable', scTable)
        app.component('scTableColumn', scTableColumn)
        app.component('scFilterBar', scFilterBar)
        app.component('scUpload', scUpload)
        app.component('scUploadMultiple', scUploadMultiple)
        app.component('scUploadFile', scUploadFile)
        app.component('scFormTable', scFormTable)
        app.component('scTableSelect', scTableSelect)
        app.component('scPageHeader', scPageHeader)
        app.component('scSelect', scSelect)
        app.component('scDialog', scDialog)
        app.component('scForm', scForm)
        app.component('scTitle', scTitle)
        app.component('scWaterMark', scWaterMark)
        app.component('scQrCode', scQrCode)
        app.component('scStatusIndicator', scStatusIndicator)
        app.component('scTrend', scTrend)
        app.component('naSearch', naSearch)
        app.component('naColAvatar', naColAvatar)
        app.component('naColOperation', naColOperation)
        app.component('naButtonAdd', naButtonAdd)
        app.component('naColIndicator', naColIndicator)
        app.component('naColTags', naColTags)
        app.component('naArea', naArea)
        app.component('naDept', naDept)
        app.component('naDicCatalog', naDicCatalog)
        app.component('naButtonBatchDel', naButtonBatchDel)
        app.component('naFormEmail', naFormEmail)
        app.component('naColTime', naColTime)
        app.component('naUserSelect', naUserSelect)

        //注册全局指令
        app.directive('auth', auth)
        app.directive('role', role)
        app.directive('time', time)
        app.directive('copy', copy)

        //统一注册el-icon图标
        for (let icon in elIcons) {
            app.component(`ElIcon${icon}`, elIcons[icon])
        }
        //统一注册sc-icon图标
        for (let icon in scIcons) {
            app.component(`ScIcon${icon}`, scIcons[icon])
        }

        //关闭async-validator全局控制台警告
        window.ASYNC_VALIDATOR_NO_WARNING = 1

        //全局代码错误捕捉
        app.config.errorHandler = errorHandler
    },
}