<template>
    <div v-show="showBox" class="mask">
        <div :style="{ 'max-width': parseInt(imgSize.width) + 30 + 'px' }" class="verifybox">
            <div class="verifybox-top">
                {{ $t('请完成安全验证') }}
                <span @click="closeBox" class="verifybox-close">
                    <el-icon>
                        <el-icon-close></el-icon-close>
                    </el-icon>
                </span>
            </div>
            <div class="verifybox-bottom" style="padding: 1rem">
                <!-- 验证码容器 -->
                <component
                    v-if="componentType"
                    :arith="arith"
                    :barSize="barSize"
                    :blockSize="blockSize"
                    :captchaType="captchaType"
                    :explain="explain"
                    :figure="figure"
                    :imgSize="imgSize"
                    :is="componentType"
                    :mode="mode"
                    :tpl="tpl"
                    :type="verifyType"
                    :vSpace="vSpace"
                    ref="instance"></component>
            </div>
        </div>
    </div>
</template>
<script type="text/babel">
/**
 * Verify 验证码组件
 * @description 分发验证码使用
 * */
import slide from './slide.vue'
import { computed, ref, toRefs, watchEffect } from 'vue'

export default {
    data() {
        return {
            tpl: 'yyyyMMdd',
        }
    },
    created() {
        this.tpl = 'yyyyMMdd'
    },
    components: {
        slide,
    },
    props: {
        captchaType: {
            type: String,
            required: true,
        },
        figure: {
            type: Number,
        },
        arith: {
            type: Number,
        },
        mode: {
            type: String,
            default: 'pop',
        },
        vSpace: {
            type: Number,
        },
        explain: {
            type: String,
        },
        imgSize: {
            type: Object,
            default() {
                return {
                    width: '310px',
                    height: '155px',
                }
            },
        },
        blockSize: {
            type: Object,
        },
        barSize: {
            type: Object,
        },
    },
    setup(props) {
        const { captchaType, figure, arith, mode, vSpace, explain, imgSize, blockSize, barSize } = toRefs(props)
        const clickShow = ref(false)
        const verifyType = ref(undefined)
        const componentType = ref(undefined)

        const instance = ref({})

        const showBox = computed(() => {
            if (mode.value === 'pop') {
                return clickShow.value
            } else {
                return true
            }
        })
        /**
         * refresh
         * @description 刷新
         * */
        const refresh = () => {
            if (instance.value.refresh) {
                instance.value.refresh()
            }
        }
        const closeBox = () => {
            clickShow.value = false
            refresh()
        }
        const show = () => {
            if (mode.value === 'pop') {
                clickShow.value = true
            }
        }
        watchEffect(() => {
            switch (captchaType.value) {
                case 'blockPuzzle':
                    verifyType.value = '2'
                    componentType.value = 'slide'
                    break
                case 'clickWord':
                    verifyType.value = ''
                    componentType.value = 'point'
                    break
            }
        })

        return {
            clickShow,
            verifyType,
            componentType,
            instance,
            showBox,
            closeBox,
            show,
        }
    },
}
</script>
<style>
.verifybox {
    position: relative;
    box-sizing: border-box;
    border-radius: 2px;
    border: 1px solid var(--el-border-color);
    background-color: var(--el-bg-color);
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
}

.verifybox-top {
    padding: 0 1rem;
    height: 50px;
    line-height: 50px;
    text-align: left;
    font-size: 1.2rem;
    color: var(--el-text-color-primary);
    border-bottom: 1px solid var(--el-border-color);
    box-sizing: border-box;
}

.verifybox-bottom {
    padding: 1rem;
    box-sizing: border-box;
}

.verifybox-close {
    position: absolute;
    top: 13px;
    right: 9px;
    width: 2rem;
    line-height: 2rem;
    height: 2rem;
    text-align: center;
    cursor: pointer;
}

.mask {
    position: fixed;
    top: 0;
    left: 0;
    z-index: 1001;
    width: 100%;
    height: 100vh;
    background: rgba(0, 0, 0, 0.3);
    /* display: none; */
    transition: all 0.5s;
}

.verify-tips {
    position: absolute;
    left: 0;
    bottom: 0;
    width: 100%;
    height: 30px;
    padding-left: 10px;
    line-height: 30px;
    color: var(--el-color-white);
}

.suc-bg {
    background-color: rgba(92, 184, 92, 0.5);
    filter: progid:DXImageTransform.Microsoft.gradient(startcolorstr=#7f5CB85C, endcolorstr=#7f5CB85C);
}

.err-bg {
    background-color: rgba(217, 83, 79, 0.5);
    filter: progid:DXImageTransform.Microsoft.gradient(startcolorstr=#7fD9534F, endcolorstr=#7fD9534F);
}

/*滑动验证码*/
.verify-bar-area {
    position: relative;
    background: var(--el-bg-color);
    text-align: center;
    -webkit-box-sizing: content-box;
    -moz-box-sizing: content-box;
    box-sizing: content-box;
    border: 1px solid var(--el-border-color);
    -webkit-border-radius: 4px;
}

.verify-bar-area .verify-move-block {
    position: absolute;
    top: 0;
    left: 0;
    background: var(--el-bg-color);
    cursor: pointer;
    -webkit-box-sizing: content-box;
    -moz-box-sizing: content-box;
    box-sizing: content-box;
}

.verify-bar-area .verify-move-block:hover {
    background-color: var(--el-color-primary) !important;
    color: var(--el-color-white);
}

.verify-bar-area .verify-left-bar {
    position: absolute;
    top: -1px;
    left: -1px;
    background: var(--el-bg-color);
    cursor: pointer;
    -webkit-box-sizing: content-box;
    -moz-box-sizing: content-box;
    box-sizing: content-box;
    border: 1px solid gainsboro;
}

.verify-img-panel {
    margin: 0;
    -webkit-box-sizing: content-box;
    -moz-box-sizing: content-box;
    box-sizing: content-box;
    border-radius: 3px;
    position: relative;
}

.verify-img-panel .verify-refresh {
    color: var(--el-color-white);
    width: 2rem;
    height: 2rem;
    text-align: center;
    padding: 0.4rem;
    cursor: pointer;
    position: absolute;
    top: 0;
    right: 0;
    z-index: 2;
}

.verify-img-panel .icon-refresh {
    font-size: 1.5rem;
    color: var(--el-color-white);
}

.verify-img-panel .verify-gap {
    background-color: var(--el-color-white);
    position: relative;
    z-index: 2;
    border: 1px solid var(--el-color-white);
}

.verify-bar-area .verify-move-block .verify-sub-block {
    position: absolute;
    text-align: center;
    z-index: 3;
}

.verify-bar-area .verify-move-block .verify-icon {
    font-size: 1.4rem;
}

.verify-bar-area .verify-msg {
    z-index: 3;
}
</style>