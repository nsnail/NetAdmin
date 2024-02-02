<template>
    <el-skeleton v-if="loading" :rows="5" animated />
    <template v-else>
        <el-container v-if="msgList.length > 0" class="nopadding">
            <el-header style="border: none">
                <div class="right-panel">
                    <el-button @click="batchRead" icon="el-icon-message" plain type="success"></el-button>
                    <el-popconfirm :title="`确定清空本页消息吗？`" @confirm="batchDel" width="15rem">
                        <template #reference>
                            <el-button v-loading="delLoading" icon="el-icon-delete" plain type="danger"></el-button>
                        </template>
                    </el-popconfirm>
                </div>
            </el-header>
            <el-main>
                <div class="msg-collapse">
                    <el-collapse
                        v-infinite-scroll="load"
                        v-loading="msgLoading"
                        v-model="currMsgId"
                        @change="change"
                        accordion
                        infinite-scroll-distance="200">
                        <el-collapse-item
                            v-for="(msg, i) in msgList"
                            :class="msg.myFlags.userSiteMsgStatus === 'read' ? 'msg-wrapper-read' : ''"
                            :key="i"
                            :name="msg.id">
                            <template #title>
                                <div class="msg-title">
                                    <div>
                                        <el-badge v-if="msg.myFlags.userSiteMsgStatus === 0" is-dot type="primary">
                                            <el-avatar :size="40" :src="msg.sender.avatar ?? $CONFIG.DEFAULT_AVATAR"></el-avatar>
                                        </el-badge>
                                        <el-avatar v-else :size="40" :src="msg.sender.avatar ?? $CONFIG.DEFAULT_AVATAR"></el-avatar>
                                    </div>
                                    <div>
                                        <h2>{{ msg.title }}</h2>
                                        <p v-if="msg.id !== currMsgId">
                                            {{ msg.summary }}
                                        </p>
                                        <p v-else>
                                            {{ msg.createdTime }}
                                        </p>
                                    </div>
                                    <div>
                                        <p>{{ msg.sender.userName }}</p>
                                        <p v-time.tip="msg.createdTime"></p>
                                    </div>
                                </div>
                            </template>
                            <div v-html="msg.content" class="msg-content"></div>
                        </el-collapse-item>
                    </el-collapse>
                </div>
            </el-main>
        </el-container>
        <el-empty v-else></el-empty>
    </template>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            msgList: [],
            currMsgId: null,
            loading: true,
            page: 1,
            delLoading: false,
            msgLoading: false,
        }
    },
    watch: {},
    computed: {},
    mounted() {},
    async created() {
        await this.load()
    },
    methods: {
        async load() {
            const res = await this.$API.sys_sitemsg.pagedQueryMine.post({ page: this.page++ })
            if (res.data.rows) {
                this.msgList = this.msgList.concat(res.data.rows)
            }
            this.loading = false
        },
        async change(id) {
            if (!id) return
            this.msgLoading = true
            const res = await this.$API.sys_sitemsg.getMine.post({ id: id })
            this.msgLoading = false
            this.msgList.find((x) => x.id === id).content = res.data.content
            await this.$API.sys_sitemsg.setSiteMsgStatus.post({ siteMsgId: id, userSiteMsgStatus: 'read' })
        },
        //批量已读
        async batchRead() {
            try {
                for (const msg of this.msgList) {
                    this.$API.sys_sitemsg.setSiteMsgStatus.post({ siteMsgId: msg.id, userSiteMsgStatus: 'read' })
                    msg.myFlags.userSiteMsgStatus = 'read'
                }
            } catch {
                //
            }
            this.$message.success(this.$t(`本页消息已标记为已读`))
        },
        //批量删除
        async batchDel() {
            this.delLoading = true
            try {
                await Promise.all(
                    this.msgList.map((msg) => {
                        return this.$API.sys_sitemsg.setSiteMsgStatus.post({ siteMsgId: msg.id, userSiteMsgStatus: 'deleted' })
                    }),
                )
            } catch {
                //
            }

            this.delLoading = false
            this.$message.success(this.$t(`本页消息已清空`))
            this.msgList = []
            this.page = 1
            this.loading = true
            await this.load()
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
.msg-collapse {
    padding: 1rem 0;
}

.msg-wrapper-read {
    filter: grayscale(1) opacity(0.6);
}

.msg-content {
    text-indent: 4rem;
    padding: 0 1rem;
}

.msg-title {
    padding: 1rem 0;
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
        min-width: 8rem;
        text-align: right;
        padding-right: 0.5rem;
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