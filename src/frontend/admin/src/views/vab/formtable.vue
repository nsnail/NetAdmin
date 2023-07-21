<template>
    <el-main>
        <el-alert
            style="margin-bottom: 20px"
            title="来自AVUE的灵感, 业务会有表单里含有对象数组的可能, 进行封装后进行增删改操作, 并且支持联动form表单的禁用属性"
            type="success"
        ></el-alert>

        <el-card shadow="never">
            <el-form
                ref="ruleForm"
                :model="form"
                :rules="rules"
                label-width="100px"
            >
                <el-form-item label="标题" prop="title">
                    <el-input v-model="form.title"></el-input>
                </el-form-item>
                <el-form-item label="表格" prop="list">
                    <sc-form-table
                        ref="table"
                        v-model="form.list"
                        :addTemplate="addTemplate"
                        drag-sort
                        placeholder="暂无数据"
                    >
                        <el-table-column label="时间" prop="time" width="180">
                            <template #default="scope">
                                <el-time-select
                                    v-model="scope.row.time"
                                ></el-time-select>
                            </template>
                        </el-table-column>
                        <el-table-column label="类型" prop="type" width="180">
                            <template #default="scope">
                                <el-select
                                    v-model="scope.row.type"
                                    placeholder="请选择"
                                >
                                    <el-option
                                        v-for="item in typeDic"
                                        :key="item.value"
                                        :label="item.label"
                                        :value="item.value"
                                    ></el-option>
                                </el-select>
                            </template>
                        </el-table-column>
                        <el-table-column
                            label="数量"
                            min-width="180"
                            prop="val"
                        >
                            <template #default="scope">
                                <el-input
                                    v-model="scope.row.val"
                                    placeholder="请输入内容"
                                ></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column
                            align="center"
                            label="checked"
                            prop="open"
                            width="85"
                        >
                            <template #default="scope">
                                <el-checkbox
                                    v-model="scope.row.checked"
                                ></el-checkbox>
                            </template>
                        </el-table-column>
                        <el-table-column
                            align="center"
                            label="开关"
                            prop="open"
                            width="80"
                        >
                            <template #default="scope">
                                <el-switch v-model="scope.row.open"></el-switch>
                            </template>
                        </el-table-column>
                    </sc-form-table>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="submitForm"
                        >保存</el-button
                    >
                    <el-button @click="resetForm">重置</el-button>
                </el-form-item>
            </el-form>
        </el-card>
        <el-card header="方法" shadow="never" style="margin-top: 15px">
            <el-button type="primary" @click="pushRow">外部插入行</el-button>
            <el-button type="primary" @click="deleteRow"
                >外部删除第一行</el-button
            >
        </el-card>
    </el-main>
</template>

<script>
export default {
    name: "formtable",
    data() {
        return {
            addTemplate: {
                time: "",
                type: "",
                val: "",
                open: true,
                checked: false,
            },
            form: {
                title: "标题",
                list: [
                    {
                        time: "09:00",
                        type: "1",
                        val: "100",
                        open: true,
                        checked: true,
                    },
                    {
                        time: "10:00",
                        type: "0",
                        val: "50",
                        open: false,
                        checked: true,
                    },
                ],
            },
            typeDic: [
                {
                    label: "全局",
                    value: "0",
                },
                {
                    label: "局部",
                    value: "1",
                },
            ],
            rules: {
                title: [{ required: true, message: "请上传", trigger: "blur" }],
            },
        };
    },
    methods: {
        submitForm() {
            this.$refs.ruleForm.validate((valid) => {
                if (valid) {
                    alert("请看控制台输出");
                    console.log(this.form);
                } else {
                    return false;
                }
            });
        },
        resetForm() {
            this.$refs.ruleForm.resetFields();
        },
        pushRow() {
            const data = {
                time: "18:00",
                type: "1",
                val: "0",
                open: true,
                checked: true,
            };
            this.$refs.table.pushRow(data);
        },
        deleteRow() {
            this.$refs.table.deleteRow(0);
        },
    },
};
</script>