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
    delButton(title, api, idField = 'id', idProc = (id) => id) {
        return {
            icon: 'el-icon-delete',
            type: 'danger',
            confirm: true,
            title: title,
            click: async (row, vue) => {
                let loading = vue.$loading()
                try {
                    const res = await api.post({
                        id: idProc(row[idField]),
                    })
                    vue.$message.success(vue.$t('删除 {count} 项', { count: res.data }))
                    vue.$refs.table.refresh()
                } catch {}
                loading?.close()
            },
        }
    },
}