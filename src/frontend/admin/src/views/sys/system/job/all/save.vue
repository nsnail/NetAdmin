<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close full-screen>
        <el-tabs v-model="tabId" tab-position="top">
            <el-tab-pane :label="$t('基本信息')">
                <el-form
                    v-loading="loading"
                    :disabled="mode === 'view'"
                    :model="form"
                    :rules="rules"
                    label-position="right"
                    label-width="20rem"
                    ref="dialogForm">
                    <el-form-item v-if="mode === 'view'" :label="$t('作业编号')" prop="id">
                        <el-input v-model="form.id" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('执行计划')" prop="executionCron">
                        <sc-cron v-model="form.executionCron" class="font-monospace" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('请求方法')" prop="httpMethod">
                        <el-select v-model="form.httpMethod" clearable filterable>
                            <el-option v-for="(item, i) in $GLOBAL.enums.httpMethods" :key="i" :label="item[1]" :value="i" />
                        </el-select>
                    </el-form-item>
                    <el-form-item :label="$t('作业名称')" prop="jobName">
                        <el-input v-model="form.jobName" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('备注')" prop="summary">
                        <el-input v-model="form.summary" clearable type="textarea" />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('上次执行时间')" prop="lastExecTime">
                        <el-input v-model="form.lastExecTime" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('上次执行耗时（毫秒）')" prop="lastExecTime">
                        <el-input v-model="form.lastDuration" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('下次执行时间')" prop="nextExecTime">
                        <el-input v-model="form.nextExecTime" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('下次执行时间编号')" prop="nextTimeId">
                        <el-input v-model="form.nextTimeId" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('请求头')" prop="requestHeader">
                        <v-ace-editor
                            v-model:value="form.requestHeader"
                            :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'github_dark' : 'github'"
                            lang="json"
                            style="height: 10rem; width: 100%" />
                        <el-button @click="form.requestHeader = jsonFormat(form.requestHeader)" link type="primary"
                            >{{ $t('JSON 格式化') }}
                        </el-button>
                    </el-form-item>
                    <el-form-item :label="$t('请求体')" prop="requestBody">
                        <v-ace-editor
                            v-model:value="form.requestBody"
                            :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'github_dark' : 'github'"
                            lang="json"
                            style="height: 15rem; width: 100%" />
                        <el-button @click="form.requestBody = jsonFormat(form.requestBody)" link type="primary">{{ $t('JSON 格式化') }}</el-button>
                    </el-form-item>
                    <el-form-item :label="$t('请求的网络地址')" prop="requestUrl">
                        <el-input v-model="form.requestUrl" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('随机延时')">
                        <div class="flex gap05">
                            <el-input
                                v-model="form.randomDelayBegin"
                                :placeholder="$t('起始值（毫秒）')"
                                class="w15"
                                clearable
                                oninput="value=value.replace(/[^0-9]/g,'')" />
                            <el-input
                                v-model="form.randomDelayEnd"
                                :placeholder="$t('结束值（毫秒）')"
                                class="w15"
                                clearable
                                oninput="value=value.replace(/[^0-9]/g,'')" />
                        </div>
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('作业状态')" prop="status">
                        <el-input v-model="form.status" clearable />
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('执行用户编号')" prop="userId">
                        <el-input v-model="form.userId" clearable />
                    </el-form-item>
                    <el-form-item v-else :label="$t('执行用户')" prop="user">
                        <na-user-select v-model="form.user" />
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
            <el-tab-pane v-if="mode === 'view'" :label="$t('执行记录')" name="record">
                <record v-if="tabId === 'record'" :job-id="form.id" />
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

        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import vkbeautify from 'vkbeautify'

const Record = defineAsyncComponent(() => import('@/views/sys/system/job/record'))
const naUserSelect = defineAsyncComponent(() => import('@/components/na-user-select'))
const scCron = defineAsyncComponent(() => import('@/components/sc-cron'))
export default {
    components: { Record, naUserSelect, scCron },
    data() {
        return {
            //表单数据
            form: {
                executionCron: '0 * * * * ?',
                httpMethod: 'Post',
                requestHeader: `{ "Content-Type": "application/json" }`,
                requestBody: '{}',
            },
            loading: true,
            mode: 'add',
            //验证规则
            rules: {
                executionCron: [
                    {
                        required: true,
                        pattern: this.$GLOBAL.chars.RGXL_CRON,
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
            tabId: '0',
            titleMap: {
                add: this.$t('新建作业'),
                edit: this.$t('编辑作业'),
                view: this.$t('查看作业'),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        jsonFormat(obj) {
            try {
                obj = this.vkbeautify().json(obj, 2)
            } catch {
                this.$message.error(this.$t('非JSON格式'))
            }
            return obj
        },
        vkbeautify() {
            return vkbeautify
        },
        //显示
        async open(data) {
            this.visible = true
            this.loading = true
            this.mode = data.mode
            if (data.row?.id) {
                const res = await this.$API.sys_job.get.post({ id: data.row.id })
                Object.assign(this.form, res.data)
            }
            this.loading = false
            this.tabId = data.tabId ?? '0'
            return this
        },

        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true
            const method = this.mode === 'add' ? this.$API.sys_job.create : this.$API.sys_job.edit
            try {
                const res = await method.post(
                    Object.assign({}, this.form, { userId: this.form.user.id, requestHeaders: JSON.parse(this.form.requestHeader) }),
                )
                if (res.data) {
                    this.$emit('success', res.data, this.mode)
                    this.$message.success(this.$t('操作成功'))
                } else {
                    this.$message.error(this.$t('操作失败'))
                }
                this.visible = false
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