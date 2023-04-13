<template>
    <el-card v-loading="loading" header="实时收入" shadow="hover">
        <scEcharts ref="c1" :option="option" height="300px"></scEcharts>
    </el-card>
</template>

<script>
import scEcharts from "@/components/scEcharts";

export default {
    title: "实时收入",
    icon: "el-icon-data-line",
    description: "Echarts组件演示",
    components: {
        scEcharts,
    },
    data() {
        return {
            loading: true,
            option: {},
        };
    },
    created() {
        const _this = this;
        setTimeout(function () {
            _this.loading = false;
        }, 500);

        this.option = {
            tooltip: {
                trigger: "axis",
            },
            xAxis: {
                boundaryGap: false,
                type: "category",
                data: (function () {
                    let now = new Date();
                    const res = [];
                    let len = 30;
                    while (len--) {
                        res.unshift(
                            now.toLocaleTimeString().replace(/^\D*/, "")
                        );
                        now = new Date(now - 2000);
                    }
                    return res;
                })(),
            },
            yAxis: [
                {
                    type: "value",
                    name: "价格",
                    splitLine: {
                        show: false,
                    },
                },
            ],
            series: [
                {
                    name: "收入",
                    type: "line",
                    symbol: "none",
                    lineStyle: {
                        width: 1,
                        color: "#06c755",
                    },
                    areaStyle: {
                        opacity: 0.1,
                        color: "#79bbff",
                    },
                    data: (function () {
                        const res = [];
                        let len = 30;
                        while (len--) {
                            res.push(Math.round(0));
                        }
                        return res;
                    })(),
                },
            ],
        };
    },
    mounted() {
        const _this = this;
        setInterval(function () {
            const o = _this.option;

            o.series[0].data.shift();
            o.series[0].data.push(Math.round(Math.random() * 100));

            o.xAxis.data.shift();
            o.xAxis.data.push(
                new Date().toLocaleTimeString().replace(/^\D*/, "")
            );

            //_this.$refs.c1.myChart.setOption(o)
        }, 2100);
    },
};
</script>