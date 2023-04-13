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


const tpl = require.context('./tpl', false, /example\.js$/)
tpl.keys().forEach((key) => {
    modules['tpl_' + key.replace(/(\.\/|\.js)/g, '')] = tpl(key).default
})

const cst = require.context('./cst', false, /\.js$/)
cst.keys().forEach((key) => {
    modules['cst_' + key.replace(/(\.\/|\.js)/g, '')] = cst(key).default
})

const res = require.context('./res', false, /\.js$/)
res.keys().forEach((key) => {
    modules['res_' + key.replace(/(\.\/|\.js)/g, '')] = res(key).default
})
const tsk = require.context('./tsk', false, /\.js$/)
tsk.keys().forEach((key) => {
    modules['tsk_' + key.replace(/(\.\/|\.js)/g, '')] = tsk(key).default
})
export default modules