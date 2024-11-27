<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close>
        <el-form v-loading="loading" :model="form" :rules="rules" label-width="10rem" ref="dialogForm">
            <el-form-item :label="$t('字典目录名称')" prop="name">
                <el-input v-model="form.name" :placeholder="$t('字典目录名称')" clearable></el-input>
            </el-form-item>
            <el-form-item :label="$t('字典目录编码')" prop="code">
                <el-input v-model="form.code" :placeholder="$t('字典目录编码')" clearable></el-input>
            </el-form-item>
            <el-form-item :label="$t('父路径')" prop="parentId">
                <catalog-select v-model="form.parentId" class="w100p" />
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="(visible = false)">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const catalogSelect = defineAsyncComponent(() => import('./components/catalog-select.vue'))
export default {
    components: { catalogSelect },
    data() {
        return {
            //表单数据
            form: {},
            loading: true,
            mode: 'add',
            //验证规则
            rules: {
                code: [{ required: true, message: '请输入字典目录编码' }],
                name: [{ required: true, message: '请输入字典目录名称' }],
            },
            titleMap: {
                add: this.$t('新增字典目录'),
                edit: this.$t('编辑字典目录'),
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            this.loading = true
            this.mode = data.mode
            this.form.parentId = data.data?.catalogId
            this.form.code = data.data?.code
            if (data.row?.id) {
                const res = await this.$API.sys_dic.getCatalog.post({ id: data.row.id })
                Object.assign(this.form, res.data)
            }
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
            const method = this.mode === 'add' ? this.$API.sys_dic.createCatalog : this.$API.sys_dic.editCatalog
            try {
                const res = await method.post(this.form)
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