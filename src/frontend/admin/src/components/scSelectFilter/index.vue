<template>
    <div class="sc-select-filter">
        <div v-if="data.length <= 0" class="sc-select-filter__no-data">{{ $t('暂无数据') }}</div>
        <div v-for="item in data" :class="`sc-select-filter__item${item.w100p ? ' sc-select-filter__item-w100p' : ''}`" :key="item.key">
            <div :style="{ width: labelWidth + 'rem' }" @click="autoHeight" class="sc-select-filter__item-title">
                <label>
                    <span>{{ item.title }}({{ item.options.length - 1 }})</span>
                    <el-icon style="display: none">
                        <el-icon-arrow-up></el-icon-arrow-up>
                    </el-icon>
                </label>
            </div>
            <div class="sc-select-filter__item-options">
                <ul>
                    <li
                        v-for="option in item.options"
                        :class="{ active: selected[item.key] && selected[item.key].includes(option.value) }"
                        :key="option.value"
                        :title="option.title"
                        @click="select(option, item)">
                        <el-icon v-if="option.icon">
                            <component :is="option.icon" />
                        </el-icon>
                        <el-badge :max="item.badgeMax ?? 999999999" :show-zero="false" :value="option.badge" badge-class="badge-small">
                            <span>{{ option.label }}</span>
                        </el-badge>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        data: { type: Array, default: () => [] },
        selectedValues: {
            type: Object,
            default: () => {
                return {}
            },
        },
        labelWidth: { type: Number, default: 6 },
        outputValueTypeToArray: { type: Boolean, default: false },
    },
    data() {
        return {
            selected: {},
            svgIconUp:
                '<svg data-v-a30f2a47="" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1024 1024"><path fill="currentColor" d="m488.832 344.32-339.84 356.672a32 32 0 0 0 0 44.16l.384.384a29.44 29.44 0 0 0 42.688 0l320-335.872 319.872 335.872a29.44 29.44 0 0 0 42.688 0l.384-.384a32 32 0 0 0 0-44.16L535.168 344.32a32 32 0 0 0-46.336 0"></path></svg>',
            svgIconDown:
                '<svg data-v-a30f2a47="" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1024 1024"><path fill="currentColor" d="M831.872 340.864 512 652.672 192.128 340.864a30.592 30.592 0 0 0-42.752 0 29.12 29.12 0 0 0 0 41.6L489.664 714.24a32 32 0 0 0 44.672 0l340.288-331.712a29.12 29.12 0 0 0 0-41.728 30.592 30.592 0 0 0-42.752 0z"></path></svg>',
        }
    },
    watch: {
        // data(val) {
        //     val.forEach((item) => {
        //         this.selected[item.key] =
        //             this.selectedValues[item.key] || (Array.isArray(item.options) && item.options.length) ? [item.options[0].value] : []
        //     })
        // },

        data: {
            immediate: true,
            handler() {
                this.$nextTick(() => {
                    for (const el of document.getElementsByClassName('sc-select-filter__item-options')) {
                        if (el.children[0].clientHeight / el.clientHeight < 1.5) {
                            el.previousSibling.children[0].children[1].style.display = 'none'
                            el.previousSibling.children[0].style.cursor = 'unset'
                            el.style.height = '3.5rem'
                        } else {
                            el.previousSibling.children[0].style.cursor = 'pointer'
                            el.previousSibling.children[0].children[1].style.display = 'unset'
                            el.previousSibling.children[0].children[1].innerHTML = this.svgIconUp
                        }
                    }
                })
            },
        },
    },
    computed: {
        selectedString() {
            const outputData = JSON.parse(JSON.stringify(this.selected))
            for (const key in outputData) {
                outputData[key] = outputData[key].join(',')
            }
            return outputData
        },
    },
    mounted() {
        //默认赋值
        this.data.forEach((item) => {
            this.selected[item.key] =
                this.selectedValues[item.key] || (Array.isArray(item.options) && item.options.length) ? [item.options[0].value] : []
        })
    },
    methods: {
        autoHeight(e) {
            if (e.currentTarget.nextSibling.style.height === 'auto') {
                e.currentTarget.nextSibling.style.height = '3.5rem'
                e.currentTarget.children[0].children[1].innerHTML = this.svgIconUp
            } else {
                if (e.currentTarget.nextSibling.children[0].clientHeight / e.currentTarget.nextSibling.clientHeight < 1.5) return
                e.currentTarget.nextSibling.style.height = 'auto'
                e.currentTarget.children[0].children[1].innerHTML = this.svgIconDown
            }
        },
        select(option, item) {
            //判断单选多选
            if (item.multiple) {
                //如果多选选择的第一个
                if (option.value === item.options[0].value) {
                    //就赋值第一个的值
                    this.selected[item.key] = [option.value]
                } else {
                    //如果选择的值已有
                    if (this.selected[item.key].includes(option.value)) {
                        //删除选择的值
                        this.selected[item.key].splice(
                            this.selected[item.key].findIndex((s) => s === option.value),
                            1,
                        )
                        //当全删光时，把第一个选中
                        if (this.selected[item.key].length === 0) {
                            this.selected[item.key] = [item.options[0].value]
                        }
                    } else {
                        //未有值的时候，追加选中值
                        this.selected[item.key].push(option.value)
                        //当含有第一个的值的时候，把第一个删除
                        if (this.selected[item.key].includes(item.options[0].value)) {
                            this.selected[item.key].splice(
                                this.selected[item.key].findIndex((s) => s === item.options[0].value),
                                1,
                            )
                        }
                    }
                }
            } else {
                //单选时，如果点击了已有值就赋值
                if (!this.selected[item.key].includes(option.value)) {
                    this.selected[item.key] = [option.value]
                } else {
                    return false
                }
            }
            this.change()
        },
        change() {
            if (this.outputValueTypeToArray) {
                this.$emit('onChange', this.selected)
            } else {
                this.$emit('onChange', this.selectedString)
            }
        },
    },
}
</script>

<style scoped>
.sc-select-filter {
    width: 100%;
    display: flex;
    flex-wrap: wrap;
}

.sc-select-filter__item {
    display: flex;
    align-items: baseline;
    width: 50%;
}

.sc-select-filter__item-title {
    width: 6rem;
}

.sc-select-filter__item-title label {
    font-size: 1.1rem;
    padding-top: 1rem;
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding-right: 1rem;
    color: var(--el-color-info);
}

.sc-select-filter__item-options {
    flex: 1;
    border-bottom: 1px dashed var(--el-border-color-light);
    height: 3.5rem;
    overflow: hidden;
}

.sc-select-filter__item-options ul {
    display: flex;
    flex-wrap: wrap;
    padding-top: 1rem;
}

.sc-select-filter__item-options li {
    list-style: none;
    cursor: pointer;
    height: 2rem;
    padding: 0 1rem;
    border-radius: 2.5rem;
    margin: 0 1rem 1rem 0;
    display: flex;
    align-items: center;
    background: var(--el-color-primary-light-9);
}

.sc-select-filter__item-options li .el-icon {
    margin-right: 0.2rem;
    font-size: 1.2rem;
}

.sc-select-filter__item-options li:hover {
    color: var(--el-color-primary);
}

.sc-select-filter__item-options li.active {
    background: var(--el-color-primary);
    color: var(--el-color-white);
    font-weight: bold;
}

.sc-select-filter__item:last-of-type .sc-select-filter__item-options {
    border: 0;
}

.sc-select-filter__no-data {
    color: var(--el-color-info);
}

.sc-select-filter__item-w100p {
    width: 100%;
}
</style>
<style>
.badge-small {
    font-size: 0.8rem;
    height: 1.3rem;
}
</style>