<template>
    <el-main>
        <el-alert
            style="margin-bottom: 20px"
            title="感谢codeMirror组件"
            type="success"
        ></el-alert>
        <el-row :gutter="15">
            <el-col :lg="24">
                <el-card header="JSON" shadow="never">
                    <sc-code-editor
                        ref="editor"
                        v-model="json"
                        :height="200"
                        mode="javascript"
                    ></sc-code-editor>
                    <div style="margin-top: 15px">
                        <el-button type="primary" @click="getCode"
                            >获取v-model</el-button
                        >
                        <el-button type="primary" @click="getValue"
                            >getValue()</el-button
                        >
                        <el-button type="primary" @click="setValue"
                            >setValue()</el-button
                        >
                    </div>
                </el-card>
            </el-col>
            <el-col :lg="12">
                <el-card header="javascript Darcula主题" shadow="never">
                    <sc-code-editor
                        v-model="js"
                        mode="javascript"
                        theme="darcula"
                    ></sc-code-editor>
                </el-card>
            </el-col>
            <el-col :lg="12">
                <el-card header="SQL" shadow="never">
                    <sc-code-editor v-model="sql" mode="sql"></sc-code-editor>
                </el-card>
            </el-col>
        </el-row>
    </el-main>
</template>
<script>
import { defineAsyncComponent } from "vue";

const scCodeEditor = defineAsyncComponent(() =>
    import("@/components/scCodeEditor")
);
export default {
    name: "codeeditor",
    components: {
        scCodeEditor,
    },
    data() {
        return {
            json: `{
    "name": "SCUI",
    "menu": [
        {
            "title": "VUE 3",
            "type": true,
            "link": "https://v3.cn.vuejs.org"
        },
        {
            "title": "elementplus",
            "type": false,
            "link": "https://element-plus.gitee.io"
        }
    ]
}`,
            js: `// Demo code (the actual new parser character stream implementation)
function StringStream(string) {
    this.pos = 0;
    this.string = string;
}`,
            sql: `SELECT \`author\`, \`title\` FROM \`posts\`
WHERE \`status\` = 'draft' AND \`author\` IN('author1','author2')
ORDER BY \`created_at\` DESC, \`id\` DESC LIMIT 0, 10;`,
        };
    },
    methods: {
        getCode() {
            this.$message("请查看控制台");
        },
        getValue() {
            this.$message("请查看控制台");
            const v = this.$refs.editor.coder.getValue();
        },
        setValue() {
            const v = `{"key":"newValue"}`;
            this.$refs.editor.coder.setValue(v);
        },
    },
};
</script>
<style></style>