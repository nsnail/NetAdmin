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
                    <el-row :gutter="10">
                        <el-col :lg="6">
                            <el-form-item :label="$t('唯一编码')" prop="id">
                                <el-input v-model="form.id" clearable />
                            </el-form-item>
                            <el-form-item :label="$t('执行耗时（毫秒）')" prop="duration">
                                <el-input v-model="form.duration" clearable />
                            </el-form-item>
                            <el-form-item :label="$t('请求方法')" prop="httpMethod">
                                <el-input v-model="form.httpMethod" clearable />
                            </el-form-item>
                            <el-form-item :label="$t('响应状态码')" prop="httpStatusCode">
                                <el-input v-model="form.httpStatusCode" clearable />
                            </el-form-item>
                            <el-form-item :label="$t('作业编号')" prop="jobId">
                                <el-input v-model="form.jobId" clearable />
                            </el-form-item>
                            <el-form-item :label="$t('请求的网络地址')" prop="requestUrl">
                                <el-input v-model="form.requestUrl" clearable type="textarea" />
                            </el-form-item>
                            <el-form-item :label="$t('执行时间编号')" prop="timeId">
                                <el-input v-model="form.timeId" clearable />
                            </el-form-item>
                            <el-form-item :label="$t('创建时间')" prop="createdTime">
                                <el-input v-model="form.createdTime" clearable />
                            </el-form-item>
                        </el-col>
                        <el-col :lg="col2Width">
                            <el-form-item v-if="form.requestBody" :label="$t('请求体')" prop="requestBody">
                                <json-viewer
                                    v-if="mode === 'view'"
                                    :expand-depth="5"
                                    :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                                    :value="JSON.parse(form.requestBody)"
                                    class="w100p children-nopadding"
                                    copyable
                                    expanded
                                    sort />
                                <el-input v-else v-model="form.requestBody" clearable rows="5" type="textarea" />
                            </el-form-item>
                            <el-form-item v-if="form.responseBody" :label="$t('响应体')" prop="responseBody">
                                <json-viewer
                                    v-if="mode === 'view'"
                                    :expand-depth="5"
                                    :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                                    :value="JSON.parse(form.responseBody)"
                                    class="w100p children-nopadding"
                                    copyable
                                    expanded
                                    sort />
                                <el-input v-else v-model="form.responseBody" clearable rows="5" type="textarea" />
                            </el-form-item>
                            <el-form-item v-if="form.requestHeader" :label="$t('请求头')" prop="requestHeader">
                                <json-viewer
                                    v-if="mode === 'view'"
                                    :expand-depth="1"
                                    :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                                    :value="JSON.parse(form.requestHeader)"
                                    class="w100p children-nopadding"
                                    copyable
                                    expanded
                                    sort />
                                <el-input v-else v-model="form.requestHeader" clearable rows="5" type="textarea" />
                            </el-form-item>
                            <el-form-item v-if="form.responseHeader" :label="$t('响应头')" prop="responseHeader">
                                <json-viewer
                                    v-if="mode === 'view'"
                                    :expand-depth="1"
                                    :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                                    :value="JSON.parse(form.responseHeader)"
                                    class="w100p children-nopadding"
                                    copyable
                                    expanded
                                    sort />
                                <el-input v-else v-model="form.responseHeader" clearable rows="5" type="textarea" />
                            </el-form-item>
                        </el-col>

                        <el-col v-if="this.esData" :lg="9">
                            <json-viewer
                                v-if="mode === 'view'"
                                :expand-depth="1"
                                :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                                :value="this.esData"
                                class="children-nopadding"
                                copyable
                                expanded
                                sort />
                        </el-col>
                    </el-row>
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
        </template>
    </sc-dialog>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            col2Width: 18,
            esData: null,
            //表单数据
            form: {},
            loading: true,
            mode: 'add',
            //验证规则
            rules: {},
            titleMap: {
                add: this.$t('新建作业记录'),
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
                let res = await this.$API.sys_job.getRecord.post({ id: data.row.id })
                Object.assign(this.form, res.data)
                const traceId = /"traceId":"(.+?)"/.exec(this.form.responseBody)
                if (traceId && traceId[1]) {
                    res = await this.$API.adm_tools.queryEsLog.post({
                        query: {
                            bool: {
                                must: [
                                    {
                                        range: {
                                            '@timestamp': {
                                                gt: new Date(this.form.createdTime).getTime() - 1000 * 60 * 60,
                                                lt: new Date(this.form.createdTime).getTime() + 1000 * 60 * 60,
                                            },
                                        },
                                    },
                                    {
                                        match_phrase: {
                                            log_source: 'NetAdmin.SysComponent.Host.Utils.RequestLogger',
                                        },
                                    },
                                    {
                                        match_phrase: {
                                            log_message: traceId[1],
                                        },
                                    },
                                ],
                            },
                        },
                        size: 1000,
                        sort: [
                            {
                                '@timestamp': 'desc',
                            },
                        ],
                    })
                    this.esData = res.data.hits.hits[0]._source
                    this.col2Width = 9
                }
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

<style scoped />