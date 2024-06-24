export default {
    buttons: [
        {
            icon: 'el-icon-view',
            click: async (row, vue) => {
                vue.dialog.save = { row, mode: 'view' }
            },
        },
        {
            icon: 'el-icon-edit',
            click: async (row, vue) => {
                vue.dialog.save = { row, mode: 'edit' }
            },
        },
    ],
}