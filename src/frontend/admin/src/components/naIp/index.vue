<template>
    <p>{{ ip ?? '-' }}</p>
    <p style="overflow: hidden">{{ region ?? '-' }}</p>
</template>
<script>
import http from '@/utils/request'

export default {
    emits: [],
    props: ['ip'],
    data() {
        return {
            region: null,
        }
    },
    mounted() {},
    created() {
        if (this.ip) {
            this.region = '...'
            http.get(`http://ip.line92.com/?ip=${this.ip}`).then((x) => {
                this.region = x.region
            })
        }
    },
    components: {},
    computed: {},
    methods: {},
}
</script>
<style scoped></style>