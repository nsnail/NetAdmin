<template>
    <sc-table-select
        v-model="user"
        :params="form"
        :props="{ label: 'userName', value: 'id' }"
        :query-api="$API.sys_user.pagedQuery"
        clearable
        ref="user">
        <template #header>
            <el-form :model="form">
                <el-form-item>
                    <el-input v-model="form.keywords" :placeholder="$t('用户编号 / 用户名 / 手机号 / 邮箱 / 备注')" @input="onInput" clearable />
                </el-form-item>
            </el-form>
        </template>
        <el-table-column :label="$t('用户编号')" prop="id" />
        <el-table-column :label="$t('用户名')" prop="userName" />
        <el-table-column :label="$t('手机号')" prop="mobile" />
    </sc-table-select>
</template>
<style scoped />
<script>
import { defineAsyncComponent } from 'vue'

const scTableSelect = defineAsyncComponent(() => import('@/components/sc-table-select'))
export default {
    props: {
        modelValue: { type: Object },
    },
    data() {
        return {
            user: {},
            form: {
                requiredFields: ['Id', 'UserName', 'Mobile'],
            },
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
                this.user = n
            },
        },
    },
    mounted() {},
    created() {},
    components: {
        scTableSelect,
    },
    computed: {},
    methods: {
        onInput() {
            this.$refs.user.reload()
        },
    },
}
</script>