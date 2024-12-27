import tool from '@/utils/tool'

export default (el, binding) => {
    let { value, modifiers } = binding
    if (!value) {
        return false
    }
    if (value.toString().length === 10) {
        value = value * 1000
    }
    if (modifiers.tip) {
        el.innerHTML = tool.time.getFormatTime(binding.instance, value)
        el.__timeout__ = setInterval(() => {
            el.innerHTML = tool.time.getFormatTime(binding.instance, value)
        }, 60000)
    } else {
        const format = el.getAttribute('format') || undefined
        el.innerHTML = tool.dateFormat(value, format)
    }
}