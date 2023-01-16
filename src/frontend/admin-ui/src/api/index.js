/* eslint-disable no-undef */
/**
 * @description 自动import导入所有 api 模块
 */

const files = require.context('./model', false, /\.js$/)
const modules = {}
files.keys().forEach((key) => {
    modules[key.replace(/(\.\/|\.js)/g, '')] = files(key).default
})
const sys = require.context('./sys', false, /\.js$/)
sys.keys().forEach((key) => {
    modules['sys_' + key.replace(/(\.\/|\.js)/g, '')] = sys(key).default
})
export default modules