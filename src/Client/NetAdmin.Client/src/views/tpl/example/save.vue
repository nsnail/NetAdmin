<template>
    <el-dialog :title="titleMap[mode]" v-model="visible" :width="500" destroy-on-close @closed="$emit('closed')">
        <el-form :model="form" :rules="rules" :disabled="mode==='show'" ref="dialogForm" label-width="100px">
            <el-form-item label="状态" prop="boolean">
                <el-switch v-model="form.enabled"></el-switch>
            </el-form-item>
        </el-form>
        <template #footer>
            <el-button @click="visible=false">取 消</el-button>
            <el-button v-if="mode!=='show'" type="primary" :loading="isSaving" @click="submit()">保 存</el-button>
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
                add: '新增',
                edit: '编辑',
                show: '查看'
            },
            visible: false,
            isSaving: false,
            //表单数据
            form: {
                id: 0,
                enabled: true
            },
            //验证规则
            rules: {
                name: [
                    {required: true, message: '请输入姓名'}
                ]
            },
            //所需数据选项
            groups: [],
            groupsProps: {
                value: "id",
                emitPath: false,
                checkStrictly: true
            }
        }
    },
    mounted() {

    },
    methods: {
        //显示
        open(mode = 'add') {
            this.mode = mode;
            this.visible = true;
            return this
        },
        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {
            });
            if (!valid) {
                return false
            }

            this.isSaving = true;
            try {
                const method = (this.mode === 'add' ? this.$API.tpl_example.create
                    : this.$API.tpl_example.update)
                const res = await method.post(this.form);
                this.$emit('success', res.data, this.mode)
                this.visible = false;
                this.$message.success("操作成功")
            } catch {
            }
            this.isSaving = false;

        },
        //表单注入数据
        setData(data) {
            // this.form.id = data.id
            // this.form.enabled = data.enabled
            //可以和上面一样单个注入，也可以像下面一样直接合并进去
            Object.assign(this.form, data)
        }
    }
}
</script>

<style>
</style>