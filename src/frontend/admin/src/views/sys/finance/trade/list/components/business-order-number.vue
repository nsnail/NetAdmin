<template>
    <el-table-column>
        <template #default="{ row }">
            <el-link :href="`${getPath(row)}?id=${row.businessOrderNumber}`" @click="(e) => click(e, row)" target="_blank"
                >{{ row.businessOrderNumber }}
            </el-link>
        </template>
    </el-table-column>
</template>
<script>
export default {
    methods: {
        getPath(row) {
            return row.tradeType === 'selfDeposit' ? '/finance/deposit' : row.tradeType === 'commission' ? '/market/income' : '/biz/message-order'
        },
        click(e, row) {
            if (document.getElementsByClassName('el-dialog').length > 0) {
            } else {
                e.preventDefault()
                this.$router.push({
                    path: this.getPath(row),
                    query: { id: row.businessOrderNumber },
                })
            }
        },
    },
}
</script>