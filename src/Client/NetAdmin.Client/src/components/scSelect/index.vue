<!--
 * @Description: 异步选择器
 * @version: 1.1
 * @Author: sakuya
 * @Date: 2021年8月3日15:53:37
 * @LastEditors: sakuya
 * @LastEditTime: 2023年2月23日15:17:24
-->

<template>
    <div class="sc-select">
        <div v-if="initLoading" class="sc-select-loading">
            <el-icon class="is-loading">
                <el-icon-loading />
            </el-icon>
        </div>
        <el-select
            :loading="loading"
            v-bind="$attrs"
            @visible-change="visibleChange"
        >
            <el-option
                v-for="item in options"
                :key="item[props.value]"
                :label="item[props.label]"
                :value="objValueType ? item : item[props.value]"
            >
                <slot :data="item" name="option"></slot>
            </el-option>
        </el-select>
    </div>
</template>

<script>
import config from "@/config/select";

export default {
    props: {
        apiObj: {
            type: Object,
            default: () => {},
        },
        dic: { type: String, default: "" },
        objValueType: { type: Boolean, default: false },
        params: { type: Object, default: () => ({}) },
    },
    data() {
        return {
            dicParams: this.params,
            loading: false,
            options: [],
            props: config.props,
            initLoading: false,
        };
    },
    created() {
        //如果有默认值就去请求接口获取options
        if (this.hasValue()) {
            this.initLoading = true;
            this.getRemoteData();
        }
    },
    methods: {
        //选项显示隐藏事件
        visibleChange(isOpen) {
            if (
                isOpen &&
                this.options.length === 0 &&
                (this.dic || this.apiObj)
            ) {
                this.getRemoteData();
            }
        },
        //获取数据
        async getRemoteData() {
            this.loading = true;
            this.dicParams[config.request.name] = this.dic;
            let res = {};
            if (this.apiObj) {
                res = await this.apiObj.get(this.params);
            } else if (this.dic) {
                res = await config.dicApiObj.get(this.params);
            }
            const response = config.parseData(res);
            this.options = response.data;
            this.loading = false;
            this.initLoading = false;
        },
        //判断是否有回显默认值
        hasValue() {
            if (
                Array.isArray(this.$attrs.modelValue) &&
                this.$attrs.modelValue.length <= 0
            ) {
                return false;
            } else return !!this.$attrs.modelValue;
        },
    },
};
</script>

<style scoped>
.sc-select {
    display: inline-block;
    position: relative;
}

.sc-select-loading {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: #fff;
    z-index: 100;
    border-radius: 5px;
    border: 1px solid #ebeef5;
    display: flex;
    align-items: center;
    padding-left: 10px;
}

.sc-select-loading i {
    font-size: 14px;
}

.dark .sc-select-loading {
    background: var(--el-bg-color-overlay);
    border-color: var(--el-border-color-light);
}
</style>