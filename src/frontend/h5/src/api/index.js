/**
 * @description 自动import导入所有 api 模块
 */

const modules = {}

const files = import.meta.glob('./**/*.js')
for await (const key of Object.keys(files)) {
    const name = key.replace(/^\.\/(.*)\.\w+$/, '$1')
    modules[name] = (await files[key]()).default
}

export default modules