<template>
    <div class="sc-icon-select">
        <div :class="{ hasValue: value }" @click="open" class="sc-icon-select__wrapper">
            <el-input v-model="value" :disabled="disabled" :prefix-icon="value || 'el-icon-plus'" readonly></el-input>
        </div>
        <el-dialog v-model="dialogVisible" :title="$t('图标选择器')" :width="760" append-to-body destroy-on-close>
            <div class="sc-icon-select__dialog">
                <el-form :rules="{}">
                    <el-form-item prop="searchText">
                        <el-input
                            v-model="searchText"
                            :placeholder="$t('搜索')"
                            class="sc-icon-select__search-input"
                            clearable
                            prefix-icon="el-icon-search"
                            size="large" />
                    </el-form-item>
                </el-form>
                <el-tabs>
                    <el-tab-pane v-for="item in data" :key="item.name" lazy>
                        <template #label>
                            {{ item.name }}
                            <el-tag size="small" style="margin-left: 0.5rem" type="info">{{ item.icons.length }}</el-tag>
                        </template>
                        <div class="sc-icon-select__list">
                            <el-scrollbar>
                                <ul @click="selectIcon">
                                    <el-empty v-if="item.icons.length === 0" :description="$t('未查询到相关图标')" :image-size="100" />
                                    <li v-for="icon in item.icons" :key="icon">
                                        <span :data-icon="icon"></span>
                                        <el-icon>
                                            <component :is="icon" />
                                        </el-icon>
                                    </li>
                                </ul>
                            </el-scrollbar>
                        </div>
                    </el-tab-pane>
                </el-tabs>
            </div>
            <template #footer>
                <el-button @click="clear" text>{{ $t('清除') }}</el-button>
                <el-button @click="dialogVisible = false">{{ $t('取消') }}</el-button>
            </template>
        </el-dialog>
    </div>
</template>

<script>
import config from '@/config/iconSelect'

export default {
    props: {
        modelValue: { type: String, default: '' },
        disabled: { type: Boolean, default: false },
    },
    data() {
        return {
            value: '',
            dialogVisible: false,
            data: [],
            searchText: '',
        }
    },
    watch: {
        modelValue(val) {
            this.value = val
        },
        value(val) {
            this.$emit('update:modelValue', val)
        },
        searchText(val) {
            this.search(val)
        },
    },
    mounted() {
        this.value = this.modelValue
        this.data.push(...config.icons)
    },
    methods: {
        open() {
            if (this.disabled) {
                return false
            }
            this.dialogVisible = true
        },
        selectIcon(e) {
            if (e.target.tagName !== 'SPAN') {
                return false
            }
            this.value = e.target.dataset.icon
            this.dialogVisible = false
        },
        clear() {
            this.value = ''
            this.dialogVisible = false
        },
        search(text) {
            if (text) {
                const filterData = JSON.parse(JSON.stringify(config.icons))
                filterData.forEach((t) => {
                    t.icons = t.icons.filter((n) => n.includes(text))
                })
                this.data = filterData
            } else {
                this.data = JSON.parse(JSON.stringify(config.icons))
            }
        },
    },
}
</script>

<style scoped>
.sc-icon-select {
    display: inline-flex;
}

.sc-icon-select__wrapper {
    cursor: pointer;
    display: inline-flex;
}

.sc-icon-select__wrapper:deep(.el-input__wrapper).is-focus {
    box-shadow: 0 0 0 1px var(--el-input-hover-border-color) inset;
}

.sc-icon-select__wrapper:deep(.el-input__inner) {
    flex-grow: 0;
    width: 0;
}

.sc-icon-select__wrapper:deep(.el-input__icon) {
    margin: 0;
    font-size: 1.2rem;
}

.sc-icon-select__wrapper.hasValue:deep(.el-input__icon) {
    color: var(--el-text-color-regular);
}

.sc-icon-select__list {
    height: 30rem;
    overflow: auto;
}

.sc-icon-select__list li {
    display: inline-block;
    width: 6rem;
    height: 6rem;
    margin: 0.4rem;
    vertical-align: top;
    transition: all 0.1s;
    border-radius: 0.3rem;
    position: relative;
}

.sc-icon-select__list li span {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 1;
    cursor: pointer;
}

.sc-icon-select__list li i {
    width: 100%;
    height: 100%;
    font-size: 2rem;
    color: var(--el-text-color-regular);
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 0.3rem;
}

.sc-icon-select__list li:hover {
    box-shadow: 0 0 1px 0.3rem var(--el-color-primary);
    background: var(--el-color-primary-light-9);
}

.sc-icon-select__list li:hover i {
    color: var(--el-color-primary);
}
</style>