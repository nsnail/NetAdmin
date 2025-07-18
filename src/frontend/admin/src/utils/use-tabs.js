import { nextTick } from 'vue'
import nProgress from 'nprogress'
import 'nprogress/nprogress.css'
import router from '@/router'
import store from '@/store'

export default {
    //刷新标签
    refresh() {
        nProgress.start()
        const route = router.currentRoute.value
        store.commit('removeKeepLive', route.name)
        store.commit('setRouteShow', false)
        nextTick(() => {
            store.commit('pushKeepLive', route.name)
            store.commit('setRouteShow', true)
            nProgress.done()
        })
    },
    //关闭标签
    close(tag) {
        const route = tag || router.currentRoute.value
        store.commit('removeViewTags', route)
        store.commit('removeIframeList', route)
        store.commit('removeKeepLive', route.name)
        const tagList = store.state['view-tags']['view-tags']
        const latestView = tagList.slice(-1)[0]
        if (latestView) {
            router.push(latestView)
        } else {
            router.push('/')
        }
    },
    //关闭标签后处理
    closeNext(next) {
        const route = router.currentRoute.value
        store.commit('removeViewTags', route)
        store.commit('removeIframeList', route)
        store.commit('removeKeepLive', route.name)
        if (next) {
            const tagList = store.state['view-tags']['view-tags']
            next(tagList)
        }
    },
    //关闭其他
    closeOther() {
        const route = router.currentRoute.value
        const tagList = [...store.state['view-tags']['view-tags']]
        tagList.forEach((tag) => {
            if ((tag.meta && tag.meta.affix) || route.fullPath === tag.fullPath) {
                return true
            } else {
                this.close(tag)
            }
        })
    },
    //设置标题
    setTitle(title) {
        store.commit('updateViewTagsTitle', title)
    },
}