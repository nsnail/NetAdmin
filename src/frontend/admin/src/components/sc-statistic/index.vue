<template>
    <div class="sc-statistic">
        <div class="sc-statistic-title">
            {{ title }}
            <el-tooltip v-if="tips" effect="light">
                <template #content>
                    <div style="width: 15rem; line-height: 2">
                        {{ tips }}
                    </div>
                </template>
                <el-icon class="sc-statistic-tips">
                    <el-icon-question-filled />
                </el-icon>
            </el-tooltip>
        </div>
        <div class="sc-statistic-content">
            <span v-if="prefix" class="sc-statistic-content-prefix">{{ prefix }}</span>
            <span class="sc-statistic-content-value">
                <slot v-if="$slots.content" name="content" />
                <template v-else>
                    {{ cmtValue }}
                </template>
            </span>
            <span v-if="suffix" class="sc-statistic-content-suffix">{{ suffix }}</span>
            <span class="sc-statistic-icon">
                <slot v-if="$slots.icon" name="icon" />
            </span>
        </div>
        <div v-if="description || $slots.default" class="sc-statistic-description">
            <slot>
                {{ description }}
            </slot>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        title: { type: String, required: true, default: '' },
        value: { type: String, required: true, default: '' },
        prefix: { type: String, default: '' },
        suffix: { type: String, default: '' },
        description: { type: String, default: '' },
        tips: { type: String, default: '' },
        groupSeparator: { type: Boolean, default: false },
    },
    data() {
        return {}
    },
    computed: {
        cmtValue() {
            return this.groupSeparator ? this.$TOOL.groupSeparator(this.value) : this.value
        },
    },
}
</script>

<style scoped>
.sc-statistic-title {
    font-size: 0.9rem;
    color: var(--el-color-info);
    margin-bottom: 0.5rem;
    display: flex;
    align-items: center;
}

.sc-statistic-tips {
    margin-left: 0.4rem;
}

.sc-statistic-content {
    font-size: 1.5rem;
    color: var(--el-text-color-primary);
}

.sc-statistic-content-value {
    font-family: 'Lucida Console', 'Microsoft YaHei', monospace;
    font-weight: bold;
}

.sc-statistic-content-prefix {
    margin-right: 0.4rem;
}

.sc-statistic-content-suffix {
    margin-left: 0.4rem;
    font-size: 0.9rem;
}

.sc-statistic-description {
    margin-top: 1rem;
    color: var(--el-color-info);
}

.dark .sc-statistic-content {
    color: var(--el-text-color-primary);
}

.sc-statistic-icon {
    float: right;
}
</style>