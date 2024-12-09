<template>
    <el-card :header="$t('授权信息')" shadow="never">
        <el-form :model="form" :rules="rules" label-width="10rem" ref="form">
            <el-form-item :label="$t('用户标识')">
                <el-input v-model="form.id" class="font-monospace" readonly></el-input>
            </el-form-item>
            <el-form-item :label="$t('授权令牌')">
                <el-input v-model="form.token" class="font-monospace" readonly rows="10" type="textarea"></el-input>
            </el-form-item>
            <el-form-item :label="$t('过期时间')">
                <el-input v-model="form.exp" class="font-monospace" readonly></el-input>
            </el-form-item>
            <el-form-item>
                <el-button v-copy="form.token" type="primary">{{ $t('复制授权令牌') }}</el-button>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script>
import tool from '@/utils/tool'

export default {
    components: {},
    methods: {},

    created() {
        this.form.token = tool.cookie.get('ACCESS-TOKEN')
        let payload = this.form.token.split(' ')[1].split('.')[1]
        payload = payload.replaceAll('-', '+').replaceAll('_', '/')
        while (payload.length % 4) payload += '='
        const jwt = JSON.parse(tool.crypto.BASE64.decrypt(payload))
        this.form.id = jwt.ContextUserToken.Token
        this.form.exp = new Date(jwt.exp * 1000)
    },
    data() {
        return {
            loading: false,
            dialog: {},
            form: {
                token: null,
                exp: null,
                id: null,
            },
            rules: {},
        }
    },
}
</script>

<style scoped></style>