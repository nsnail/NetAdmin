/**
 * @description 自动import导入所有 vuex 模块
 */

import { createStore } from 'vuex'

const files = import.meta.globEager('./modules/*.js')
const modules = {}
Object.keys(files).forEach((key) => {
    modules[key.replace(/^\.\/modules\/(.*)\.js$/g, '$1')] = files[key].default
})

export default createStore({
    modules,
})