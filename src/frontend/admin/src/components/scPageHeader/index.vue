<template>
    <div class="sc-page-header">
        <div v-if="icon" class="sc-page-header__icon">
            <span>
                <el-icon><component :is="icon" /></el-icon>
            </span>
        </div>
        <div class="sc-page-header__title">
            <h2>{{ title }}</h2>
            <p v-if="description || $slots.default">
                <slot>
                    {{ description }}
                </slot>
            </p>
        </div>
        <div v-if="$slots.main" class="sc-page-header__main">
            <slot name="main"></slot>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        title: { type: String, required: true, default: '' },
        description: { type: String, default: '' },
        icon: { type: String, default: '' },
    },
}
</script>

<style scoped>
.sc-page-header {
    background: var(--el-bg-color);
    border-bottom: 1px solid var(--el-border-color-light);
    padding: 1.5rem 2rem;
    display: flex;
}

.sc-page-header__icon {
    width: 4rem;
}

.sc-page-header__icon span {
    width: 2.5rem;
    height: 2.5rem;
    background: var(--el-color-primary);
    border-radius: 40%;
    display: flex;
    align-items: center;
    justify-content: center;
}

.sc-page-header__icon span i {
    color: var(--el-color-white);
    font-size: 1.1rem;
}

.sc-page-header__title {
    flex: 1;
}

.sc-page-header__title h2 {
    font-size: 1.3rem;
    color: var(--el-text-color-primary);
    font-weight: bold;
    margin-top: 0.3rem;
}

.sc-page-header__title p {
    font-size: 1rem;
    color: var(--el-color-info);
    margin-top: 1rem;
}
</style>