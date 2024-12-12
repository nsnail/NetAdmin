export default {
    //生成svg矩形
    createSVG(color, name) {
        const svg = document.createElement('svg')
        svg.setAttribute('xmlns', 'http://www.w3.org/2000/svg')

        svg.setAttribute('width', 50)
        svg.setAttribute('height', 50)

        // <rect> background
        const rect = document.createElement('rect')
        rect.setAttribute('fill', color)
        rect.setAttribute('x', 0)
        rect.setAttribute('y', 0)
        rect.setAttribute('width', '100%')
        rect.setAttribute('height', '100%')

        svg.appendChild(rect)

        // <text> name
        const text = document.createElement('text')

        text.setAttribute('fill', 'white')
        text.setAttribute('x', '50%')
        text.setAttribute('y', '50%')
        text.setAttribute('text-anchor', 'middle')
        text.setAttribute('font-size', '16')
        text.setAttribute('font-weight', '900')

        // IE/Edge don't support alignment-baseline
        // @see https://msdn.microsoft.com/en-us/library/gg558060(v=vs.85).aspx
        if (document.documentMode || /Edge/.test(navigator.userAgent)) {
            text.setAttribute('dy', '0.35em')
        } else {
            text.setAttribute('alignment-baseline', 'middle')
        }
        text.textContent = name
        svg.appendChild(text)
        return svg
    },
}