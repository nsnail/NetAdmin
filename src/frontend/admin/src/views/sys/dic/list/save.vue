<template>
    <scDialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close>
        <div v-loading="loading">
            <el-tabs tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form :disabled="mode === 'view'" :model="form" :rules="rules" label-width="10rem" ref="dialogForm">
                        <el-form-item :label="$t('归属字典目录')" prop="catalogId">
                            <catalog-select v-model="form.catalogId" class="w100p" />
                        </el-form-item>
                        <el-form-item :label="$t('项名')" prop="key">
                            <el-input v-model="form.key" clearable></el-input>
                        </el-form-item>
                        <el-form-item :label="$t('项值')" prop="value">
                            <el-input v-model="form.value" clearable></el-input>
                        </el-form-item>
                        <el-form-item :label="$t('备注')" prop="summary">
                            <el-input v-model="form.summary" clearable></el-input>
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
const catalogSelect = defineAsyncComponent(() => import('../components/catalog-select.vue'))
export default {
    components: { catalogSelect },
    data() {
        return {
            //表单数据
            form: {},
            loading: true,
            mode: 'add',
            //验证规则
            rules: {
                catalogId: [{ required: true, message: '请选择归属字典目录' }],
                key: [{ required: true, message: '请输入项名' }],
                value: [{ required: true, message: '请输入项值' }],
            },
            titleMap: {
                add: this.$t('新建字典项'),
                edit: this.$t('编辑字典项'),
                view: this.$t('查看字典项'),
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
            this.form.catalogId = data.data?.catalogId
            if (data.row?.id) {
                const res = await this.$API.sys_dic.getContent.post({ id: data.row.id })
                Object.assign(this.form, res.data)
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

            const method = this.mode === 'add' ? this.$API.sys_dic.createContent : this.$API.sys_dic.editContent
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
    },
}
</script>

<style scoped></style>