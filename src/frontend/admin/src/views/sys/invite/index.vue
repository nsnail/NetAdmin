<template>
    <na-table-page
        :columns="{
            'user.userName': {
                label: $t(`邀请关系`),
                show: [`list`],
                width: 300,
            },
            id: {
                headerAlign: `center`,
                is: `na-col-user`,
                clickOpenDialog: this.$GLOBAL.hasApiPermission(`api/sys/user/get`),
                nestProp: `user.userName`,
                nestProp2: `id`,
                width: 170,
                label: $t(`新用户`),
                show: [`list`],
            },
            createdTime: {
                label: $t(`注册时间`),
                align: `right`,
                show: [`list`],
                width: 170,
            },
            ownerId: {
                headerAlign: `center`,
                is: `na-col-user`,
                clickOpenDialog: this.$GLOBAL.hasApiPermission(`api/sys/user/get`),
                nestProp: `owner.userName`,
                nestProp2: `ownerId`,
                width: 170,
                label: $t(`邀请人`),
                show: [`list`],
            },
        }"
        :dy-filters="dyFilters"
        :operations="operations"
        :row-buttons="[{ title: $t(`更改邀请人`), click: modifyInviter }]"
        :summary="$t(`号码明细`)"
        :table-props="{
            queryApi: $API.sys_userinvite.query,
            defaultExpandAll: true,
            hidePagination: true,
        }"
        entity-name="sys_userinvite" />

    <set-inviter-dialog
        v-if="dialog.setInviter"
        @closed="dialog.setInviter = null"
        @mounted="$refs.setInviterDialog.open(dialog.setInviter)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="setInviterDialog"></set-inviter-dialog>
</template>
<script>
import { defineAsyncComponent } from 'vue'
const setInviterDialog = defineAsyncComponent(() => import('./set-inviter'))
export default {
    components: {
        setInviterDialog,
    },
    created() {},
    data() {
        return {
            dialog: {},
            dyFilters: [],
            operations: [],
        }
    },
    methods: {
        modifyInviter(row) {
            // this.dialog.setInviter = { data: row }
        },
    },
    props: {
        row: { type: Object },
    },
}
</script>
<style scoped />