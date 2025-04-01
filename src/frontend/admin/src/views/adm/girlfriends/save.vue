<template>
    <scDialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close full-screen>
        <div v-loading="loading">
            <el-tabs v-model="tabId" tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form :disabled="mode === 'view'" :model="form" :rules="rules" label-width="15rem" ref="dialogForm">
                        <el-form-item :label="$t('姓名')" prop="name">
                            <el-input v-model="form.name" :placeholder="$t('请输入姓名')" clearable></el-input>
                        </el-form-item>
                        <el-form-item :label="$t('年龄')" prop="age">
                            <el-input-number v-model="form.age" :min="0" controls-position="right"  style="width: 10%">
                            </el-input-number>
                        </el-form-item>
                        <el-form-item :label="$t('身高')" prop="height">
                            <el-input-number v-model="form.height" :min="0" controls-position="right"  style="width: 10%">
                                <template #suffix>
                                    <span>cm</span>
                                </template>
                            </el-input-number>
                        </el-form-item>
                        <el-form-item :label="$t('爱好')" prop="interest">
                            <el-input v-model="form.interest" clearable type="textarea"></el-input>
                        </el-form-item>
                        <el-form-item :label="$t('种子')" prop="seed">
                            <el-input v-model="form.seed" clearable type="textarea"></el-input>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('原始数据')">
                    <JsonViewer
                        :expand-depth="5"
                        :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                        :value="form"
                        copyable
                        expanded
                        sort></JsonViewer>
                </el-tab-pane>
            </el-tabs>
        </div>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </scDialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'

const User = defineAsyncComponent(() => import('@/views/sys/user'))
export default {
    components: { User },
    data() {
        return {
            //所需数据选项
            depts: [],
            deptsProps: {
                label: 'name',
                value: 'id',
                emitPath: false,
                checkStrictly: true,
            },
            //表单数据
            form: {},
            loading: true,
            mode: 'add',
            //验证规则
            rules: {
                name: [{ required: true, message: '请输入姓名' }],
                age: [{ required: true, message: '请输入年龄' }],
                height: [{ required: true, message: '请输入身高' }]
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
            this.visible = true
            this.loading = true
            this.mode = data.mode
            if (data.row?.id) {
                const res = await this.$API.adm_girlfriends.get.post({ id: data.row.id })
                Object.assign(this.form, res.data)
            }
            this.loading = false
            if (data.tabId) {
                this.tabId = data.tabId
            }
            return this
        },
        //加载树数据
        async getGroup() {
            const res = await this.$API.adm_girlfriends.query.post()
            this.depts = res.data
        },
        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true
            const method = this.mode === 'add' ? this.$API.adm_girlfriends.create : this.$API.adm_girlfriends.edit
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
        this.$emit('mounted')
        this.getGroup()
    },
}
</script>

<style scoped>

</style>