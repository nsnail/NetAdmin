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
import 'vue3-json-viewer/dist/vue3-json-viewer.css'
import JsonViewer from 'vue3-json-viewer'

// VAceEditor
import ace from 'ace-builds'
import 'ace-builds/src-noconflict/mode-json' // Load the language definition file used below
import 'ace-builds/src-noconflict/theme-github' // Load the theme definition file used below
import 'ace-builds/src-noconflict/theme-github_dark' // Load the theme definition file used below
import { VAceEditor } from 'vue3-ace-editor'

// sc组件
import scDialog from '@/components/scDialog'
import scSelectFilter from '@/components/scSelectFilter'
import scStatistic from '@/components/scStatistic'
import scStatusIndicator from '@/components/scMini/scStatusIndicator'
import scTable from '@/components/scTable'

// net-admin组件
import naButtonBulkDel from '@/components/naButtonBulkDel'
import naColId from '@/components/naColId'
import naColIndicator from '@/components/naColIndicator'
import naColOperation from '@/components/naColOperation'
import naColUser from '@/components/naColUser'
import naSearch from '@/components/naSearch'

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
            hasPermission: function (p) {
                return this.permissions.includes('*/*/*') || this.permissions.some((a) => a === p)
            },
            hasApiPermission: function (p) {
                return this.apiPermissions.includes('*/*/*') || this.apiPermissions.some((a) => a === p)
            },
        }

        app.use(JsonViewer)

        app.component('VAceEditor', VAceEditor)

        // net-admin组件
        app.component('naButtonBulkDel', naButtonBulkDel)
        app.component('naColId', naColId)
        app.component('naColIndicator', naColIndicator)
        app.component('naColOperation', naColOperation)
        app.component('naColUser', naColUser)
        app.component('naSearch', naSearch)

        // sc组件
        app.component('scDialog', scDialog)
        app.component('scSelectFilter', scSelectFilter)
        app.component('scStatistic', scStatistic)
        app.component('scStatusIndicator', scStatusIndicator)
        app.component('scTable', scTable)

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