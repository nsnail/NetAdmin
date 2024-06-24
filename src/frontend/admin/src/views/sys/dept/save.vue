<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close full-screen>
        <div v-loading="loading">
            <el-tabs v-model="tabId" tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form :disabled="mode === 'view'" :model="form" :rules="rules" label-width="15rem" ref="dialogForm">
                        <el-form-item :label="$t('上级部门')" prop="parentId">
                            <el-cascader
                                v-model="form.parentId"
                                :options="depts"
                                :props="deptsProps"
                                :show-all-levels="false"
                                clearable
                                style="width: 100%"></el-cascader>
                        </el-form-item>
                        <el-form-item :label="$t('部门名称')" prop="name">
                            <el-input v-model="form.name" :placeholder="$t('请输入部门名称')" clearable></el-input>
                        </el-form-item>
                        <el-form-item :label="$t('排序')" prop="sort">
                            <el-input-number v-model="form.sort" :min="0" controls-position="right" style="width: 100%"></el-input-number>
                        </el-form-item>
                        <el-form-item :label="$t('启用')" prop="enabled">
                            <el-switch v-model="form.enabled"></el-switch>
                        </el-form-item>
                        <el-form-item :label="$t('备注')" prop="summary">
                            <el-input v-model="form.summary" clearable type="textarea"></el-input>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('用户列表')" name="user">
                    <user v-if="tabId === 'user'" :dept-id="form.id"></user>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('原始数据')">
                    <json-viewer
                        :expand-depth="5"
                        :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                        :value="form"
                        copyable
                        expanded
                        sort></json-viewer>
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

const User = defineAsyncComponent(() => import('@/views/sys/user/index.vue'))
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
            form: { enabled: true, sort: 100 },
            loading: false,
            mode: 'add',
            //验证规则
            rules: {
                sort: [
                    {
                        required: true,
                        message: '请输入排序',
                        trigger: 'change',
                    },
                ],
                name: [{ required: true, message: '请输入部门名称' }],
            },
            tabId: '0',
            titleMap: {
                add: this.$t('新增部门'),
                edit: this.$t('编辑部门'),
                view: this.$t('查看部门'),
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
                const res = await this.$API.sys_dept.get.post({ id: data.row.id })
                Object.assign(this.form, res.data)
            }
            this.loading = false
            return this
        },
        //加载树数据
        async getGroup() {
            const res = await this.$API.sys_dept.query.post()
            this.depts = res.data
        },
        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true
            const method = this.mode === 'add' ? this.$API.sys_dept.create : this.$API.sys_dept.edit
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

<style scoped></style>