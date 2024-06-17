// noinspection ES6UnusedImports

import * as elIcons from '@element-plus/icons-vue'
import * as scIcons from '@/assets/icons'
import api from '@/api'
import config from '@/config'
import errorHandler from '@/utils/errorHandler'
import http from '@/utils/request'
import tool from '@/utils/tool'

// 自定义指令
import auth from '@/directives/auth'
import copy from '@/directives/copy'
import role from '@/directives/role'
import time from '@/directives/time'

// vue3-json-viewer
import 'vue3-json-viewer/dist/index.css'
import JsonViewer from 'vue3-json-viewer'

// VAceEditor
import ace from 'ace-builds'
import 'ace-builds/src-noconflict/mode-json' // Load the language definition file used below
import 'ace-builds/src-noconflict/theme-github' // Load the theme definition file used below
import 'ace-builds/src-noconflict/theme-github_dark' // Load the theme definition file used below
import { VAceEditor } from 'vue3-ace-editor'

// sc组件
import scCron from '@/components/scCron/index.vue'
import scDialog from '@/components/scDialog'
import scFilterBar from '@/components/scFilterBar'
import scFormTable from '@/components/scFormTable'
import scPageHeader from '@/components/scPageHeader'
import scSelect from '@/components/scSelect'
import scSelectFilter from '@/components/scSelectFilter'
import scStatistic from '@/components/scStatistic/index.vue'
import scStatusIndicator from '@/components/scMini/scStatusIndicator'
import scTable from '@/components/scTable'
import scTableColumn from '@/components/scTable/column.js'
import scTableSelect from '@/components/scTableSelect'
import scTrend from '@/components/scMini/scTrend'
import scUpload from '@/components/scUpload'
import scUploadFile from '@/components/scUpload/file'
import scUploadMultiple from '@/components/scUpload/multiple'
import scWaterMark from '@/components/scWaterMark'

// net-admin组件
import naArea from '@/components/naArea/index.vue'
import naButtonAdd from '@/components/naButtonAdd/index.vue'
import naButtonBatchDel from '@/components/naButtonBatchDel/index.vue'
import naColAvatar from '@/components/naColAvatar'
import naColId from '@/components/naColId/index.vue'
import naColIndicator from '@/components/naColIndicator/index.vue'
import naColOperation from '@/components/naColOperation'
import naColTags from '@/components/naColTags/index.vue'
import naColTime from '@/components/naColTime/index.vue'
import naColUser from '@/components/naColUser/index.vue'
import naDept from '@/components/naDept/index.vue'
import naDicCatalog from '@/components/naDicCatalog/index.vue'
import naFormEmail from '@/components/naFormEmail/index.vue'
import naIp from '@/components/naIp/index.vue'
import naSearch from '@/components/naSearch'
import naUserSelect from '@/components/naUserSelect/index.vue'

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

        app.use(JsonViewer)

        app.component('VAceEditor', VAceEditor)

        // net-admin组件
        app.component('naArea', naArea)
        app.component('naButtonAdd', naButtonAdd)
        app.component('naButtonBatchDel', naButtonBatchDel)
        app.component('naColAvatar', naColAvatar)
        app.component('naColId', naColId)
        app.component('naColIndicator', naColIndicator)
        app.component('naColOperation', naColOperation)
        app.component('naColTags', naColTags)
        app.component('naColTime', naColTime)
        app.component('naColUser', naColUser)
        app.component('naDept', naDept)
        app.component('naDicCatalog', naDicCatalog)
        app.component('naFormEmail', naFormEmail)
        app.component('naIp', naIp)
        app.component('naSearch', naSearch)
        app.component('naUserSelect', naUserSelect)

        // sc组件
        app.component('scCron', scCron)
        app.component('scDialog', scDialog)
        app.component('scFilterBar', scFilterBar)
        app.component('scFormTable', scFormTable)
        app.component('scPageHeader', scPageHeader)
        app.component('scSelect', scSelect)
        app.component('scSelectFilter', scSelectFilter)
        app.component('scStatistic', scStatistic)
        app.component('scStatusIndicator', scStatusIndicator)
        app.component('scTable', scTable)
        app.component('scTableColumn', scTableColumn)
        app.component('scTableSelect', scTableSelect)
        app.component('scTrend', scTrend)
        app.component('scUpload', scUpload)
        app.component('scUploadFile', scUploadFile)
        app.component('scUploadMultiple', scUploadMultiple)
        app.component('scWaterMark', scWaterMark)

        //注册全局指令
        app.directive('auth', auth)
        app.directive('copy', copy)
        app.directive('role', role)
        app.directive('time', time)

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