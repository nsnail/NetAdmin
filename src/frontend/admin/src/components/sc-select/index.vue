<template>
    <div class="sc-select">
        <div v-if="initLoading" class="sc-select-loading">
            <el-icon class="is-loading">
                <el-icon-loading />
            </el-icon>
        </div>
        <el-select v-bind="$attrs" :allow-create="allowCreate" :loading="loading" @visible-change="visibleChange">
            <el-option
                v-for="item in options"
                :key="$TOOL.getNestedProperty(item, props.value)"
                :label="typeof props.label === `function` ? props.label(item) : $TOOL.getNestedProperty(item, props.label)"
                :value="objValueType ? item : $TOOL.getNestedProperty(item, props.value)">
                <slot :data="item" name="option" />
            </el-option>
        </el-select>
    </div>
</template>

<script>
import selectConfig from '@/config/select'

export default {
    props: {
        queryApi: {
            type: Object,
            default: () => {},
        },
        dic: { type: String, default: '' },
        objValueType: { type: Boolean, default: false },
        allowCreate: { type: Boolean, default: false },
        params: { type: Object, default: () => ({}) },
        config: { type: Object },
    },
    data() {
        return {
            dicParams: this.params,
            loading: false,
            options: [],
            props: Object.assign(selectConfig, this.config || {}).props,
            initLoading: false,
        }
    },
    created() {
        //如果有默认值就去请求接口获取options
        if (this.hasValue()) {
            this.initLoading = true
            this.getRemoteData()
        }
    },
    methods: {
        //选项显示隐藏事件
        visibleChange(isOpen) {
            if (isOpen && (this.dic || this.queryApi)) {
                this.getRemoteData()
            }
        },
        //获取数据
        async getRemoteData() {
            this.loading = true
            this.dicParams[selectConfig.request.name] = this.dic
            let res = {}
            if (this.queryApi) {
                res = await this.queryApi.post(this.params)
            } else if (this.dic) {
                res = await selectConfig.dicApiObj.get(this.params)
            }
            const response = selectConfig.parseData(res)
            this.options = response.data
            this.loading = false
            this.initLoading = false
        },
        //判断是否有回显默认值
        hasValue() {
            if (Array.isArray(this.$attrs.modelValue) && this.$attrs.modelValue.length <= 0) {
                return false
            } else return !!this.$attrs.modelValue
        },
    },
}
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
    background: var(--el-color-white);
    z-index: 100;
    border-radius: 0.4rem;
    border: 1px solid var(--el-border-color-lighter);
    display: flex;
    align-items: center;
    padding-left: 1rem;
}

.sc-select-loading i {
    font-size: 1.1rem;
}

.dark .sc-select-loading {
    background: var(--el-bg-color-overlay);
    border-color: var(--el-border-color-light);
}
</style>