<template>
    <el-row :gutter="40">
        <el-col v-if="!form.id">
            <el-empty
                :image-size="100"
                description="请选择左侧菜单后操作"
            ></el-empty>
        </el-col>
        <template v-else>
            <el-col>
                <h2>{{ form.meta.title || "新增菜单" }}</h2>
                <el-form
                    ref="dialogForm"
                    :model="form"
                    :rules="rules"
                    label-position="left"
                    label-width="100px"
                >
                    <el-form-item label="显示名称" prop="meta.title">
                        <el-input
                            v-model="form.meta.title"
                            clearable
                            placeholder="菜单显示名字"
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="上级菜单" prop="parentId">
                        <el-cascader
                            v-model="form.parentId"
                            :options="menuOptions"
                            :props="menuProps"
                            :show-all-levels="false"
                            clearable
                            placeholder="顶级菜单"
                        ></el-cascader>
                    </el-form-item>
                    <el-form-item label="类型" prop="meta.type">
                        <el-radio-group v-model="form.meta.type">
                            <el-radio-button
                                v-for="(item, i) in this.$CONFIG.ENUMS
                                    .menuTypes"
                                :key="i"
                                :label="i"
                                >{{ item[1] }}
                            </el-radio-button>
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item label="别名" prop="name">
                        <el-input
                            v-model="form.name"
                            clearable
                            placeholder="菜单别名"
                        ></el-input>
                        <div class="el-form-item-msg">
                            系统唯一且与内置组件名一致，否则导致缓存失效。如类型为Iframe的菜单，别名将代替源地址显示在地址栏
                        </div>
                    </el-form-item>
                    <el-form-item label="菜单图标" prop="meta.icon">
                        <sc-icon-select
                            v-model="form.meta.icon"
                            clearable
                        ></sc-icon-select>
                    </el-form-item>
                    <el-form-item label="路由地址" prop="path">
                        <el-input
                            v-model="form.path"
                            clearable
                            placeholder=""
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="排序" prop="sort">
                        <el-input-number
                            v-model="form.sort"
                            :min="0"
                            controls-position="right"
                            style="width: 100%"
                        ></el-input-number>
                    </el-form-item>
                    <el-form-item label="重定向" prop="redirect">
                        <el-input
                            v-model="form.redirect"
                            clearable
                            placeholder=""
                        ></el-input>
                    </el-form-item>
                    <el-form-item label="菜单高亮" prop="active">
                        <el-input
                            v-model="form.active"
                            clearable
                            placeholder=""
                        ></el-input>
                        <div class="el-form-item-msg">
                            子节点或详情页需要高亮的上级菜单路由地址
                        </div>
                    </el-form-item>
                    <el-form-item label="视图" prop="component">
                        <el-input
                            v-model="form.component"
                            clearable
                            placeholder=""
                        >
                            <template #prepend>views/</template>
                        </el-input>
                        <div class="el-form-item-msg">
                            如父节点、链接或Iframe等没有视图的菜单不需要填写
                        </div>
                    </el-form-item>
                    <el-form-item label="颜色" prop="color">
                        <el-color-picker
                            v-model="form.meta.color"
                            :predefine="predefineColors"
                        ></el-color-picker>
                    </el-form-item>
                    <el-form-item label="是否隐藏" prop="meta.hidden">
                        <el-checkbox v-model="form.meta.hidden"
                            >隐藏菜单
                        </el-checkbox>
                        <el-checkbox v-model="form.meta.hiddenBreadCrumb"
                            >隐藏面包屑
                        </el-checkbox>
                        <div class="el-form-item-msg">
                            菜单不显示在导航中，但用户依然可以访问，例如详情页
                        </div>
                    </el-form-item>
                    <el-form-item label="整页路由" prop="fullPage">
                        <el-switch v-model="form.meta.fullPage" />
                    </el-form-item>
                    <el-form-item label="标签" prop="tag">
                        <el-input
                            v-model="form.meta.tag"
                            clearable
                            placeholder=""
                        ></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button
                            :loading="loading"
                            type="primary"
                            @click="save"
                            >保 存
                        </el-button>
                    </el-form-item>
                </el-form>
            </el-col>
        </template>
    </el-row>
</template>

<script>
import scIconSelect from "@/components/scIconSelect";

export default {
    components: {
        scIconSelect,
    },
    props: {
        menu: {
            type: Object,
            default: () => {},
        },
    },
    data() {
        return {
            form: {
                id: 0,
                parentId: 0,
                name: "",
                path: "",
                component: "",
                redirect: "",
                meta: {
                    title: "",
                    icon: "",
                    active: "",
                    color: "",
                    type: "menu",
                    fullPage: false,
                    tag: "",
                },
                apiList: [],
            },
            menuOptions: [],
            menuProps: {
                emitPath: false,
                value: "id",
                label: "title",
                checkStrictly: true,
            },
            predefineColors: [
                "#ff4500",
                "#ff8c00",
                "#ffd700",
                "#67C23A",
                "#00ced1",
                "#06c755",
                "#c71585",
            ],
            rules: {
                meta: {
                    title: [{ required: true, message: "请输入显示名称" }],
                },
            },
            apiListAddTemplate: {
                code: "",
                url: "",
            },
            loading: false,
        };
    },
    watch: {
        menu: {
            handler() {
                this.menuOptions = this.treeToMap(this.menu);
            },
            deep: true,
        },
    },
    mounted() {},
    methods: {
        //简单化菜单
        treeToMap(tree) {
            const map = [];
            tree.forEach((item) => {
                const obj = {
                    id: item.id,
                    parentId: item.parentId,
                    title: item.meta.title,
                    children:
                        item.children && item.children.length > 0
                            ? this.treeToMap(item.children)
                            : null,
                };
                map.push(obj);
            });
            return map;
        },
        //保存
        async save() {
            await this.$refs.dialogForm.validate(async (valid) => {
                if (valid) {
                    this.loading = true;
                    try {
                        if (!this.form.parentId) this.form.parentId = 0;
                        const res = await this.$API.sys_menu.update.post(
                            this.form
                        );
                        this.$message.success("保存成功");
                        this.$emit("success", res.data);
                    } catch {}
                    this.loading = false;
                }
            });
        },
        //表单注入数据
        setData(data, pid) {
            this.form = data;
            this.form.apiList = data.apiList || [];
            this.form.parentId = pid;
        },
    },
};
</script>

<style scoped>
h2 {
    font-size: 17px;
    color: #3c4a54;
    padding: 0 0 30px 0;
}

.apilist {
    border-left: 1px solid #eee;
}

[data-theme="dark"] h2 {
    color: #fff;
}

[data-theme="dark"] .apilist {
    border-color: #434343;
}
</style>