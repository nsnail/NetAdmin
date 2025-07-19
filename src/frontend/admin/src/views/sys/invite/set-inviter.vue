<template>
    <sc-dialog v-model="visible" :title="$t(`修改邀请人`)" @closed="$emit('closed')" destroy-on-close>
        <el-form :model="form" label-position="right" label-width="12rem" ref="form">
            <el-form-item :label="$t(`用户`)">
                <el-input v-model="form.user.userName" disabled placeholder="placeholder"></el-input>
            </el-form-item>
            <el-form-item :label="$t(`邀请人`)">
                <sc-select
                    v-model="form.ownerId"
                    :config="{ props: { label: `userName`, value: `id` } }"
                    :query-api="$API.sys_user.query"
                    clearable
                    filterable />
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible = false">取消</el-button>
            <el-button :disabled="loading" :loading="loading" @click="submit" type="primary">保存</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            loading: true,
            visible: false,
            form: {},
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            this.loading = true
            Object.assign(this.form, data.data)
            this.loading = false
            return this
        },

        //表单提交方法
        async submit() {
            this.loading = true
            this.$API.sys_userinvite.edit.post(this.form)
            this.$emit('success')
            this.visible = false
            this.loading = false
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped />