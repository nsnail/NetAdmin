<template>
    <sc-dialog
        v-model="visible"
        :title="`${titleMap[mode]}：${form?.id ?? '...'}`"
        :width="800"
        @closed="$emit('closed')"
        destroy-on-close
        full-screen>
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
                        <sc-editor v-model="form.content" :placeholder="$t('请输入')" class="w100p" height="800"></sc-editor>
                    </el-form-item>

                    <el-form-item :label="$t('送至角色')" prop="roleIds">
                        <sc-select
                            v-if="!this.loading"
                            v-model="form.roleIds"
                            :apiObj="$API.sys_role.query"
                            :config="{ props: { label: 'name', value: 'id' } }"
                            class="w100p"
                            clearable
                            filterable
                            multiple />
                    </el-form-item>
                    <el-form-item :label="$t('送至部门')" prop="deptIds">
                        <na-dept v-model="form.deptIds" :multiple="true" class="w100p"></na-dept>
                    </el-form-item>
                    <el-form-item :label="$t('送至用户')" prop="userIds">
                        <na-user-select v-model="form.userIds" :multiple="true" class="w100p"></na-user-select>
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
                        :theme="this.$TOOL.data.get('APP_DARK') ? 'dark' : 'light'"
                        :value="form"
                        copyable
                        expanded
                        sort></json-viewer>
                </el-tab-pane>
            </el-tabs>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button v-if="mode !== 'view'" :loading="loading" @click="submit" type="primary">保 存</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import scEditor from '@/components/scEditor/index.vue'

export default {
    components: {
        scEditor,
    },
    emits: ['success', 'closed'],
    data() {
        return {
            mode: 'add',
            titleMap: {
                view: '查看消息',
                add: '新增消息',
                edit: '编辑消息',
            },
            visible: false,
            loading: false,
            //表单数据
            form: {
                userIds: [],
                roleIds: [],
                deptIds: [],
            },
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
        }
    },
    mounted() {},
    methods: {
        //显示
        async open(mode = 'add', data) {
            this.visible = true
            this.loading = true
            this.mode = mode
            if (data) {
                const res = await this.$API.sys_sitemsg.get.post({ id: data.id })
                Object.assign(this.form, res.data, {
                    roleIds: res.data.roles?.map((x) => x.id) ?? [],
                    deptIds: res.data.depts?.map((x) => x.id) ?? [],
                    userIds: res.data.users ?? [],
                })
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

            try {
                const method = this.mode === 'add' ? this.$API.sys_sitemsg.create : this.$API.sys_sitemsg.edit
                this.loading = true
                const res = await method.post(Object.assign({}, this.form, { userIds: this.form.userIds.map((x) => x.id) }))
                this.loading = false
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success('操作成功')
            } catch {
                //
                this.loading = false
            }
        },
    },
}
</script>