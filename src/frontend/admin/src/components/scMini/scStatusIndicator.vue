<template>
    <span :class="[{ 'sc-status-processing': pulse }, 'sc-state-bg--' + type]" class="sc-state"></span>
</template>

<script>
export default {
    props: {
        type: { type: String, default: 'primary' },
        pulse: { type: Boolean, default: false },
    },
}
</script>

<style scoped>
.sc-state {
    display: inline-block;
    background: var(--el-color-black);
    width: 0.6rem;
    height: 0.6rem;
    border-radius: 50%;
    vertical-align: middle;
}

.sc-status-processing {
    position: relative;
}

.sc-status-processing:after {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    border-radius: 50%;
    background: inherit;
    content: '';
    animation: warn 1.2s ease-in-out infinite;
}

.sc-state-bg--primary {
    background: var(--el-color-primary);
}

.sc-state-bg--success {
    background: var(--el-color-success);
}

.sc-state-bg--warning {
    background: var(--el-color-warning);
}

.sc-state-bg--danger {
    background: var(--el-color-danger);
}

.sc-state-bg--info {
    background: var(--el-color-info);
}

@keyframes warn {
    0% {
        transform: scale(0.5);
        opacity: 1;
    }
    30% {
        opacity: 0.7;
    }
    100% {
        transform: scale(2.5);
        opacity: 0;
    }
}
</style>