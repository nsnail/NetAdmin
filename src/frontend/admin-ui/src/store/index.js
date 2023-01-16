/**
 * @description 自动import导入所有 vuex 模块
 */

import {createStore} from 'vuex';

// eslint-disable-next-line no-undef
const files = require.context('./modules', false, /\.js$/);
const modules = {}
files.keys().forEach((key) => {
    modules[key.replace(/(\.\/|\.js)/g, '')] = files(key).default
})

export default createStore({
    modules
});