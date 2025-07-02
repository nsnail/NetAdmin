import { createApp } from 'vue'
import elementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import 'element-plus/theme-chalk/display.css'
import global from '@/global'
import i18n from '@/locales'
import router from '@/router'
import store from '@/store'
import app from '@/app'
import preload from '@/utils/preload'
import passiveEventListener from '@/utils/passive-event-listener'

const appInstance = createApp(app)
appInstance.use(elementPlus)
appInstance.use(store)
appInstance.use(i18n)
appInstance.use(global)
appInstance.use(passiveEventListener)

preload.install(appInstance).then(() => {
    appInstance.use(router)
    //挂载appInstance
    appInstance.mount('#app')
})