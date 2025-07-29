<template>
    <sc-dialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close>
        <el-form v-loading="loading" :model="form" :rules="rules" label-width="10rem" ref="dialogForm">
            <el-form-item :label="$t('文档分类名称')" prop="name">
                <el-input v-model="form.name" :placeholder="$t('文档分类名称')" clearable />
            </el-form-item>
            <el-form-item :label="$t('文档分类编码')" prop="code">
                <el-input v-model="form.code" :placeholder="$t('文档分类编码')" clearable />
            </el-form-item>
            <el-form-item :label="$t('父路径')" prop="parentId">
                <catalog-select v-model="form.parentId" class="w100p" />
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const catalogSelect = defineAsyncComponent(() => import('./components/catalog-select'))
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
                code: [{ required: true, message: '请输入文档分类编码' }],
                name: [{ required: true, message: '请输入文档分类名称' }],
            },
            titleMap: {
                add: this.$t('新建文档分类'),
                edit: this.$t('编辑文档分类'),
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
                const res = await this.$API.sys_doc.getCatalog.post({ id: data.row.id })
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
            const method = this.mode === 'add' ? this.$API.sys_doc.createCatalog : this.$API.sys_doc.editCatalog
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

<style scoped />