import store from '@/store'
import { nextTick } from 'vue'

export function beforeEach(to, from) {
    const adminMain = document.querySelector('#admin-ui-main')
    if (!adminMain) {
        return false
    }
    store.commit('updateViewTags', {
        fullPath: from.fullPath,
        scrollTop: adminMain.scrollTop,
    })
}

export async function afterEach(to) {
    const adminMain = document.querySelector('#admin-ui-main')
    if (!adminMain) {
        return false
    }
    await nextTick(() => {
        const beforeRoute = store.state['view-tags']['view-tags'].filter((v) => v.fullPath === to.fullPath)[0]
        if (beforeRoute) {
            adminMain.scrollTop = beforeRoute.scrollTop || 0
        }
    })
}