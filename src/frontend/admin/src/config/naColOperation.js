export default {
    buttons: [
        {
            icon: 'el-icon-view',
            click: async (row, vue) => {
                vue.loading = true
                vue.dialog.save = true
                await vue.$nextTick()
                await vue.$refs.saveDialog.open('view', row)
                vue.loading = false
            },
        },
        {
            icon: 'el-icon-edit',
            click: async (row, vue) => {
                vue.loading = true
                vue.dialog.save = true
                await vue.$nextTick()
                await vue.$refs.saveDialog.open('edit', row)
                vue.loading = false
            },
        },
    ],
}