/*
 * @Author: Xujianchen
 * @Date: 2023-02-28 14:12:15
 * @LastEditors: Xujianchen
 * @LastEditTime: 2023-03-18 12:45:04
 * @Description:
 */
import { markRaw } from 'vue'

const resultComps = {}
const files = import.meta.glob('./*.vue', { eager: true })
Object.keys(files).forEach((fileName) => {
    let comp = files[fileName]
    resultComps[fileName.replace(/^\.\/(.*)\.\w+$/, '$1')] = comp.default
})
export default markRaw(resultComps)