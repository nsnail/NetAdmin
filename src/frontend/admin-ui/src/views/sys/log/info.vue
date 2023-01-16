<template>

    <el-main style="padding:0 20px;">
        <el-descriptions :column="1" border size="small">
            <el-descriptions-item label="id">{{ data.id }}</el-descriptions-item>
            <el-descriptions-item label="apiId">{{ data.apiId }}</el-descriptions-item>
            <el-descriptions-item label="apiSummary">{{ data.apiSummary }}</el-descriptions-item>
            <el-descriptions-item label="clientIp">{{ data.clientIp }}</el-descriptions-item>
            <el-descriptions-item label="createdTime">{{ data.createdTime }}</el-descriptions-item>
            <el-descriptions-item label="createdUserName">{{ data.createdUserName }}</el-descriptions-item>
            <el-descriptions-item label="duration">{{ data.duration }}</el-descriptions-item>
            <el-descriptions-item label="environment">{{ data.environment }}</el-descriptions-item>
            <el-descriptions-item label="exception">{{ data.exception }}</el-descriptions-item>
            <el-descriptions-item label="extraData">{{ data.extraData }}</el-descriptions-item>
            <el-descriptions-item label="method">{{ data.method }}</el-descriptions-item>
            <el-descriptions-item label="os">{{ data.os }}</el-descriptions-item>
            <el-descriptions-item label="referUrl">{{ data.referUrl }}</el-descriptions-item>
            <el-descriptions-item label="requestBody">{{ data.requestBody }}</el-descriptions-item>
            <el-descriptions-item label="requestContentType">{{ data.requestContentType }}</el-descriptions-item>
            <el-descriptions-item label="requestHeaders">{{ data.requestHeaders }}</el-descriptions-item>
            <el-descriptions-item label="requestUrl">{{ data.requestUrl }}</el-descriptions-item>
            <el-descriptions-item label="responseBody">{{ data.responseBody }}</el-descriptions-item>
            <el-descriptions-item label="responseContentType">{{ data.responseContentType }}</el-descriptions-item>
            <el-descriptions-item label="responseHeaders">{{ data.responseHeaders }}</el-descriptions-item>
            <el-descriptions-item label="serverIp">{{ data.serverIp }}</el-descriptions-item>
            <el-descriptions-item label="statusCode">{{ data.statusCode }}</el-descriptions-item>
            <el-descriptions-item label="userAgent">{{ data.userAgent }}</el-descriptions-item>
        </el-descriptions>
    </el-main>
</template>

<script>
export default {
    data() {
        return {
            data: {},
            activeNames: ['1'],
            typeMap: {
                'info': "info",
                'warn': "warning",
                'error': "error"
            }
        }
    },
    methods: {
        setData(data) {
            this.data = data
        }
    }
}
</script>

<style scoped>
.code {
    background: #848484;
    padding: 15px;
    color: #fff;
    font-size: 12px;
    border-radius: 4px;
}
</style>