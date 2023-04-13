<template>
    <el-main style="padding: 0 20px">
        <el-tabs
            v-model="activeName"
            v-loading="loading"
            class="demo-tabs"
            @tab-click="tabClick"
        >
            <el-tab-pane label="基本信息" name="first">
                <el-descriptions :column="1" border title="">
                    <el-descriptions-item
                        v-for="(val, key) in data"
                        :key="key"
                        :label="key"
                        >{{ val }}
                    </el-descriptions-item>
                </el-descriptions>
            </el-tab-pane>
            <el-tab-pane label="详细信息" name="second">
                <el-descriptions :column="1" border title="">
                    <el-descriptions-item
                        v-for="(val, key) in detail"
                        :key="key"
                        :label="key"
                    >
                        <pre>{{ val }}</pre>
                    </el-descriptions-item>
                </el-descriptions>
            </el-tab-pane>
            <el-tab-pane label="回调记录" name="third">
                <el-descriptions
                    v-for="(item, i) in callback"
                    :key="i"
                    :column="1"
                    :title="item.createdTime"
                    border
                    style="margin-bottom: 1rem"
                >
                    <el-descriptions-item
                        v-for="(val, key) in item"
                        :key="key"
                        :label="key"
                    >
                        <pre
                            v-if="key === 'reqContent' || key === 'ackHeader'"
                            >{{ JSON.stringify(JSON.parse(val), null, 4) }}</pre
                        >
                        <pre v-else>{{ val }}</pre>
                    </el-descriptions-item>
                </el-descriptions>
            </el-tab-pane>
        </el-tabs>
    </el-main>
</template>

<script>
export default {
    components: {},
    data() {
        return {
            loading: false,
            activeName: "first",
            data: {
                id: "",
            },
            detail: null,
            callback: null,
        };
    },
    mounted() {},
    methods: {
        async tabClick(data) {
            this.loading = true;
            try {
                if (data.index === "1" && !this.detail) {
                    const res = await this.$API.tsk_viewdetail.query.post({
                        filter: { id: this.data.id },
                    });
                    this.detail = res.data[0];
                    this.detail.content = JSON.stringify(
                        JSON.parse(this.detail.content),
                        null,
                        4
                    );
                    const result = JSON.parse(this.detail.result);
                    if (result.dc_data && result.dc_data.retBody) {
                        result.dc_data.retBody = JSON.parse(
                            result.dc_data.retBody
                        );
                    }
                    this.detail.result = JSON.stringify(result, null, 4);
                } else if (data.index === "2" && !this.callback) {
                    const res2 = await this.$API.tsk_viewcallback.query.post({
                        dynamicFilter: {
                            field: "taskId",
                            value: this.data.id,
                            operator: "eq",
                        },
                    });
                    this.callback = res2.data;
                }
            } catch {}
            this.loading = false;
        },
        //注入数据
        setData(data) {
            this.data = data;
        },
    },
};
</script>

<style></style>