<template>
    <sc-dialog
        v-model="visible"
        :full-screen="dialogFullScreen.includes(mode)"
        :title="titleMap[mode]"
        @closed="$emit(`closed`)"
        destroy-on-close
        ref="dialog">
        <div v-loading="loading">
            <el-tabs v-model="tabId" :class="{ 'hide-tabs': !tabs || !tabs[mode] || tabs[mode].length === 0 }">
                <el-tab-pane :label="$t(`基本信息`)" name="basic">
                    <el-form
                        :disabled="![`edit`, `add`].includes(mode)"
                        :model="form"
                        :rules="rules"
                        label-position="right"
                        label-width="12rem"
                        ref="dialogForm">
                        <template v-for="(item, i) in columns" :key="i">
                            <el-form-item v-if="item.show?.includes(mode)" :label="item.label" :prop="i">
                                <el-date-picker
                                    v-bind="item.detail?.props"
                                    v-if="i.endsWith(`Time`)"
                                    v-model="form[i]"
                                    :disabled="item.disabled?.includes(mode)"
                                    type="datetime"
                                    value-format="YYYY-MM-DD HH:mm:ss" />
                                <el-select v-else-if="item.enum" v-model="form[i]" :disabled="item.disabled?.includes(mode)" clearable filterable>
                                    <el-option
                                        v-for="e in Object.entries(this.$GLOBAL.enums[item.enum]).map((x) => {
                                            return {
                                                value: x[0],
                                                text: item.enumSelectText ? item.enumSelectText(x) : item.enumText ? item.enumText(x) : x[1][1],
                                            }
                                        })"
                                        :key="e.value"
                                        :label="e.text"
                                        :value="e.value" />
                                </el-select>
                                <el-switch
                                    v-else-if="typeof form[i] === `boolean` || item.isBoolean"
                                    v-model="form[i]"
                                    :disabled="item.disabled?.includes(mode)" />
                                <template v-else-if="item.detail?.vModelValue">
                                    <component
                                        v-bind="item.detail?.props"
                                        v-if="this.opened"
                                        v-model:value="form[i]"
                                        :disabled="item.disabled?.includes(mode)"
                                        :is="item.detail.is" />
                                </template>
                                <component
                                    v-bind="item.detail?.props"
                                    v-else
                                    v-model="form[i]"
                                    :disabled="item.disabled?.includes(mode)"
                                    :is="item.detail?.is ?? `el-input`" />
                            </el-form-item>
                        </template>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane v-bind="item" v-for="(item, i) in tabs[mode]" v-if="tabs" :key="i" :name="item.name">
                    <component v-bind="item.props" v-if="item.name === tabId" :is="item.component" :row="form" @closed="paneClosed" />
                </el-tab-pane>
            </el-tabs>
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
    watch: {
        mode(n) {
            if (this.dialogFullScreen.includes(n) && !this.$refs.dialog.isFullscreen) {
                this.$refs.dialog.setFullscreen()
            }
        },
    },
    data() {
        return {
            mode: '',
            opened: false,
            tabId: `basic`,
            rules: {},
            visible: false,
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
        async paneClosed() {
            this.visible = false
        },
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
                                    {
                                        type: x[1].rule.type ?? 'string',
                                        validator: x[1].rule.validator,
                                        message: this.$t(`{field} 不正确`, { field: x[1].label }),
                                    },
                                ],
                            ]
                        }
                        return null
                    })
                    .filter((x) => x),
            )

            if (data.row?.id) {
                const res = await this.$API[this.entityName].get.post({ id: data.row.id })
                Object.assign(this.form, this.$TOOL.nestedToDotNotation(res.data))
                this.titleMap.edit = this.$t(`编辑{summary}: {id}`, { summary: this.summary, id: this.form.id })
                this.titleMap.view = this.$t(`查看{summary}: {id}`, { summary: this.summary, id: this.form.id })
            } else {
                Object.assign(this.form, this.$TOOL.nestedToDotNotation(data.row))
            }
            this.loading = false
            this.opened = true
            return this
        },

        //表单提交方法
        async submit() {
            this.loading = true
            if (this.tabId === `basic`) {
                const valid = await this.$refs.dialogForm.validate().catch(() => {})
                if (!valid) {
                    this.loading = false
                    return false
                }
                const method = this.mode === `add` ? this.$API[this.entityName].create : this.$API[this.entityName].edit
                try {
                    const res = await method.post(this.$TOOL.dotNotationToNested(this.form))
                    this.$emit(`success`, res.data, this.mode)
                    this.visible = false
                    this.$message.success(this.$t(`操作成功`))
                } catch {}
            } else {
                if (await this.tabs.submit(this.$refs, this.tabId)) {
                    this.visible = false
                }
            }

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
        dialogFullScreen: { type: Array },
        tabs: { type: Array },
    },
}
</script>

<style scoped>
.hide-tabs :deep(.el-tabs__nav-scroll) {
    display: none;
}
</style>