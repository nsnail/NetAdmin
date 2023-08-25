export default {
    passwordText: (_this) => {
        return {
            required: true,
            message: '8位以上数字字母组合',
            pattern: _this.$GLOBAL.chars.RGX_PASSWORD,
        }
    },

    passwordText2: (passwordText) => [
        { required: true, message: '请再次输入密码' },
        {
            validator: (rule, value, callback) => {
                if (value !== passwordText()) {
                    callback(new Error('两次输入密码不一致'))
                } else {
                    callback()
                }
            },
        },
    ],
}