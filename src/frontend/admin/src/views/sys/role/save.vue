<template>
    <el-dialog v-model="visible" :title="titleMap[mode]" :width="800" destroy-on-close fullscreen @closed="$emit('closed')">
        <el-tabs tab-position="top">
            <el-tab-pane label="基本信息">
                <el-form ref="dialogForm" :disabled="mode === 'view'" :model="form" :rules="rules" label-width="100px">
                    <el-form-item label="角色名称" prop="name">
                        <el-input v-model="form.name" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="排序" prop="sort">
                        <el-input-number v-model="form.sort" :min="0" controls-position="right" style="width: 100%"></el-input-number>
                    </el-form-item>
                    <el-form-item label="启用" prop="enabled">
                        <el-switch v-model="form.enabled"></el-switch>
                    </el-form-item>
                    <el-form-item label="无限权限" prop="ignorePermissionControl">
                        <el-switch v-model="form.ignorePermissionControl"></el-switch>
                    </el-form-item>
                    <el-form-item label="备注" prop="summary">
                        <el-input v-model="form.summary" clearable type="textarea"></el-input>
                    </el-form-item>
                </el-form>
            </el-tab-pane>
            <el-tab-pane label="菜单权限">
                <div class="treeMain">
                    <el-tree
                        ref="menu"
                        :data="trees.menu"
                        :props="{
                            label: (data) => data.meta.title,
                        }"
                        default-expand-all
                        node-key="id"
                        show-checkbox></el-tree>
                </div>
            </el-tab-pane>
            <el-tab-pane label="接口权限">
                <div class="treeMain">
                    <el-tree
                        ref="api"
                        :data="trees.api"
                        :props="{ label: (data) => data.summary }"
                        default-expand-all
                        node-key="id"
                        show-checkbox></el-tree>
                </div>
            </el-tab-pane>
            <el-tab-pane label="数据范围">
                <el-form label-width="100px">
                    <el-form-item label="规则类型">
                        <el-select v-model="form.dataScope" :disabled="mode === 'view'">
                            <el-option v-for="(item, i) in this.$GLOBAL.enums.dataScopes" :key="i" :label="item[1]" :value="i"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item v-show="form.dataScope === 'specificDept'" label="选择部门">
                        <div class="treeMain" style="width: 100%">
                            <el-tree
                                ref="dept"
                                :data="trees.dept"
                                :props="{ label: (data) => data.name }"
                                default-expand-all
                                node-key="id"
                                show-checkbox></el-tree>
                        </div>
                    </el-form-item>
                </el-form>
            </el-tab-pane>
            <el-tab-pane label="控制台">
                <el-form label-width="100px">
                    <el-form-item label="控制台视图">
                        <el-select v-model="form.displayDashboard" :disabled="mode === 'view'">
                            <el-option :value="true" label="仪表板"></el-option>
                            <el-option :value="false" label="工作台"></el-option>
                        </el-select>
                        <div class="el-form-item-msg">用于控制角色登录后控制台的视图</div>
                    </el-form-item>
                </el-form>
            </el-tab-pane>
            <el-tab-pane v-if="mode === 'view'" label="Json">
                <json-viewer :expand-depth="5" :expanded="true" :value="form" boxed copyable sort></json-viewer>
            </el-tab-pane>
        </el-tabs>
        <template #footer>
            <el-button @click="visible = false">取 消</el-button>
            <el-button v-if="mode !== 'view'" :loading="loading" type="primary" @click="submit">保 存</el-button>
        </template>
    </el-dialog>
</template>

<script>
import JsonViewer from 'vue-json-viewer'

export default {
    components: {
        JsonViewer,
    },
    emits: ['success', 'closed'],
    data() {
        return {
            mode: 'add',
            titleMap: {
                add: '新增角色',
                edit: '编辑角色',
                view: '查看角色',
            },
            visible: false,
            loading: false,
            trees: {
                menu: [],
                api: [],
                dept: [],
            },
            //表单数据
            form: { displayDashboard: false, sort: 100, enabled: true },
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
        }
    },
    mounted() {},
    methods: {
        async getTrees(name) {
            this.trees[name] = (await this.$API[`sys_${name}`].query.post()).data
            await this.$nextTick()
            await this.$refs[name].setCheckedKeys(
                (this.form[`${name}Ids`] || []).filter((key) => this.$refs[name].getNode(key).isLeaf),
                true,
            )
        },
        //显示
        open(mode = 'add', data) {
            this.mode = mode
            if (data) {
                Object.assign(this.form, data)
            }
            this.getTrees('menu')
            this.getTrees('api')
            this.getTrees('dept')
            this.visible = true
            return this
        },
        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }

            this.loading = true
            const method = this.mode === 'add' ? this.$API.sys_role.create : this.$API.sys_role.update
            const postData = Object.assign({}, this.form, {
                deptIds: this.$refs.dept.getCheckedKeys().concat(this.$refs.dept.getHalfCheckedKeys()),
                menuIds: this.$refs.menu.getCheckedKeys().concat(this.$refs.menu.getHalfCheckedKeys()),
                apiIds: this.$refs.api.getCheckedKeys().concat(this.$refs.api.getHalfCheckedKeys()),
            })
            try {
                const res = await method.post(postData)
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success('操作成功')
            } catch {
                //
            }
            this.loading = false
        },
    },
}
</script>

<style></style>