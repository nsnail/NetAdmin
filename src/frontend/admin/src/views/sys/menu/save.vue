<template>
    <el-row :gutter="40">
        <el-col v-if="!form.id">
            <el-empty :description="$t('请选择左侧菜单后操作')" :image-size="100" />
        </el-col>
        <template v-else>
            <el-col>
                <h2>{{ form.meta.title || '新建菜单' }}</h2>
                <el-form :model="form" :rules="rules" label-width="15rem" ref="dialogForm">
                    <el-form-item :label="$t('显示名称')" prop="meta.title">
                        <el-input v-model="form.meta.title" :placeholder="$t('菜单显示名字')" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('上级菜单')" prop="parentId">
                        <el-cascader
                            v-model="form.parentId"
                            :options="treeOptions"
                            :placeholder="$t('顶级菜单')"
                            :props="{
                                emitPath: false,
                                value: 'id',
                                label: 'title',
                                checkStrictly: true,
                            }"
                            :show-all-levels="false"
                            clearable />
                    </el-form-item>
                    <el-form-item :label="$t('类型')" prop="meta.type">
                        <el-radio-group v-model="form.meta.type">
                            <el-radio-button v-for="(item, i) in this.$GLOBAL.enums.menuTypes" :key="i" :label="i">{{
                                this.$t(item[1])
                            }}</el-radio-button>
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item :label="$t('别名')" prop="name">
                        <el-input v-model="form.name" :placeholder="$t('菜单别名')" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('菜单图标')" prop="meta.icon">
                        <sc-icon-select v-model="form.meta.icon" clearable />
                    </el-form-item>
                    <el-form-item :label="$t('路由地址')" prop="path">
                        <el-input v-model="form.path" clearable placeholder="" />
                    </el-form-item>
                    <el-form-item :label="$t('排序')" prop="sort">
                        <el-input-number v-model="form.sort" :min="0" controls-position="right" style="width: 100%" />
                    </el-form-item>
                    <el-form-item :label="$t('重定向')" prop="redirect">
                        <el-input v-model="form.redirect" clearable placeholder="" />
                    </el-form-item>
                    <el-form-item :label="$t('菜单高亮')" prop="active">
                        <el-input v-model="form.active" clearable placeholder="" />
                    </el-form-item>
                    <el-form-item :label="$t('视图')" prop="component">
                        <el-input v-model="form.component" clearable placeholder="">
                            <template #prepend>views/</template>
                        </el-input>
                    </el-form-item>
                    <el-form-item :label="$t('颜色')" prop="color">
                        <el-color-picker v-model="form.meta.color" :predefine="predefineColors" />
                    </el-form-item>
                    <el-form-item :label="$t('是否隐藏')" prop="meta.hidden">
                        <el-checkbox v-model="form.meta.hidden">{{ $t('隐藏菜单') }}</el-checkbox>
                        <el-checkbox v-model="form.meta.hiddenBreadCrumb">{{ $t('隐藏面包屑') }}</el-checkbox>
                    </el-form-item>
                    <el-form-item :label="$t('整页路由')" prop="fullPage">
                        <el-switch v-model="form.meta.fullPage" />
                    </el-form-item>
                    <el-form-item :label="$t('标签')" prop="tag">
                        <el-input v-model="form.meta.tag" clearable placeholder="" />
                    </el-form-item>
                    <el-form-item>
                        <el-button :loading="loading" @click="save" type="primary">{{ $t('保存') }}</el-button>
                    </el-form-item>
                </el-form>
            </el-col>
        </template>
    </el-row>
</template>

<script>
import scIconSelect from '@/components/sc-icon-select'

export default {
    components: { scIconSelect },
    data() {
        return {
            form: {
                meta: {},
            },
            loading: false,
            treeOptions: [],
            rules: {
                meta: {
                    title: [{ required: true, message: '请输入显示名称' }],
                },
            },
        }
    },
    methods: {
        //简单化菜单
        treeToMap(tree) {
            const map = []
            for (const item of tree) {
                map.push({
                    id: item.id,
                    parentId: item.parentId,
                    title: item.meta.title,
                    children: item.children && item.children.length > 0 ? this.treeToMap(item.children) : null,
                })
            }
            return map
        },
        //保存
        async save() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }

            this.loading = true
            try {
                const res = await this.$API.sys_menu.edit.post(this.form)
                this.$message.success('保存成功')
                this.$emit('success', res.data)
            } catch {
                //
            }
            this.loading = false
        },
        //表单注入数据
        setData(data, pid) {
            this.form = Object.assign({}, data, { parentId: pid })
        },
    },
    mounted() {},
    props: {
        tree: {
            type: Object,
            default: () => {},
        },
    },
    watch: {
        tree: {
            handler() {
                this.treeOptions = this.treeToMap(this.tree)
            },
            deep: true,
        },
    },
}
</script>

<style scoped>
h2 {
    margin-bottom: 2rem;
    text-align: center;
}
</style>