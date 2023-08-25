export default {
    mobile: (_this) => {
        return {
            required: true,
            message: '您的手机号码',
            pattern: _this.$GLOBAL.chars.RGX_MOBILE,
        }
    },
    mobileNoUsed: (_this, id) => {
        return {
            validator: async (rule, valueEquals, callback) => {
                try {
                    const res = await _this.$API.sys_user.checkMobileAvailable.post({
                        mobile: valueEquals,
                        id: id(),
                    })
                    if (res.data) {
                        return callback()
                    }
                } catch {
                    //
                }
                callback(new Error('手机号已被使用'))
            },
            trigger: 'blur',
        }
    },

    code: () => {
        return {
            required: true,
            message: '请输入4位数字验证码',
        }
    },
}