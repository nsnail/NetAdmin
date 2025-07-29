<template>
    <na-table-page
        :columns="{
            'user.userName': {
                label: $t(`裂变关系`),
                show: [`list`],
                width: 300,
            },
            id: {
                headerAlign: `center`,
                is: `na-col-user`,
                showUserDialog: this.$GLOBAL.hasApiPermission(`api/sys/user/get`),
                nestProp: `user`,
                width: 170,
                label: $t(`粉丝账号`),
                show: [`list`],
            },
            'user.roles': {
                is: `na-col-tags`,
                field: `name`,
                label: $t(`粉丝角色`),
                show: [`list`],
                width: 300,
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
                showUserDialog: this.$GLOBAL.hasApiPermission(`api/sys/user/get`),
                nestProp: `owner`,
                width: 170,
                label: $t(`上级`),
                show: [`list`],
            },
            selfRechargeAllowed: {
                label: $t(`允许自助充值`),
                width: 120,
                align: `center`,
                countBy: true,
                show: [`list`, `view`],
                isSwitch: { onChange: `setSelfRechargeAllowed` },
            },
        }"
        :dy-filters="dyFilters"
        :operations="operations"
        :right-buttons="[
            {
                label: $t(`创建粉丝账号`),
                icon: `el-icon-plus`,
                type: `primary`,
                click: onCreateClick,
            },
        ]"
        :row-buttons="[
            {
                label: $t(`更改上级`),
                click: onSetInviterClick,
                condition: () => {
                    return this.$GLOBAL.hasApiPermission('api/sys/user.invite/set.inviter')
                },
            },
            {
                label: $t(`更改角色`),
                click: onSetRoleClick,
                condition: () => {
                    return this.$GLOBAL.hasApiPermission('api/sys/user.invite/set.fans.role')
                },
            },
        ]"
        :summary="$t(`号码明细`)"
        :table-props="{
            queryApi: $API.sys_userinvite.query,
            defaultExpandAll: true,
            hidePagination: true,
            contextAdvs: [
                {
                    label: $t(`创建粉丝账号`),
                    icon: `el-icon-plus`,
                    action: onCreateClick,
                },
            ],
        }"
        entity-name="sys_userinvite"
        ref="tablePage" />
    <set-inviter-dialog
        v-if="dialog.setInviter"
        @closed="dialog.setInviter = null"
        @mounted="$refs.setInviterDialog.open(dialog.setInviter)"
        @success="(data, mode) => $refs.tablePage.$refs.table.refresh()"
        ref="setInviterDialog"></set-inviter-dialog>
    <create-fans-dialog
        v-if="dialog.createFans"
        @closed="dialog.createFans = null"
        @mounted="$refs.createFansDialog.open(dialog.createFans)"
        @success="(data, mode) => $refs.tablePage.$refs.table.refresh()"
        ref="createFansDialog"></create-fans-dialog>
    <set-role-dialog
        v-if="dialog.setRole"
        @closed="dialog.setRole = null"
        @mounted="$refs.setRoleDialog.open(dialog.setRole)"
        @success="(data, mode) => $refs.tablePage.$refs.table.refresh()"
        ref="setRoleDialog"></set-role-dialog>
</template>
t
<script>
import { defineAsyncComponent } from 'vue'
const setInviterDialog = defineAsyncComponent(() => import('./set-inviter'))
const createFansDialog = defineAsyncComponent(() => import('./create-fans'))
const setRoleDialog = defineAsyncComponent(() => import('./set-role'))
export default {
    components: {
        setInviterDialog,
        createFansDialog,
        setRoleDialog,
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
        onCreateClick() {
            this.dialog.createFans = {}
        },
        onSetInviterClick(row) {
            this.dialog.setInviter = { data: row }
        },
        onSetRoleClick(row) {
            this.dialog.setRole = { data: row }
        },
    },
    props: {
        row: { type: Object },
    },
}
</script>
<style scoped />