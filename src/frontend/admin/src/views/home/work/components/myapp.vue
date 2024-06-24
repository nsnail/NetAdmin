<template>
    <div>
        <ul class="myMods">
            <li v-for="mod in myMods" :key="mod.path" :style="{ background: mod.meta.color || '#909399' }">
                <a v-if="mod.meta.type === 'link'" :href="mod.path" target="_blank">
                    <el-icon>
                        <component :is="mod.meta.icon || `el-icon-menu`" />
                    </el-icon>
                    <p>{{ mod.meta.title }}</p>
                </a>
                <router-link v-else :to="{ path: mod.path }">
                    <el-icon>
                        <component :is="mod.meta.icon || `el-icon-menu`" />
                    </el-icon>
                    <p>{{ mod.meta.title }}</p>
                </router-link>
            </li>
            <li @click="addMods" class="modItem-add">
                <a href="javascript:void(0)">
                    <el-icon>
                        <el-icon-plus />
                    </el-icon>
                </a>
            </li>
        </ul>

        <el-drawer v-model="modsDrawer" :size="570" :title="$t('添加应用')" destroy-on-close>
            <div class="setMods">
                <h4>{{ $t('我的常用') }} ( {{ myMods.length }} )</h4>
                <draggable v-model="myMods" animation="200" group="people" item-key="path" tag="ul">
                    <template #item="{ element }">
                        <li :style="{ background: element.meta.color || '#909399' }">
                            <el-icon>
                                <component :is="element.meta.icon || el - icon - menu" />
                            </el-icon>
                            <p>{{ element.meta.title }}</p>
                        </li>
                    </template>
                </draggable>
            </div>
            <div class="setMods">
                <h4>{{ $t('全部应用') }} ( {{ filterMods.length }} )</h4>
                <draggable v-model="filterMods" :sort="false" animation="200" group="people" item-key="path" tag="ul">
                    <template #item="{ element }">
                        <li :style="{ background: element.meta.color || '#909399' }">
                            <el-icon>
                                <component :is="element.meta.icon || el - icon - menu" />
                            </el-icon>
                            <p>{{ element.meta.title }}</p>
                        </li>
                    </template>
                </draggable>
            </div>
            <template #footer>
                <el-button @click="modsDrawer = false">{{ $t('取消') }}</el-button>
                <el-button @click="saveMods" type="primary">{{ $t('保存') }}</el-button>
            </template>
        </el-drawer>
    </div>
</template>

<script>
import draggable from 'vuedraggable'

export default {
    components: {
        draggable,
    },
    data() {
        return {
            mods: [],
            myMods: [],
            myModsName: [],
            filterMods: [],
            modsDrawer: false,
        }
    },
    mounted() {
        this.getMods()
    },
    methods: {
        addMods() {
            this.modsDrawer = true
        },
        getMods() {
            //这里可用改为读取远程数据
            this.myModsName = this.$TOOL.data.get('APP_SET_MY_MODS') || []
            this.filterMenu(this.$GLOBAL.menu)
            this.myMods = this.mods.filter((item) => {
                return this.myModsName.includes(item.name)
            })
            this.filterMods = this.mods.filter((item) => {
                return !this.myModsName.includes(item.name)
            })
        },
        filterMenu(map) {
            map.forEach((item) => {
                if (item.meta.hidden || item.meta.type === 'button') {
                    return false
                }
                if (item.meta.type === 'iframe') {
                    item.path = `/i/${item.name}`
                }
                if (item.children && item.children.length > 0) {
                    this.filterMenu(item.children)
                } else {
                    this.mods.push(item)
                }
            })
        },
        saveMods() {
            this.$TOOL.data.set(
                'APP_SET_MY_MODS',
                this.myMods.map((v) => v.name),
            )
            this.$message.success(this.$t('设置常用成功'))
            this.modsDrawer = false
        },
    },
}
</script>

<style scoped>
.myMods {
    list-style: none;
    margin: -0.8rem;
}

.myMods li {
    display: inline-block;
    width: 7rem;
    height: 7rem;
    vertical-align: top;
    transition: all 0.3s ease;
    margin: 1rem;
    border-radius: 0.4rem;
}

.myMods li:hover {
    opacity: 0.8;
}

.myMods li a {
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    text-align: center;
    color: #fff;
}

.myMods li i {
    font-size: 2rem;
    color: #fff;
}

.myMods li p {
    font-size: 0.9rem;
    color: #fff;
    margin-top: 1rem;
    width: 100%;
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
}

.modItem-add {
    border: 1px dashed #ddd;
    cursor: pointer;
}

.modItem-add i {
    font-size: 2.3rem;
    color: #999 !important;
}

.modItem-add:hover,
.modItem-add:hover i {
    border-color: #21a675;
    color: #21a675 !important;
}

.setMods {
    padding: 0 1.5rem;
}

.setMods h4 {
    font-size: 1.1rem;
    font-weight: normal;
}

.setMods ul {
    margin: 1.5rem -0.4rem;
    min-height: 7rem;
}

.setMods li {
    display: inline-block;
    width: 6rem;
    height: 6rem;
    text-align: center;
    margin: 0.4rem;
    color: #fff;
    vertical-align: top;
    padding: 1rem 0.3rem 0.3rem;
    cursor: move;
    border-radius: 0.3rem;
}

.setMods li i {
    font-size: 1.5rem;
}

.setMods li p {
    font-size: 0.9rem;
    margin-top: 1rem;
}

.setMods li.sortable-ghost {
    opacity: 0.3;
}
</style>