<!--
 * @Descripttion: 页面头部样式组件
 * @version: 1.0
 * @Author: sakuya
 * @Date: 2021年7月20日08:49:07
 * @LastEditors:
 * @LastEditTime:
-->

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
    background: #fff;
    border-bottom: 1px solid #e6e6e6;
    padding: 1.5rem 2rem;
    display: flex;
}

.sc-page-header__icon {
    width: 4rem;
}

.sc-page-header__icon span {
    width: 2.5rem;
    height: 2.5rem;
    background: #21a675;
    border-radius: 40%;
    display: flex;
    align-items: center;
    justify-content: center;
}

.sc-page-header__icon span i {
    color: #fff;
    font-size: 1.1rem;
}

.sc-page-header__title {
    flex: 1;
}

.sc-page-header__title h2 {
    font-size: 1.3rem;
    color: #3c4a54;
    font-weight: bold;
    margin-top: 0.3rem;
}

.sc-page-header__title p {
    font-size: 1rem;
    color: #999;
    margin-top: 1rem;
}

[data-theme='dark'] .sc-page-header {
    background: #2b2b2b;
    border-color: var(--el-border-color-base);
}

[data-theme='dark'] .sc-page-header__title h2 {
    color: #c0c0c0;
}
</style>