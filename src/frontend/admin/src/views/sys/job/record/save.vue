<template>
    <sc-dialog v-model="visible" :title="titleMap[mode]" :width="800" @closed="$emit('closed')" destroy-on-close full-screen>
        <el-form
            v-loading="loading"
            :disabled="mode === 'view'"
            :model="form"
            :rules="rules"
            label-position="right"
            label-width="150px"
            ref="dialogForm">
            <el-tabs tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form-item :label="$t('唯一编码')" prop="id"><el-input v-model="form.id" clearable /></el-form-item
                    ><el-form-item :label="$t('执行耗时（毫秒）')" prop="duration"><el-input v-model="form.duration" clearable /></el-form-item
                    ><el-form-item :label="$t('请求方法')" prop="httpMethod"><el-input v-model="form.httpMethod" clearable /></el-form-item
                    ><el-form-item :label="$t('HTTP 状态码')" prop="httpStatusCode"><el-input v-model="form.httpStatusCode" clearable /></el-form-item
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
                view: this.$t('查看作业记录'),
                add: this.$t('新增作业记录'),
                edit: this.$t('编辑作业记录'),
            },
            visible: false,
            loading: false,
            //表单数据
            form: {},
            //验证规则
            rules: {},
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
                const res = await this.$API.sys_job.recordGet.post({ id: data.id })
                Object.assign(this.form, res.data)
            }
            this.loading = false
            return this
        },
    },
}
</script>