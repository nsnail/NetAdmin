<template>
    <el-container>
        <el-header>
            <div class="right-panel">
                <na-button-add :vue="this" />
                <el-button icon="el-icon-delete" plain type="danger" @click="batchDel"></el-button>
            </div>
        </el-header>
        <el-main class="nopadding">
            <el-scrollbar>
                <div class="demo-collapse">
                    <el-collapse v-model="msgId" accordion @change="change">
                        <el-collapse-item
                            v-for="(msg, i) in msgList"
                            :key="i"
                            :name="msg.id"
                            :class="msg.myFlags.userSiteMsgStatus === 'read' ? 'msg-wrapper-read' : ''">
                            <template #title>
                                <div class="msg-title">
                                    <div>
                                        <el-badge v-if="msg.myFlags.userSiteMsgStatus === 0" is-dot type="primary">
                                            <el-avatar :size="40" :src="msg.avatar"></el-avatar>
                                        </el-badge>
                                        <el-avatar v-else :size="40" :src="msg.avatar"></el-avatar>
                                    </div>
                                    <div>
                                        <h2>{{ msg.title }}</h2>
                                        <p v-if="msg.id !== msgId">
                                            {{ msg.content }}
                                        </p>
                                        <p v-else>
                                            {{ msg.createdTime }}
                                        </p>
                                    </div>
                                    <div>
                                        <span v-time.tip="msg.createdTime"></span>
                                    </div>
                                </div>
                            </template>
                            <div class="msg-content">{{ msg.content }}</div>
                        </el-collapse-item>
                    </el-collapse>
                </div>
            </el-scrollbar>
        </el-main>
    </el-container>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            msgList: [],
            loading: false,
            msgId: null,
        }
    },
    watch: {},
    computed: {},
    mounted() {},
    async created() {
        const res = await this.$API.sys_sitemsg.pagedQueryMine.post()
        this.msgList = res.data.rows
    },
    methods: {
        async change(id) {
            if (!id) return
            await this.$API.sys_sitemsg.setSiteMsgStatus.post({ siteMsgId: id, userSiteMsgStatus: 'read' })
        },
        //删除
        async rowDel(row) {
            try {
                const res = await this.$API.sys_sitemsg.delete.post({ id: row.id })
                this.$refs.table.refresh()
                this.$message.success(`删除 ${res.data} 项`)
            } catch {
                //
            }
        },
        //批量删除
        async batchDel() {
            // let loading
            // try {
            //     await this.$confirm(`确定删除选中的 ${this.selection.length} 项吗？`, '提示', {
            //         type: 'warning',
            //     })
            //     loading = this.$loading()
            //     const res = await this.$API.sys_sitemsg.bulkDelete.post({
            //         items: this.selection,
            //     })
            //     this.$refs.table.refresh()
            //     this.$message.success(`删除 ${res.data} 项`)
            // } catch {
            //     //
            // }
            // loading?.close()
        },
    },
}
</script>
<style scoped>
/deep/ .el-collapse-item__header {
    height: unset;
    line-height: unset;
}
</style>
<style lang="scss" scoped>
.demo-collapse {
    padding: 1rem;
}
.msg-wrapper-read {
    * {
        color: var(--el-color-info-light-3);
    }
}
.msg-content {
    text-indent: 4rem;
    padding: 0 1rem;
}
.msg-title {
    padding: 1rem;
    text-align: left;
    cursor: pointer;
    gap: 1rem;
    p {
        overflow: hidden;
    }
    > div:nth-child(2) {
        flex-grow: 1;
        line-height: 2rem;
        height: 4rem;
        * {
            height: 2rem;
            overflow: hidden;
        }
    }
    > div:last-child {
        min-width: 5rem;
        color: var(--el-color-info-light-3);
    }
    align-items: center;
    width: 100%;
    display: flex;
    h2 {
        font-size: 1.1rem;
    }
}
</style>