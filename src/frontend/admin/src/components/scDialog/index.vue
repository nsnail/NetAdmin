<!--
 * @Descripttion: 弹窗扩展组件
 * @version: 2.0
 * @Author: sakuya
 * @Date: 2021年8月27日08:51:52
 * @LastEditors: Xujianchen
 * @LastEditTime: 2023-03-18 13:04:33
-->

<template>
    <div class="sc-dialog" ref="scDialog">
        <el-dialog v-bind="$attrs" v-model="dialogVisible" :fullscreen="isFullscreen" :show-close="false" draggable ref="dialog">
            <template #header>
                <slot name="header">
                    <span class="el-dialog__title">{{ title }}</span>
                </slot>
                <div class="sc-dialog__headerbtn">
                    <button v-if="showFullscreen" @click="setFullscreen" aria-label="fullscreen" type="button">
                        <el-icon v-if="isFullscreen" class="el-dialog__close">
                            <el-icon-bottom-left />
                        </el-icon>
                        <el-icon v-else class="el-dialog__close">
                            <el-icon-full-screen />
                        </el-icon>
                    </button>
                    <button v-if="showClose" @click="closeDialog" aria-label="close" type="button">
                        <el-icon class="el-dialog__close">
                            <el-icon-close />
                        </el-icon>
                    </button>
                </div>
            </template>
            <div v-loading="loading">
                <slot></slot>
            </div>
            <template #footer>
                <slot name="footer"></slot>
            </template>
        </el-dialog>
    </div>
</template>

<script>
export default {
    props: {
        modelValue: { type: Boolean, default: false },
        title: { type: String, default: '' },
        showClose: { type: Boolean, default: true },
        showFullscreen: { type: Boolean, default: true },
        loading: { type: Boolean, default: false },
        fullScreen: { type: Boolean, default: false },
    },
    data() {
        return {
            dialogVisible: false,
            isFullscreen: this.fullScreen,
        }
    },
    watch: {
        modelValue() {
            this.dialogVisible = this.modelValue
        },
    },
    mounted() {
        this.dialogVisible = this.modelValue
    },
    methods: {
        //关闭
        closeDialog() {
            this.dialogVisible = false
        },
        //最大化
        setFullscreen() {
            this.isFullscreen = !this.isFullscreen
        },
    },
}
</script>

<style scoped>
.sc-dialog__headerbtn {
    position: absolute;
    top: var(--el-dialog-padding-primary);
    right: var(--el-dialog-padding-primary);
}

.sc-dialog__headerbtn button {
    padding: 0;
    background: transparent;
    border: none;
    outline: none;
    cursor: pointer;
    font-size: var(--el-message-close-size, 1.2rem);
    margin-left: 1rem;
    color: var(--el-color-info);
}

.sc-dialog__headerbtn button:hover .el-dialog__close {
    color: var(--el-color-primary);
}

.sc-dialog:deep(.el-dialog).is-fullscreen {
    display: flex;
    flex-direction: column;
    top: 0 !important;
    left: 0 !important;
}

.sc-dialog:deep(.el-dialog) .el-dialog__body {
    padding-top: 1rem;
}

.sc-dialog:deep(.el-dialog).is-fullscreen .el-dialog__body {
    flex: 1;
    overflow: auto;
}

.sc-dialog:deep(.el-dialog).is-fullscreen .el-dialog__footer {
    padding-bottom: 1rem;
    border-top: 1px solid var(--el-border-color-base);
}
</style>