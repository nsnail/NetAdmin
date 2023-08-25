<template>
    <router-view></router-view>
    <el-main>
        <el-row :gutter="15">
            <el-col :lg="6" :md="8" :sm="12" :xl="6" :xs="24">
                <el-card :body-style="{ padding: '0px' }" shadow="hover">
                    <div class="code-item">
                        <div :style="{background: 'blue'}" class="img">
                            <el-icon
                                :style="`background-image: -webkit-linear-gradient(top left, #fff, blue 100px)`">
                                <component :is="`sc-icon-js`"/>
                            </el-icon>
                        </div>
                        <div class="title">
                            <h2>生成前端代码</h2>
                            <p>
                                <el-button @click="generateJsCode()">生成</el-button>
                            </p>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col :lg="6" :md="8" :sm="12" :xl="6" :xs="24">
                <el-card :body-style="{ padding: '0px' }" shadow="hover">
                    <div class="code-item">
                        <div :style="{background: 'orange'}" class="img">
                            <el-icon
                                :style="`background-image: -webkit-linear-gradient(top left, #fff, blue 100px)`">
                                <component :is="`sc-icon-csharp`"/>
                            </el-icon>
                        </div>
                        <div class="title">
                            <h2>生成后端代码</h2>
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
            <el-col :lg="6" :md="8" :sm="12" :xl="6" :xs="24">
                <el-card :body-style="{ padding: '0px' }" shadow="hover">
                    <div class="code-item">
                        <div :style="{background: 'green'}" class="img">
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
    data() {
        return {
            form: {
                svgCode: '',
                iconName: ''
            },
            formCs: {
                moduleName: '',
                ///     模块说明
                moduleRemark: '',
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
                await this.$API.sys_dev.generateJsCode.post()
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