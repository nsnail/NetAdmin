export default {
    passwordText: (_this) => {
        return {
            required: true,
            message: _this.$t('8位以上数字字母组合'),
            pattern: _this.$GLOBAL.chars.RGX_PASSWORD,
        }
    },

    passwordText2: (_this, passwordText) => [
        { required: true, message: _this.$t('请再次输入密码') },
        {
            validator: (rule, value, callback) => {
                if (value !== passwordText()) {
                    callback(new Error(_this.$t('两次输入密码不一致')))
                } else {
                    callback()
                }
            },
        },
    ],
}