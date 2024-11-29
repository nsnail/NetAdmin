<template>
    <sc-dialog v-model="visible" :title="`${title}`" @closed="$emit('closed')" destroy-on-close>
        <el-form :model="form" ref="form">
            <el-form-item
                v-for="(row, index) in form.rows"
                :label="`第${index + 1}行`"
                :prop="'rows.' + index + '.value'"
                :rules="{
                    required: true,
                    message: '请输入以空格分隔的24分栏布局：如【24】或【12 12】或【8 8 8】',
                    trigger: 'blur',
                }">
                <el-input
                    v-model="form.rows[index].value"
                    :input="form.rows[index].value = form.rows[index].value.replace(/[^0-9 ]/g, '')"
                    placeholder="请输入以空格分隔的24分栏布局：如【24】或【12 12】或【8 8 8】">
                    <template #append>
                        <el-button @click.prevent="form.rows.splice(index, 1)" icon="delete">删除</el-button>
                    </template>
                </el-input>
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="this.form.rows.push({ value: '' })">{{ $t('添加一行') }}</el-button>
            <el-button @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            title: '添加自定义布局',
            visible: false,
            form: {
                rows: [{ value: '' }],
            },
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {
        //显示
        async open({ title }) {
            this.visible = true
            this.title = title

            return this
        },
        async submit() {
            const valid = await this.$refs.form.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.form.rows.forEach((r) => {
                r.value = r.value
                    .split(' ')
                    .map((x) => x.trim())
                    .filter((x) => x !== '')
                    .join(',')
            })

            let l = this.form.rows.map((x) => x.value).join('-')
            if (l !== '') {
                this.$emit('onCustomLayout', l)
            }
            this.visible = false
        },
    },
    created() {},
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped></style>