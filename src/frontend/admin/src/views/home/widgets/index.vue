<template>
    <div :class="['widgets-home', customizing ? 'customizing' : '']" ref="main">
        <div class="widgets-content">
            <div class="widgets" ref="widgets">
                <div class="widgets-wrapper">
                    <div v-if="nowCompsList?.length <= 0" class="no-widgets">
                        <el-empty :description="$t('没有部件啦')" :image-size="280" />
                    </div>
                    <el-row :gutter="15">
                        <el-col v-bind:key="index" v-for="(item, index) in grid.layout" :md="item" :xs="24">
                            <draggable
                                v-model="grid.compsList[index]"
                                animation="200"
                                class="draggable-box"
                                dragClass="aaaaa"
                                fallbackOnBody
                                force-fallback
                                group="people"
                                handle=".customize-overlay"
                                item-key="com">
                                <template #item="{ element }">
                                    <div class="widgets-item">
                                        <component :is="allComps[element]" />
                                        <div v-if="customizing" class="customize-overlay">
                                            <el-button @click="remove(element)" class="close" icon="el-icon-close" plain size="small" type="danger" />
                                            <label>
                                                <el-icon>
                                                    <component :is="allComps[element].icon" />
                                                </el-icon>
                                                {{ $t(allComps[element].title) }}</label
                                            >
                                        </div>
                                    </div>
                                </template>
                            </draggable>
                        </el-col>
                    </el-row>
                </div>
            </div>
        </div>
        <div v-if="customizing" class="widgets-aside">
            <el-container>
                <el-header>
                    <div class="widgets-aside-title">
                        <el-icon>
                            <el-icon-circle-plus-filled />
                        </el-icon>
                        {{ $t('添加部件') }}
                    </div>
                    <div>
                        <el-button v-if="customizing" @click="save" icon="el-icon-check" round type="primary">{{ $t('完成') }}</el-button>
                    </div>
                    <div @click="close()" class="widgets-aside-close">
                        <el-icon>
                            <el-icon-close />
                        </el-icon>
                    </div>
                </el-header>
                <el-header style="height: auto">
                    <div class="selectLayout">
                        <layout
                            v-for="l in layouts"
                            :active="grid.layout.join(',') === l.replaceAll('-', ',')"
                            :layout="l"
                            @onSetLayout="setLayout" />
                        <layout
                            v-for="l in customLayouts"
                            :active="grid.layout.join(',') === l.replaceAll('-', ',')"
                            :layout="l"
                            @onSetLayout="setLayout" />
                    </div>
                </el-header>
                <el-header style="height: auto">
                    <el-button @click="this.dialog.customLayout = { title: this.$t('添加自定义布局') }" style="margin: 0 auto">
                        {{ this.$t('添加自定义布局') }}
                    </el-button>
                </el-header>
                <el-main class="nopadding">
                    <div class="widgets-list">
                        <div v-if="myCompsList.length <= 0" class="widgets-list-nodata">
                            <el-empty :description="$t('没有部件啦')" :image-size="60" />
                        </div>
                        <div v-for="item in myCompsList" :key="item.title" class="widgets-list-item">
                            <div class="item-logo">
                                <el-icon>
                                    <component :is="item.icon" />
                                </el-icon>
                            </div>
                            <div class="item-info">
                                <h2>{{ item.title }}</h2>
                                <p>{{ item.description }}</p>
                            </div>
                            <div class="item-actions">
                                <el-button @click="push(item)" icon="el-icon-plus" size="small" type="primary" />
                            </div>
                        </div>
                    </div>
                </el-main>
                <el-footer>
                    <el-button @click="backDefault()" size="small">{{ $t('恢复默认') }}</el-button>
                </el-footer>
            </el-container>
        </div>
    </div>

    <div @click="custom" class="layout-setting">
        <el-icon>
            <el-icon-setting />
        </el-icon>
    </div>

    <custom-layout-dialog
        v-if="dialog.customLayout"
        @closed="dialog.customLayout = null"
        @mounted="$refs.customLayoutDialog.open(dialog.customLayout)"
        @onCustomLayout="(l) => (customLayouts = [l])"
        ref="customLayoutDialog" />
</template>

<script>
import draggable from 'vuedraggable'
import allComps from './components'
import { defineAsyncComponent } from 'vue'
const customLayoutDialog = defineAsyncComponent(() => import('./dialog/custom-layout-dialog'))
const layout = defineAsyncComponent(() => import('./components/components/layout'))

export default {
    components: {
        draggable,
        customLayoutDialog,
        layout,
    },
    data() {
        return {
            customizing: false,
            allComps: allComps,
            selectLayout: [],
            defaultGrid: this.$CONFIG.APP_SET_HOME_GRID,
            grid: [],
            layouts: ['12,6,6', '24-12,12', '24-16,8', '24-24-24'],
            customLayouts: [],
            dialog: {},
        }
    },
    created() {
        const roleDefaultGrid = this.$GLOBAL.user.roles.filter((x) => x.displayDashboard).sort((x, y) => y.id - x.id)[0].dashboardLayout
        if (roleDefaultGrid) {
            this.defaultGrid = JSON.parse(roleDefaultGrid)
        }
        this.loadGrid()
    },
    mounted() {
        this.$nextTick(() => this.$emit('on-mounted'))
    },
    computed: {
        allCompsList() {
            const allCompsList = []
            for (const key in this.allComps) {
                allCompsList.push({
                    key: key,
                    title: allComps[key].title,
                    icon: allComps[key].icon,
                    description: allComps[key].description,
                })
            }
            const myCompsList = this.grid.compsList.reduce(function (a, b) {
                return a.concat(b)
            })
            for (let comp of allCompsList) {
                const _item = myCompsList.find((item) => {
                    return item === comp.key
                })
                if (_item) {
                    comp.disabled = true
                }
            }
            return allCompsList
        },
        myCompsList() {
            const myGrid = this.$TOOL.data.get('DASHBOARD_GRID')
            return this.allCompsList.filter((item) => !item.disabled && !myGrid?.includes(item.key))
        },
        nowCompsList() {
            return this.grid.compsList?.reduce(function (a, b) {
                return a.concat(b)
            })
        },
    },
    methods: {
        //开启自定义
        custom() {
            this.customizing = !this.customizing
            const oldWidth = this.$refs.widgets.offsetWidth
            this.$nextTick(() => {
                const scale = this.$refs.widgets.offsetWidth / oldWidth
                this.$refs.widgets.style.setProperty('transform', `scale(${this.customizing ? scale : 1})`)
            })
            this.$emit('on-customizing', this.customizing)
        },
        //设置布局
        setLayout(layout) {
            this.grid.layout = layout

            // 初始化
            layout.forEach((_, i) => {
                if (!this.grid.compsList[i]) this.grid.compsList[i] = []
            })

            if (layout.join(',') === '24') {
                this.grid.compsList[0] = [...this.grid.compsList[0], ...this.grid.compsList[1], ...this.grid.compsList[2]]
                this.grid.compsList[1] = []
                this.grid.compsList[2] = []
            }
        },
        //追加
        push(item) {
            let target = this.grid.compsList[0]
            target.push(item.key)
        },
        //隐藏组件
        remove(item) {
            const newCompsList = this.grid.compsList
            newCompsList.forEach((obj, index) => {
                newCompsList[index] = obj.filter((o) => o !== item)
            })
        },
        //保存
        save() {
            this.customizing = false
            this.$refs.widgets.style.removeProperty('transform')
            this.$TOOL.data.set('APP_SET_HOME_GRID', this.grid)
            this.$emit('on-customizing', this.customizing)
        },
        //恢复默认
        backDefault() {
            this.customizing = false
            this.$refs.widgets.style.removeProperty('transform')
            this.grid = JSON.parse(JSON.stringify(this.defaultGrid))
            this.$TOOL.data.remove('APP_SET_HOME_GRID')
            this.$emit('on-customizing', this.customizing)
        },
        //关闭
        close() {
            this.customizing = false
            this.$refs.widgets.style.removeProperty('transform')
            this.loadGrid()
            this.$emit('on-customizing', this.customizing)
        },
        loadGrid() {
            this.grid = this.$TOOL.data.get('APP_SET_HOME_GRID') || JSON.parse(JSON.stringify(this.defaultGrid))
        },
    },
}
</script>

<style lang="scss" scoped>
.widgets-home {
    display: flex;
    flex-direction: row;
    flex: 1;
    height: 100%;
}

.widgets-content {
    flex: 1;
    overflow: auto;
    overflow-x: hidden;
    font-family: 'Lucida Console', 'Microsoft YaHei', monospace;
}

.widgets-aside {
    width: 25rem;
    background: var(--el-color-white);
    position: relative;
    overflow: auto;
}

.widgets-aside-title {
    font-size: 1.1rem;
    display: flex;
    align-items: center;
    justify-content: center;
}

.widgets-aside-title i {
    margin-right: 0.8rem;
    font-size: 1.4rem;
}

.widgets-aside-close {
    font-size: 1.4rem;
    width: 2.5rem;
    height: 2.5rem;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 0.3rem;
    cursor: pointer;
}

.widgets-aside-close:hover {
    background: rgba(180, 180, 180, 0.1);
}

.widgets-top {
    margin-bottom: 1rem;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.widgets-top-title {
    font-size: 1.4rem;
    font-weight: bold;
}

.widgets {
    transform-origin: top left;
    transition: transform 0.15s;
}

.draggable-box {
    height: 100%;
}

.customizing .widgets-wrapper {
    margin-right: -25rem;
}

.customizing .widgets-wrapper .el-col {
    padding-bottom: 1rem;
}

.customizing .widgets-wrapper .draggable-box {
    border: 1px dashed var(--el-color-primary);
    padding: 1rem;
}

.customizing .widgets-wrapper .no-widgets {
    display: none;
}

.customizing .widgets-item {
    position: relative;
    margin-bottom: 1rem;
}

.customize-overlay {
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 1;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    background: rgba(255, 255, 255, 0.9);
    cursor: move;
}

.customize-overlay label {
    background: var(--el-color-primary);
    color: var(--el-color-white);
    height: 4rem;
    padding: 0 3rem;
    border-radius: 3rem;
    font-size: 1.4rem;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: move;
}

.customize-overlay label i {
    margin-right: 1rem;
    font-size: 2rem;
}

.customize-overlay .close {
    position: absolute;
    top: 1rem;
    right: 1rem;
}

.customize-overlay .close:focus,
.customize-overlay .close:hover {
    background: var(--el-button-hover-color);
}

.widgets-list-item {
    display: flex;
    flex-direction: row;
    padding: 1rem;
    align-items: center;
}

.widgets-list-item .item-logo {
    width: 3rem;
    height: 3rem;
    border-radius: 50%;
    background: rgba(180, 180, 180, 0.1);
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 1.4rem;
    margin-right: 1rem;
}

.widgets-list-item .item-info {
    flex: 1;
}

.widgets-list-item .item-info h2 {
    font-size: 1.2rem;
    font-weight: normal;
    cursor: default;
}

.widgets-list-item .item-info p {
    font-size: 0.9rem;
    color: var(--el-color-info);
    cursor: default;
}

.widgets-list-item:hover {
    background: rgba(180, 180, 180, 0.1);
}

.widgets-wrapper .sortable-ghost {
    opacity: 0.5;
}

.selectLayout {
    width: 100%;
    display: flex;
}

.dark {
    .customize-overlay {
        background: rgba(43, 43, 43, 0.9);
    }
}

@media (max-width: 77rem) {
    .customizing .widgets {
        transform: scale(1) !important;
    }
    .customizing .widgets-aside {
        width: 100%;
        position: absolute;
        top: 50%;
        right: 0;
        bottom: 0;
    }
    .customizing .widgets-wrapper {
        margin-right: 0;
    }
}
</style>