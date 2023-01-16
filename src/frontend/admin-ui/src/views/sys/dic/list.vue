<template>
    <el-dialog :title="titleMap[mode]" v-model="visible" :width="400" destroy-on-close @closed="$emit('closed')">
        <el-form :model="form" :rules="rules" ref="dialogForm" label-width="100px" label-position="left">
            <el-form-item label="所属字典" prop="catalogId">
                <el-cascader v-model="form.catalogId" :options="dic" :props="dicProps" :show-all-levels="false"
                             clearable></el-cascader>
            </el-form-item>
            <el-form-item label="项名称" prop="key">
                <el-input v-model="form.key" clearable></el-input>
            </el-form-item>
            <el-form-item label="键值" prop="value">
                <el-input v-model="form.value" clearable></el-input>
            </el-form-item>
            <el-form-item label="是否有效" prop="enabled">
                <el-switch v-model="form.enabled"></el-switch>
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible=false">取 消</el-button>
            <el-button type="primary" :loading="isSaveing" @click="submit()">保 存</el-button>
        </template>
    </el-dialog>
</template>

<script>
export default {
    emits: ['success', 'closed'],
    data() {
        return {
            mode: "add",
            titleMap: {
                add: '新增项',
                edit: '编辑项'
            },
            visible: false,
            isSaveing: false,
            form: {
                version: 0,
                id: 0,
                catalogId: 0,
                key: "",
                value: "",
                enabled: true
            },
            rules: {
                catalogId: [
                    {required: true, message: '请选择所属字典'}
                ],
                key: [
                    {required: true, message: '请输入项名称'}
                ],
                value: [
                    {required: true, message: '请输入键值'}
                ]
            },
            dic: [],
            dicProps: {
                value: "id",
                label: "label",
                emitPath: false,
                checkStrictly: true
            }
        }
    },
    mounted() {
        if (this.params) {
            this.form.catalogId = this.params.id
        }
        this.getDic()
    },
    methods: {
        //显示
        open(mode = 'add') {
            this.mode = mode;
            this.visible = true;
            return this;
        },
        //获取字典列表
        async getDic() {
            var res = await this.$API.sys_dic.queryCatalog.post();
            this.dic = res.data;
        },
        //表单提交方法
        submit() {
            this.$refs.dialogForm.validate(async (valid) => {
                if (valid) {
                    this.isSaveing = true
                    try {
                        const method = (this.mode == 'add' ? this.$API.sys_dic.createContent
                            : this.$API.sys_dic.updateContent)
                        var res = await method.post(this.form);
                        this.$emit('success', res.data, this.mode)
                        this.visible = false;
                        this.$message.success("操作成功")
                    } catch {

                    }
                    this.isSaveing = false

                }
            })
        },
        //表单注入数据
        setData(data) {
            this.form.version = data.version
            this.form.id = data.id
            this.form.key = data.key
            this.form.value = data.value
            this.form.enabled = data.enabled
            this.form.catalogId = data.catalogId


        }
    }
}
</script>

<style>
</style>