<template>
    <div class="sc-search">
        <el-input
            v-model="input"
            :placeholder="$t('搜索')"
            :trigger-on-focus="false"
            @input="inputChange"
            @keydown="inputKeyDown"
            autofocus
            clearable
            prefix-icon="el-icon-search"
            ref="input"
            size="large" />
        <div v-if="history.length > 0" class="sc-search-history">
            <el-tag
                v-for="(item, index) in history"
                :key="item"
                @click="historyClick(item)"
                @close="historyClose(index)"
                closable
                effect="dark"
                type="info"
                >{{ item }}
            </el-tag>
        </div>
        <div class="sc-search-result">
            <div v-if="result.length <= 0" class="sc-search-no-result">{{ $t('暂无搜索结果') }}</div>
            <ul v-else>
                <el-scrollbar max-height="30rem">
                    <li
                        v-for="(item, i) in result"
                        :class="{ 'sc-search-result-li-hover': this.cursor.index === i }"
                        :key="item.path"
                        @click="to(item)">
                        <el-icon>
                            <component :is="item.icon || 'el-icon-menu'" />
                        </el-icon>
                        <span class="title">{{ item.breadcrumb }}</span>
                    </li>
                </el-scrollbar>
            </ul>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            cursor: {
                index: 0,
            },
            input: '',
            menu: [],
            result: [],
            history: [],
        }
    },
    watch: {
        result() {
            this.cursor.index = 0
        },
    },
    async mounted() {
        this.history = this.$TOOL.data.get('SEARCH_HISTORY') || []
        this.filterMenu(this.$GLOBAL.menu)
        await this.$nextTick()
        await new Promise((x) => setTimeout(x, 200))
        this.$refs.input.focus()
    },
    methods: {
        inputKeyDown(e) {
            if (e.keyCode === 13) {
                document
                    .getElementsByClassName('sc-search-result')[0]
                    ?.getElementsByClassName('el-scrollbar__view')[0]
                    ?.children[this.cursor.index]?.dispatchEvent(
                        new MouseEvent('click', {
                            view: window,
                            bubbles: true,
                            cancelable: false,
                        }),
                    )
                return
            }
            if (e.keyCode === 40) {
                this.cursor.index = ++this.cursor.index % this.result.length
                e.preventDefault()
                return
            }
            if (e.keyCode === 38) {
                this.cursor.index = --this.cursor.index % this.result.length
                if (this.cursor.index < 0) {
                    this.cursor.index += this.result.length
                }
                e.preventDefault()
                return
            }
        },
        inputChange(value) {
            if (value) {
                this.result = this.menuFilter(value)
            } else {
                this.result = []
            }
        },
        filterMenu(map) {
            map.forEach((item) => {
                if (item.meta.hidden || item.meta.type === 'button') {
                    return false
                }
                if (item.meta.type === 'iframe') {
                    item.path = `/i/${item.name}`
                }
                if (item.children && item.children.length > 0 && !item.component) {
                    this.filterMenu(item.children)
                } else {
                    this.menu.push(item)
                }
            })
        },
        menuFilter(queryString) {
            const res = []
            //过滤菜单树
            const filterMenu = this.menu.filter((item) => {
                if (item.meta.title.toLowerCase().indexOf(queryString.toLowerCase()) >= 0) {
                    return true
                }
                if (item.name.toLowerCase().indexOf(queryString.toLowerCase()) >= 0) {
                    return true
                }
            })
            //匹配系统路由
            const router = this.$router.getRoutes()
            const filterRouter = filterMenu.map((m) => {
                if (m.meta.type === 'link') {
                    return router.find((r) => r.path === '/' + m.path)
                } else {
                    return router.find((r) => r.path === m.path)
                }
            })
            //重组对象
            filterRouter.forEach((item) => {
                res.push({
                    name: item.name,
                    type: item.meta.type,
                    path: item.meta.type === 'link' ? item.path.slice(1) : item.path,
                    icon: item.meta.icon,
                    title: item.meta.title,
                    breadcrumb: item.meta.breadcrumb.map((v) => v.meta.title).join(' - '),
                })
            })
            return res
        },
        to(item) {
            if (!this.history.includes(this.input)) {
                this.history.push(this.input)
                this.$TOOL.data.set('SEARCH_HISTORY', this.history)
            }
            if (item.type === 'link') {
                setTimeout(() => {
                    let a = document.createElement('a')
                    a.style = 'display: none'
                    a.target = '_blank'
                    a.href = item.path
                    document.body.appendChild(a)
                    a.click()
                    document.body.removeChild(a)
                }, 10)
            } else {
                this.$router.push({ path: item.path })
            }
            this.$emit('success', true)
        },
        historyClick(text) {
            this.input = text
            this.inputChange(text)
        },
        historyClose(index) {
            this.history.splice(index, 1)
            if (this.history.length <= 0) {
                this.$TOOL.data.remove('SEARCH_HISTORY')
            } else {
                this.$TOOL.data.set('SEARCH_HISTORY', this.history)
            }
        },
    },
}
</script>

<style scoped>
.sc-search-no-result {
    text-align: center;
    margin: 3rem 0;
    color: var(--el-color-info);
}

.sc-search-history {
    display: flex;
    flex-wrap: wrap;
    gap: 0.5rem;
    margin-top: 1rem;
}

.sc-search-history .el-tag {
    cursor: pointer;
}

.sc-search-result {
    margin-top: 1rem;
}

.sc-search-result li {
    height: 4rem;
    padding: 0 1rem;
    background: var(--el-bg-color-overlay);
    border: 1px solid var(--el-border-color-light);
    list-style: none;
    border-radius: 0.4rem;
    margin-bottom: 0.4rem;
    font-size: 1.1rem;
    display: flex;
    align-items: center;
    cursor: pointer;
}

.sc-search-result li i {
    font-size: 1.5rem;
    margin-right: 1rem;
}

.sc-search-result-li-hover,
.sc-search-result li:hover {
    background: var(--el-color-primary) !important;
    color: var(--el-color-white) !important;
    border-color: var(--el-color-primary) !important;
}
</style>