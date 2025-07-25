<template>
    <div style="position: relative">
        <div v-if="type === '2'" :style="{ height: parseInt(setSize.imgHeight) + vSpace + 'px' }" class="verify-img-out">
            <div :style="{ width: setSize.imgWidth, height: setSize.imgHeight }" class="verify-img-panel">
                <img :src="backImgBase" alt="" style="width: 100%; height: 100%; display: block" />
                <div v-show="showRefresh" @click="refresh" class="verify-refresh">
                    <el-icon>
                        <el-icon-refresh />
                    </el-icon>
                </div>
                <transition name="tips">
                    <span v-if="tipWords" :class="passFlag ? 'suc-bg' : 'err-bg'" class="verify-tips">{{ tipWords }}</span>
                </transition>
            </div>
        </div>
        <!-- 公共部分 -->
        <div
            :style="{
                width: setSize.imgWidth,
                height: barSize.height,
                'line-height': barSize.height,
            }"
            class="verify-bar-area">
            <span v-text="text" class="verify-msg" />
            <div
                :style="{
                    width: leftBarWidth !== undefined ? leftBarWidth : barSize.height,
                    height: barSize.height,
                    'border-color': leftBarBorderColor,
                    transaction: transitionWidth,
                }"
                class="verify-left-bar">
                <span v-text="finishText" class="verify-msg" />
                <div
                    :style="{
                        width: barSize.height,
                        height: barSize.height,
                        'background-color': moveBlockBackgroundColor,
                        left: moveBlockLeft,
                        transition: transitionLeft,
                    }"
                    @mousedown="start"
                    @touchstart="start"
                    class="verify-move-block">
                    <el-icon>
                        <el-icon-arrow-right />
                    </el-icon>
                    <div
                        v-if="type === '2'"
                        :style="{
                            width: Math.floor((parseInt(setSize.imgWidth) * 47) / 310) + 'px',
                            height: setSize.imgHeight,
                            top: '-' + (parseInt(setSize.imgHeight) + vSpace) + 'px',
                            'background-size': setSize.imgWidth + ' ' + setSize.imgHeight,
                        }"
                        class="verify-sub-block">
                        <img :src="blockBackImgBase" alt="" style="width: 100%; height: 100%; display: block; -webkit-user-drag: none" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script type="text/babel">
/**
 * VerifySlide
 * @description 滑块
 * */
import { computed, getCurrentInstance, nextTick, onMounted, reactive, ref, toRefs, watch } from 'vue'
import captcha from '@/api/sys/captcha'
import tool from '@/utils/tool'
//  "captchaType":"blockPuzzle",
export default {
    props: {
        captchaType: {
            type: String,
        },
        tpl: {
            type: String,
        },
        type: {
            type: String,
            default: '1',
        },
        //弹出式pop，固定fixed
        mode: {
            type: String,
            default: 'fixed',
        },
        vSpace: {
            type: Number,
            default: 5,
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
            default() {
                return {
                    width: '50px',
                    height: '50px',
                }
            },
        },
        barSize: {
            type: Object,
            default() {
                return {
                    width: '310px',
                    height: '40px',
                }
            },
        },
    },
    setup(props, context) {
        const { mode, captchaType, vSpace, imgSize, barSize, type, blockSize, explain } = toRefs(props)
        const { proxy } = getCurrentInstance()
        let secretKey = ref(tool.crypto.generateDerivedKey(proxy.tpl)), //后端返回的ase加密秘钥
            passFlag = ref(''), //是否通过的标识
            backImgBase = ref('data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7'), //验证码背景图片
            blockBackImgBase = ref('data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7'), //验证滑块的背景图片
            backToken = ref(''), //后端返回的唯一token值
            startMoveTime = ref(''), //移动开始的时间
            endMoveTime = ref(''), //移动结束的时间
            tipsBackColor = ref(''), //提示词的背景颜色
            tipWords = ref(''),
            text = ref(''),
            finishText = ref(''),
            setSize = reactive({
                imgHeight: 0,
                imgWidth: 0,
                barHeight: 0,
                barWidth: 0,
            }),
            top = ref(0),
            left = ref(0),
            moveBlockLeft = ref(undefined),
            leftBarWidth = ref(undefined),
            // 移动中样式
            moveBlockBackgroundColor = ref(undefined),
            leftBarBorderColor = ref('gainsboro'),
            iconColor = ref(undefined),
            iconClass = ref('icon-right'),
            status = ref(false), //鼠标状态
            isEnd = ref(false), //是够验证完成
            showRefresh = ref(true),
            transitionLeft = ref(''),
            transitionWidth = ref(''),
            startLeft = ref(0)
        const barArea = computed(() => {
            return proxy.$el.querySelector('.verify-bar-area')
        })

        function resetSize(vm) {
            let img_width, img_height, bar_width, bar_height //图片的宽度、高度，移动条的宽度、高度

            const parentWidth = vm.$el.parentNode.offsetWidth || window.offsetWidth
            const parentHeight = vm.$el.parentNode.offsetHeight || window.offsetHeight
            if (vm.imgSize.width.indexOf('%') !== -1) {
                img_width = (parseInt(vm.imgSize.width) / 100) * parentWidth + 'px'
            } else {
                img_width = vm.imgSize.width
            }

            if (vm.imgSize.height.indexOf('%') !== -1) {
                img_height = (parseInt(vm.imgSize.height) / 100) * parentHeight + 'px'
            } else {
                img_height = vm.imgSize.height
            }

            if (vm.barSize.width.indexOf('%') !== -1) {
                bar_width = (parseInt(vm.barSize.width) / 100) * parentWidth + 'px'
            } else {
                bar_width = vm.barSize.width
            }

            if (vm.barSize.height.indexOf('%') !== -1) {
                bar_height = (parseInt(vm.barSize.height) / 100) * parentHeight + 'px'
            } else {
                bar_height = vm.barSize.height
            }

            return {
                imgWidth: img_width,
                imgHeight: img_height,
                barWidth: bar_width,
                barHeight: bar_height,
            }
        }

        function init() {
            text.value = explain.value
            getPicture()
            nextTick(() => {
                let { imgHeight, imgWidth, barHeight, barWidth } = resetSize(proxy)
                setSize.imgHeight = imgHeight
                setSize.imgWidth = imgWidth
                setSize.barHeight = barHeight
                setSize.barWidth = barWidth
                proxy.$parent.$emit('ready', proxy)
            })
            window.removeEventListener('touchmove', function (e) {
                move(e)
            })
            window.removeEventListener('mousemove', function (e) {
                move(e)
            })
            //鼠标松开
            window.removeEventListener('touchend', function () {
                end()
            })
            window.removeEventListener('mouseup', function () {
                end()
            })
            window.addEventListener('touchmove', function (e) {
                move(e)
            })
            window.addEventListener('mousemove', function (e) {
                move(e)
            })
            //鼠标松开
            window.addEventListener('touchend', function () {
                end()
            })
            window.addEventListener('mouseup', function () {
                end()
            })
        }

        watch(type, () => {
            init()
        })
        onMounted(() => {
            // 禁止拖拽
            init()
            proxy.$el.onselectstart = function () {
                return false
            }
        })

        //鼠标按下
        function start(e) {
            let x
            e = e || window.event
            if (!e.touches) {
                //兼容PC端
                x = e.clientX
            } else {
                //兼容移动端
                x = e.touches[0].pageX
            }
            startLeft.value = Math.floor(x - barArea.value.getBoundingClientRect().left)
            startMoveTime.value = new Date() //开始滑动的时间
            if (isEnd.value === false) {
                text.value = ''
                moveBlockBackgroundColor.value = 'var(--el-color-primary)'
                leftBarBorderColor.value = 'var(--el-color-primary)'
                iconColor.value = 'white'
                e.stopPropagation()
                status.value = true
            }
        }

        //鼠标移动
        function move(e) {
            let x
            e = e || window.event
            if (status.value && isEnd.value === false) {
                if (!e.touches) {
                    //兼容PC端
                    x = e.clientX
                } else {
                    //兼容移动端
                    x = e.touches[0].pageX
                }
                const bar_area_left = barArea.value.getBoundingClientRect().left
                let move_block_left = x - bar_area_left //小方块相对于父元素的left值
                if (move_block_left >= barArea.value.offsetWidth - parseInt(parseInt(blockSize.value.width) / 2) - 2) {
                    move_block_left = barArea.value.offsetWidth - parseInt(parseInt(blockSize.value.width) / 2) - 2
                }
                if (move_block_left <= 0) {
                    move_block_left = parseInt(parseInt(blockSize.value.width) / 2)
                }
                //拖动后小方块的left值
                moveBlockLeft.value = move_block_left - startLeft.value + 'px'
                leftBarWidth.value = move_block_left - startLeft.value + 'px'
            }
        }

        //鼠标松开
        function end() {
            endMoveTime.value = new Date()
            //判断是否重合
            if (status.value && isEnd.value === false) {
                let moveLeftDistance = parseInt((moveBlockLeft.value || '').replace('px', ''))
                moveLeftDistance = (moveLeftDistance * 310) / parseInt(setSize.imgWidth)
                let data = {
                    verifyData: secretKey.value ? tool.crypto.AES.encrypt(moveLeftDistance.toString(), secretKey.value) : moveLeftDistance,
                    id: backToken.value,
                }
                captcha.verifyCaptcha.post(data).then((res) => {
                    if (res.data) {
                        moveBlockBackgroundColor.value = '#5cb85c'
                        leftBarBorderColor.value = '#5cb85c'
                        iconColor.value = 'white'
                        iconClass.value = 'icon-check'
                        showRefresh.value = false
                        isEnd.value = true
                        if (mode.value === 'pop') {
                            setTimeout(() => {
                                proxy.$parent.clickShow = false
                                refresh()
                            }, 1500)
                        }
                        passFlag.value = true
                        tipWords.value = proxy.$t('{secs} 秒 验证成功', { secs: ((endMoveTime.value - startMoveTime.value) / 1000).toFixed(2) })
                        setTimeout(() => {
                            tipWords.value = ''
                            proxy.$parent.closeBox()
                            proxy.$parent.$emit('success', data)
                        }, 1000)
                    } else {
                        moveBlockBackgroundColor.value = '#d9534f'
                        leftBarBorderColor.value = '#d9534f'
                        iconColor.value = 'white'
                        iconClass.value = 'icon-close'
                        passFlag.value = false
                        setTimeout(function () {
                            refresh()
                        }, 1000)
                        proxy.$parent.$emit('error', proxy)
                        tipWords.value = proxy.$t('验证失败')
                        setTimeout(() => {
                            tipWords.value = ''
                        }, 1000)
                    }
                })
                status.value = false
            }
        }

        const refresh = () => {
            showRefresh.value = true
            finishText.value = ''
            transitionLeft.value = 'left .3s'
            moveBlockLeft.value = 0
            leftBarWidth.value = undefined
            transitionWidth.value = 'width .3s'
            leftBarBorderColor.value = 'gainsboro'
            moveBlockBackgroundColor.value = 'white'
            iconColor.value = 'black'
            iconClass.value = 'icon-right'
            isEnd.value = false
            getPicture()
            setTimeout(() => {
                transitionWidth.value = ''
                transitionLeft.value = ''
                text.value = explain.value
            }, 300)
        }

        // 请求背景图片和验证图片
        async function getPicture() {
            let data = {
                captchaType: captchaType.value,
            }
            let res
            try {
                res = await captcha.getCaptchaImage.post(data)
            } catch {
                tipWords.value = '发生错误'
            }
            backImgBase.value = res.data.backgroundImage
            blockBackImgBase.value = res.data.sliderImage
            backToken.value = res.data.id
            secretKey.value = tool.crypto.AES.encrypt(res.data.id, tool.crypto.generateDerivedKey(proxy.tpl))
            if (secretKey.value.length > 32) secretKey.value = secretKey.value.substring(0, 32)
            proxy.$parent.$emit('apiReady', res.data)
        }

        return {
            secretKey, //后端返回的ase加密秘钥
            passFlag, //是否通过的标识
            backImgBase, //验证码背景图片
            blockBackImgBase, //验证滑块的背景图片
            backToken, //后端返回的唯一token值
            startMoveTime, //移动开始的时间
            endMoveTime, //移动结束的时间
            tipsBackColor, //提示词的背景颜色
            tipWords,
            text,
            finishText,
            setSize,
            top,
            left,
            moveBlockLeft,
            leftBarWidth,
            // 移动中样式
            moveBlockBackgroundColor,
            leftBarBorderColor,
            iconColor,
            iconClass,
            status, //鼠标状态
            isEnd, //是够验证完成
            showRefresh,
            transitionLeft,
            transitionWidth,
            barArea,
            refresh,
            start,
        }
    },
}
</script>