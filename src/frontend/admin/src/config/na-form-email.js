export default {
    email: (_this) => {
        return {
            required: true,
            message: _this.$t('您的邮箱地址'),
            pattern: _this.$GLOBAL.chars.RGXL_EMAIL,
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
                callback(new Error(_this.$t('邮箱已被使用')))
            },
            trigger: 'blur',
        }
    },

    code: (_this) => {
        return {
            required: true,
            message: _this.$t('请输入4位数字验证码'),
        }
    },
}