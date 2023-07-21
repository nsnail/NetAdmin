export default {
    //运算符
    operator: [
        {
            label: "等于",
            value: "=",
        },
        {
            label: "不等于",
            value: "!=",
        },
        {
            label: "大于",
            value: ">",
        },
        {
            label: "大于等于",
            value: ">=",
        },
        {
            label: "小于",
            value: "<",
        },
        {
            label: "小于等于",
            value: "<=",
        },
        {
            label: "包含",
            value: "include",
        },
        {
            label: "不包含",
            value: "notinclude",
        },
    ],
    //过滤结果运算符的分隔符
    separator: "|",
    //返回值的格式'
    valueFormat: "{key}:{value}{separator}{operator}",
    //获取我的常用
    // eslint-disable-next-line no-unused-vars
    getMy: function (name) {
        return new Promise((resolve) => {
            const list = [];
            setTimeout(() => {
                resolve(list);
            }, 500);
        });
    },
    /**
     * 常用保存处理 返回resolve后继续操作
     * @name scFilterBar组件的props->filterName
     * @obj 过滤项整理好的对象
     */
    // eslint-disable-next-line no-unused-vars
    saveMy: function (name, obj) {
        return new Promise((resolve) => {
            setTimeout(() => {
                resolve(true);
            }, 500);
        });
    },
    /**
     * 常用删除处理 返回resolve后继续操作
     * @name scFilterBar组件的props->filterName
     */
    // eslint-disable-next-line no-unused-vars
    delMy: function (name) {
        return new Promise((resolve) => {
            setTimeout(() => {
                resolve(true);
            }, 500);
        });
    },
};