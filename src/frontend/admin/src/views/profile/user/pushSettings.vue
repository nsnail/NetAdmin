<template>
    <el-card header="事务待办" shadow="never">
        <el-form ref="form" label-position="left" label-width="180px" style="margin-top: 20px">
            <el-form-item label="有新的待办">
                <el-checkbox v-model="form.new">短信推送</el-checkbox>
                <el-checkbox v-model="form.new_wx">微信推送</el-checkbox>
            </el-form-item>
            <el-form-item label="待办有效时剩24小时">
                <el-checkbox v-model="form.timeout">短信推送</el-checkbox>
                <el-checkbox v-model="form.timeout_wx">微信推送</el-checkbox>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script>
export default {
    data() {
        return {
            form: {
                new: true,
                timeout: true,
            },
        }
    },
}
</script>

<style></style>