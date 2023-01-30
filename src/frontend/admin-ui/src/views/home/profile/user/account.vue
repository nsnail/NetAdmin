<template>

    <el-card header="个人信息" shadow="never">
        <el-form ref="form" :model="form" label-width="120px" style="margin-top:20px;">
            <el-form-item prop="avatar">
                <sc-upload v-model="form.avatar" title="上传头像"></sc-upload>
            </el-form-item>
            <el-form-item label="账号">
                <el-input v-model="form.userName" disabled></el-input>
                <div class="el-form-item-msg">账号信息用于登录，系统不允许修改</div>
            </el-form-item>
            <el-form-item label="手机号">
                <el-input v-model="form.mobile" disabled prefix-icon="el-icon-iphone">
                    <template v-slot:append>
                        <el-button v-if="form.mobile" icon="sc-icon-unlink" @click="unlinkMobile">解绑手机</el-button>
                        <el-button v-else icon="sc-icon-link" @click="linkMobile">绑定手机</el-button>
                    </template>

                </el-input>
            </el-form-item>
            <el-form-item label="邮箱">
                <el-input v-model="form.email" disabled prefix-icon="el-icon-message">
                    <template v-slot:append>
                        <el-button v-if="form.mobile" icon="sc-icon-unlink" @click="unlinkMobile">解绑邮箱</el-button>
                        <el-button v-else icon="sc-icon-link">绑定邮箱</el-button>
                    </template>

                </el-input>
            </el-form-item>
        </el-form>
    </el-card>
    <el-dialog
        v-model="dialogVisible"
        title="提示">

        <el-form ref="form" :model="form" label-width="80px">
            <el-form-item label="手机号">
                <el-input v-model="form.mobile" disabled prefix-icon="el-icon-iphone">
                    <template v-slot:append>
                        <el-button icon="sc-icon-send" @click="sendSmsCode">发送验证码</el-button>
                    </template>
                </el-input>
            </el-form-item>
            <el-form-item label="验证码">
                <el-input v-model="form.mobile" prefix-icon="el-icon-message">
                </el-input>
            </el-form-item>
        </el-form>

        <template v-slot:footer>
            <span class="dialog-footer">
            <el-button @click="dialogVisible = false">取 消</el-button>
            <el-button type="primary" @click="dialogVisible = false">解除绑定</el-button>
        </span>
        </template>
        <Verify
            ref="verify"
            :imgSize="{width:'310px',height:'155px'}"
            captchaType="blockPuzzle"
            mode="pop"
            @success="captchaSuccess"
        ></Verify>
    </el-dialog>

</template>


<script>
import tool from "@/utils/tool";
import Verify from "@/views/login/components/verifition/Verify.vue";

export default {
    components: {Verify},
    data() {
        return {
            dialogVisible: false,
            form: {}
        }
    }, mounted() {
        this.form = tool.data.get("USER_INFO");
    },
    methods: {
        async captchaSuccess(obj) {
            try {
                await this.$API.sys_sms.sendSmsCode.post({
                    mobile: this.form.mobile,
                    type: 'unlinkMobile', verifyCaptchaReq: obj
                })
            } catch {
            }
        },
        unlinkMobile() {
            this.dialogVisible = true;
        }, linkMobile() {
            this.dialogVisible = true;
        },
        sendSmsCode() {
            this.$refs.verify.show();
        }
    }
}
</script>

<style>
</style>