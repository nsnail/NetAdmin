<template>
    <common-page v-loading="loading" title="注册新账号">
        <el-steps :active="stepActive" finish-status="success" simple>
            <el-step title="填写帐号"/>
            <el-step title="完成注册"/>
        </el-steps>
        <el-form v-if="stepActive==0" ref="stepForm_0" :label-width="120" :model="form" :rules="rules">
            <el-form-item label="登录账号" prop="userName">
                <el-input v-model="form.userName" placeholder="请输入登录账号"></el-input>
                <div class="el-form-item-msg">登录账号将作为登录时的唯一凭证</div>
            </el-form-item>
            <el-form-item label="登录密码" prop="passwordText">
                <el-input v-model="form.passwordText" placeholder="请输入登录密码" show-password
                          type="password"></el-input>
                <sc-password-strength v-model="form.passwordText"></sc-password-strength>
                <div class="el-form-item-msg">请输入包含英文、数字的8位以上密码</div>
            </el-form-item>
            <el-form-item label="确认密码" prop="passwordText2">
                <el-input v-model="form.passwordText2" placeholder="请再一次输入登录密码" show-password
                          type="password"></el-input>
            </el-form-item>
            <el-form-item label="" prop="agree">
                <el-checkbox v-model="form.agree" label="">已阅读并同意</el-checkbox>
                <span class="link" @click="showAgree=true">《平台服务协议》</span>
            </el-form-item>
        </el-form>

        <div v-if="stepActive==1">
            <el-result icon="success" sub-title="可以使用登录账号以及手机号登录系统" title="注册成功">
                <template #extra>
                    <el-button type="primary" @click="goLogin">前去登录</el-button>
                </template>
            </el-result>
        </div>
        <el-form style="text-align: center;">
            <el-button v-if="stepActive==0" type="primary" @click="save">提交</el-button>
        </el-form>
        <el-dialog v-model="showAgree" :width="800" destroy-on-close title="平台服务协议">
            平台服务协议
            <template #footer>
                <el-button @click="showAgree=false">取消</el-button>
                <el-button type="primary" @click="showAgree=false;form.agree=true;">我已阅读并同意</el-button>
            </template>
        </el-dialog>

        <Verify
            ref="verify"
            :imgSize="{width:'310px',height:'155px'}"
            captchaType="blockPuzzle"
            mode="pop"
            @success="captchaSuccess"
        ></Verify>
    </common-page>
</template>

<script>
import scPasswordStrength from '@/components/scPasswordStrength';
import commonPage from './components/commonPage'
import Verify from "@/views/login/components/verifition/Verify.vue";

export default {
    components: {
        Verify,
        commonPage,
        scPasswordStrength
    },
    data() {
        return {
            loading: false,
            stepActive: 0,
            showAgree: false,
            form: {
                passwordText: "",
                passwordText2: "",
                agree: false,
                userName: "",
            },
            rules: {
                userName: [
                    {required: true, message: '请输入账号名'}
                ],
                passwordText: [
                    {required: true, message: '请输入密码'}
                ],
                passwordText2: [
                    {required: true, message: '请再次输入密码'},
                    {
                        validator: (rule, value, callback) => {
                            if (value !== this.form.passwordText) {
                                callback(new Error('两次输入密码不一致'));
                            } else {
                                callback();
                            }
                        }
                    }
                ],
                agree: [
                    {
                        validator: (rule, value, callback) => {
                            if (!value) {
                                callback(new Error('请阅读并同意协议'));
                            } else {
                                callback();
                            }
                        }
                    }
                ],
            }
        }
    },
    mounted() {

    },
    methods: {
        async captchaSuccess(obj) {
            this.loading = true
            try {
                await this.$API.sys_user.register.post(Object.assign({verifyCaptchaReq: obj}, this.form))
                this.stepActive += 1
            } catch {
            }
            this.loading = false
        },
        pre() {
            this.stepActive -= 1
        },
        next() {
            const formName = `stepForm_${this.stepActive}`
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.stepActive += 1
                } else {
                    return false
                }
            })
        },
        async save() {
            const formName = `stepForm_${this.stepActive}`
            await this.$refs[formName].validate(async (valid) => {
                if (valid) {
                    this.$refs.verify.show()
                } else {
                    return false
                }
            })
        },
        goLogin() {
            this.$router.push({
                path: '/login'
            })
        }
    }
}
</script>

<style scoped>


</style>