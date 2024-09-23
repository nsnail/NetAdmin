<template>
    <sc-dialog
        v-model="visible"
        :title="`${titleMap[mode]}：${form?.id ?? '...'}`"
        @closed="$emit('closed')"
        append-to-body
        destroy-on-close
        full-screen>
        <el-form
            v-loading="loading"
            :disabled="mode === 'view' && tabId !== 'log'"
            :model="form"
            :rules="rules"
            label-position="right"
            label-width="12rem"
            ref="dialogForm">
            <el-tabs v-model="tabId" tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form-item v-if="mode === 'view'" :label="$t('唯一编码')" prop="id">
                        <el-input v-model="form.id" clearable></el-input>
                    </el-form-item>
                    <el-form-item :label="$t('姓名')" prop="realName">
                        <el-input v-model="form.realName" :placeholder="$t('真实姓名')" clearable></el-input>
                    </el-form-item>
                    <el-row :gutter="10">
                        <el-col :lg="12">
                            <el-form-item :label="$t('年龄')" prop="age">
                                <el-input v-model="form.age" :placeholder="$t('年龄')" clearable></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('身高')" prop="height">
                                <el-input v-model="form.height" :placeholder="$t('请输入身高')" clearable></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>

                    <el-form-item :label="$t('爱好')" prop="hobby">
                        <el-input v-model="form.hobby" clearable type="textarea"></el-input>
                    </el-form-item>
                </el-tab-pane>
            </el-tabs>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'

const log = defineAsyncComponent(() => import('@/views/sys/log/operation/index.vue'))
export default {
    components: { log },
    data() {
        return {
            //表单数据
            form: {},
            loading: true,
            mode: 'add',
            //验证规则
            rules: {
                realName: [
                    {
                        required: true,
                    },
                ],
            },
            tabId: '0',
            titleMap: {
                add: this.$t('新增女朋友'),
                edit: this.$t('编辑女朋友'),
                view: this.$t('查看女朋友'),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            console.log(data);
            this.visible = true
            this.loading = true
            this.mode = data.mode
            if (data.row?.id) {
                let res = await this.$API.adm_girlfriends.get.post({ id: data.row.id })
                if (res && res.code == "succeed") {
                    this.form = res.data
                    console.log("form: ", this.form)
                }
            }
            this.loading = false
            return this
        },

        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true
            const method = this.mode === 'add' ? this.$API.adm_girlfriends.create : this.$API.adm_girlfriends.edit
            console.log("submit: ", valid, this.mode, method)
            try {
                const res = await method.post(this.form)
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success(this.$t('操作成功'))
            } catch {}
            this.loading = false
        },
    },
    mounted() {
        console.log(this.$GLOBAL)
        console.log(this.$API)
        this.$emit('mounted')
    },
}
</script>

<style scoped>
.password {
    display: flex;
    width: 100%;
    gap: 0.5rem;
}
</style>