<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close full-screen>
        <el-form
            v-loading="loading"
            :disabled="mode === 'view'"
            :model="form"
            :rules="rules"
            label-position="right"
            label-width="15rem"
            ref="dialogForm">
            <el-tabs tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form-item :label="$t('唯一编码')" prop="id"><el-input v-model="form.id" clearable /></el-form-item
                    ><el-form-item :label="$t('执行耗时（毫秒）')" prop="duration"><el-input v-model="form.duration" clearable /></el-form-item
                    ><el-form-item :label="$t('请求方法')" prop="httpMethod"><el-input v-model="form.httpMethod" clearable /></el-form-item
                    ><el-form-item :label="$t('响应状态码')" prop="httpStatusCode"><el-input v-model="form.httpStatusCode" clearable /></el-form-item
                    ><el-form-item :label="$t('作业编号')" prop="jobId"><el-input v-model="form.jobId" clearable /></el-form-item
                    ><el-form-item :label="$t('请求体')" prop="requestBody"
                        ><el-input v-model="form.requestBody" clearable rows="5" type="textarea" /></el-form-item
                    ><el-form-item :label="$t('请求头')" prop="requestHeader">
                        <el-input v-model="form.requestHeader" clearable rows="5" type="textarea" /></el-form-item
                    ><el-form-item :label="$t('请求的网络地址')" prop="requestUrl"><el-input v-model="form.requestUrl" clearable /></el-form-item
                    ><el-form-item :label="$t('响应体')" prop="responseBody"
                        ><el-input v-model="form.responseBody" clearable rows="5" type="textarea" /></el-form-item
                    ><el-form-item :label="$t('响应头')" prop="responseHeader">
                        <el-input v-model="form.responseHeader" clearable rows="5" type="textarea" /></el-form-item
                    ><el-form-item :label="$t('执行时间编号')" prop="timeId"><el-input v-model="form.timeId" clearable /></el-form-item
                    ><el-form-item :label="$t('创建时间')" prop="createdTime"><el-input v-model="form.createdTime" clearable /></el-form-item>
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
        </el-form>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            //表单数据
            form: {},
            loading: false,
            mode: 'add',
            //验证规则
            rules: {},
            titleMap: {
                add: this.$t('新增作业记录'),
                edit: this.$t('编辑作业记录'),
                view: this.$t('查看作业记录'),
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
                const res = await this.$API.sys_job.getRecord.post({ id: data.row.id })
                Object.assign(this.form, res.data)
            }
            this.loading = false
            return this
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped></style>