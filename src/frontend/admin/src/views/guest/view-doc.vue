<template>
    <article v-if="doc" v-loading="loading" class="article">
        <section
            :class="this.$TOOL.data.get('APP_SET_DARK') || this.$CONFIG.APP_SET_DARK ? 'aie-theme-dark' : 'aie-theme-light'"
            ref="editor"></section>
    </article>
    <not-found v-else />
</template>

<script>
import { AiEditor } from 'aieditor'
import 'aieditor/dist/style.css'
import { defineAsyncComponent } from 'vue'
import sysConfig from '@/config'
import tool from '@/utils/tool'

const notFound = defineAsyncComponent(() => import('@/layout/other/404'))

export default {
    components: { notFound },
    data() {
        return {
            loading: true,
            doc: { title: '' },
        }
    },
    async created() {
        const res = await this.$API.sys_doc.viewContent.post({ id: this.$route.params.id })
        this.doc = res.data
        this.loading = false
        if (this.doc) {
            document.title = this.doc.title
            const aiEditor = new AiEditor({
                editable: false,
                element: this.$refs.editor,
                content: `<h1 style="text-align:center">${this.doc.title}</h1>${this.doc.body}`,
            })
            aiEditor.changeLang(this.$TOOL.data.get('APP_SET_LANG') || sysConfig.APP_SET_LANG)
        } else {
            await tool.data.set('LOGIN_REDIRECT', window.location.href)
        }
    },
    methods: {},
}
</script>

<style scoped>
.article {
    display: flex;
    height: 100%;
    flex-direction: column;

    > h1 {
        padding: 1rem;
    }

    > section {
        flex: 1;
    }
}

.article:deep(.aie-container) {
    border: none;
}
</style>