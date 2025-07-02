<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close full-screen>
        <el-form
            v-loading="loading"
            :disabled="mode === 'view'"
            :model="form"
            :rules="rules"
            label-position="right"
            label-width="10rem"
            ref="dialogForm">
            <el-tabs tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form-item v-if="mode === 'view'" :label="$t('消息编号')" prop="id">
                        <el-input v-model="form.id" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('消息类型')" prop="msgType">
                        <el-select v-model="form.msgType" clearable filterable>
                            <el-option v-for="(item, i) in $GLOBAL.enums.siteMsgTypes" :key="i" :label="item[1]" :value="i" />
                        </el-select>
                    </el-form-item>
                    <el-form-item :label="$t('消息主题')" prop="title">
                        <el-input v-model="form.title" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('消息内容')" prop="content">
                        <div
                            :class="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'aie-theme-dark' : 'aie-theme-light'"
                            ref="editor"
                            style="width: 100%; height: 30rem"></div>
                    </el-form-item>

                    <el-form-item :label="$t('送至角色')" prop="roleIds">
                        <sc-select
                            v-if="!this.loading"
                            v-model="form.roleIds"
                            :config="{ props: { label: 'name', value: 'id' } }"
                            :query-api="$API.sys_role.query"
                            class="w100p"
                            clearable
                            filterable
                            multiple />
                    </el-form-item>
                    <el-form-item :label="$t('送至部门')" prop="deptIds">
                        <na-dept v-model="form.deptIds" :multiple="true" class="w100p" />
                    </el-form-item>
                    <el-form-item :label="$t('送至用户')" prop="userIds">
                        <na-user-select v-model="form.userIds" :multiple="true" class="w100p" />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('创建时间')" prop="createdTime">
                        <el-input v-model="form.createdTime" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('修改时间')" prop="modifiedTime">
                        <el-input v-model="form.modifiedTime" clearable />
                    </el-form-item>
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
        </el-form>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { AiEditor } from 'aieditor'
import 'aieditor/dist/style.css'
import sysConfig from '@/config'
import { defineAsyncComponent } from 'vue'
const naDept = defineAsyncComponent(() => import('@/components/na-dept'))
const naUserSelect = defineAsyncComponent(() => import('@/components/na-user-select'))
const scSelect = defineAsyncComponent(() => import('@/components/sc-select'))
export default {
    components: {
        naDept,
        naUserSelect,
        scSelect,
    },
    data() {
        return {
            //表单数据
            form: {
                userIds: [],
                roleIds: [],
                deptIds: [],
            },
            loading: true,
            mode: 'add',
            //验证规则
            rules: {
                title: [
                    {
                        required: true,
                        message: '消息主题不能为空',
                    },
                ],
                msgType: [
                    {
                        required: true,
                        message: '请选择消息类型',
                    },
                ],
                content: [
                    {
                        required: true,
                        message: '消息内容不能为空',
                    },
                ],
            },
            titleMap: {
                add: this.$t('新建消息'),
                edit: this.$t('编辑消息'),
                view: this.$t('查看消息'),
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
                const res = await this.$API.sys_sitemsg.get.post({ id: data.row.id })
                Object.assign(this.form, res.data, {
                    roleIds: res.data.roles?.map((x) => x.id) ?? [],
                    deptIds: res.data.depts?.map((x) => x.id) ?? [],
                    userIds: res.data.users ?? [],
                })
            }
            this.loading = false

            await this.$nextTick()
            const aiEditor = new AiEditor({
                element: this.$refs.editor,
                placeholder: this.$t('请输入消息内容...'),
                content: this.form.content,
                onChange: (content) => {
                    this.form.content = content.getHtml()
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
            const method = this.mode === 'add' ? this.$API.sys_sitemsg.create : this.$API.sys_sitemsg.edit
            try {
                const res = await method.post(Object.assign({}, this.form, { userIds: this.form.userIds.map((x) => x.id) }))
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

<style scoped />