import './assets/main.css'
import { createApp } from 'vue'
import App from './App.vue'
import { createPinia } from 'pinia'
import router from './router'
import ddf from '@/ddf'
import van from '@/van'
import '@vant/touch-emulator'

const app = createApp(App)
app.use(createPinia())
app.use(router)
app.use(van)
await ddf.install(app)
app.mount('#app')