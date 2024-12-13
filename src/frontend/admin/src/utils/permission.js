/**
 * 是否含有不限分类，有则表示全部允许通过
 */
export function permissionAll(permissions) {
    return permissions.includes('*/*/*')
}

/**
 * 比对两组数据是否一致
 * @param news
 * @param old
 * @returns {boolean}
 */
export function judementSameArr(news, old) {
    let count = 0
    const len = news.length
    for (let i in news) {
        for (let j in old) {
            if (news[i] === old[j]) {
                count++
            }
        }
    }

    return count === len
}

export function rolePermission(data, user) {
    if (permissionAll(data)) return true
    if (!user || !user.roles) {
        return false
    }
    return user.roles.some((x) => x.name === data)
}