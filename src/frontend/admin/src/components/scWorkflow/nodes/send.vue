<template>
    <div class="node-wrap">
        <div @click="show" class="node-wrap-box">
            <div class="title" style="background: #3296fa">
                <el-icon class="icon">
                    <el-icon-promotion />
                </el-icon>
                <span>{{ nodeConfig.nodeName }}</span>
                <el-icon @click.stop="delNode()" class="close">
                    <el-icon-close />
                </el-icon>
            </div>
            <div class="content">
                <span v-if="toText(nodeConfig)">{{ toText(nodeConfig) }}</span>
                <span v-else class="placeholder">请选择人员</span>
            </div>
        </div>
        <add-node v-model="nodeConfig.childNode"></add-node>
        <el-drawer v-model="drawer" :size="500" :title="$t('抄送人设置')" append-to-body destroy-on-close>
            <template #header>
                <div class="node-wrap-drawer__title">
                    <label v-if="!isEditTitle" @click="editTitle"
                        >{{ form.nodeName }}
                        <el-icon class="node-wrap-drawer__title-edit">
                            <el-icon-edit />
                        </el-icon>
                    </label>
                    <el-input
                        v-if="isEditTitle"
                        v-model="form.nodeName"
                        @blur="saveTitle"
                        @keyup.enter="saveTitle"
                        clearable
                        ref="nodeTitle"></el-input>
                </div>
            </template>
            <el-container>
                <el-main style="padding: 0 20px 20px 20px">
                    <el-form label-position="top">
                        <el-form-item :label="$t('选择要抄送的人员')">
                            <el-button @click="selectHandle(1, form.nodeUserList)" icon="el-icon-plus" round type="primary">选择人员</el-button>
                            <div class="tags-list">
                                <el-tag v-for="(user, index) in form.nodeUserList" :key="user.id" @close="delUser(index)" closable
                                    >{{ user.name }}
                                </el-tag>
                            </div>
                        </el-form-item>
                        <el-form-item label="">
                            <el-checkbox v-model="form.userSelectFlag" :label="$t('允许发起人自选抄送人')"></el-checkbox>
                        </el-form-item>
                    </el-form>
                </el-main>
                <el-footer>
                    <el-button @click="save" type="primary">保存</el-button>
                    <el-button @click="drawer = false">取消</el-button>
                </el-footer>
            </el-container>
        </el-drawer>
    </div>
</template>

<script>
import addNode from './addNode'

export default {
    inject: ['select'],
    props: {
        modelValue: {
            type: Object,
            default: () => {},
        },
    },
    components: {
        addNode,
    },
    data() {
        return {
            nodeConfig: {},
            drawer: false,
            isEditTitle: false,
            form: {},
        }
    },
    watch: {
        modelValue() {
            this.nodeConfig = this.modelValue
        },
    },
    mounted() {
        this.nodeConfig = this.modelValue
    },
    methods: {
        show() {
            this.form = {}
            this.form = JSON.parse(JSON.stringify(this.nodeConfig))
            this.drawer = true
        },
        editTitle() {
            this.isEditTitle = true
            this.$nextTick(() => {
                this.$refs.nodeTitle.focus()
            })
        },
        saveTitle() {
            this.isEditTitle = false
        },
        save() {
            this.$emit('update:modelValue', this.form)
            this.drawer = false
        },
        delNode() {
            this.$emit('update:modelValue', this.nodeConfig.childNode)
        },
        delUser(index) {
            this.form.nodeUserList.splice(index, 1)
        },
        selectHandle(type, data) {
            this.select(type, data)
        },
        toText(nodeConfig) {
            if (nodeConfig.nodeUserList && nodeConfig.nodeUserList.length > 0) {
                const users = nodeConfig.nodeUserList.map((item) => item.name).join('、')
                return users
            } else {
                if (nodeConfig.userSelectFlag) {
                    return '发起人自选'
                } else {
                    return false
                }
            }
        },
    },
}
</script>

<style></style>