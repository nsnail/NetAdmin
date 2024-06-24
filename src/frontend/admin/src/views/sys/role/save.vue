<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close full-screen>
        <div v-loading="loading">
            <el-tabs v-model="tabId" tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form :disabled="mode === 'view'" :model="form" :rules="rules" label-width="15rem" ref="dialogForm">
                        <el-form-item :label="$t('角色名称')" prop="name">
                            <el-input v-model="form.name" clearable></el-input>
                        </el-form-item>
                        <el-form-item :label="$t('排序')" prop="sort">
                            <el-input-number v-model="form.sort" :min="0" controls-position="right" style="width: 100%"></el-input-number>
                        </el-form-item>
                        <el-form-item :label="$t('启用')" prop="enabled">
                            <el-switch v-model="form.enabled"></el-switch>
                        </el-form-item>
                        <el-form-item :label="$t('无限权限')" prop="ignorePermissionControl">
                            <el-switch v-model="form.ignorePermissionControl"></el-switch>
                        </el-form-item>
                        <el-form-item :label="$t('备注')" prop="summary">
                            <el-input v-model="form.summary" clearable type="textarea"></el-input>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane :label="$t('菜单权限')">
                    <div class="treeMain">
                        <el-tree
                            :data="trees.menu"
                            :props="{
                                label: (data) => data.meta.title,
                            }"
                            default-expand-all
                            node-key="id"
                            ref="menu"
                            show-checkbox></el-tree>
                    </div>
                </el-tab-pane>
                <el-tab-pane :label="$t('接口权限')">
                    <div class="treeMain">
                        <el-tree
                            :data="trees.api"
                            :props="{ label: (data) => data.summary }"
                            default-expand-all
                            node-key="id"
                            ref="api"
                            show-checkbox></el-tree>
                    </div>
                </el-tab-pane>
                <el-tab-pane :label="$t('数据范围')">
                    <el-form label-width="10rem">
                        <el-form-item :label="$t('规则类型')">
                            <el-select v-model="form.dataScope" :disabled="mode === 'view'">
                                <el-option v-for="(item, i) in this.$GLOBAL.enums.dataScopes" :key="i" :label="item[1]" :value="i"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item v-show="form.dataScope === 'specificDept'" :label="$t('选择部门')">
                            <div class="treeMain" style="width: 100%">
                                <el-tree
                                    :data="trees.dept"
                                    :props="{ label: (data) => data.name }"
                                    default-expand-all
                                    node-key="id"
                                    ref="dept"
                                    show-checkbox></el-tree>
                            </div>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane :label="$t('控制台')">
                    <el-form label-width="10rem">
                        <el-form-item :label="$t('控制台视图')">
                            <el-select v-model="form.displayDashboard" :disabled="mode === 'view'">
                                <el-option :label="$t('仪表板')" :value="true"></el-option>
                                <el-option :label="$t('工作台')" :value="false"></el-option>
                            </el-select>
                            <div class="el-form-item-msg">{{ $t('用于控制角色登录后控制台的视图') }}</div>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('用户列表')" name="user">
                    <user v-if="tabId === 'user'" :role-id="form.id"></user>
                </el-tab-pane>
                <el-tab-pane v-if="mode === 'view'" :label="$t('原始数据')">
                    <json-viewer
                        :expand-depth="5"
                        :theme="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'dark' : 'light'"
                        :value="form"
                        copyable
                        expanded
                        sort></json-viewer>
                </el-tab-pane>
            </el-tabs>
        </div>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'

const User = defineAsyncComponent(() => import('@/views/sys/user/index.vue'))
export default {
    components: { User },
    data() {
        return {
            //表单数据
            form: { displayDashboard: false, sort: 100, enabled: true },
            loading: false,
            mode: 'add',
            //验证规则
            rules: {
                sort: [
                    {
                        required: true,
                        message: '请输入排序',
                        trigger: 'change',
                    },
                ],
                name: [{ required: true, message: '请输入角色名称' }],
            },
            tabId: '0',
            titleMap: {
                add: this.$t('新增角色'),
                edit: this.$t('编辑角色'),
                view: this.$t('查看角色'),
            },
            trees: {
                menu: [],
                api: [],
                dept: [],
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        async getTrees(name) {
            const res = await this.$API[`sys_${name}`].query.post()
            this.trees[name] = res.data
            await this.$nextTick()
            await this.$refs[name].setCheckedKeys(
                (this.form[`${name}Ids`] || []).filter((key) => this.$refs[name].getNode(key).isLeaf),
                true,
            )
        },
        //显示
        async open(data) {
            this.visible = true
            this.loading = true
            this.mode = data.mode
            if (data.row?.id) {
                const res = await this.$API.sys_role.get.post({ id: data.row.id })
                Object.assign(this.form, res.data)
            }
            await this.getTrees('menu')
            await this.getTrees('api')
            await this.getTrees('dept')
            this.loading = false
            return this
        },

        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true
            const postData = Object.assign({}, this.form, {
                deptIds: this.$refs.dept.getCheckedKeys().concat(this.$refs.dept.getHalfCheckedKeys()),
                menuIds: this.$refs.menu.getCheckedKeys().concat(this.$refs.menu.getHalfCheckedKeys()),
                apiIds: this.$refs.api.getCheckedKeys().concat(this.$refs.api.getHalfCheckedKeys()),
            })
            const method = this.mode === 'add' ? this.$API.sys_role.create : this.$API.sys_role.edit
            try {
                const res = await method.post(postData)
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success(this.$t('操作成功'))
            } catch {}
            this.loading = false
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped></style>