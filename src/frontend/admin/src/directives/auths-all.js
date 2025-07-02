import { judementSameArr, permissionAll } from '@/utils/permission'

/**
 * 用户权限指令
 * @directive 单个权限验证（v-auth="'xxx'"）
 * @directive 多个权限验证，满足一个则显示（v-auths="['xxx','xxx']"）
 * @directive 多个权限验证，全部满足则显示（v-auths-all="['xxx','xxx']"）
 */
export default {
    mounted(el, binding) {
        if (permissionAll(binding.instance.$GLOBAL.permissions)) {
            return
        }
        const flag = judementSameArr(binding.value, binding.instance.$GLOBAL.permissions)
        if (!flag) el.parentNode.removeChild(el)
    },
}