import {permissionAll} from '@/utils/permission'
import tool from '@/utils/tool';

/**
 * �û�Ȩ��ָ��
 * @directive ����Ȩ����֤��v-auth="'xxx'"��
 * @directive ���Ȩ����֤������һ������ʾ��v-auths="['xxx','xxx']"��
 * @directive ���Ȩ����֤��ȫ����������ʾ��v-auths-all="['xxx','xxx']"��
 */
export default {
    mounted(el, binding) {
        if (permissionAll()) {
            return
        }
        let permissions = tool.data.get("PERMISSIONS");
        if (!permissions.some((v) => v === binding.value)) el.parentNode.removeChild(el);
    }
}