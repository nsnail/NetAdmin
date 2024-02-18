<template>
    <sc-dialog v-model="visible" :title="titleMap[mode]" :width="800" @closed="$emit('closed')" destroy-on-close full-screen>
        <el-tabs v-model="tabIndex" tab-position="top">
            <el-tab-pane :label="$t('基本信息')" :name="0">
                <el-form
                    v-loading="loading"
                    :disabled="mode === 'view'"
                    :model="form"
                    :rules="rules"
                    label-position="right"
                    label-width="150px"
                    ref="dialogForm">
                    <el-form-item v-if="mode === 'view'" :label="$t('作业编号')" prop="id">
                        <el-input v-model="form.id" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('执行计划')" prop="executionCron">
                        <el-input v-model="form.executionCron" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('请求方法')" prop="httpMethod">
                        <el-select v-model="form.httpMethod" clearable filterable>
                            <el-option v-for="(item, i) in $GLOBAL.enums.httpMethods" :key="i" :label="item[1]" :value="i" />
                        </el-select>
                    </el-form-item>
                    <el-form-item :label="$t('作业名称')" prop="jobName">
                        <el-input v-model="form.jobName" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('上次执行时间')" prop="lastExecTime">
                        <el-input v-model="form.lastExecTime" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('下次执行时间')" prop="nextExecTime">
                        <el-input v-model="form.nextExecTime" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('下次执行时间编号')" prop="nextTimeId">
                        <el-input v-model="form.nextTimeId" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('请求头')" prop="requestHeader">
                        <el-input v-model="form.requestHeader" clearable rows="5" type="textarea" />
                    </el-form-item>
                    <el-form-item :label="$t('请求体')" prop="requestBody">
                        <el-input v-model="form.requestBody" clearable rows="5" type="textarea" />
                    </el-form-item>
                    <el-form-item :label="$t('请求的网络地址')" prop="requestUrl">
                        <el-input v-model="form.requestUrl" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('作业状态')" prop="status">
                        <el-input v-model="form.status" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('执行用户编号')" prop="userId">
                        <el-input v-model="form.userId" clearable />
                    </el-form-item>
                    <el-form-item v-else :label="$t('执行用户')" prop="user">
                        <na-user-select v-model="form.user"></na-user-select>
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('创建时间')" prop="createdTime">
                        <el-input v-model="form.createdTime" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('修改时间')" prop="modifiedTime">
                        <el-input v-model="form.modifiedTime" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('数据版本')" prop="version">
                        <el-input v-model="form.version" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('创建者编号')" prop="createdUserId">
                        <el-input v-model="form.createdUserId" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('创建者用户名')" prop="createdUserName">
                        <el-input v-model="form.createdUserName" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('修改者编号')" prop="modifiedUserId">
                        <el-input v-model="form.modifiedUserId" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('修改者用户名')" prop="modifiedUserName">
                        <el-input v-model="form.modifiedUserName" clearable />
                    </el-form-item>
                </el-form>
            </el-tab-pane>
            <el-tab-pane v-if="mode === 'view'" :label="$t('执行记录')" :name="1">
                <record v-if="tabIndex === 1" :keywords="form.id" />
            </el-tab-pane>
            <el-tab-pane v-if="mode === 'view'" :label="$t('原始数据')" :name="2">
                <json-viewer
                    :expand-depth="5"
                    :expanded="true"
                    :theme="this.$TOOL.data.get('APP_DARK') ? 'dark' : 'light'"
                    :value="form"
                    copyable
                    sort></json-viewer>
            </el-tab-pane>
        </el-tabs>

        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button v-if="mode !== 'view'" :loading="loading" @click="submit" type="primary">保 存</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import scEditor from '@/components/scEditor/index.vue'
import Record from '@/views/sys/job/record/index.vue'

export default {
    components: {
        Record,
        scEditor,
    },
    emits: ['success', 'closed'],
    data() {
        return {
            tabIndex: 0,
            mode: 'add',
            titleMap: {
                view: this.$t('查看作业'),
                add: this.$t('新增作业'),
                edit: this.$t('编辑作业'),
            },
            visible: false,
            loading: false,
            //表单数据
            form: {
                executionCron: '* * * * *',
                httpMethod: 'Post',
                requestHeader: `{ "Content-Type": "application/json" }`,
                requestBody: '{}',
            },
            //验证规则
            rules: {
                executionCron: [
                    {
                        required: true,
                        pattern: this.$GLOBAL.chars.RGX_CRON,
                        message: this.$t('执行计划不正确'),
                    },
                ],
                httpMethod: [
                    {
                        required: true,
                        message: this.$t('请求方不能为空'),
                    },
                ],
                jobName: [
                    {
                        required: true,
                        message: this.$t('作业名称不能为空'),
                    },
                ],
                requestHeader: [
                    {
                        validator: (rule, value, callback) => {
                            if (!value) return callback()
                            try {
                                JSON.parse(value)
                            } catch {
                                return callback(this.$t('请求头不正确'))
                            }
                            return callback()
                        },
                    },
                ],
                requestUrl: [
                    {
                        required: true,
                        pattern: this.$GLOBAL.chars.RGX_URL,
                        message: this.$t('请求的网络地址不正确'),
                    },
                ],
                user: [
                    {
                        required: true,
                        trigger: 'blur',
                        message: this.$t('执行用户不能为空'),
                        validator: (rule, value, callback) => {
                            if (value.id) callback()
                            else callback(new Error())
                        },
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
                const res = await this.$API.sys_job.get.post({ id: data.id })
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

            try {
                const method = this.mode === 'add' ? this.$API.sys_job.create : this.$API.sys_job.update
                this.loading = true
                const res = await method.post(
                    Object.assign({}, this.form, { userId: this.form.user.id, requestHeaders: JSON.parse(this.form.requestHeader) }),
                )
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