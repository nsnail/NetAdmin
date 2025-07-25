<template>
    <div class="admin-ui-tags">
        <ul ref="tags">
            <li
                v-bind:key="tag"
                v-for="tag in tagList"
                :class="[isActive(tag) ? 'active' : '', tag.meta.affix ? 'affix' : '']"
                @contextmenu.prevent="openContextMenu($event, tag)">
                <router-link :to="tag">
                    <span>{{ $t(tag.meta.title) }}</span>
                    <el-icon v-if="!tag.meta.affix" @click.prevent.stop="closeSelectedTag(tag)">
                        <el-icon-close />
                    </el-icon>
                </router-link>
            </li>
        </ul>
    </div>

    <transition name="el-zoom-in-top">
        <ul v-if="contextMenuVisible" :style="{ left: left + 'px', top: top + 'px' }" class="contextmenu" id="contextmenu">
            <li @click="refreshTab()">
                <el-icon>
                    <el-icon-refresh />
                </el-icon>
                {{ $t('刷新') }}
            </li>
            <li @click="scheduledRefresh()">
                <el-icon>
                    <el-icon-alarm-clock />
                </el-icon>
                {{ $t('定时刷新') }}
            </li>
            <hr />
            <li :class="contextMenuItem.meta.affix ? 'disabled' : ''" @click="closeTabs()">
                <el-icon>
                    <el-icon-close />
                </el-icon>
                {{ $t('关闭标签') }}
            </li>
            <li @click="closeOtherTabs()">
                <el-icon>
                    <el-icon-folder-delete />
                </el-icon>
                {{ $t('关闭其他标签') }}
            </li>
            <hr />
            <li @click="maximize()">
                <el-icon>
                    <el-icon-full-screen />
                </el-icon>
                {{ $t('最大化') }}
            </li>
            <li @click="openWindow()">
                <el-icon>
                    <el-icon-copy-document />
                </el-icon>
                {{ $t('在新的窗口中打开') }}
            </li>
        </ul>
    </transition>
</template>

<script>
import sortable from 'sortablejs'

export default {
    name: 'tags',
    data() {
        return {
            refreshTimer: null,
            contextMenuVisible: false,
            contextMenuItem: null,
            left: 0,
            top: 0,
            tagList: this.$store.state['view-tags']['view-tags'],
            tipDisplayed: false,
        }
    },
    props: {
        vue: { type: Object },
    },
    watch: {
        $route(e) {
            this.addViewTags(e)
            //判断标签容器是否出现滚动条
            this.$nextTick(() => {
                const tags = this.$refs.tags
                if (tags && tags.scrollWidth > tags.clientWidth) {
                    //确保当前标签在可视范围内
                    let targetTag = tags.querySelector('.active')
                    targetTag.scrollIntoView()
                    //显示提示
                    if (!this.tipDisplayed) {
                        this.$msgbox({
                            type: 'warning',
                            center: true,
                            title: '提示',
                            message: '当前标签数量过多，可通过鼠标滚轴滚动标签栏。关闭标签数量可减少系统性能消耗。',
                            confirmButtonText: '知道了',
                        })
                        this.tipDisplayed = true
                    }
                }
            })
        },
        contextMenuVisible(value) {
            const cm = (e) => {
                const sp = document.getElementById('contextmenu')
                if (sp && !sp.contains(e.target)) {
                    this.closeMenu()
                }
            }
            if (value) {
                document.body.addEventListener('click', (e) => cm(e))
            } else {
                document.body.removeEventListener('click', (e) => cm(e))
            }
        },
    },
    created() {
        const menu = this.$router.sc_getMenu()
        const dashboardRoute = this.treeFind(menu, (node) => node.path === this.$CONFIG.DASHBOARD_URL)
        if (dashboardRoute) {
            dashboardRoute.fullPath = dashboardRoute.path
            this.addViewTags(dashboardRoute)
            this.addViewTags(this.$route)
        }
    },
    mounted() {
        this.tagDrop()
        this.scrollInit()
    },
    methods: {
        //查找树
        treeFind(tree, func) {
            for (const data of tree) {
                if (func(data)) return data
                if (data.children) {
                    const res = this.treeFind(data.children, func)
                    if (res) return res
                }
            }
            return null
        },
        //标签拖拽排序
        tagDrop() {
            const target = this.$refs.tags
            sortable.create(target, {
                draggable: 'li',
                animation: 300,
            })
        },
        //增加tag
        addViewTags(route) {
            if (route.name && !route.meta.fullpage) {
                this.$store.commit('pushViewTags', route)
                this.$store.commit('pushKeepLive', route.name)
            }
        },
        //高亮tag
        isActive(route) {
            return route.fullPath === this.$route.fullPath
        },
        //关闭tag
        closeSelectedTag(tag, autoPushLatestView = true) {
            const nowTagIndex = this.tagList.findIndex((item) => item.fullPath === tag.fullPath)
            this.$store.commit('removeViewTags', tag)
            this.$store.commit('removeIframeList', tag)
            this.$store.commit('removeKeepLive', tag.name)
            if (autoPushLatestView && this.isActive(tag)) {
                const leftView = this.tagList[nowTagIndex - 1]
                if (leftView) {
                    this.$router.push(leftView)
                } else {
                    this.$router.push('/')
                }
            }
        },
        //tag右键
        openContextMenu(e, tag) {
            this.contextMenuItem = tag
            this.contextMenuVisible = true
            this.left = e.clientX + 1
            this.top = e.clientY + 1

            //FIX 右键菜单边缘化位置处理
            this.$nextTick(() => {
                let sp = document.getElementById('contextmenu')
                if (document.body.offsetWidth - e.clientX < sp.offsetWidth) {
                    this.left = document.body.offsetWidth - sp.offsetWidth + 1
                    this.top = e.clientY + 1
                }
            })
        },
        //关闭右键菜单
        closeMenu() {
            this.contextMenuItem = null
            this.contextMenuVisible = false
        },
        async scheduledRefresh() {
            this.closeMenu()
            try {
                const sleep = await this.$prompt(this.$t('刷新时间间隔（秒）'), this.$t('定时刷新'), {
                    inputPattern: /^[1-9]\d*$/,
                    inputErrorMessage: this.$t('时间必须为数字'),
                    inputValue: '10',
                })
                const sleepSecs = parseInt(sleep.value)
                this.$message({
                    showClose: true,
                    onClose: () => clearInterval(this.refreshTimer),
                    type: 'warning',
                    customClass: 'refreshTimer',
                    message: this.$t('{s} 秒后刷新...', { s: sleepSecs }),
                    duration: 0,
                })
                this.refreshTimer = setInterval(() => {
                    const el = document.getElementsByClassName('refreshTimer')[0].getElementsByClassName('el-message__content')[0]
                    let num = parseInt(/(\d+)/.exec(el.innerHTML)[0])
                    if (num === 1) {
                        this.vue.routerViewKey = Math.random()
                        num = sleepSecs + 1
                    }
                    el.innerHTML = el.innerHTML.replace(/\d+/, (num - 1).toString())
                }, 1000)
            } catch {}
        },
        //TAB 刷新
        refreshTab() {
            this.closeMenu()
            this.vue.routerViewKey = Math.random()
        },
        //TAB 关闭
        closeTabs() {
            const nowTag = this.contextMenuItem
            if (!nowTag.meta.affix) {
                this.closeSelectedTag(nowTag)
                this.contextMenuVisible = false
            }
        },
        //TAB 关闭其他
        closeOtherTabs() {
            const nowTag = this.contextMenuItem
            //判断是否当前路由，否的话跳转
            if (this.$route.fullPath !== nowTag.fullPath) {
                this.$router.push({
                    path: nowTag.fullPath,
                    query: nowTag.query,
                })
            }
            const tags = [...this.tagList]
            tags.forEach((tag) => {
                if ((tag.meta && tag.meta.affix) || nowTag.fullPath === tag.fullPath) {
                    return true
                } else {
                    this.closeSelectedTag(tag, false)
                }
            })
            this.contextMenuVisible = false
        },
        //TAB 最大化
        maximize() {
            const nowTag = this.contextMenuItem
            this.contextMenuVisible = false
            //判断是否当前路由，否的话跳转
            if (this.$route.fullPath !== nowTag.fullPath) {
                this.$router.push({
                    path: nowTag.fullPath,
                    query: nowTag.query,
                })
            }
            document.getElementById('app').classList.add('main-maximize')
        },
        //新窗口打开
        openWindow() {
            const nowTag = this.contextMenuItem
            const url = nowTag.href || '/'
            if (!nowTag.meta.affix) {
                this.closeSelectedTag(nowTag)
            }
            window.open(url)
            this.contextMenuVisible = false
        },
        //横向滚动
        scrollInit() {
            const scrollDiv = this.$refs.tags
            scrollDiv.addEventListener('mousewheel', handler, false) || scrollDiv.addEventListener('DOMMouseScroll', handler, false)

            function handler(event) {
                const detail = event.wheelDelta || event.detail
                //火狐上滚键值-3 下滚键值3，其他内核上滚键值120 下滚键值-120
                const moveForwardStep = 1
                const moveBackStep = -1
                let step
                if (detail === 3 || (detail < 0 && detail !== -3)) {
                    step = moveForwardStep * 50
                } else {
                    step = moveBackStep * 50
                }
                scrollDiv.scrollLeft += step
            }
        },
    },
}
</script>

<style>
.contextmenu {
    position: fixed;
    width: 15rem;
    margin: 0;
    border-radius: 0;
    background: var(--el-bg-color-overlay);
    border: 1px solid var(--el-border-color-light);
    box-shadow: 0 0.2rem 1rem 0 rgba(0, 0, 0, 0.1);
    z-index: 3000;
    list-style-type: none;
    padding: 1rem 0;
}

.contextmenu hr {
    margin: 0.4rem 0;
    border: none;
    height: 1px;
    font-size: 0;
    background-color: var(--el-border-color-light);
}

.contextmenu li {
    display: flex;
    align-items: center;
    margin: 0;
    cursor: pointer;
    line-height: 2.5rem;
    padding: 0 1.3rem;
    color: var(--el-text-color-primary);
}

.contextmenu li i {
    font-size: 1.1rem;
    margin-right: 1rem;
}

.contextmenu li.disabled {
    cursor: not-allowed;
    color: var(--el-text-color-disabled);
    background: transparent;
}

.dark .contextmenu li {
    color: var(--el-text-color-primary);
}
</style>