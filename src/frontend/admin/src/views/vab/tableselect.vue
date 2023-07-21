<!--
 * @Description: 表格选择器组件演示
 * @version: 1.0
 * @Author: sakya
 * @Date: 2021年6月8日09:29:14
 * @LastEditors: sakya
 * @LastEditTime: 2021年6月8日09:29:14
-->

<template>
    <el-main>
        <el-alert
            style="margin-bottom: 20px"
            title="select深度改造的表格选择器, 非常适用于大量数据选择的场景"
            type="success"
        ></el-alert>
        <el-card header="单选" shadow="never">
            <sc-table-select
                v-model="value2"
                :apiObj="apiObj"
                :params="params"
                :props="props"
                :table-width="600"
                @change="change"
            >
                <el-table-column
                    label="ID"
                    prop="id"
                    width="180"
                ></el-table-column>
                <el-table-column label="姓名" prop="user"></el-table-column>
            </sc-table-select>
        </el-card>
        <div style="height: 15px"></div>
        <el-card header="多选" shadow="never">
            <sc-table-select
                v-model="value"
                :apiObj="apiObj"
                :props="props"
                :table-width="700"
                clearable
                collapse-tags
                collapse-tags-tooltip
                multiple
                @change="change"
            >
                <template #header="{ form, submit }">
                    <el-form :inline="true" :model="form">
                        <el-form-item>
                            <el-select
                                v-model="form.sex"
                                :teleported="false"
                                clearable
                                placeholder="性别"
                            >
                                <el-option label="男" value="1"></el-option>
                                <el-option label="女" value="2"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item>
                            <el-date-picker
                                v-model="form.date"
                                :teleported="false"
                                placeholder="注册时间"
                                type="date"
                                value-format="YYYY-MM-DD"
                            ></el-date-picker>
                        </el-form-item>
                        <el-form-item>
                            <el-button type="primary" @click="submit"
                                >查询</el-button
                            >
                        </el-form-item>
                    </el-form>
                    <el-alert
                        title="自定义FORM插糟 传递了form对象和提交方法"
                        type="info"
                    />
                </template>
                <el-table-column
                    label="ID"
                    prop="id"
                    width="180"
                ></el-table-column>
                <el-table-column
                    label="姓名"
                    prop="user"
                    width="100"
                ></el-table-column>
                <el-table-column
                    label="最后请求IP"
                    prop="cip"
                    width="150"
                ></el-table-column>
                <el-table-column label="注册时间" prop="time"></el-table-column>
            </sc-table-select>
        </el-card>
    </el-main>
</template>

<script>
export default {
    name: "tableselect",
    data() {
        return {
            apiObj: this.$API.demo.page,
            params: {
                name: "demoName",
            },
            value: [
                {
                    id: "410000199512025445",
                    user: "魏磊",
                },
                {
                    id: "520000198407304275",
                    user: "史平",
                },
            ],
            value2: {
                id: "520000198407304275",
                user: "史平",
            },
            props: {
                label: "user",
                value: "id",
                keyword: "keyword",
            },
        };
    },
    computed: {},
    mounted() {},
    methods: {
        //值变化
        change(val) {
            this.$message("change事件，返回详情查看控制台");
        },
    },
};
</script>

<style scoped></style>