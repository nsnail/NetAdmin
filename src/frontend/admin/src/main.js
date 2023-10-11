import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import zhCn from 'element-plus/es/locale/lang/zh-cn'
import 'element-plus/dist/index.css'
import 'element-plus/theme-chalk/display.css'
import global from './global'
import i18n from './locales'
import router from './router'
import store from './store'
import App from './App.vue'
import preload from '@/utils/preload'

const app = createApp(App)
app.use(ElementPlus, { locale: zhCn })
app.use(store)
app.use(i18n)
app.use(global)

preload.install(app).then(() => {
    app.use(router)
    //挂载app
    app.mount('#app')
})