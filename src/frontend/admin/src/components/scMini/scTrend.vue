<template>
    <span :class="'sc-trend--' + type" class="sc-trend">
        <el-icon v-if="iconType === 'P'" class="sc-trend-icon"><el-icon-top /></el-icon>
        <el-icon v-if="iconType === 'N'" class="sc-trend-icon"><el-icon-bottom /></el-icon>
        <el-icon v-if="iconType === 'Z'" class="sc-trend-icon"><el-icon-right /></el-icon>
        <em class="sc-trend-prefix">{{ prefix }}</em>
        <em class="sc-trend-value">{{ modelValue }}</em>
        <em class="sc-trend-suffix">{{ suffix }}</em>
    </span>
</template>

<script>
export default {
    props: {
        modelValue: { type: Number, default: 0 },
        prefix: { type: String, default: '' },
        suffix: { type: String, default: '' },
        reverse: { type: Boolean, default: false },
    },
    computed: {
        absValue() {
            return Math.abs(this.modelValue)
        },
        iconType(v) {
            if (this.modelValue === 0) {
                v = 'Z'
            } else if (this.modelValue < 0) {
                v = 'N'
            } else if (this.modelValue > 0) {
                v = 'P'
            }
            return v
        },
        type(v) {
            if (this.modelValue === 0) {
                v = 'Z'
            } else if (this.modelValue < 0) {
                v = this.reverse ? 'P' : 'N'
            } else if (this.modelValue > 0) {
                v = this.reverse ? 'N' : 'P'
            }
            return v
        },
    },
}
</script>

<style scoped>
.sc-trend {
    display: flex;
    align-items: center;
}

.sc-trend-icon {
    margin-right: 0.2rem;
}

.sc-trend em {
    font-style: normal;
}

.sc-trend-prefix {
    margin-right: 0.2rem;
}

.sc-trend-suffix {
    margin-left: 0.2rem;
}

.sc-trend--P {
    color: var(--el-color-danger);
}

.sc-trend--N {
    color: var(--el-color-success);
}

.sc-trend--Z {
    color: var(--el-color-info);
}
</style>