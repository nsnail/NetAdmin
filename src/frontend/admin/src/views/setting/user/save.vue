<template>
    <el-dialog
        v-model="visible"
        :title="titleMap[mode]"
        :width="500"
        destroy-on-close
        @closed="$emit('closed')"
    >
        <el-form
            ref="dialogForm"
            :disabled="mode === 'show'"
            :model="form"
            :rules="rules"
            label-position="left"
            label-width="100px"
        >
            <el-form-item label="头像" prop="avatar">
                <sc-upload v-model="form.avatar" title="上传头像"></sc-upload>
            </el-form-item>
            <el-form-item label="登录账号" prop="userName">
                <el-input
                    v-model="form.userName"
                    clearable
                    placeholder="用于登录系统"
                ></el-input>
            </el-form-item>
            <el-form-item label="姓名" prop="name">
                <el-input
                    v-model="form.name"
                    clearable
                    placeholder="请输入完整的真实姓名"
                ></el-input>
            </el-form-item>
            <template v-if="mode === 'add'">
                <el-form-item label="登录密码" prop="password">
                    <el-input
                        v-model="form.password"
                        clearable
                        show-password
                        type="password"
                    ></el-input>
                </el-form-item>
                <el-form-item label="确认密码" prop="password2">
                    <el-input
                        v-model="form.password2"
                        clearable
                        show-password
                        type="password"
                    ></el-input>
                </el-form-item>
            </template>
            <el-form-item label="所属部门" prop="dept">
                <el-cascader
                    v-model="form.dept"
                    :options="depts"
                    :props="deptsProps"
                    clearable
                    style="width: 100%"
                ></el-cascader>
            </el-form-item>
            <el-form-item label="所属角色" prop="group">
                <el-select
                    v-model="form.group"
                    filterable
                    multiple
                    style="width: 100%"
                >
                    <el-option
                        v-for="item in groups"
                        :key="item.id"
                        :label="item.label"
                        :value="item.id"
                    />
                </el-select>
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button
                v-if="mode !== 'show'"
                :loading="isSaving"
                type="primary"
                @click="submit()"
                >保 存
            </el-button>
        </template>
    </el-dialog>
</template>

<script>
export default {
    emits: ["success", "closed"],
    data() {
        return {
            mode: "add",
            titleMap: {
                add: "新增用户",
                edit: "编辑用户",
                show: "查看",
            },
            visible: false,
            isSaving: false,
            //表单数据
            form: {
                id: "",
                userName: "",
                avatar: "",
                name: "",
                dept: "",
                group: [],
            },
            //验证规则
            rules: {
                avatar: [{ required: true, message: "请上传头像" }],
                userName: [{ required: true, message: "请输入登录账号" }],
                name: [{ required: true, message: "请输入真实姓名" }],
                password: [
                    { required: true, message: "请输入登录密码" },
                    {
                        validator: (rule, value, callback) => {
                            if (this.form.password2 !== "") {
                                this.$refs.dialogForm.validateField(
                                    "password2"
                                );
                            }
                            callback();
                        },
                    },
                ],
                password2: [
                    { required: true, message: "请再次输入密码" },
                    {
                        validator: (rule, value, callback) => {
                            if (value !== this.form.password) {
                                callback(new Error("两次输入密码不一致!"));
                            } else {
                                callback();
                            }
                        },
                    },
                ],
                dept: [{ required: true, message: "请选择所属部门" }],
                group: [
                    {
                        required: true,
                        message: "请选择所属角色",
                        trigger: "change",
                    },
                ],
            },
            //所需数据选项
            groups: [],
            groupsProps: {
                value: "id",
                multiple: true,
                checkStrictly: true,
            },
            depts: [],
            deptsProps: {
                value: "id",
                checkStrictly: true,
            },
        };
    },
    mounted() {
        this.getGroup();
        this.getDept();
    },
    methods: {
        //显示
        open(mode = "add") {
            this.mode = mode;
            this.visible = true;
            return this;
        },
        //加载树数据
        async getGroup() {
            const res = await this.$API.system.role.list.get();
            this.groups = res.data.rows;
        },
        async getDept() {
            const res = await this.$API.system.dept.list.get();
            this.depts = res.data;
        },
        //表单提交方法
        submit() {
            this.$refs.dialogForm.validate(async (valid) => {
                if (valid) {
                    this.isSaving = true;
                    const res = await this.$API.demo.post.post(this.form);
                    this.isSaving = false;
                    if (res.code === "succeed") {
                        this.$emit("success", this.form, this.mode);
                        this.visible = false;
                        this.$message.success("操作成功");
                    } else {
                        await this.$alert(res.message, "提示", {
                            type: "error",
                        });
                    }
                } else {
                    return false;
                }
            });
        },
        //表单注入数据
        setData(data) {
            this.form.id = data.id;
            this.form.userName = data.userName;
            this.form.avatar = data.avatar;
            this.form.name = data.name;
            this.form.group = data.group;
            this.form.dept = data.dept;

            //可以和上面一样单个注入，也可以像下面一样直接合并进去
            //Object.assign(this.form, data)
        },
    },
};
</script>

<style></style>