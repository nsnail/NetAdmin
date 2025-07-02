<template>
    <transition name="el-zoom-in-top">
        <div v-if="visible" :style="{ left: left + 'px', top: top + 'px' }" @contextmenu.prevent="fun" class="sc-contextmenu" ref="contextmenu">
            <ul class="sc-contextmenu__menu">
                <slot />
            </ul>
        </div>
    </transition>
</template>

<script>
export default {
    provide() {
        return {
            menuClick: this.menuClick,
        }
    },
    data() {
        return {
            visible: false,
            top: 0,
            left: 0,
        }
    },
    watch: {
        visible(value) {
            if (value) {
                document.body.addEventListener('click', this.cm, true)
            } else {
                document.body.removeEventListener('click', this.cm, true)
            }
        },
    },
    mounted() {},
    methods: {
        cm(e) {
            let sp = this.$refs.contextmenu
            if (sp && !sp.contains(e.target)) {
                this.closeMenu()
            }
        },
        menuClick(command) {
            this.closeMenu()
            this.$emit('command', command)
        },
        openMenu(e) {
            e.preventDefault()
            this.visible = true
            this.left = e.clientX + 1
            this.top = e.clientY + 1

            this.$nextTick(() => {
                const ex = e.clientX + 1
                const ey = e.clientY + 1
                const innerWidth = window.innerWidth
                const innerHeight = window.innerHeight
                const menuHeight = this.$refs.contextmenu.offsetHeight
                const menuWidth = this.$refs.contextmenu.offsetWidth
                //位置修正公示
                //left = (当前点击X + 菜单宽度 > 可视区域宽度 ? 可视区域宽度 - 菜单宽度 : 当前点击X)
                //top = (当前点击Y + 菜单高度 > 可视区域高度 ? 当前点击Y - 菜单高度 : 当前点击Y)
                this.left = ex + menuWidth > innerWidth ? innerWidth - menuWidth : ex
                this.top = ey + menuHeight > innerHeight ? ey - menuHeight : ey
            })
            this.$emit('visibleChange', true)
        },
        closeMenu() {
            this.visible = false
            this.$emit('visibleChange', false)
        },
        fun() {
            return false
        },
    },
}
</script>

<style>
.sc-contextmenu {
    position: fixed;
    z-index: 3000;
    font-size: 0.9rem;
}

.sc-contextmenu__menu {
    display: inline-block;
    min-width: 10rem;
    border: 1px solid var(--el-border-color-light);
    background: var(--el-bg-color-overlay);
    box-shadow: 0 0.2rem 1rem 0 rgba(0, 0, 0, 0.1);
    z-index: 3000;
    list-style-type: none;
    padding: 0 0;
}

.sc-contextmenu__menu > hr {
    margin: 0.4rem 0;
    border: none;
    height: 1px;
    font-size: 0;
    background-color: var(--el-border-color-light);
}

.sc-contextmenu__menu > li {
    margin: 0;
    cursor: pointer;
    line-height: 2.5rem;
    padding: 0 1.3rem 0 0.8rem;
    color: var(--el-text-color-primary);
    display: flex;
    justify-content: space-between;
    white-space: nowrap;
    text-decoration: none;
    position: relative;
}

.sc-contextmenu__menu > li:hover {
    background-color: var(--el-color-primary-light-9);
    color: var(--el-color-primary-light-2);
}

.sc-contextmenu__menu > li.disabled {
    cursor: not-allowed;
    color: var(--el-text-color-disabled);
    background: transparent;
}

.sc-contextmenu__icon {
    display: inline-block;
    width: 1rem;
    font-size: 1.1rem;
    margin-right: 0.8rem;
}

.sc-contextmenu__suffix {
    margin-left: 3rem;
    color: var(--el-color-info);
}

.sc-contextmenu__menu li ul {
    position: absolute;
    top: 0;
    left: 100%;
    display: none;
    margin: -0.8rem 0;
}
</style>