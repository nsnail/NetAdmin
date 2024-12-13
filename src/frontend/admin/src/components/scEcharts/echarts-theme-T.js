import colorTool from '@/utils/color'

export default {
    async build() {
        const style = getComputedStyle(document.documentElement)
        let el_color_primary
        while ((el_color_primary = style.getPropertyValue('--el-color-primary')) === 'rgba(0, 0, 0, 0)') {
            await new Promise((x) => setTimeout(x, 100))
        }
        const el_color_info = style.getPropertyValue('--el-color-info')
        const el_text_color_primary = style.getPropertyValue('--el-text-color-primary')
        const el_border_color_lighter = style.getPropertyValue('--el-border-color-lighter')
        return {
            color: [el_color_primary, colorTool.lighten(el_color_primary, 0.5), ...colorTool.colorSets],
            grid: {
                left: '3%',
                right: '3%',
                bottom: '10',
                top: '40',
                containLabel: true,
            },
            pie: {
                label: {
                    color: el_color_info,
                },
            },
            gauge: {
                axisTick: {
                    lineStyle: {
                        color: el_color_info,
                    },
                },
                splitLine: {
                    lineStyle: {
                        color: el_color_info,
                    },
                },
                axisLabel: {
                    color: el_color_info,
                },
                title: {
                    color: el_color_info,
                },
            },
            legend: {
                textStyle: {
                    color: el_color_info,
                },
                inactiveColor: 'rgba(128,128,128,0.4)',
            },
            categoryAxis: {
                axisLine: {
                    show: true,
                    lineStyle: {
                        color: 'rgba(128,128,128,0.2)',
                        width: 1,
                    },
                },
                axisTick: {
                    show: false,
                    lineStyle: {
                        color: el_text_color_primary,
                    },
                },
                axisLabel: {
                    color: el_color_info,
                },
                splitLine: {
                    show: false,
                    lineStyle: {
                        color: [el_border_color_lighter],
                    },
                },
                splitArea: {
                    show: false,
                    areaStyle: {
                        color: ['rgba(255,255,255,0.01)', 'rgba(0,0,0,0.01)'],
                    },
                },
            },
            valueAxis: {
                axisLine: {
                    show: false,
                    lineStyle: {
                        color: el_color_info,
                    },
                },
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: 'rgba(128,128,128,0.2)',
                    },
                },
            },
        }
    },
}