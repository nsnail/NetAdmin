<template>
    <div v-if="navMenus.length <= 0" style="padding: 20px">
        <el-alert :closable="false" :title="$t('无子集菜单')" center type="info" />
    </div>
    <template v-bind:key="navMenu" v-for="navMenu in navMenus">
        <el-menu-item v-if="!hasChildren(navMenu)" :index="navMenu.path">
            <nav-menu v-if="navMenu.meta && navMenu.meta.type === 'link'" :href="navMenu.path" @click.stop="() => {}" target="_blank"></nav-menu>
            <el-icon v-if="navMenu.meta && navMenu.meta.icon">
                <component :is="navMenu.meta.icon || 'el-icon-menu'" />
            </el-icon>
            <template #title>
                <span>{{ this.$t(navMenu.meta.title) }}</span>
                <span v-if="navMenu.meta.tag" class="menu-tag">{{ navMenu.meta.tag }}</span>
            </template>
        </el-menu-item>
        <el-sub-menu v-else :index="navMenu.path">
            <template #title>
                <el-icon v-if="navMenu.meta && navMenu.meta.icon">
                    <component :is="navMenu.meta.icon || 'el-icon-menu'" />
                </el-icon>
                <span>{{ this.$t(navMenu.meta.title) }}</span>
                <span v-if="navMenu.meta.tag" class="menu-tag">{{ navMenu.meta.tag }}</span>
            </template>
            <nav-menu :navMenus="navMenu.children" />
        </el-sub-menu>
    </template>
</template>

<script>
export default {
    props: ['navMenus'],
    data() {
        return {}
    },
    methods: {
        hasChildren(item) {
            return item.children && !item.children.every((item) => item.meta.hidden)
        },
    },
}
</script>