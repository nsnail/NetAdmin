import api from '@/api'
import tool from '@/utils/tool'

export default {
    async install(app, global) {
        const preloads = await Promise.all([
            (async () => {
                try {
                    return await api.sys_menu.userMenus.post()
                } catch {
                    //
                }
            })(),
            (async () => {
                try {
                    return await api.sys_user.userInfo.post()
                } catch {
                    //
                }
            })(),
            (async () => {
                try {
                    return await api.sys_constant.getEnums.post()
                } catch {
                    //
                }
            })(),
            (async () => {
                try {
                    return await api.sys_constant.getNumbers.post()
                } catch {
                    //
                }
            })(),
            (async () => {
                try {
                    return await api.sys_constant.getChars.post()
                } catch {
                    //
                }
            })(),
        ])

        if (!global) global = app.config.globalProperties.$GLOBAL
        global.menu = preloads[0]?.data
        global.user = Object.assign(
            {
                isSuperAdmin: preloads[1]?.data?.roles.findIndex((x) => x.ignorePermissionControl) >= 0,
                isAdmin: preloads[1]?.data?.roles.findIndex((x) => x.dataScope === 'all') >= 0,
                isChannel: preloads[1]?.data?.roles.findIndex((x) => x.dataScope === 'deptWithChildren') >= 0,
                isSeller: preloads[1]?.data?.roles.findIndex((x) => x.dataScope === 'deptWithSon') >= 0,
            },
            preloads[1]?.data,
        )
        global.enums = preloads[2]?.data
        global.numbers = preloads[3]?.data
        global.chars = preloads[4]?.data
        global.permissions = global.user.isSuperAdmin ? ['*/*/*'] : tool.recursiveFindProperty(preloads[0]?.data, 'type', 'button').map((x) => x.tag)
        global.apiPermissions = global.user.isSuperAdmin
            ? ['*/*/*']
            : preloads[1]?.data?.roles
                  ?.map((x) => (x.apiIds ?? []).join(','))
                  ?.join(',')
                  .split(',')
    },
}