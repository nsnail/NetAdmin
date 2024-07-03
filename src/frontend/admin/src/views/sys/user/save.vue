<template>
    <sc-dialog
        v-model="visible"
        :title="`${titleMap[mode]}：${form?.id ?? '...'}`"
        @closed="$emit('closed')"
        append-to-body
        destroy-on-close
        full-screen>
        <el-form
            v-loading="loading"
            :disabled="mode === 'view' && tabId !== 'log'"
            :model="form"
            :rules="rules"
            label-position="right"
            label-width="12rem"
            ref="dialogForm">
            <el-tabs v-model="tabId" tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form-item prop="avatar">
                        <sc-upload v-model="form.avatar" :title="$t('上传头像')"></sc-upload>
                    </el-form-item>
                    <el-form-item v-if="mode === 'view'" :label="$t('唯一编码')" prop="id">
                        <el-input v-model="form.id" clearable></el-input>
                    </el-form-item>
                    <el-form-item :label="$t('登录账号')" prop="userName">
                        <el-input v-model="form.userName" :placeholder="$t('用于登录系统')" clearable></el-input>
                    </el-form-item>
                    <el-row :gutter="10">
                        <el-col :lg="12">
                            <el-form-item :label="$t('手机号')" prop="mobile">
                                <el-input v-model="form.mobile" :placeholder="$t('请输入手机号')" clearable></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('邮箱')" prop="email">
                                <el-input v-model="form.email" :placeholder="$t('请输入邮箱')" clearable></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>

                    <el-form-item :label="$t('登录密码')" prop="passwordText">
                        <div class="password">
                            <el-input
                                v-model="form.passwordText"
                                clearable
                                maxlength="16"
                                oninput="value=value.replace(/[^\w]/g,'')"
                                placeholder="8位以上数字字母组合"></el-input>
                            <el-button @click="form.passwordText = '1234qwer'">{{ $t('初始密码') }}</el-button>
                        </div>
                    </el-form-item>

                    <el-form-item :label="$t('所属角色')" prop="roleIds">
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
                    <el-form-item :label="$t('所属部门')" prop="deptId">
                        <na-dept v-model="form.deptId" class="w100p"></na-dept>
                    </el-form-item>

                    <template v-if="mode !== 'add'">
                        <el-form-item :label="$t('启用')" prop="enabled">
                            <el-switch v-model="form.enabled"></el-switch>
                        </el-form-item>
                    </template>
                    <el-form-item :label="$t('备注')" prop="summary">
                        <el-input v-model="form.summary" clearable type="textarea"></el-input>
                    </el-form-item>
                </el-tab-pane>
                <el-tab-pane :label="$t('档案信息')">
                    <el-row :gutter="10">
                        <el-col :lg="12">
                            <el-form-item :label="$t('真实姓名')" prop="profile.realName">
                                <el-input v-model="form.profile.realName" clearable></el-input>
                            </el-form-item>
                        </el-col>

                        <el-col :lg="12">
                            <el-form-item :label="$t('性别')" prop="profile.sex">
                                <el-select v-model="form.profile.sex" clearable filterable>
                                    <el-option v-for="(item, i) in $GLOBAL.enums.sexes" :key="i" :label="item[1]" :value="i" />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('出生日期')" prop="profile.bornDate">
                                <el-date-picker
                                    v-model="form.profile.bornDate"
                                    :placeholder="$t('出生日期')"
                                    :teleported="false"
                                    type="date"
                                    value-format="YYYY-MM-DD"></el-date-picker>
                            </el-form-item>
                        </el-col>

                        <el-col :lg="12">
                            <el-form-item :label="$t('婚姻状况')" prop="profile.sex">
                                <el-select v-model="form.profile.marriageStatus" clearable filterable>
                                    <el-option v-for="(item, i) in $GLOBAL.enums.marriageStatues" :key="i" :label="item[1]" :value="i" />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('身高')" prop="profile.height">
                                <el-input v-model="form.profile.height" :max="250" :min="100" clearable type="number">
                                    <template v-slot:append>cm</template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('职业')" prop="profile.profession">
                                <el-input v-model="form.profile.profession" clearable></el-input>
                            </el-form-item>
                        </el-col>

                        <el-col :lg="12">
                            <el-form-item :label="$t('证件类型')" prop="profile.certificateType">
                                <el-select v-model="form.profile.certificateType" clearable filterable>
                                    <el-option v-for="(item, i) in $GLOBAL.enums.certificateTypes" :key="i" :label="item[1]" :value="i" />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('证件号码')" prop="profile.certificateNumber">
                                <el-input v-model="form.profile.certificateNumber" clearable type="text"></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('民族')" prop="profile.certificateNumber">
                                <el-select v-model="form.profile.nation" clearable filterable>
                                    <el-option v-for="(item, i) in $GLOBAL.enums.nations" :key="i" :label="item[1]" :value="i" />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('籍贯')" prop="profile.nationArea">
                                <na-area v-model="form.profile.nationArea"></na-area>
                            </el-form-item>
                        </el-col>

                        <el-col :lg="12">
                            <el-form-item :label="$t('政治面貌')" prop="profile.politicalStatus">
                                <el-select v-model="form.profile.politicalStatus" clearable filterable>
                                    <el-option v-for="(item, i) in $GLOBAL.enums.politicalStatues" :key="i" :label="item[1]" :value="i" />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('住宅电话')" prop="profile.homeTelephone">
                                <el-input v-model="form.profile.homeTelephone" clearable type="tel">
                                    <template v-slot:prepend>+86</template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="24">
                            <el-form-item :label="$t('住宅地址')" prop="profile.homeAddress">
                                <el-input v-model="form.profile.homeAddress" clearable>
                                    <template v-slot:prepend>
                                        <na-area v-model="form.profile.homeArea" style="width: 15rem"></na-area>
                                    </template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('工作单位')" prop="profile.companyName">
                                <el-input v-model="form.profile.companyName" clearable></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('工作电话')" prop="profile.companyTelephone">
                                <el-input v-model="form.profile.companyTelephone" clearable type="tel">
                                    <template v-slot:prepend>+86</template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="24">
                            <el-form-item :label="$t('工作地址')" prop="profile.companyAddress">
                                <el-input v-model="form.profile.companyAddress" clearable>
                                    <template v-slot:prepend>
                                        <na-area v-model="form.profile.companyArea" style="width: 15rem"></na-area>
                                    </template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('文化程度')" prop="profile.education">
                                <el-select v-model="form.profile.education" clearable filterable>
                                    <el-option v-for="(item, i) in $GLOBAL.enums.educations" :key="i" :label="item[1]" :value="i" />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('毕业学校')" prop="profile.graduateSchool">
                                <el-input v-model="form.profile.graduateSchool" clearable></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('紧急联系人')" prop="profile.emergencyContactName">
                                <el-input v-model="form.profile.emergencyContactName" clearable></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12">
                            <el-form-item :label="$t('联系人手机号')" prop="profile.emergencyContactMobile">
                                <el-input v-model="form.profile.emergencyContactMobile" clearable type="tel"></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="24">
                            <el-form-item :label="$t('联系人地址')" prop="profile.emergencyContactAddress">
                                <el-input v-model="form.profile.emergencyContactAddress" clearable>
                                    <template v-slot:prepend>
                                        <na-area v-model="form.profile.emergencyContactArea" style="width: 15rem"></na-area>
                                    </template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="24">
                            <el-form-item :label="$t('应用配置')" prop="profile.appConfig">
                                <v-ace-editor
                                    v-model:value="form.profile.appConfig"
                                    :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'github_dark' : 'github'"
                                    lang="json"
                                    style="height: 10rem; width: 100%" />
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('操作日志')" name="log">
                    <log v-if="tabId === 'log'" :user-id="form.id"></log>
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
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'

const log = defineAsyncComponent(() => import('@/views/sys/log/operation/index.vue'))
export default {
    components: { log },
    data() {
        return {
            //表单数据
            form: {
                profile: {
                    appConfig: '[]',
                    nationArea: '',
                    homeArea: '',
                    companyArea: '',
                    emergencyContactArea: '',
                },
            },
            loading: false,
            mode: 'add',
            //验证规则
            rules: {
                userName: [
                    {
                        required: true,
                        message: '4位以上字母、数字或下划线',
                        pattern: this.$GLOBAL.chars.RGX_USERNAME,
                    },
                ],
                mobile: [
                    {
                        message: '您的手机号码',
                        pattern: this.$GLOBAL.chars.RGX_MOBILE,
                    },
                ],
                email: [
                    {
                        message: '您的电子邮箱',
                        pattern: this.$GLOBAL.chars.RGX_EMAIL,
                    },
                ],
                passwordText: [
                    {
                        required: false,
                        message: '8位以上数字字母组合',
                        pattern: this.$GLOBAL.chars.RGX_PASSWORD,
                    },
                ],
                deptId: [{ required: true, message: '请选择所属部门' }],
                roleIds: [
                    {
                        required: true,
                        message: '请选择所属角色',
                        trigger: 'change',
                    },
                ],
            },
            tabId: '0',
            titleMap: {
                add: this.$t('新增用户'),
                edit: this.$t('编辑用户'),
                view: this.$t('查看用户'),
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
            if (this.mode === 'add') {
                this.rules.passwordText[0].required = true
            }
            if (data.row?.id) {
                const res = await this.$API.sys_user.get.post({ id: data.row.id })
                Object.assign(this.form, res.data, {
                    roleIds: res.data.roles.map((x) => x.id),
                    deptId: res.data.dept.id,
                })
                await this.getProfile()
            }
            this.loading = false
            return this
        },

        async getProfile() {
            const res = await this.$API.sys_user.queryProfile.post({
                dynamicFilter: {
                    field: 'id',
                    operator: 'eq',
                    value: this.form.id,
                },
            })
            Object.assign(this.form.profile, res.data[0])
        },

        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true
            const method = this.mode === 'add' ? this.$API.sys_user.create : this.$API.sys_user.edit
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
.password {
    display: flex;
    width: 100%;
    gap: 0.5rem;
}
</style>