<template>
    <el-container>
        <el-header style="border: none; padding-bottom: 0">
            <el-tabs v-model="tabId" class="w100p">
                <el-tab-pane :label="$t(`交易记录`)" name="list"></el-tab-pane>
                <el-tab-pane :label="$t(`冻结记录`)" name="frozen"></el-tab-pane>
            </el-tabs>
        </el-header>
        <el-main class="nopadding">
            <component :is="tabId" />
        </el-main>
    </el-container>
</template>

<script>
import { defineAsyncComponent } from 'vue'
const list = defineAsyncComponent(() => import('./list'))
const frozen = defineAsyncComponent(() => import('./frozen'))

export default {
    components: { list, frozen },
    computed: {},
    created() {},
    data() {
        return {
            tabId: 'list',
        }
    },
    inject: ['reload'],
    methods: {},
    mounted() {},
    watch: {},
}
</script>
<style scoped></style>