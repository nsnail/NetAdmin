<!--
 * @Description: 系统计划任务配置
 * @version: 1.2
 * @Author: sakuya
 * @Date: 2021年7月7日09:28:32
 * @LastEditors: sakuya
 * @LastEditTime: 2021年7月10日20:56:47
-->

<template>
    <el-main>
        <el-row :gutter="15">
            <el-col
                v-for="item in list"
                :key="item.id"
                :lg="6"
                :md="8"
                :sm="12"
                :xl="6"
                :xs="24"
            >
                <el-card class="task task-item" shadow="hover">
                    <h2>{{ item.title }}</h2>
                    <ul>
                        <li>
                            <h4>执行类</h4>
                            <p>{{ item.handler }}</p>
                        </li>
                        <li>
                            <h4>定时规则</h4>
                            <p>{{ item.cron }}</p>
                        </li>
                    </ul>
                    <div class="bottom">
                        <div class="state">
                            <el-tag v-if="item.state === '1'" size="small"
                                >准备就绪
                            </el-tag>
                            <el-tag
                                v-if="item.state === '-1'"
                                size="small"
                                type="info"
                                >停用
                            </el-tag>
                        </div>
                        <div class="handler">
                            <el-popconfirm
                                title="确定立即执行吗？"
                                @confirm="run(item)"
                            >
                                <template #reference>
                                    <el-button
                                        circle
                                        icon="el-icon-caret-right"
                                        type="primary"
                                    ></el-button>
                                </template>
                            </el-popconfirm>
                            <el-dropdown trigger="click">
                                <el-button
                                    circle
                                    icon="el-icon-more"
                                    plain
                                    type="primary"
                                ></el-button>
                                <template #dropdown>
                                    <el-dropdown-menu>
                                        <el-dropdown-item @click="edit(item)"
                                            >编辑
                                        </el-dropdown-item>
                                        <el-dropdown-item @click="logs(item)"
                                            >日志
                                        </el-dropdown-item>
                                        <el-dropdown-item
                                            divided
                                            @click="del(item)"
                                            >删除
                                        </el-dropdown-item>
                                    </el-dropdown-menu>
                                </template>
                            </el-dropdown>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col :lg="6" :md="8" :sm="12" :xl="6" :xs="24">
                <el-card class="task task-add" shadow="never" @click="add">
                    <el-icon>
                        <el-icon-plus />
                    </el-icon>
                    <p>添加计划任务</p>
                </el-card>
            </el-col>
        </el-row>
    </el-main>

    <save-dialog
        v-if="dialog.save"
        ref="saveDialog"
        @closed="dialog.save = false"
        @success="handleSuccess"
    ></save-dialog>

    <el-drawer
        v-model="dialog.logsVisible"
        :size="600"
        destroy-on-close
        direction="rtl"
        title="计划任务日志"
    >
        <logs></logs>
    </el-drawer>
</template>

<script>
import saveDialog from "./save";
import logs from "./logs";

export default {
    name: "task",
    components: {
        saveDialog,
        logs,
    },
    provide() {
        return {
            list: this.list,
        };
    },
    data() {
        return {
            dialog: {
                save: false,
                logsVisible: false,
            },
            list: [
                {
                    id: "1",
                    title: "清理服务器缓存",
                    handler: "cleanUpCacheHandler",
                    cron: "59 59 23 * * ? *",
                    state: "1",
                },
                {
                    id: "2",
                    title: "自动审核",
                    handler: "automaticAuditHandler",
                    cron: "0 0 * * * ? *",
                    state: "1",
                },
                {
                    id: "3",
                    title: "清理未实名用户",
                    handler: "deleteUserHandler",
                    cron: "0 0 0 * * ? *",
                    state: "-1",
                },
            ],
        };
    },
    mounted() {},
    methods: {
        add() {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open();
            });
        },
        edit(task) {
            this.dialog.save = true;
            this.$nextTick(() => {
                this.$refs.saveDialog.open("edit").setData(task);
            });
        },
        del(task) {
            this.$confirm(`确认删除 ${task.title} 计划任务吗？`, "提示", {
                type: "warning",
                confirmButtonText: "删除",
                confirmButtonClass: "el-button--danger",
            })
                .then(() => {
                    this.list.splice(
                        this.list.findIndex((item) => item.id === task.id),
                        1
                    );
                })
                .catch(() => {
                    //取消
                });
        },
        logs() {
            this.dialog.logsVisible = true;
        },
        run(task) {
            this.$message.success(`已成功执行计划任务：${task.title}`);
        },
        //本地更新数据
        handleSuccess(data, mode) {
            if (mode === "add") {
                data.id = new Date().getTime();
                this.list.push(data);
            } else if (mode === "edit") {
                this.list
                    .filter((item) => item.id === data.id)
                    .forEach((item) => {
                        Object.assign(item, data);
                    });
            }
        },
    },
};
</script>

<style scoped>
.task {
    height: 210px;
}

.task-item h2 {
    font-size: 15px;
    color: #3c4a54;
    padding-bottom: 15px;
}

.task-item li {
    list-style-type: none;
    margin-bottom: 10px;
}

.task-item li h4 {
    font-size: 12px;
    font-weight: normal;
    color: #999;
}

.task-item li p {
    margin-top: 5px;
}

.task-item .bottom {
    border-top: 1px solid #ebeef5;
    text-align: right;
    padding-top: 10px;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.task-add {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    text-align: center;
    cursor: pointer;
    color: #999;
}

.task-add:hover {
    color: #409eff;
}

.task-add i {
    font-size: 30px;
}

.task-add p {
    font-size: 12px;
    margin-top: 20px;
}

.dark .task-item .bottom {
    border-color: var(--el-border-color-light);
}
</style>