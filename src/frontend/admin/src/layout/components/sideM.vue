<template>
    <div v-drag @click="showMobileNav($event)" class="mobile-nav-button" draggable="false">
        <el-icon>
            <el-icon-menu />
        </el-icon>
    </div>

    <el-drawer v-model="nav" :size="240" :title="$t('移动端菜单')" :with-header="false" destroy-on-close direction="ltr" ref="mobileNavBox">
        <el-container class="mobile-nav">
            <el-header>
                <div class="logo-bar">
                    <img class="logo" src="@/assets/img/logo.png" /><span>{{ $CONFIG.APP_NAME }}</span>
                </div>
            </el-header>
            <el-main>
                <el-scrollbar>
                    <el-menu
                        :default-active="$route.meta.active || $route.fullPath"
                        @select="select"
                        active-text-color="#409EFF"
                        background-color="#424c50"
                        router
                        text-color="#fff">
                        <NavMenu :navMenus="menu"></NavMenu>
                    </el-menu>
                </el-scrollbar>
            </el-main>
        </el-container>
    </el-drawer>
</template>

<script>
import NavMenu from './NavMenu.vue'

export default {
    components: {
        NavMenu,
    },
    data() {
        return {
            nav: false,
            menu: [],
        }
    },
    computed: {},
    created() {
        const menu = this.$router.sc_getMenu()
        this.menu = this.filterUrl(menu)
    },

    watch: {},
    methods: {
        showMobileNav(e) {
            const isdrag = e.currentTarget.getAttribute('drag-flag')
            if (isdrag === 'true') {
                return false
            } else {
                this.nav = true
            }
        },
        select() {
            this.$refs.mobileNavBox.handleClose()
        },
        //转换外部链接的路由
        filterUrl(map) {
            const newMap = []
            map &&
                map.forEach((item) => {
                    item.meta = item.meta ? item.meta : {}
                    //处理隐藏
                    if (item.meta.hidden || item.meta.type === 'button') {
                        return false
                    }
                    //处理http
                    if (item.meta.type === 'iframe') {
                        item.path = `/i/${item.name}`
                    }
                    //递归循环
                    if (item.children && item.children.length > 0) {
                        item.children = this.filterUrl(item.children)
                    }
                    newMap.push(item)
                })
            return newMap
        },
    },
    directives: {
        drag(el) {
            let oDiv = el //当前元素
            let firstTime = '',
                lastTime = ''
            //禁止选择网页上的文字
            // document.onselectstart = function() {
            //     return false;
            // };
            oDiv.onmousedown = function (e) {
                //鼠标按下，计算当前元素距离可视区的距离
                let disX = e.clientX - oDiv.offsetLeft
                let disY = e.clientY - oDiv.offsetTop
                document.onmousemove = function (e) {
                    oDiv.setAttribute('drag-flag', true)
                    firstTime = new Date().getTime()
                    //通过事件委托，计算移动的距离
                    let l = e.clientX - disX
                    let t = e.clientY - disY

                    //移动当前元素

                    if (t > 0 && t < document.body.clientHeight - 50) {
                        oDiv.style.top = t + 'px'
                    }
                    if (l > 0 && l < document.body.clientWidth - 50) {
                        oDiv.style.left = l + 'px'
                    }
                }
                document.onmouseup = function () {
                    lastTime = new Date().getTime()
                    if (lastTime - firstTime > 200) {
                        oDiv.setAttribute('drag-flag', false)
                    }
                    document.onmousemove = null
                    document.onmouseup = null
                }
                //return false不加的话可能导致黏连，就是拖到一个地方时div粘在鼠标上不下来，相当于onmouseup失效
                return false
            }
        },
    },
}
</script>

<style scoped>
.mobile-nav-button {
    position: fixed;
    bottom: 1rem;
    left: 1rem;
    z-index: 10;
    width: 4rem;
    height: 4rem;
    background: #409eff;
    box-shadow: 0 0.2rem 1rem 0 rgba(64, 158, 255, 1);
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
}

.mobile-nav-button i {
    color: #fff;
    font-size: 1.5rem;
}

.mobile-nav {
    background: #424c50;
}

.mobile-nav .el-header {
    background: transparent;
    border: 0;
}

.mobile-nav .el-main {
    padding: 0;
}

.mobile-nav .logo-bar {
    display: flex;
    align-items: center;
    font-weight: bold;
    font-size: 1.5rem;
    color: #fff;
}

.mobile-nav .logo-bar img {
    width: 2.5rem;
    margin-right: 1rem;
}
</style>