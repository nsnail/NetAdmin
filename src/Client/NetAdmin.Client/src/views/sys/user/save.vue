<template>
    <el-dialog
        v-model="visible"
        :title="titleMap[mode]"
        :width="800"
        destroy-on-close
        @closed="$emit('closed')"
    >
        <el-form
            ref="dialogForm"
            :disabled="mode === 'view'"
            :model="form"
            :rules="rules"
            label-position="right"
            label-width="100px"
        >
            <el-tabs tab-position="top">
                <el-tab-pane label="基本信息">
                    <el-form-item label="头像" prop="avatar">
                        <sc-upload
                            v-model="form.avatar"
                            title="上传头像"
                        ></sc-upload>
                    </el-form-item>
                    <el-form-item label="登录账号" prop="userName">
                        <el-input
                            v-model="form.userName"
                            clearable
                            placeholder="用于登录系统"
                        ></el-input>
                    </el-form-item>
                    <el-row :gutter="10">
                        <el-col :lg="12" :xs="24">
                            <el-form-item label="手机号" prop="mobile">
                                <el-input
                                    v-model="form.mobile"
                                    clearable
                                    placeholder="请输入手机号"
                                ></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item label="邮箱" prop="email">
                                <el-input
                                    v-model="form.email"
                                    clearable
                                    placeholder="请输入邮箱"
                                ></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <template v-if="mode === 'add'">
                        <el-row :gutter="10">
                            <el-col :lg="12" :xs="24">
                                <el-form-item
                                    label="登录密码"
                                    prop="passwordText"
                                >
                                    <el-input
                                        v-model="form.passwordText"
                                        clearable
                                        show-password
                                        type="password"
                                    ></el-input>
                                </el-form-item>
                            </el-col>
                            <el-col :lg="12" :xs="24">
                                <el-form-item
                                    label="确认密码"
                                    prop="passwordText2"
                                >
                                    <el-input
                                        v-model="form.passwordText2"
                                        clearable
                                        show-password
                                        type="password"
                                    ></el-input>
                                </el-form-item>
                            </el-col>
                        </el-row>
                    </template>

                    <el-form-item label="所属角色" prop="roleIds">
                        <el-select
                            v-model="form.roleIds"
                            clearable
                            filterable
                            multiple
                            style="width: 100%"
                        >
                            <el-option
                                v-for="item in roles"
                                :key="item.id"
                                :label="item.name"
                                :value="item.id"
                            />
                        </el-select>
                    </el-form-item>
                    <el-form-item label="所属部门" prop="deptId">
                        <el-cascader
                            v-model="form.deptId"
                            :options="depts"
                            :props="deptsProps"
                            clearable
                            style="width: 100%"
                        ></el-cascader>
                    </el-form-item>
                    <el-form-item label="所属岗位" prop="positionIds">
                        <el-select
                            v-model="form.positionIds"
                            clearable
                            filterable
                            multiple
                            style="width: 100%"
                        >
                            <el-option
                                v-for="item in positions"
                                :key="item.id"
                                :label="item.name"
                                :value="item.id"
                            />
                        </el-select>
                    </el-form-item>

                    <template v-if="mode !== 'add'">
                        <el-form-item label="启用" prop="enabled">
                            <el-switch v-model="form.enabled"></el-switch>
                        </el-form-item>
                    </template>
                    <el-form-item label="备注" prop="profile.summary">
                        <el-input
                            v-model="form.profile.summary"
                            clearable
                            type="textarea"
                        ></el-input>
                    </el-form-item>
                </el-tab-pane>
                <el-tab-pane label="档案信息">
                    <el-row :gutter="10">
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="真实姓名"
                                prop="profile.realName"
                            >
                                <el-input
                                    v-model="form.profile.realName"
                                    clearable
                                ></el-input>
                            </el-form-item>
                        </el-col>

                        <el-col :lg="12" :xs="24">
                            <el-form-item label="性别" prop="profile.sex">
                                <el-select
                                    v-model="form.profile.sex"
                                    clearable
                                    filterable
                                    style="width: 100%"
                                >
                                    <el-option
                                        v-for="(item, i) in $CONFIG.ENUMS.sexes"
                                        :key="i"
                                        :label="item[1]"
                                        :value="i"
                                    />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="出生日期"
                                prop="profile.bornDate"
                            >
                                <el-date-picker
                                    v-model="form.profile.bornDate"
                                    :teleported="false"
                                    placeholder="出生日期"
                                    style="width: 100%"
                                    type="date"
                                    value-format="YYYY-MM-DD"
                                ></el-date-picker>
                            </el-form-item>
                        </el-col>

                        <el-col :lg="12" :xs="24">
                            <el-form-item label="婚姻状况" prop="profile.sex">
                                <el-select
                                    v-model="form.profile.marriageStatus"
                                    clearable
                                    filterable
                                    style="width: 100%"
                                >
                                    <el-option
                                        v-for="(item, i) in $CONFIG.ENUMS
                                            .marriageStatues"
                                        :key="i"
                                        :label="item[1]"
                                        :value="i"
                                    />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item label="身高" prop="profile.height">
                                <el-input
                                    v-model="form.profile.height"
                                    :max="250"
                                    :min="100"
                                    clearable
                                    type="number"
                                >
                                    <template v-slot:append>cm</template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="职业"
                                prop="profile.profession"
                            >
                                <el-input
                                    v-model="form.profile.profession"
                                    clearable
                                ></el-input>
                            </el-form-item>
                        </el-col>

                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="证件类型"
                                prop="profile.certificateType"
                            >
                                <el-select
                                    v-model="form.profile.certificateType"
                                    clearable
                                    filterable
                                    style="width: 100%"
                                >
                                    <el-option
                                        v-for="(item, i) in $CONFIG.ENUMS
                                            .certificateTypes"
                                        :key="i"
                                        :label="item[1]"
                                        :value="i"
                                    />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="证件号码"
                                prop="profile.certificateNumber"
                            >
                                <el-input
                                    v-model="form.profile.certificateNumber"
                                    clearable
                                    type="text"
                                ></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="民族"
                                prop="profile.certificateNumber"
                            >
                                <el-select
                                    v-model="form.profile.nation"
                                    clearable
                                    filterable
                                    style="width: 100%"
                                >
                                    <el-option
                                        v-for="(item, i) in $CONFIG.ENUMS
                                            .nations"
                                        :key="i"
                                        :label="item[1]"
                                        :value="i"
                                    />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="籍贯"
                                prop="profile.nationArea"
                            >
                                <sc-table-select
                                    ref="tabSelectNationArea"
                                    v-model="form.profile.nationArea"
                                    :apiObj="$API.sys_dic.pagedQueryContent"
                                    :params="areaParams"
                                    :props="{ label: 'key', value: 'value' }"
                                    :table-width="600"
                                    clearable
                                >
                                    <template #header>
                                        <el-form :model="areaParams">
                                            <el-form-item>
                                                <el-input
                                                    v-model="areaParams.keyword"
                                                    clearable
                                                    placeholder="请输入地区或代码"
                                                    @input="
                                                        tabSelectAreaSearch(
                                                            $refs.tabSelectNationArea
                                                        )
                                                    "
                                                ></el-input>
                                            </el-form-item>
                                        </el-form>
                                    </template>
                                    <el-table-column
                                        label="地区"
                                        prop="key"
                                        width="400"
                                    ></el-table-column>
                                    <el-table-column
                                        label="代码"
                                        prop="value"
                                    ></el-table-column>
                                </sc-table-select>
                            </el-form-item>
                        </el-col>

                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="政治面貌"
                                prop="profile.politicalStatus"
                            >
                                <el-select
                                    v-model="form.profile.politicalStatus"
                                    clearable
                                    filterable
                                    style="width: 100%"
                                >
                                    <el-option
                                        v-for="(item, i) in $CONFIG.ENUMS
                                            .politicalStatues"
                                        :key="i"
                                        :label="item[1]"
                                        :value="i"
                                    />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="住宅电话"
                                prop="profile.homeTelephone"
                            >
                                <el-input
                                    v-model="form.profile.homeTelephone"
                                    clearable
                                    type="tel"
                                >
                                    <template v-slot:prepend>+86</template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="24">
                            <el-form-item
                                label="住宅地址"
                                prop="profile.homeAddress"
                            >
                                <el-input
                                    v-model="form.profile.homeAddress"
                                    clearable
                                >
                                    <template v-slot:prepend>
                                        <sc-table-select
                                            ref="tabSelectHomeArea"
                                            v-model="form.profile.homeArea"
                                            :apiObj="
                                                $API.sys_dic.pagedQueryContent
                                            "
                                            :params="areaParams"
                                            :props="{
                                                label: 'key',
                                                value: 'value',
                                            }"
                                            :table-width="600"
                                            clearable
                                        >
                                            <template #header>
                                                <el-form :model="areaParams">
                                                    <el-form-item>
                                                        <el-input
                                                            v-model="
                                                                areaParams.keyword
                                                            "
                                                            clearable
                                                            placeholder="请输入地区或代码"
                                                            @input="
                                                                tabSelectAreaSearch(
                                                                    $refs.tabSelectHomeArea
                                                                )
                                                            "
                                                        ></el-input>
                                                    </el-form-item>
                                                </el-form>
                                            </template>
                                            <el-table-column
                                                label="地区"
                                                prop="key"
                                                width="400"
                                            ></el-table-column>
                                            <el-table-column
                                                label="代码"
                                                prop="value"
                                            ></el-table-column>
                                        </sc-table-select>
                                    </template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="工作单位"
                                prop="profile.companyName"
                            >
                                <el-input
                                    v-model="form.profile.companyName"
                                    clearable
                                ></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="工作电话"
                                prop="profile.companyTelephone"
                            >
                                <el-input
                                    v-model="form.profile.companyTelephone"
                                    clearable
                                    type="tel"
                                >
                                    <template v-slot:prepend>+86</template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="24">
                            <el-form-item
                                label="工作地址"
                                prop="profile.companyAddress"
                            >
                                <el-input
                                    v-model="form.profile.companyAddress"
                                    clearable
                                >
                                    <template v-slot:prepend>
                                        <sc-table-select
                                            ref="tabSelectCompanyArea"
                                            v-model="form.profile.companyArea"
                                            :apiObj="
                                                $API.sys_dic.pagedQueryContent
                                            "
                                            :params="areaParams"
                                            :props="{
                                                label: 'key',
                                                value: 'value',
                                            }"
                                            :table-width="600"
                                            clearable
                                        >
                                            <template #header>
                                                <el-form :model="areaParams">
                                                    <el-form-item>
                                                        <el-input
                                                            v-model="
                                                                areaParams.keyword
                                                            "
                                                            clearable
                                                            placeholder="请输入地区或代码"
                                                            @input="
                                                                tabSelectAreaSearch(
                                                                    $refs.tabSelectCompanyArea
                                                                )
                                                            "
                                                        ></el-input>
                                                    </el-form-item>
                                                </el-form>
                                            </template>
                                            <el-table-column
                                                label="地区"
                                                prop="key"
                                                width="400"
                                            ></el-table-column>
                                            <el-table-column
                                                label="代码"
                                                prop="value"
                                            ></el-table-column>
                                        </sc-table-select>
                                    </template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="文化程度"
                                prop="profile.education"
                            >
                                <el-select
                                    v-model="form.profile.education"
                                    clearable
                                    filterable
                                    style="width: 100%"
                                >
                                    <el-option
                                        v-for="(item, i) in $CONFIG.ENUMS
                                            .educations"
                                        :key="i"
                                        :label="item[1]"
                                        :value="i"
                                    />
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="毕业学校"
                                prop="profile.graduateSchool"
                            >
                                <el-input
                                    v-model="form.profile.graduateSchool"
                                    clearable
                                ></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="紧急联系人"
                                prop="profile.emergencyContactName"
                            >
                                <el-input
                                    v-model="form.profile.emergencyContactName"
                                    clearable
                                ></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :lg="12" :xs="24">
                            <el-form-item
                                label="联系人手机号"
                                prop="profile.emergencyContactMobile"
                            >
                                <el-input
                                    v-model="
                                        form.profile.emergencyContactMobile
                                    "
                                    clearable
                                    type="tel"
                                ></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="24">
                            <el-form-item
                                label="联系人地址"
                                prop="profile.emergencyContactAddress"
                            >
                                <el-input
                                    v-model="
                                        form.profile.emergencyContactAddress
                                    "
                                    clearable
                                >
                                    <template v-slot:prepend>
                                        <sc-table-select
                                            ref="tabSelectEmergencyContactArea"
                                            v-model="
                                                form.profile
                                                    .emergencyContactArea
                                            "
                                            :apiObj="
                                                $API.sys_dic.pagedQueryContent
                                            "
                                            :params="areaParams"
                                            :props="{
                                                label: 'key',
                                                value: 'value',
                                            }"
                                            :table-width="600"
                                            clearable
                                        >
                                            <template #header>
                                                <el-form :model="areaParams">
                                                    <el-form-item>
                                                        <el-input
                                                            v-model="
                                                                areaParams.keyword
                                                            "
                                                            clearable
                                                            placeholder="请输入地区或代码"
                                                            @input="
                                                                tabSelectAreaSearch(
                                                                    $refs.tabSelectEmergencyContactArea
                                                                )
                                                            "
                                                        ></el-input>
                                                    </el-form-item>
                                                </el-form>
                                            </template>
                                            <el-table-column
                                                label="地区"
                                                prop="key"
                                                width="400"
                                            ></el-table-column>
                                            <el-table-column
                                                label="代码"
                                                prop="value"
                                            ></el-table-column>
                                        </sc-table-select>
                                    </template>
                                </el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" label="Json">
                    <json-viewer
                        :expand-depth="5"
                        :expanded="true"
                        :value="form"
                        boxed
                        copyable
                        sort
                    ></json-viewer>
                </el-tab-pane>
            </el-tabs>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button
                v-if="mode !== 'view'"
                :loading="isSaving"
                type="primary"
                @click="submit()"
                >保 存
            </el-button>
        </template>
    </el-dialog>
</template>

<script>
import JsonViewer from "vue-json-viewer";

export default {
    components: {
        JsonViewer,
    },
    emits: ["success", "closed"],
    data() {
        return {
            areaParams: {
                dynamicFilter: {
                    field: "catalogid",
                    operator: "eq",
                    value: this.$CONFIG.NUMBERS.DIC_CATALOG_ID_GEO_AREA,
                    logic: "or",
                    filters: [],
                },
                prop: "id",
                order: "Ascending",
                value: {},
            },
            mode: "add",
            titleMap: {
                view: "查看用户",
                add: "新增用户",
                edit: "编辑用户",
                show: "查看",
            },
            visible: false,
            isSaving: false,
            //表单数据
            form: {
                profile: {
                    nationArea: {},
                    homeArea: {},
                    companyArea: {},
                    emergencyContactArea: {},
                },
            },
            //验证规则
            rules: {
                userName: [
                    {
                        required: true,
                        message:
                            this.$CONFIG.LOC_STRINGS
                                .more_than_4_digits_alphanumeric_underline,
                        pattern: this.$CONFIG.STRINGS.RGX_USERNAME,
                    },
                ],
                mobile: [
                    {
                        message:
                            this.$CONFIG.LOC_STRINGS
                                .mobile_phone_number_that_can_be_used_normally,
                        pattern: this.$CONFIG.STRINGS.RGX_MOBILE,
                    },
                ],
                email: [
                    {
                        message:
                            this.$CONFIG.LOC_STRINGS
                                .email_address_that_can_be_used_normally,
                        pattern: this.$CONFIG.STRINGS.RGX_EMAIL,
                    },
                ],
                passwordText: [
                    {
                        required: true,
                        message:
                            this.$CONFIG.LOC_STRINGS
                                .number_letter_combination_of_more_than_8_digits,
                        pattern: this.$CONFIG.STRINGS.RGX_PASSWORD,
                    },
                    {
                        validator: (rule, value, callback) => {
                            if (this.form.passwordText2 !== "") {
                                this.$refs.dialogForm.validateField(
                                    "passwordText2"
                                );
                            }
                            callback();
                        },
                    },
                ],
                passwordText2: [
                    { required: true, message: "请再次输入密码" },
                    {
                        validator: (rule, value, callback) => {
                            if (value !== this.form.passwordText) {
                                callback(new Error("两次输入密码不一致!"));
                            } else {
                                callback();
                            }
                        },
                    },
                ],
                deptId: [{ required: true, message: "请选择所属部门" }],
                roleIds: [
                    {
                        required: true,
                        message: "请选择所属角色",
                        trigger: "change",
                    },
                ],
                positionIds: [
                    {
                        required: true,
                        message: "请选择所属岗位",
                        trigger: "change",
                    },
                ],
            },
            //所需数据选项
            roles: [],
            positions: [],
            geoAreas: [],
            depts: [],
            deptsProps: {
                label: "name",
                emitPath: false,
                value: "id",
                checkStrictly: true,
            },
        };
    },
    mounted() {
        this.getRoles();
        this.getPositions();
        this.getDept();
        this.getGeoAreas();
    },
    methods: {
        tabSelectAreaSearch(control) {
            this.areaParams.dynamicFilter.filters = [];
            if (this.areaParams.keyword) {
                this.areaParams.dynamicFilter.filters.push({
                    field: "key",
                    operator: "contains",
                    value: this.areaParams.keyword,
                });
                this.areaParams.dynamicFilter.filters.push({
                    field: "value",
                    operator: "contains",
                    value: this.areaParams.keyword,
                });
            }

            control.reload();
        },
        //显示
        open(mode = "add") {
            this.mode = mode;
            this.visible = true;
            return this;
        },
        //加载树数据
        async getRoles() {
            const res = await this.$API.sys_role.query.post();
            this.roles = res.data;
        },
        async getPositions() {
            const res = await this.$API.sys_position.query.post();
            this.positions = res.data;
        },
        async getGeoAreas() {
            const res = await this.$API.sys_dic.queryContent.post({
                dynamicFilter: {
                    field: "catalogid",
                    operator: "eq",
                    value: this.$CONFIG.NUMBERS.DIC_CATALOG_ID_GEO_AREA,
                },
            });
            this.geoAreas = res.data;
        },
        async getProfile() {
            const res = await this.$API.sys_user.queryProfile.post({
                dynamicFilter: {
                    field: "id",
                    operator: "eq",
                    value: this.form.id,
                },
            });
            Object.assign(this.form.profile, res.data[0]);
        },
        async getDept() {
            const res = await this.$API.sys_dept.query.post();
            this.depts = res.data;
        },
        //表单提交方法
        submit() {
            this.$refs.dialogForm.validate(async (valid) => {
                if (valid) {
                    this.isSaving = true;
                    const reqData = JSON.parse(JSON.stringify(this.form));

                    try {
                        if (!reqData.mobile) reqData.mobile = null;
                        if (!reqData.profile.height)
                            reqData.profile.height = null;
                        if (
                            !reqData.profile.nationArea ||
                            !reqData.profile.nationArea.value
                        )
                            reqData.profile.nationArea = null;
                        if (
                            !reqData.profile.homeArea ||
                            !reqData.profile.homeArea.value
                        )
                            reqData.profile.homeArea = null;
                        if (
                            !reqData.profile.companyArea ||
                            !reqData.profile.companyArea.value
                        )
                            reqData.profile.companyArea = null;
                        if (
                            !reqData.profile.emergencyContactArea ||
                            !reqData.profile.emergencyContactArea.value
                        )
                            reqData.profile.emergencyContactArea = null;
                        const method =
                            this.mode === "add"
                                ? this.$API.sys_user.create
                                : this.$API.sys_user.update;
                        const res = await method.post(reqData);
                        this.$emit("success", res.data, this.mode);
                        this.visible = false;
                        this.$message.success("操作成功");
                    } catch {}
                    this.isSaving = false;
                } else {
                    return false;
                }
            });
        },
        //表单注入数据
        setData(data) {
            //可以和上面一样单个注入，也可以像下面一样直接合并进去
            Object.assign(this.form, data);
            this.form.positionIds = this.form.positions.map((x) => x.id);
            this.form.roleIds = this.form.roles.map((x) => x.id);
            this.form.deptId = this.form.dept.id;

            this.getProfile();
        },
    },
};
</script>

<style></style>