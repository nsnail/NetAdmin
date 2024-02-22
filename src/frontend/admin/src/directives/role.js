import { rolePermission } from '@/utils/permission'

export default {
    mounted(el, binding) {
        const { value } = binding
        if (Array.isArray(value)) {
            let ishas = false
            value.forEach((item) => {
                if (rolePermission(item, binding.instance.$GLOBAL.user)) {
                    ishas = true
                }
            })
            if (!ishas) {
                el.parentNode.removeChild(el)
            }
        } else {
            if (!rolePermission(value, binding.instance.$GLOBAL.user)) {
                el.parentNode.removeChild(el)
            }
        }
    },
}