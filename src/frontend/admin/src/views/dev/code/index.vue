<template>
    <el-main>
        <el-row :gutter="15">
            <el-col>
                <el-card :body-style="{ padding: `0` }" shadow="hover">
                    <div class="code-item">
                        <div :style="{ background: `orange` }" class="img">
                            <el-icon>
                                <component :is="`sc-icon-csharp`" />
                            </el-icon>
                        </div>
                        <div class="title">
                            <el-form :model="form" :rules="rules" label-position="right" label-width="10rem" ref="form">
                                <el-form-item :label="$t(`选择项目`)" prop="project">
                                    <sc-select
                                        v-model="form.project"
                                        :config="{ props: { label: `item1`, value: `item2` } }"
                                        :query-api="$API.sys_dev.getDomainProjects"
                                        class="w100p"
                                        clearable
                                        filterable />
                                </el-form-item>
                                <el-form-item :label="$t(`实体名称`)" prop="entityName">
                                    <el-input v-model="form.entityName" placeholder="Example"></el-input>
                                </el-form-item>
                                <el-form-item :label="$t(`基类`)" prop="baseClass">
                                    <sc-select
                                        v-model="form.baseClass"
                                        :config="{ props: { label: `item1`, value: `item1` } }"
                                        :query-api="$API.sys_dev.getEntityBaseClasses"
                                        class="w100p"
                                        clearable
                                        filterable />
                                </el-form-item>
                                <el-form-item :label="$t(`实现接口`)" prop="interfaces">
                                    <sc-select
                                        v-model="form.interfaces"
                                        :config="{ props: { label: `item1`, value: `item1` } }"
                                        :query-api="$API.sys_dev.getFieldInterfaces"
                                        class="w100p"
                                        clearable
                                        filterable
                                        multiple />
                                </el-form-item>
                                <el-form-item :label="$t(`描述`)" prop="summary">
                                    <el-input v-model="form.summary" placeholder="example" />
                                </el-form-item>
                                <el-form-item :label="$t(`字段列表`)" prop="fieldList">
                                    <sc-form-table v-model="form.fieldList" :addTemplate="{}" :placeholder="$t(`暂无数据`)" drag-sort ref="formTable">
                                        <el-table-column :label="$t(`字段名称`)" prop="name" width="150">
                                            <template #default="{ row }">
                                                <el-input v-model="row.name" placeholder="Username" />
                                            </template>
                                        </el-table-column>
                                        <el-table-column :label="$t(`字段说明`)" prop="summary" width="150">
                                            <template #default="{ row }">
                                                <el-input v-model="row.summary" :placeholder="$t(`用户名`)" />
                                            </template>
                                        </el-table-column>
                                        <el-table-column :label="$t(`示例值`)" prop="example" width="150">
                                            <template #default="{ row }">
                                                <el-input v-model="row.example" :placeholder="$t(`root`)" />
                                            </template>
                                        </el-table-column>
                                        <el-table-column :label="$t(`字段类型`)" prop="type" width="150">
                                            <template #default="{ row }">
                                                <el-select
                                                    v-loading="loading"
                                                    v-model="row.type"
                                                    :remote-method="remoteMethod"
                                                    filterable
                                                    remote
                                                    remote-show-suffix
                                                    reserve-keyword>
                                                    <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value" />
                                                </el-select>
                                            </template>
                                        </el-table-column>
                                        <el-table-column :label="$t(`数据库字段类型`)" prop="dbType">
                                            <template #default="{ row }">
                                                <el-select v-model="row.dbType" clearable filterable>
                                                    <el-option v-for="(item, i) in dbTypes" :key="i" :label="item[0]" :value="item[0]" />
                                                </el-select>
                                            </template>
                                        </el-table-column>
                                        <el-table-column :label="$t(`可空`)" align="center" prop="isNullable" width="100">
                                            <template #default="{ row }">
                                                <el-switch v-model="row.isNullable" />
                                            </template>
                                        </el-table-column>
                                        <el-table-column :label="$t(`主键`)" align="center" prop="isPrimary" width="100">
                                            <template #default="{ row }">
                                                <el-switch v-model="row.isPrimary" />
                                            </template>
                                        </el-table-column>
                                        <el-table-column :label="$t(`值类型`)" align="center" prop="isStruct" width="100">
                                            <template #default="{ row }">
                                                <el-switch v-model="row.isStruct" />
                                            </template>
                                        </el-table-column>
                                    </sc-form-table>
                                </el-form-item>
                                <el-form-item>
                                    <el-button @click="submit" type="primary">{{ $t(`生成`) }}</el-button>
                                </el-form-item>
                            </el-form>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col>
                <el-card :body-style="{ padding: `0` }" shadow="hover">
                    <div class="code-item">
                        <div :style="{ background: `blue` }" class="img">
                            <el-icon>
                                <component :is="`sc-icon-js`" />
                            </el-icon>
                        </div>
                        <div class="title">
                            <p>
                                <el-button @click="generateJsCode()">{{ $t(`生成`) }}</el-button>
                            </p>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col>
                <el-card :body-style="{ padding: `0` }" shadow="hover">
                    <div class="code-item">
                        <div :style="{ background: `green` }" class="img">
                            <el-icon>
                                <component :is="`el-icon-picture`" />
                            </el-icon>
                        </div>
                        <div class="title">
                            <p>
                                <el-input v-model="iconForm.iconName" :placeholder="$t(`图标名称`)" />
                            </p>
                            <p>
                                <el-input v-model="iconForm.svgCode" :placeholder="$t(`粘贴SVG代码`)" />
                            </p>
                            <p>
                                <el-row align="middle">
                                    <el-col :span="12">
                                        <el-button @click="generateIconCode()">{{ $t(`生成`) }}</el-button>
                                    </el-col>
                                    <el-col :span="12">
                                        <el-link href="https://www.iconfont.cn/" target="_blank">Iconfont</el-link>
                                    </el-col>
                                </el-row>
                            </p>
                        </div>
                    </div>
                </el-card>
            </el-col>
        </el-row>
    </el-main>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const scFormTable = defineAsyncComponent(() => import('@/components/sc-form-table'))

export default {
    components: {
        scFormTable,
    },
    created() {
        this.dbTypes = Object.entries(this.$GLOBAL.chars).filter((x) => x[0].indexOf(`FLG_DB_FIELD_TYPE`) === 0)
    },
    data() {
        return {
            loading: false,
            options: [],
            dbTypes: [],
            iconForm: {
                svgCode: ``,
                iconName: ``,
            },
            form: {
                entityName: `Example`,
                interfaces: [`IFieldSummary`, `IFieldOwner`, `IFieldSort`, `IFieldEnabled`],
                summary: this.$t(`示例`),
                baseClass: `VersionEntity`,
                fieldList: [
                    {
                        name: `Id`,
                        summary: this.$t(`示例编号`),
                        example: `123456`,
                        type: `long`,
                        isPrimary: true,
                        isStruct: true,
                    },
                    {
                        name: `Name`,
                        summary: this.$t(`名称`),
                        example: this.$t(`老王`),
                        type: `string`,
                        dbType: `FLG_DB_FIELD_TYPE_VARCHAR_31`,
                    },
                    {
                        name: `Gender`,
                        summary: this.$t(`性别`),
                        example: this.$t(`Male`),
                        type: `Genders`,
                        isNullable: true,
                        isStruct: true,
                    },
                ],
            },
            rules: {
                project: [{ required: true, message: this.$t(`请选择项目`) }],
                entityName: [{ required: true, message: this.$t(`请输入实体名称`) }],
                summary: [{ required: true, message: this.$t(`请输入描述`) }],
                baseClass: [{ required: true, message: this.$t(`请选择基类`) }],
            },
        }
    },
    methods: {
        async generateIconCode() {
            try {
                await this.$API.sys_dev.generateIconCode.post(this.iconForm)
                this.$message.success($t(`生成完毕`))
            } catch {}
        },
        async generateJsCode() {
            try {
                await this.$API.sys_dev.generateJsCode.post()
                this.$message.success($t(`生成完毕`))
            } catch {}
        },
        async remoteMethod(query) {
            if (query) {
                await new Promise((x) => setTimeout(x, 100))
                this.loading = true
                const res = await this.$API.sys_dev.getDotnetDataTypes.post({ startWith: query })
                this.options = res.data.map((x) => {
                    return {
                        label: x,
                        value: x,
                    }
                })
                this.loading = false
            } else {
                this.options = []
            }
        },
        async submit() {
            let primaryCount = 0
            for (const field of this.form.fieldList ?? []) {
                if (field.isPrimary) {
                    primaryCount++
                }
                if (!field.name) {
                    this.$message.error(this.$t(`缺少字段名`))
                    return
                }
                if (!field.summary) {
                    this.$message.error(this.$t(`缺少字段说明`))
                    return
                }
                if (!field.example) {
                    this.$message.error(this.$t(`缺少字段示例值`))
                    return
                }
                if (!field.type) {
                    this.$message.error(this.$t(`缺少字段类型`))
                    return
                }
            }
            if (primaryCount !== 1) {
                this.$message.error(this.$t(`主键字段数量不为1`))
                return
            }

            const valid = await this.$refs.form.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true
            try {
                const res = await this.$API.sys_dev.generateCsCode.post(this.form)
                this.$emit(`success`, res.data, this.mode)
                this.visible = false
                this.$message.success(this.$t(`操作成功`))
            } catch {}
            this.loading = false
        },
    },
}
</script>

<style scoped>
.el-card {
    margin-bottom: 1rem;
}

.code-item {
    cursor: pointer;
}

.code-item .img {
    width: 100%;
    height: 10rem;
    display: flex;
    align-items: center;
    justify-content: center;
}

.code-item .img i {
    font-size: 7.7rem;
    color: var(--el-color-white);
    -webkit-text-fill-color: transparent;
}

.code-item .title {
    padding: 1rem;
}

.code-item .title h2 {
    font-size: 1.2rem;
}

.code-item .title h4 {
    font-size: 0.9rem;
    color: var(--el-color-info);
    font-weight: normal;
    margin-top: 0.4rem;
}

.code-item .title p {
    margin-top: 1rem;
}
</style>