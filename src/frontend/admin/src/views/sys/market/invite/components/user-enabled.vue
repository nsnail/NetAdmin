<template>
    <el-table-column>
        <template #default="{ row }">
            <el-switch v-model="row.user.enabled" @change="onSwitchChange(row)"></el-switch>
        </template>
    </el-table-column>
</template>
<script>
export default {
    watch: {},
    created() {},
    data() {
        return {
            message: '',
        }
    },
    methods: {
        async onSwitchChange(row) {
            try {
                await this.$API.sys_userinvite.setEnabled.post(row)
                this.$message.success(this.$t(`操作成功`))
            } catch {
                //
            }
            this.onChange()
        },
    },
    props: {
        onChange: { type: Function },
    },
}
</script>