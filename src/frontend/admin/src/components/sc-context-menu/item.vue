<template>
    <hr v-if="divided" />
    <li :class="disabled ? 'disabled' : ''" @click.stop="liClick" @mouseenter="openSubmenu($event)" @mouseleave="closeSubmenu($event)">
        <span class="title">
            <el-icon class="sc-contextmenu__icon"><component v-if="icon" :is="icon" /></el-icon>
            {{ title }}
        </span>
        <span class="sc-contextmenu__suffix">
            <el-icon v-if="$slots.default"><el-icon-arrow-right /></el-icon>
            <template v-else>{{ suffix }}</template>
        </span>
        <ul v-if="$slots.default" class="sc-contextmenu__menu">
            <slot />
        </ul>
    </li>
</template>

<script>
export default {
    props: {
        command: { type: String, default: '' },
        title: { type: String, default: '' },
        suffix: { type: String, default: '' },
        icon: { type: String, default: '' },
        divided: { type: Boolean, default: false },
        disabled: { type: Boolean, default: false },
    },
    inject: ['menuClick'],
    methods: {
        liClick() {
            if (this.$slots.default) {
                return false
            }
            if (this.disabled) {
                return false
            }
            this.menuClick(this.command)
        },
        openSubmenu(e) {
            const menu = e.target.querySelector('ul')
            if (!menu) {
                return false
            }
            if (this.disabled) {
                return false
            }
            menu.style.display = 'inline-block'
            const rect = menu.getBoundingClientRect()
            const menuX = rect.left
            const menuY = rect.top
            const innerWidth = window.innerWidth
            const innerHeight = window.innerHeight
            const menuHeight = menu.offsetHeight
            const menuWidth = menu.offsetWidth
            if (menuX + menuWidth > innerWidth) {
                menu.style.left = 'auto'
                menu.style.right = '100%'
            }
            if (menuY + menuHeight > innerHeight) {
                menu.style.top = 'auto'
                menu.style.bottom = '0'
            }
        },
        closeSubmenu(e) {
            const menu = e.target.querySelector('ul')
            if (!menu) {
                return false
            }
            menu.removeAttribute('style')
            menu.style.display = 'none'
        },
    },
}
</script>

<style />