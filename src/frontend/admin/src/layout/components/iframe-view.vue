<template>
    <div v-show="$route.meta.type === 'iframe'" class="iframe-pages">
        <iframe
            v-for="item in iframeList"
            v-show="$route.meta.url === item.meta.url"
            :key="item.meta.url"
            :src="item.meta.url"
            frameborder="0"></iframe>
    </div>
</template>

<script>
export default {
    data() {
        return {}
    },
    watch: {
        $route(e) {
            this.push(e)
        },
    },
    created() {
        this.push(this.$route)
    },
    computed: {
        iframeList() {
            return this.$store.state.iframe.iframeList
        },
        isMobile() {
            return this.$store.state.global.isMobile
        },
        layoutTags() {
            return this.$store.state.global.layoutTags
        },
    },
    mounted() {},
    methods: {
        push(route) {
            if (route.meta.type === 'iframe') {
                if (this.isMobile || !this.layoutTags) {
                    this.$store.commit('setIframeList', route)
                } else {
                    this.$store.commit('pushIframeList', route)
                }
            } else {
                if (this.isMobile || !this.layoutTags) {
                    this.$store.commit('clearIframeList')
                }
            }
        },
    },
}
</script>

<style scoped>
.iframe-pages {
    width: 100%;
    height: 100%;
    background: var(--el-color-white);
}

iframe {
    border: 0;
    width: 100%;
    height: 100%;
    display: block;
}
</style>