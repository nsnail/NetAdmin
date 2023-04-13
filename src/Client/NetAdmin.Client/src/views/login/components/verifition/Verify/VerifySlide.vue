<template>
    <div style="position: relative">
        <div
            v-if="type === '2'"
            :style="{ height: parseInt(setSize.imgHeight) + vSpace + 'px' }"
            class="verify-img-out"
        >
            <div
                :style="{ width: setSize.imgWidth, height: setSize.imgHeight }"
                class="verify-img-panel"
            >
                <img
                    :src="backImgBase"
                    alt=""
                    style="width: 100%; height: 100%; display: block"
                />
                <div
                    v-show="showRefresh"
                    class="verify-refresh"
                    @click="refresh"
                >
                    <i class="iconfont icon-refresh"></i>
                </div>
                <transition name="tips">
                    <span
                        v-if="tipWords"
                        :class="passFlag ? 'suc-bg' : 'err-bg'"
                        class="verify-tips"
                        >{{ tipWords }}</span
                    >
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
            class="verify-bar-area"
        >
            <span class="verify-msg" v-text="text"></span>
            <div
                :style="{
                    width:
                        leftBarWidth !== undefined
                            ? leftBarWidth
                            : barSize.height,
                    height: barSize.height,
                    'border-color': leftBarBorderColor,
                    transaction: transitionWidth,
                }"
                class="verify-left-bar"
            >
                <span class="verify-msg" v-text="finishText"></span>
                <div
                    :style="{
                        width: barSize.height,
                        height: barSize.height,
                        'background-color': moveBlockBackgroundColor,
                        left: moveBlockLeft,
                        transition: transitionLeft,
                    }"
                    class="verify-move-block"
                    @mousedown="start"
                    @touchstart="start"
                >
                    <i
                        :class="['verify-icon iconfont', iconClass]"
                        :style="{ color: iconColor }"
                    ></i>
                    <div
                        v-if="type === '2'"
                        :style="{
                            width:
                                Math.floor(
                                    (parseInt(setSize.imgWidth) * 47) / 310
                                ) + 'px',
                            height: setSize.imgHeight,
                            top:
                                '-' +
                                (parseInt(setSize.imgHeight) + vSpace) +
                                'px',
                            'background-size':
                                setSize.imgWidth + ' ' + setSize.imgHeight,
                        }"
                        class="verify-sub-block"
                    >
                        <img
                            :src="blockBackImgBase"
                            alt=""
                            style="
                                width: 100%;
                                height: 100%;
                                display: block;
                                -webkit-user-drag: none;
                            "
                        />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script type="text/babel">
/* eslint-disable */
/**
 * VerifySlide
 * @description 滑块
 * */
import { aesEncrypt } from "./../utils/ase";
import { resetSize } from "./../utils/util";
import {
    computed,
    getCurrentInstance,
    nextTick,
    onMounted,
    reactive,
    ref,
    toRefs,
    watch,
} from "vue";
import captcha from "@/api/sys/captcha";
//  "captchaType":"blockPuzzle",
export default {
    name: "VerifySlide",
    props: {
        captchaType: {
            type: String,
        },
        type: {
            type: String,
            default: "1",
        },
        //弹出式pop，固定fixed
        mode: {
            type: String,
            default: "fixed",
        },
        vSpace: {
            type: Number,
            default: 5,
        },
        explain: {
            type: String,
            default: "向右滑动完成验证",
        },
        imgSize: {
            type: Object,
            default() {
                return {
                    width: "310px",
                    height: "155px",
                };
            },
        },
        blockSize: {
            type: Object,
            default() {
                return {
                    width: "50px",
                    height: "50px",
                };
            },
        },
        barSize: {
            type: Object,
            default() {
                return {
                    width: "310px",
                    height: "40px",
                };
            },
        },
    },
    setup(props, context) {
        const {
            mode,
            captchaType,
            vSpace,
            imgSize,
            barSize,
            type,
            blockSize,
            explain,
        } = toRefs(props);
        const { proxy } = getCurrentInstance();
        let secretKey = ref(""), //后端返回的ase加密秘钥
            passFlag = ref(""), //是否通过的标识
            backImgBase = ref(
                "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7"
            ), //验证码背景图片
            blockBackImgBase = ref(
                "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7"
            ), //验证滑块的背景图片
            backToken = ref(""), //后端返回的唯一token值
            startMoveTime = ref(""), //移动开始的时间
            endMovetime = ref(""), //移动结束的时间
            tipsBackColor = ref(""), //提示词的背景颜色
            tipWords = ref(""),
            text = ref(""),
            finishText = ref(""),
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
            leftBarBorderColor = ref("#ddd"),
            iconColor = ref(undefined),
            iconClass = ref("icon-right"),
            status = ref(false), //鼠标状态
            isEnd = ref(false), //是够验证完成
            showRefresh = ref(true),
            transitionLeft = ref(""),
            transitionWidth = ref(""),
            startLeft = ref(0);
        const barArea = computed(() => {
            return proxy.$el.querySelector(".verify-bar-area");
        });

        function init() {
            text.value = explain.value;
            getPictrue();
            nextTick(() => {
                let { imgHeight, imgWidth, barHeight, barWidth } =
                    resetSize(proxy);
                setSize.imgHeight = imgHeight;
                setSize.imgWidth = imgWidth;
                setSize.barHeight = barHeight;
                setSize.barWidth = barWidth;
                proxy.$parent.$emit("ready", proxy);
            });
            window.removeEventListener("touchmove", function (e) {
                move(e);
            });
            window.removeEventListener("mousemove", function (e) {
                move(e);
            });
            //鼠标松开
            window.removeEventListener("touchend", function () {
                end();
            });
            window.removeEventListener("mouseup", function () {
                end();
            });
            window.addEventListener("touchmove", function (e) {
                move(e);
            });
            window.addEventListener("mousemove", function (e) {
                move(e);
            });
            //鼠标松开
            window.addEventListener("touchend", function () {
                end();
            });
            window.addEventListener("mouseup", function () {
                end();
            });
        }

        watch(type, () => {
            init();
        });
        onMounted(() => {
            // 禁止拖拽
            init();
            proxy.$el.onselectstart = function () {
                return false;
            };
        });

        //鼠标按下
        function start(e) {
            let x;
            e = e || window.event;
            if (!e.touches) {
                //兼容PC端
                x = e.clientX;
            } else {
                //兼容移动端
                x = e.touches[0].pageX;
            }
            console.log(barArea);
            startLeft.value = Math.floor(
                x - barArea.value.getBoundingClientRect().left
            );
            startMoveTime.value = +new Date(); //开始滑动的时间
            if (isEnd.value === false) {
                text.value = "";
                moveBlockBackgroundColor.value = "#337ab7";
                leftBarBorderColor.value = "#337AB7";
                iconColor.value = "#fff";
                e.stopPropagation();
                status.value = true;
            }
        }

        //鼠标移动
        function move(e) {
            let x;
            e = e || window.event;
            if (status.value && isEnd.value === false) {
                if (!e.touches) {
                    //兼容PC端
                    x = e.clientX;
                } else {
                    //兼容移动端
                    x = e.touches[0].pageX;
                }
                const bar_area_left =
                    barArea.value.getBoundingClientRect().left;
                let move_block_left = x - bar_area_left; //小方块相对于父元素的left值
                if (
                    move_block_left >=
                    barArea.value.offsetWidth -
                        parseInt(parseInt(blockSize.value.width) / 2) -
                        2
                ) {
                    move_block_left =
                        barArea.value.offsetWidth -
                        parseInt(parseInt(blockSize.value.width) / 2) -
                        2;
                }
                if (move_block_left <= 0) {
                    move_block_left = parseInt(
                        parseInt(blockSize.value.width) / 2
                    );
                }
                //拖动后小方块的left值
                moveBlockLeft.value = move_block_left - startLeft.value + "px";
                leftBarWidth.value = move_block_left - startLeft.value + "px";
            }
        }

        //鼠标松开
        function end() {
            endMovetime.value = +new Date();
            //判断是否重合
            if (status.value && isEnd.value === false) {
                let moveLeftDistance = parseInt(
                    (moveBlockLeft.value || "").replace("px", "")
                );
                moveLeftDistance =
                    (moveLeftDistance * 310) / parseInt(setSize.imgWidth);
                let data = {
                    verifyData: secretKey.value
                        ? aesEncrypt(moveLeftDistance, secretKey.value)
                        : moveLeftDistance,
                    id: backToken.value,
                };
                captcha.verifyCaptcha.post(data).then((res) => {
                    if (res.data) {
                        moveBlockBackgroundColor.value = "#5cb85c";
                        leftBarBorderColor.value = "#5cb85c";
                        iconColor.value = "#fff";
                        iconClass.value = "icon-check";
                        showRefresh.value = false;
                        isEnd.value = true;
                        if (mode.value === "pop") {
                            setTimeout(() => {
                                proxy.$parent.clickShow = false;
                                refresh();
                            }, 1500);
                        }
                        passFlag.value = true;
                        tipWords.value = `${(
                            (endMovetime.value - startMoveTime.value) /
                            1000
                        ).toFixed(2)}秒 验证成功`;
                        setTimeout(() => {
                            tipWords.value = "";
                            proxy.$parent.closeBox();
                            proxy.$parent.$emit("success", data);
                        }, 1000);
                    } else {
                        moveBlockBackgroundColor.value = "#d9534f";
                        leftBarBorderColor.value = "#d9534f";
                        iconColor.value = "#fff";
                        iconClass.value = "icon-close";
                        passFlag.value = false;
                        setTimeout(function () {
                            refresh();
                        }, 1000);
                        proxy.$parent.$emit("error", proxy);
                        tipWords.value = "验证失败";
                        setTimeout(() => {
                            tipWords.value = "";
                        }, 1000);
                    }
                });
                status.value = false;
            }
        }

        const refresh = () => {
            showRefresh.value = true;
            finishText.value = "";
            transitionLeft.value = "left .3s";
            moveBlockLeft.value = 0;
            leftBarWidth.value = undefined;
            transitionWidth.value = "width .3s";
            leftBarBorderColor.value = "#ddd";
            moveBlockBackgroundColor.value = "#fff";
            iconColor.value = "#000";
            iconClass.value = "icon-right";
            isEnd.value = false;
            getPictrue();
            setTimeout(() => {
                transitionWidth.value = "";
                transitionLeft.value = "";
                text.value = explain.value;
            }, 300);
        };

        // 请求背景图片和验证图片
        async function getPictrue() {
            let data = {
                captchaType: captchaType.value,
            };
            let res;
            try {
                res = await captcha.getCaptchaImage.post(data);
            } catch {
                tipWords.value = "发生错误";
            }
            backImgBase.value = res.data.backgroundImage;
            blockBackImgBase.value = res.data.sliderImage;
            backToken.value = res.data.id;
            secretKey.value = aesEncrypt(res.data.id);
            if (secretKey.value.length > 32)
                secretKey.value = secretKey.value.substring(0, 32);
            proxy.$parent.$emit("apiReady", res.data);
        }

        return {
            secretKey, //后端返回的ase加密秘钥
            passFlag, //是否通过的标识
            backImgBase, //验证码背景图片
            blockBackImgBase, //验证滑块的背景图片
            backToken, //后端返回的唯一token值
            startMoveTime, //移动开始的时间
            endMovetime, //移动结束的时间
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
        };
    },
};
</script>