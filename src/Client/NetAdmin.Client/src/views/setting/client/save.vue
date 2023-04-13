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
            :model="form"
            :rules="rules"
            label-position="left"
            label-width="100px"
        >
            <el-form-item label="应用标识" prop="appId">
                <el-input v-model="form.appId" clearable></el-input>
            </el-form-item>
            <el-form-item label="应用名称" prop="appName">
                <el-input v-model="form.appName" clearable></el-input>
            </el-form-item>
            <el-form-item label="秘钥" prop="secret">
                <el-input v-model="form.secret" clearable></el-input>
            </el-form-item>
            <el-form-item label="类型范围" prop="type">
                <el-checkbox-group v-model="form.type">
                    <el-checkbox-button label="ALL"></el-checkbox-button>
                    <el-checkbox-button label="UPDATA"></el-checkbox-button>
                    <el-checkbox-button label="QUERY"></el-checkbox-button>
                    <el-checkbox-button label="INSERT"></el-checkbox-button>
                </el-checkbox-group>
            </el-form-item>
            <el-form-item label="授权至" prop="exp">
                <el-date-picker
                    v-model="form.exp"
                    placeholder="选择日期时间"
                    type="datetime"
                    value-format="YYYY-MM-DD HH:mm:ss"
                ></el-date-picker>
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button :loading="isSaving" type="primary" @click="submit()"
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
                add: "新增APP",
                edit: "编辑APP",
            },
            form: {
                id: "",
                appId: "",
                appName: "",
                secret: "",
                type: [],
                exp: "",
            },
            rules: {
                appId: [{ required: true, message: "请输入应用标识" }],
                appName: [{ required: true, message: "请输入应用名称" }],
                secret: [{ required: true, message: "请输入秘钥" }],
                type: [
                    {
                        required: true,
                        message: "请选择类型范围",
                        trigger: "change",
                    },
                ],
                exp: [
                    {
                        required: true,
                        message: "请选择授权到期日期",
                        trigger: "change",
                    },
                ],
            },
            visible: false,
            isSaving: false,
        };
    },
    methods: {
        //显示
        open(mode = "add") {
            this.mode = mode;
            this.visible = true;
            return this;
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
                }
            });
        },
        //表单注入数据
        setData(data) {
            this.form.id = data.id;
            this.form.appId = data.appId;
            this.form.appName = data.appName;
            this.form.secret = data.secret;
            this.form.type = data.type;
            this.form.exp = data.exp;
        },
    },
};
</script>

<style></style>