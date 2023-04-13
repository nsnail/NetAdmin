<template>
    <div ref="main" :class="['widgets-home', customizing ? 'customizing' : '']">
        <div class="widgets-content">
            <div class="widgets-top">
                <div class="widgets-top-title">控制台</div>
                <div class="widgets-top-actions">
                    <el-button
                        v-if="customizing"
                        icon="el-icon-check"
                        round
                        type="primary"
                        @click="save"
                        >完成
                    </el-button>
                    <el-button
                        v-else
                        icon="el-icon-edit"
                        round
                        type="primary"
                        @click="custom"
                        >自定义
                    </el-button>
                </div>
            </div>
            <div ref="widgets" class="widgets">
                <div class="widgets-wrapper">
                    <div v-if="nowCompsList.length <= 0" class="no-widgets">
                        <el-empty
                            :image-size="280"
                            description="没有部件啦"
                            image="img/no-widgets.svg"
                        ></el-empty>
                    </div>
                    <el-row :gutter="15">
                        <el-col
                            v-for="(item, index) in grid.layout"
                            v-bind:key="index"
                            :md="item"
                            :xs="24"
                        >
                            <draggable
                                v-model="grid.copmsList[index]"
                                animation="200"
                                class="draggable-box"
                                dragClass="aaaaa"
                                fallbackOnBody
                                force-fallback
                                group="people"
                                handle=".customize-overlay"
                                item-key="com"
                            >
                                <template #item="{ element }">
                                    <div class="widgets-item">
                                        <component
                                            :is="allComps[element]"
                                        ></component>
                                        <div
                                            v-if="customizing"
                                            class="customize-overlay"
                                        >
                                            <el-button
                                                class="close"
                                                icon="el-icon-close"
                                                plain
                                                size="small"
                                                type="danger"
                                                @click="remove(element)"
                                            ></el-button>
                                            <label>
                                                <el-icon>
                                                    <component
                                                        :is="
                                                            allComps[element]
                                                                .icon
                                                        "
                                                    />
                                                </el-icon>
                                                {{
                                                    allComps[element].title
                                                }}</label
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
                        添加部件
                    </div>
                    <div class="widgets-aside-close" @click="close()">
                        <el-icon>
                            <el-icon-close />
                        </el-icon>
                    </div>
                </el-header>
                <el-header style="height: auto">
                    <div class="selectLayout">
                        <div
                            :class="{
                                active: grid.layout.join(',') === '12,6,6',
                            }"
                            class="selectLayout-item item01"
                            @click="setLayout([12, 6, 6])"
                        >
                            <el-row :gutter="2">
                                <el-col :span="12"><span></span></el-col>
                                <el-col :span="6"><span></span></el-col>
                                <el-col :span="6"><span></span></el-col>
                            </el-row>
                        </div>
                        <div
                            :class="{
                                active: grid.layout.join(',') === '24,16,8',
                            }"
                            class="selectLayout-item item02"
                            @click="setLayout([24, 16, 8])"
                        >
                            <el-row :gutter="2">
                                <el-col :span="24"><span></span></el-col>
                                <el-col :span="16"><span></span></el-col>
                                <el-col :span="8"><span></span></el-col>
                            </el-row>
                        </div>
                        <div
                            :class="{ active: grid.layout.join(',') === '24' }"
                            class="selectLayout-item item03"
                            @click="setLayout([24])"
                        >
                            <el-row :gutter="2">
                                <el-col :span="24"><span></span></el-col>
                                <el-col :span="24"><span></span></el-col>
                                <el-col :span="24"><span></span></el-col>
                            </el-row>
                        </div>
                    </div>
                </el-header>
                <el-main class="nopadding">
                    <div class="widgets-list">
                        <div
                            v-if="myCompsList.length <= 0"
                            class="widgets-list-nodata"
                        >
                            <el-empty
                                :image-size="60"
                                description="没有部件啦"
                            ></el-empty>
                        </div>
                        <div
                            v-for="item in myCompsList"
                            :key="item.title"
                            class="widgets-list-item"
                        >
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
                                <el-button
                                    icon="el-icon-plus"
                                    size="small"
                                    type="primary"
                                    @click="push(item)"
                                ></el-button>
                            </div>
                        </div>
                    </div>
                </el-main>
                <el-footer style="height: 51px">
                    <el-button size="small" @click="backDefaul()"
                        >恢复默认
                    </el-button>
                </el-footer>
            </el-container>
        </div>
    </div>
</template>

<script>
import draggable from "vuedraggable";
import allComps from "./components";

export default {
    components: {
        draggable,
    },
    data() {
        return {
            customizing: false,
            allComps: allComps,
            selectLayout: [],
            defaultGrid: this.$CONFIG.DEFAULT_GRID,
            grid: [],
        };
    },
    created() {
        this.grid =
            this.$TOOL.data.get("grid") ||
            JSON.parse(JSON.stringify(this.defaultGrid));
    },
    mounted() {
        this.$emit("on-mounted");
    },
    computed: {
        allCompsList() {
            const allCompsList = [];
            for (let key in this.allComps) {
                allCompsList.push({
                    key: key,
                    title: allComps[key].title,
                    icon: allComps[key].icon,
                    description: allComps[key].description,
                });
            }
            const myCopmsList = this.grid.copmsList.reduce(function (a, b) {
                return a.concat(b);
            });
            for (let comp of allCompsList) {
                const _item = myCopmsList.find((item) => {
                    return item === comp.key;
                });
                if (_item) {
                    comp.disabled = true;
                }
            }
            return allCompsList;
        },
        myCompsList() {
            const myGrid = this.$TOOL.data.get("DASHBOARD_GRID");
            return this.allCompsList.filter(
                (item) => !item.disabled && myGrid.includes(item.key)
            );
        },
        nowCompsList() {
            return this.grid.copmsList.reduce(function (a, b) {
                return a.concat(b);
            });
        },
    },
    methods: {
        //开启自定义
        custom() {
            this.customizing = true;
            const oldWidth = this.$refs.widgets.offsetWidth;
            this.$nextTick(() => {
                const scale = this.$refs.widgets.offsetWidth / oldWidth;
                this.$refs.widgets.style.setProperty(
                    "transform",
                    `scale(${scale})`
                );
            });
        },
        //设置布局
        setLayout(layout) {
            this.grid.layout = layout;
            if (layout.join(",") === "24") {
                this.grid.copmsList[0] = [
                    ...this.grid.copmsList[0],
                    ...this.grid.copmsList[1],
                    ...this.grid.copmsList[2],
                ];
                this.grid.copmsList[1] = [];
                this.grid.copmsList[2] = [];
            }
        },
        //追加
        push(item) {
            let target = this.grid.copmsList[0];
            target.push(item.key);
        },
        //隐藏组件
        remove(item) {
            const newCopmsList = this.grid.copmsList;
            newCopmsList.forEach((obj, index) => {
                newCopmsList[index] = obj.filter((o) => o !== item);
            });
        },
        //保存
        save() {
            this.customizing = false;
            this.$refs.widgets.style.removeProperty("transform");
            this.$TOOL.data.set("grid", this.grid);
        },
        //恢复默认
        backDefaul() {
            this.customizing = false;
            this.$refs.widgets.style.removeProperty("transform");
            this.grid = JSON.parse(JSON.stringify(this.defaultGrid));
            this.$TOOL.data.remove("grid");
        },
        //关闭
        close() {
            this.customizing = false;
            this.$refs.widgets.style.removeProperty("transform");
        },
    },
};
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
    padding: 15px;
}

.widgets-aside {
    width: 360px;
    background: #fff;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    position: relative;
    overflow: auto;
}

.widgets-aside-title {
    font-size: 14px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.widgets-aside-title i {
    margin-right: 10px;
    font-size: 18px;
}

.widgets-aside-close {
    font-size: 18px;
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 3px;
    cursor: pointer;
}

.widgets-aside-close:hover {
    background: rgba(180, 180, 180, 0.1);
}

.widgets-top {
    margin-bottom: 15px;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.widgets-top-title {
    font-size: 18px;
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
    margin-right: -360px;
}

.customizing .widgets-wrapper .el-col {
    padding-bottom: 15px;
}

.customizing .widgets-wrapper .draggable-box {
    border: 1px dashed var(--el-color-primary);
    padding: 15px;
}

.customizing .widgets-wrapper .no-widgets {
    display: none;
}

.customizing .widgets-item {
    position: relative;
    margin-bottom: 15px;
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
    color: #fff;
    height: 40px;
    padding: 0 30px;
    border-radius: 40px;
    font-size: 18px;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: move;
}

.customize-overlay label i {
    margin-right: 15px;
    font-size: 24px;
}

.customize-overlay .close {
    position: absolute;
    top: 15px;
    right: 15px;
}

.customize-overlay .close:focus,
.customize-overlay .close:hover {
    background: var(--el-button-hover-color);
}

.widgets-list {
}

.widgets-list-item {
    display: flex;
    flex-direction: row;
    padding: 15px;
    align-items: center;
}

.widgets-list-item .item-logo {
    width: 40px;
    height: 40px;
    border-radius: 50%;
    background: rgba(180, 180, 180, 0.1);
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 18px;
    margin-right: 15px;
    color: #6a8bad;
}

.widgets-list-item .item-info {
    flex: 1;
}

.widgets-list-item .item-info h2 {
    font-size: 16px;
    font-weight: normal;
    cursor: default;
}

.widgets-list-item .item-info p {
    font-size: 12px;
    color: #999;
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

.selectLayout-item {
    width: 60px;
    height: 60px;
    border: 2px solid var(--el-border-color-light);
    padding: 5px;
    cursor: pointer;
    margin-right: 15px;
}

.selectLayout-item span {
    display: block;
    background: var(--el-border-color-light);
    height: 46px;
}

.selectLayout-item.item02 span {
    height: 30px;
}

.selectLayout-item.item02 .el-col:nth-child(1) span {
    height: 14px;
    margin-bottom: 2px;
}

.selectLayout-item.item03 span {
    height: 14px;
    margin-bottom: 2px;
}

.selectLayout-item:hover {
    border-color: var(--el-color-primary);
}

.selectLayout-item.active {
    border-color: var(--el-color-primary);
}

.selectLayout-item.active span {
    background: var(--el-color-primary);
}

.dark {
    .widgets-aside {
        background: #2b2b2b;
    }

    .customize-overlay {
        background: rgba(43, 43, 43, 0.9);
    }
}

@media (max-width: 992px) {
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