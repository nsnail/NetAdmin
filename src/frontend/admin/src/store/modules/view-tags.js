import router from '@/router'

export default {
    state: {
        'view-tags': [],
    },
    mutations: {
        pushViewTags(state, route) {
            let backPathIndex = state['view-tags'].findIndex((item) => item.fullPath === router.options.history.state.back)
            let target = state['view-tags'].find((item) => item.fullPath === route.fullPath)
            let isName = route.name
            if (!target && isName) {
                if (backPathIndex === -1) {
                    state['view-tags'].push(route)
                } else {
                    state['view-tags'].splice(backPathIndex + 1, 0, route)
                }
            }
        },
        removeViewTags(state, route) {
            state['view-tags'].forEach((item, index) => {
                if (item.fullPath === route.fullPath) {
                    state['view-tags'].splice(index, 1)
                }
            })
        },
        updateViewTags(state, route) {
            state['view-tags'].forEach((item) => {
                if (item.fullPath === route.fullPath) {
                    item = Object.assign(item, route)
                }
            })
        },
        updateViewTagsTitle(state, title = '') {
            const nowFullPath = location.hash.substring(1)
            state['view-tags'].forEach((item) => {
                if (item.fullPath === nowFullPath) {
                    item.meta.title = title
                }
            })
        },
        clearViewTags(state) {
            state['view-tags'] = []
        },
    },
}