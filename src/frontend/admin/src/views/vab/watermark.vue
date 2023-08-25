<template>
    <el-main>
        <el-card shadow="never">
            <sc-water-mark ref="wm" subtext="admin@scui.com" text="欢迎体验SCUI">
                <el-table :data="tableData" border stripe style="width: 100%">
                    <el-table-column label="Date" prop="date" width="180" />
                    <el-table-column label="Name" prop="name" width="180" />
                    <el-table-column label="Address" prop="address" />
                </el-table>
            </sc-water-mark>
        </el-card>
        <el-card shadow="never" style="margin-top: 15px">
            <el-button type="primary" @click="create">创建水印</el-button>
            <el-button type="primary" @click="clear">移除水印</el-button>
        </el-card>
    </el-main>
</template>

<script>
export default {
    name: 'watermark',
    data() {
        return {
            tableData: [
                {
                    date: '2016-05-03',
                    name: 'Tom',
                    address: 'No. 189, Grove St, Los Angeles',
                },
                {
                    date: '2016-05-02',
                    name: 'Tom',
                    address: 'No. 189, Grove St, Los Angeles',
                },
                {
                    date: '2016-05-04',
                    name: 'Tom',
                    address: 'No. 189, Grove St, Los Angeles',
                },
                {
                    date: '2016-05-01',
                    name: 'Tom',
                    address: 'No. 189, Grove St, Los Angeles',
                },
            ],
        }
    },
    mounted() {},
    methods: {
        create() {
            this.$refs.wm.create()
        },
        clear() {
            this.$refs.wm.clear()
        },
    },
}
</script>

<style></style>