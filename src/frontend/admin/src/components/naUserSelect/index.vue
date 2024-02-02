<template>
    <sc-table-select
        v-model="user"
        :apiObj="$API.sys_user.pagedQuery"
        :params="form"
        :props="{ label: 'userName', value: 'id' }"
        :table-width="600"
        clearable
        ref="user">
        <template #header>
            <el-form :model="form">
                <el-form-item>
                    <el-input
                        v-model="form.keywords"
                        :placeholder="$t('用户编号 / 用户名 / 手机号 / 邮箱 / 备注')"
                        @input="onInput"
                        clearable></el-input>
                </el-form-item>
            </el-form>
        </template>
        <el-table-column :label="$t('用户编号')" prop="id"></el-table-column>
        <el-table-column :label="$t('用户名')" prop="userName"></el-table-column>
        <el-table-column :label="$t('手机号')" prop="mobile"></el-table-column>
    </sc-table-select>
</template>
<style scoped></style>
<script>
export default {
    props: {
        modelValue: { type: Object },
    },
    data() {
        return {
            user: {},
            form: {},
        }
    },
    watch: {
        user(n) {
            this.$emit('update:modelValue', n)
        },

        modelValue: {
            immediate: true,
            deep: true,
            handler(n) {
                this.user = n ?? {}
            },
        },
    },
    mounted() {},
    created() {},
    components: {},
    computed: {},
    methods: {
        onInput() {
            this.$refs.user.reload()
        },
    },
}
</script>