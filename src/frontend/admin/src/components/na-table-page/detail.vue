<template>
    <sc-dialog v-model="visible" :full-screen="dialogFullScreen" :title="titleMap[mode]" @closed="$emit(`closed`)" destroy-on-close>
        <div v-loading="loading">
            <el-form
                :disabled="![`edit`, `add`].includes(mode)"
                :model="form"
                :rules="rules"
                label-position="right"
                label-width="12rem"
                ref="dialogForm">
                <template v-for="(item, i) in columns" :key="i">
                    <el-form-item v-if="item.show?.includes(mode)" :label="item.label" :prop="i">
                        <el-date-picker v-if="i.endsWith(`Time`)" v-model="form[i]" :disabled="item.disabled?.includes(mode)" type="datetime" />
                        <el-select v-else-if="item.enum" v-model="form[i]" :disabled="item.disabled?.includes(mode)">
                            <el-option
                                v-for="e in Object.entries(this.$GLOBAL.enums[item.enum]).map((x) => {
                                    return { value: x[0], text: x[1][1] }
                                })"
                                :key="e.value"
                                :label="e.text"
                                :value="e.value" />
                        </el-select>
                        <el-switch
                            v-else-if="typeof form[i] === `boolean` || item.isBoolean"
                            v-model="form[i]"
                            :disabled="item.disabled?.includes(mode)" />
                        <el-input v-else v-model="form[i]" :disabled="item.disabled?.includes(mode)" />
                    </el-form-item>
                </template>
            </el-form>
        </div>
        <template #footer>
            <el-button @click="visible = false">{{ $t(`取消`) }}</el-button>
            <el-button v-if="mode !== `view`" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t(`保存`) }}</el-button>
        </template>
    </sc-dialog>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            rules: {},
            visible: false,
            mode: `add`,
            loading: false,
            form: {},
            titleMap: {
                add: this.$t(`新建{summary}`, { summary: this.summary }),
                edit: this.$t(`编辑{summary}: {id}`, { summary: this.summary, id: `...` }),
                view: this.$t(`查看{summary}: {id}`, { summary: this.summary, id: `...` }),
            },
        }
    },
    created() {},
    emits: [`success`, `closed`, `mounted`],
    methods: {
        //显示
        async open(data) {
            this.visible = true
            this.loading = true
            this.mode = data.mode
            this.rules = Object.fromEntries(
                Object.entries(this.columns)
                    .map((x) => {
                        if (x[1].rule?.required) {
                            return [
                                x[0],
                                [
                                    { required: true, message: this.$t(`{field} 不能为空`, { field: x[1].label }) },
                                    { validator: x[1].rule.validator, message: this.$t(`{field} 不正确`, { field: x[1].label }) },
                                ],
                            ]
                        }
                        return null
                    })
                    .filter((x) => x),
            )

            if (data.row?.id) {
                const res = await this.$API[this.entityName].get.post({ id: data.row.id })
                Object.assign(this.form, res.data)
                this.titleMap.edit = this.$t(`编辑{summary}: {id}`, { summary: this.summary, id: this.form.id })
                this.titleMap.view = this.$t(`查看{summary}: {id}`, { summary: this.summary, id: this.form.id })
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
            const method = this.mode === `add` ? this.$API[this.entityName].create : this.$API[this.entityName].edit
            try {
                const res = await method.post(this.form)
                this.$emit(`success`, res.data, this.mode)
                this.visible = false
                this.$message.success(this.$t(`操作成功`))
            } catch {}
            this.loading = false
        },
    },
    mounted() {
        this.$emit(`mounted`)
    },
    props: {
        entityName: { type: String },
        summary: { type: String },
        columns: { type: Array },
        dialogFullScreen: { type: Boolean },
    },
}
</script>

<style scoped />