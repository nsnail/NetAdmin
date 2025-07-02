<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close full-screen>
        <div v-loading="loading">
            <el-tabs tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form :disabled="mode === 'view'" :model="form" :rules="rules" label-width="10rem" ref="dialogForm">
                        <el-form-item :label="$t('归属文档分类')" prop="catalogId">
                            <catalog-select v-model="form.catalogId" class="w100p" />
                        </el-form-item>
                        <el-form-item :label="$t('文档标题')" prop="title">
                            <el-input v-model="form.title" clearable />
                        </el-form-item>
                        <el-form-item :label="$t('档案可见性')" prop="visibility">
                            <el-select v-model="form.visibility" clearable filterable>
                                <el-option v-for="(item, i) in $GLOBAL.enums.archiveVisibilities" :key="i" :label="item[1]" :value="i" />
                            </el-select>
                        </el-form-item>
                        <el-form-item :label="$t('文档内容')" prop="body">
                            <div
                                :class="
                                    this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK
                                        ? 'aie-theme-dark editor'
                                        : 'aie-theme-light editor'
                                "
                                ref="editor"></div>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('原始数据')">
                    <json-viewer
                        :expand-depth="5"
                        :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                        :value="form"
                        copyable
                        expanded
                        sort />
                </el-tab-pane>
            </el-tabs>
        </div>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import { AiEditor } from 'aieditor'
import 'aieditor/dist/style.css'
import sysConfig from '@/config'
const catalogSelect = defineAsyncComponent(() => import('../components/catalog-select'))
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
                catalogId: [{ required: true, message: this.$t('请选择归属文档分类') }],
                title: [{ required: true, message: this.$t('请输入文档标题') }],
                body: [{ required: true, message: this.$t('请输入文档内容') }],
                visibility: [{ required: true, message: this.$t('请选择档案可见性') }],
            },
            titleMap: {
                add: this.$t('新建文档'),
                edit: this.$t('编辑文档'),
                view: this.$t('查看文档'),
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
                const res = await this.$API.sys_doc.getContent.post({ id: data.row.id })
                Object.assign(this.form, res.data)
            }
            this.loading = false

            await this.$nextTick()
            const aiEditor = new AiEditor({
                element: this.$refs.editor,
                placeholder: this.$t('请输入消息内容...'),
                content: this.form.body,
                onChange: (body) => {
                    this.form.body = body.getHtml()
                },
            })
            aiEditor.changeLang(this.$TOOL.data.get('APP_SET_LANG') || sysConfig.APP_SET_LANG)

            return this
        },

        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true

            const method = this.mode === 'add' ? this.$API.sys_doc.createContent : this.$API.sys_doc.editContent
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

<style scoped>
.editor {
    width: 100%;
    height: 50rem;
}
</style>