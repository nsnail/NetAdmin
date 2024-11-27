export default {
    buttons: [
        {
            icon: 'el-icon-view',
            title: '查看',
            click: async (row, vue) => {
                vue.dialog.save = { row, mode: 'view' }
            },
        },
        {
            icon: 'el-icon-edit',
            title: '编辑',
            click: async (row, vue) => {
                vue.dialog.save = { row, mode: 'edit' }
            },
        },
    ],
}