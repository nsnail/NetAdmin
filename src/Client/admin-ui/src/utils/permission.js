import tool from '@/utils/tool';

/**
 * �Ƿ��в��޷��࣬�����ʾȫ������ͨ��
 */
export function permissionAll() {
    const allPermissions = "*/*/*"
    let permissions = tool.data.get("PERMISSIONS");
    return permissions.includes(allPermissions);
}

/**
 * �ȶ����������Ƿ�һ��
 * @param news
 * @param old
 * @returns {boolean}
 */
export function judementSameArr(news, old) {
    //
    //
    let count = 0;
    const leng = news.length;
    for (let i in news) {
        for (let j in old) {
            if (news[i] === old[j]) {
                count++;
                //
            }
        }
    }
    //
    return count === leng;
}

export function permission(data) {
    let permissions = tool.data.get("PERMISSIONS");
    if (!permissions) {
        return false;
    }
    let isHave = permissions.includes(data);
    return isHave;
}

export function rolePermission(data) {
    let userInfo = tool.data.get("USER_INFO");
    if (!userInfo) {
        return false;
    }
    let role = userInfo.role;
    if (!role) {
        return false;
    }
    let isHave = role.includes(data);
    return isHave;
}