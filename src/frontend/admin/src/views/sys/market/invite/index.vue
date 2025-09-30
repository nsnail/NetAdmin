<template>
    <na-table-page
        :columns="{
            'user.userName': {
                label: $t(`裂变关系`),
                show: [`list`],
                width: 300,
            },
            id: {
                extra: ['user.userName'],
                headerAlign: `center`,
                is: `na-col-user`,
                userObjPath: `user`,
                width: 170,
                label: $t(`粉丝账号`),
                show: [`list`],
            },
            'user.roles': {
                is: `na-col-tags`,
                field: `name`,
                label: $t(`粉丝角色`),
                condition: () => this.$GLOBAL.user.isAdmin || this.$GLOBAL.user.isChannel,
                show: [`list`],
                width: 300,
            },
            createdTime: {
                label: $t(`注册时间`),
                align: `right`,
                show: [`list`],
                width: 170,
                operator: `dateRange`,
            },
            'owner.userName': {
                show: [],
            },
            'owner.id': {
                extra: ['owner.userName'],
                headerAlign: `center`,
                is: `na-col-user`,
                userObjPath: `owner`,
                width: 170,
                label: $t(`上级`),
                condition: () => this.$GLOBAL.user.isAdmin || this.$GLOBAL.user.isChannel,
                show: [`list`],
            },
            'channel.userName': {
                show: [],
            },
            'channel.id': {
                extra: ['channel.userName'],
                headerAlign: `center`,
                is: `na-col-user`,
                userObjPath: `channel`,
                width: 170,
                label: $t(`渠道`),
                condition: () => this.$GLOBAL.user.isAdmin || this.$GLOBAL.user.isChannel,
                show: [`list`, `view`],
            },
            selfDepositAllowed: {
                label: $t(`允许自助充值`),
                width: 120,
                align: `center`,
                selectFilter: { countBy: true },
                show: [`list`, `view`],
                isSwitch: { onChange: `setSelfDepositAllowed` },
            },
            'user.enabled': {
                label: $t(`账号已启用`),
                width: 120,
                align: `center`,
                show: [`list`],
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/user.invite/set.enabled`),
                onChange: () => this.$refs.tablePage.$refs.table.refresh(),
                is: userEnabled,
            },
        }"
        :custom-search-controls="[
            {
                type: 'remote-select',
                field: ['filter', 'id'],
                api: $API.sys_userinvite.query,
                params: { filter: { isPlainQuery: true } },
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/user.invite/query`),
                config: { props: { label: 'user.userName', value: 'user.id' } },
                placeholder: $t('粉丝用户'),
                style: 'width:15rem',
            },
            {
                type: 'remote-select',
                field: ['dy', 'owner.id'],
                api: $API.sys_userinvite.query,
                params: { filter: { isPlainQuery: true } },
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/user.invite/query`),
                config: { props: { label: 'user.userName', value: 'user.id' } },
                placeholder: $t('上级用户'),
                style: 'width:15rem',
            },
            {
                type: 'remote-select',
                field: ['dy', 'channel.id'],
                condition: () => this.$GLOBAL.hasApiPermission(`api/sys/user/query`),
                api: $API.sys_user.query,
                params: { filter: { roleId: 698928437833740 } },
                config: { props: { label: 'userName', value: 'id' } },
                placeholder: $t('渠道'),
                style: 'width:15rem',
            },
        ]"
        :dy-filters="dyFilters"
        :operations="operations"
        :right-buttons="[
            {
                label: $t(`创建粉丝账号`),
                props: {
                    icon: `el-icon-plus`,
                    type: `primary`,
                },
                click: this.onCreateClick,
            },
        ]"
        :row-buttons="[
            {
                label: $t(`更改上级`),
                click: this.onSetInviterClick,
                condition: () => {
                    return this.$GLOBAL.hasApiPermission('api/sys/user.invite/set.inviter')
                },
            },
            {
                label: $t(`更改角色`),
                click: this.onSetRoleClick,
                condition: () => {
                    return this.$GLOBAL.hasApiPermission('api/sys/user.invite/set.fans.role')
                },
            },
            {
                label: $t(`资金划拨`),
                click: (row) => {
                    this.dialog.user = { data: { user: row.user, tabId: 'wallet' } }
                },
            },
        ]"
        :show-selection="false"
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
    <user-dialog
        v-if="dialog.user"
        @closed="dialog.user = null"
        @mounted="$refs.userDialog.open(dialog.user)"
        @success="(data, mode) => $refs.tablePage.$refs.table.refresh()"
        ref="userDialog"></user-dialog>
</template>
t
<script>
import { defineAsyncComponent } from 'vue'
const setInviterDialog = defineAsyncComponent(() => import('./set-inviter'))
const createFansDialog = defineAsyncComponent(() => import('./create-fans'))
const setRoleDialog = defineAsyncComponent(() => import('./set-role'))
const userDialog = defineAsyncComponent(() => import('./user'))
export default {
    components: {
        setInviterDialog,
        createFansDialog,
        setRoleDialog,
        userDialog,
    },
    created() {
        if (!this.$GLOBAL.user.isAdmin) {
            this.dyFilters.push({
                field: `owner.id`,
                operator: `eq`,
                value: this.$GLOBAL.user.id,
            })
        } else if (this.ownerId) {
            this.dyFilters.push({
                field: `owner.id`,
                operator: `eq`,
                value: this.ownerId,
            })
        }
    },
    data() {
        return {
            dialog: {},
            dyFilters: [],
            operations: [],
            userEnabled: defineAsyncComponent(() => import('./components/user-enabled')),
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
        ownerId: { type: Number },
    },
}
</script>
<style scoped />