<template>
    <el-drawer v-model="visible" :size="size" :title="title" @closed="$emit('closed')" destroy-on-close>
        <el-main v-loading="!data" style="height: 100%">
            <el-descriptions v-if="data" :column="1" border class="font-monospace" size="small">
                <el-descriptions-item v-for="(item, i) in data" :key="i" :label="i" label-class-name="w15">
                    {{ item }}
                </el-descriptions-item>
            </el-descriptions>
            <el-empty v-if="esData && !esData.hits" v-loading="true"></el-empty>
            <el-descriptions
                v-for="(desc, i) in esData.hits.hits.map((x) => x._source)"
                v-if="esData?.hits?.hits?.length > 0"
                :column="1"
                :key="i"
                class="trace-log mt-8"
                size="small">
                <el-descriptions-item
                    v-for="(item, j) in Object.entries(desc)
                        .filter((x) => ['@timestamp', 'log_level', 'log_thread', 'log_source', 'log_message'].includes(x[0]))
                        .sort()"
                    :key="j"
                    :label="item[0]"
                    label-class-name="w15">
                    <span v-if="item[0] === '@timestamp'">
                        {{ $TOOL.dateFormat(item[1]) }}
                    </span>
                    <span v-else v-html="$TOOL.highLightKeywords($TOOL.unicodeDecode(item[1].toString()))" />
                </el-descriptions-item>
            </el-descriptions>
        </el-main>
    </el-drawer>
</template>

<script>
export default {
    emits: ['success', 'closed'],
    props: {
        size: { type: String, default: '80%' },
    },
    data() {
        return {
            title: null,
            visible: false,
            esData: null,
            data: null,
        }
    },
    methods: {
        async open(title, query, queryEs) {
            this.data = null
            this.title = null
            this.esData = null
            this.visible = true
            const res = await query()
            this.title = title(res.data)
            this.data = this.$TOOL.sortProperties(res.data)
            if (queryEs) {
                this.esData = {}
                this.$API.adm_tools.queryEsLog.post(queryEs).then((res) => {
                    this.esData = res.data
                })
            }
            return this
        },
    },
}
</script>
<style scoped></style>