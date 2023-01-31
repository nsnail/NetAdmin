<template>
    <router-view></router-view>
    <el-main>
        <el-row :gutter="15">
            <el-col :xl="6" :lg="6" :md="8" :sm="12" :xs="24">
                <el-card shadow="hover" :body-style="{ padding: '0px' }">
                    <div class="code-item">
                        <div class="img" :style="{background: 'blue'}">
                            <el-icon
                                :style="`background-image: -webkit-linear-gradient(top left, #fff, blue 100px)`">
                                <component :is="`sc-icon-js`"/>
                            </el-icon>
                        </div>
                        <div class="title">
                            <h2>生成前端代码</h2>
                            <p>
                                <el-input v-model="form.projectPath" placeholder="项目路径"></el-input>
                            </p>
                            <p>
                                <el-button @click="generateJsCode()">生成</el-button>
                            </p>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col :xl="6" :lg="6" :md="8" :sm="12" :xs="24">
                <el-card shadow="hover" :body-style="{ padding: '0px' }">
                    <div class="code-item">
                        <div class="img" :style="{background: 'orange'}">
                            <el-icon
                                :style="`background-image: -webkit-linear-gradient(top left, #fff, blue 100px)`">
                                <component :is="`sc-icon-csharp`"/>
                            </el-icon>
                        </div>
                        <div class="title">
                            <h2>生成后端代码</h2>
                            <p>
                                <el-input v-model="formCs.projectPath" placeholder="项目路径"></el-input>
                            </p>
                            <p>
                                <el-input v-model="formCs.type" placeholder="模块类型"></el-input>
                            </p>
                            <p>
                                <el-input v-model="formCs.moduleName" placeholder="模块名称"></el-input>
                            </p>
                            <p>
                                <el-input v-model="formCs.moduleRemark" placeholder="模块说明"></el-input>
                            </p>
                            <p>
                                <el-button @click="generateCsCode()">生成</el-button>
                            </p>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col :xl="6" :lg="6" :md="8" :sm="12" :xs="24">
                <el-card shadow="hover" :body-style="{ padding: '0px' }">
                    <div class="code-item">
                        <div class="img" :style="{background: 'green'}">
                            <el-icon
                                :style="`background-image: -webkit-linear-gradient(top left, #fff, green 100px)`">
                                <component :is="`el-icon-picture`"/>
                            </el-icon>
                        </div>
                        <div class="title">
                            <h2>生成图标代码</h2>
                            <p>
                                <el-input v-model="form.iconName" placeholder="图标名称"></el-input>
                            </p>
                            <p>
                                <el-input v-model="form.svgCode" placeholder="粘贴SVG代码"></el-input>
                            </p>
                            <p>
                                <el-row align="middle">
                                    <el-col :span="12">
                                        <el-button @click="generateIconCode()">生成</el-button>
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
export default {
    name: 'autocode',
    data() {
        return {
            form: {
                svgCode: '',
                iconName: '',
                projectPath: "d:\\Work\\SVN\\Tao\\NetAdmin\\src\\Client\\admin-ui",
            },
            formCs: {
                moduleName: '',
                ///     模块说明
                moduleRemark: '',
                ///     项目路径
                projectPath: 'd:\\Work\\SVN\\Tao\\NetAdmin\\src\\Server',
                ///     模块类型
                type: 'Sys',
            }
        }
    },
    methods: {
        async generateIconCode() {
            try {
                await this.$API.sys_dev.generateIconCode.post(this.form)
                this.$message.success('生成完毕')
            } catch {

            }
        },
        async generateJsCode() {
            try {
                await this.$API.sys_dev.generateJsCode.post(null, {
                    params: {
                        projectPath:
                        this.form.projectPath
                    }
                })
                this.$message.success('生成完毕')
            } catch {

            }
        },
        async generateCsCode() {
            try {
                await this.$API.sys_dev.generateCsCode.post(this.formCs)
                this.$message.success('生成完毕')
            } catch {

            }
        }
    }
}
</script>

<style scoped>
.el-card {
    margin-bottom: 15px;
}

.code-item {
    cursor: pointer;
}

.code-item .img {
    width: 100%;
    height: 150px;
    background: #09f;
    display: flex;
    align-items: center;
    justify-content: center;
}

.code-item .img i {
    font-size: 100px;
    color: #fff;
    background-image: -webkit-linear-gradient(top left, #fff, #09f 100px);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
}

.code-item .title {
    padding: 15px;
}

.code-item .title h2 {
    font-size: 16px;
}

.code-item .title h4 {
    font-size: 12px;
    color: #999;
    font-weight: normal;
    margin-top: 5px;
}

.code-item .title p {
    margin-top: 15px;
}
</style>