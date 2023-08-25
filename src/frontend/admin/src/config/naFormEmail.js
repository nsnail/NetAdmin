export default {
    email: (_this) => {
        return {
            required: true,
            message: '您的邮箱地址',
            pattern: _this.$GLOBAL.chars.RGX_EMAIL,
        }
    },
    emailNoUsed: (_this, id) => {
        return {
            validator: async (rule, valueEquals, callback) => {
                try {
                    const res = await _this.$API.sys_user.checkEmailAvailable.post({
                        email: valueEquals,
                        id: id(),
                    })
                    if (res.data) {
                        return callback()
                    }
                } catch {
                    //
                }
                callback(new Error('邮箱已被使用'))
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