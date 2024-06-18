<template>
    <router-view></router-view>
    <el-main>
        <el-row :gutter="15">
            <el-col :lg="6" :md="8" :sm="12" :xl="6" :xs="24">
                <el-card :body-style="{ padding: '0' }" shadow="hover">
                    <div class="code-item">
                        <div :style="{ background: 'blue' }" class="img">
                            <el-icon :style="`background-image: -webkit-linear-gradient(top left, #fff, blue 10rem)`">
                                <component :is="`sc-icon-js`" />
                            </el-icon>
                        </div>
                        <div class="title">
                            <h2>{{ $t('生成前端代码') }}</h2>
                            <p>
                                <el-button @click="generateJsCode()">{{ $t('生成') }}</el-button>
                            </p>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col :lg="6" :md="8" :sm="12" :xl="6" :xs="24">
                <el-card :body-style="{ padding: '0' }" shadow="hover">
                    <div class="code-item">
                        <div :style="{ background: 'orange' }" class="img">
                            <el-icon :style="`background-image: -webkit-linear-gradient(top left, #fff, blue 10rem)`">
                                <component :is="`sc-icon-csharp`" />
                            </el-icon>
                        </div>
                        <div class="title">
                            <h2>{{ $t('生成后端代码') }}</h2>
                            <p>
                                <el-input v-model="formCs.type" :placeholder="$t('模块类型')"></el-input>
                            </p>
                            <p>
                                <el-input v-model="formCs.moduleName" :placeholder="$t('模块名称')"></el-input>
                            </p>
                            <p>
                                <el-input v-model="formCs.moduleRemark" :placeholder="$t('模块说明')"></el-input>
                            </p>
                            <p>
                                <el-button @click="generateCsCode()">{{ $t('生成') }}</el-button>
                            </p>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col :lg="6" :md="8" :sm="12" :xl="6" :xs="24">
                <el-card :body-style="{ padding: '0' }" shadow="hover">
                    <div class="code-item">
                        <div :style="{ background: 'green' }" class="img">
                            <el-icon :style="`background-image: -webkit-linear-gradient(top left, #fff, green 10rem)`">
                                <component :is="`el-icon-picture`" />
                            </el-icon>
                        </div>
                        <div class="title">
                            <h2>{{ $t('生成图标代码') }}</h2>
                            <p>
                                <el-input v-model="form.iconName" :placeholder="$t('图标名称')"></el-input>
                            </p>
                            <p>
                                <el-input v-model="form.svgCode" :placeholder="$t('粘贴SVG代码')"></el-input>
                            </p>
                            <p>
                                <el-row align="middle">
                                    <el-col :span="12">
                                        <el-button @click="generateIconCode()">{{ $t('生成') }}</el-button>
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
            <el-col :lg="6" :md="8" :sm="12" :xl="6" :xs="24">
                <el-card :body-style="{ padding: '0' }" shadow="hover">
                    <div class="code-item">
                        <div :style="{ background: 'gray' }" class="img">
                            <el-icon :style="`background-image: -webkit-linear-gradient(top left, #fff, green 10rem)`">
                                <component :is="`el-icon-picture`" />
                            </el-icon>
                        </div>
                        <div class="title">
                            <h2>{{ $t('生成表格代码') }}</h2>
                            <p>
                                <el-input v-model="form.summaryInfo" :placeholder="$t('注释信息')" type="textarea"></el-input>
                            </p>
                            <p>
                                <el-input v-model="form.tableCode" :placeholder="$t('表格代码')" type="textarea"></el-input>
                            </p>
                            <p>
                                <el-input v-model="form.formCode" :placeholder="$t('表单代码')" type="textarea"></el-input>
                            </p>
                            <p>
                                <el-button @click="generateTableCode()">{{ $t('生成') }}</el-button>
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
                iconName: '',
            },
            formCs: {
                moduleName: '',
                ///     模块说明
                moduleRemark: '',
                ///     模块类型
                type: 'SysComponent',
            },
        }
    },
    methods: {
        async generateIconCode() {
            try {
                await this.$API.sys_dev.generateIconCode.post(this.form)
                this.$message.success('生成完毕')
            } catch {}
        },
        async generateTableCode() {
            for (const line of this.form.summaryInfo.split('\n')) {
                if (!line) continue
                let lineSplit = line.split(',')
                this.form.tableCode += `<el-table-column prop="${lineSplit[0].slice(0, 1).toLowerCase()}${lineSplit[0].slice(1)}" label="${
                    lineSplit[1]
                }" />`

                this.form.formCode += `<el-form-item  prop="${lineSplit[0].slice(0, 1).toLowerCase()}${lineSplit[0].slice(1)}" label="${
                    lineSplit[1]
                }"><el-input v-model="form.${lineSplit[0].slice(0, 1).toLowerCase()}${lineSplit[0].slice(1)}" clearable /></el-form-item>`
            }
        },
        async generateJsCode() {
            try {
                await this.$API.sys_dev.generateJsCode.post()
                this.$message.success('生成完毕')
            } catch {}
        },
        async generateCsCode() {
            try {
                await this.$API.sys_dev.generateCsCode.post(this.formCs)
                this.$message.success('生成完毕')
            } catch {}
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
    background: #09f;
    display: flex;
    align-items: center;
    justify-content: center;
}

.code-item .img i {
    font-size: 7.7rem;
    color: #fff;
    background-image: -webkit-linear-gradient(top left, #fff, #09f 10rem);
    -webkit-background-clip: text;
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
    color: #999;
    font-weight: normal;
    margin-top: 0.4rem;
}

.code-item .title p {
    margin-top: 1rem;
}
</style>