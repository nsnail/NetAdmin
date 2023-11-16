<template>
    <el-card :header="$t('近7天操作记录')" shadow="never">
        <scTable ref="table" :data="data" height="auto" hideDo paginationLayout="total, prev, pager, next">
            <sc-table-column :label="$t('序号')" type="index"></sc-table-column>
            <sc-table-column :label="$t('业务名称')" min-width="240" prop="title"></sc-table-column>
            <sc-table-column label="IP" prop="ip" width="150"></sc-table-column>
            <sc-table-column :label="$t('结果')" prop="code" width="150">
                <el-tag type="success">成功</el-tag>
            </sc-table-column>
            <sc-table-column :label="$t('操作时间')" prop="time" width="150"></sc-table-column>
        </scTable>
    </el-card>
</template>

<script>
import ScTable from '@/components/scTable/index.vue'

export default {
    components: { ScTable },
    data() {
        return {
            data: [
                {
                    title: '修改用户 lolowan',
                    ip: '211.187.11.18',
                    code: '成功',
                    time: '2022-10-10 08:41:17',
                },
                {
                    title: '用户登录',
                    ip: '211.187.11.18',
                    code: '成功',
                    time: '2022-10-10 08:21:51',
                },
            ],
        }
    },
}
</script>

<style></style>