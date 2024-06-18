<template></template>

<script>
import { h } from 'vue'

export default {
    data() {
        return {}
    },
    async created() {
        setInterval(async () => {
            // 检查版本
            const res = await this.$API.sys_tools.getVersion.post({})
            if (res.data !== this.$TOOL.data.get('APP_VERSION')) {
                this.$TOOL.data.set('APP_VERSION', res.data)
                this.showTip(res.data.slice(0, res.data.indexOf('+')))
            }
        }, 10000)
    },
    methods: {
        /**
         * 通知消息
         */
        showTip(version) {
            const contents = []
            const msg = h('p', { style: 'width:230px;display:flex;justify-content:space-between' }, [
                h('span', {}, this.$t('即将开始更新……')),
                h('a', { style: 'color:#409eff', href: 'javascript:window.location.reload()' }, this.$t('立即更新')),
            ])
            const task = h('p', { style: 'font-weight:bold' }, version)
            const progress = h(
                'div',
                {
                    class: {
                        'el-progress-plus': true,
                    },
                    style: {
                        width: '230px',
                        height: '6px',
                        'background-color': '#409eff',
                        'margin-top': '6px',
                        'border-radius': '6px',
                    },
                    percentage: 100,
                },
                '',
            )
            const br = h('p', { style: 'width: auto; height: 10px' }, '')
            contents.push(task)
            contents.push(msg)
            contents.push(progress)
            contents.push(br)

            let classID = 1
            let className = 'el-notification-plus' + classID
            // 判断是否已存在该通知元素，以及最多限制生成100个通知
            while (document.getElementsByClassName(className)[0]) {
                if (classID > 100) {
                    // 无法生成元素

                    break
                } else {
                    // 继续累加
                    classID++
                    className = 'el-notification-plus' + classID
                }
            }

            // 实例化通知
            const notifyInstance = this.$notify({
                title: this.$t('发现新版本'),
                type: 'success',
                customClass: className,
                message: h('div', {}, contents),
                duration: 0,
                onClick: () => {},
            })

            // 启动倒计时
            let timer = this.countDown(className, notifyInstance)

            // 获取 Notification 的DOM元素
            const ElNotificationPlus = document.getElementsByClassName(className)[0]

            // 为 Notification 元素 定义鼠标进入方法，暂停倒计时
            ElNotificationPlus.onmouseenter = () => {
                clearInterval(timer)
            }

            // 为 Notification 元素 定义鼠标移出方法，继续倒计时
            ElNotificationPlus.onmouseleave = () => {
                clearInterval(timer)
                timer = this.countDown(className, notifyInstance)
            }
        },

        /**
         * 倒计时
         */
        countDown(className, notifyInstance) {
            const timer = setInterval(() => {
                try {
                    if (notifyInstance) {
                        const ElNotificationPlus = document.getElementsByClassName(className)[0]

                        const ElProgressPlus = ElNotificationPlus.getElementsByClassName('el-progress-plus')[0]

                        let percentage = ElProgressPlus.getAttribute('percentage')
                        if (percentage >= 0) {
                            percentage = percentage - 0.5
                            ElProgressPlus.setAttribute('percentage', percentage)
                            ElProgressPlus.style.width = 230 * (percentage / 100) + 'px'
                        } else {
                            // 清除定时器
                            clearInterval(timer)
                            // 手动关闭通知
                            setTimeout(() => {
                                notifyInstance.close()
                            }, 50)
                            window.location.reload()
                        }
                    } else {
                        // 清除定时器
                        clearInterval(timer)
                    }
                } catch (error) {
                    // 清除定时器
                    clearInterval(timer)
                }
            }, 50)

            return timer
        },
    },
}
</script>

<style scoped></style>