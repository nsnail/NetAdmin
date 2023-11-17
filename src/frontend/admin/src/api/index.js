/**
 * @description 自动import导入所有 api 模块
 */

const files = import.meta.glob('./*/*.js', { eager: true })
const modules = {}
Object.keys(files).forEach((key) => {
    modules[key.replace(/^\.\/(.*?)\/(.*)\.js$/g, '$1_$2')] = files[key].default
})

export default modules