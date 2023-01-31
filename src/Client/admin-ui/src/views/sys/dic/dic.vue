<template>
    <el-dialog :title="titleMap[mode]" v-model="visible" :width="330" destroy-on-close @closed="$emit('closed')">
        <el-form :model="form" :rules="rules" ref="dialogForm" label-width="120px" label-position="left">
            <el-form-item label="编码" prop="code">
                <el-input v-model="form.code" clearable placeholder="字典编码"></el-input>
            </el-form-item>
            <el-form-item label="字典名称" prop="name">
                <el-input v-model="form.name" clearable placeholder="字典显示名称"></el-input>
            </el-form-item>
            <el-form-item label="父路径" prop="parentId">
                <el-cascader v-model="form.parentId" :options="dic" :props="dicProps" :show-all-levels="false"
                             clearable></el-cascader>
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
                add: '新增字典',
                edit: '编辑字典'
            },
            visible: false,
            isSaveing: false,
            form: {},
            rules: {
                code: [
                    {required: true, message: '请输入编码'}
                ],
                name: [
                    {required: true, message: '请输入字典名称'}
                ]
            },
            dic: [],
            dicProps: {
                value: "id",
                label: "name",
                emitPath: false,
                checkStrictly: true
            }
        }
    },
    mounted() {
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
            var res = await this.$API.sys_dic.queryCatalog.post()
            this.dic = res.data;
        },
        //表单提交方法
        submit() {
            this.$refs.dialogForm.validate(async (valid) => {
                if (valid) {
                    this.isSaveing = true
                    try {
                        if (!this.form.parentId) this.form.parentId = 0;

                        const method = (this.mode == 'add' ? this.$API.sys_dic.createCatalog
                            : this.$API.sys_dic.updateCatalog)


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
            //可以和上面一样单个注入，也可以像下面一样直接合并进去
            Object.assign(this.form, data)
        }
    }
}
</script>

<style>
</style>