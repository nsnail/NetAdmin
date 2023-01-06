/**
 * @description 自动import导入所有 api 模块
 */

const model = require.context('./model', false, /\.js$/)
const modules = {}
model.keys().forEach((key) => {
	modules[key.replace(/(\.\/|\.js)/g, '')] = model(key).default
})
const sys = require.context('./sys', false, /\.js$/)
sys.keys().forEach((key) => {
	modules['sys_'+key.replace(/(\.\/|\.js)/g, '')] = sys(key).default
})
export default modules