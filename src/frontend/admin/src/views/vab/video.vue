<template>
    <el-main>
        <el-alert style="margin-bottom: 20px" title="播放器使用来自西瓜视频的西瓜播放器xgplayer" type="success"></el-alert>
        <el-row :gutter="15">
            <el-col :lg="12">
                <el-card header="MP4点播" shadow="never">
                    <sc-video :options="options" :src="playUrl"></sc-video>
                </el-card>
            </el-col>
            <el-col :lg="12">
                <el-card header="HlS(m3u8)直播" shadow="never">
                    <el-row :gutter="1">
                        <el-col :span="12">
                            <sc-video :options="{ volume: 0 }" :src="liveUrl" isLive></sc-video>
                        </el-col>
                        <el-col :span="12">
                            <sc-video :options="{ volume: 0 }" :src="liveUrl_2" isLive></sc-video>
                        </el-col>
                    </el-row>
                    <el-row :gutter="1" style="margin-top: 1px">
                        <el-col :span="12">
                            <sc-video :options="{ volume: 0 }" :src="liveUrl_3" isLive></sc-video>
                        </el-col>
                        <el-col :span="12">
                            <sc-video :options="{ volume: 0 }" :src="liveUrl_4" isLive></sc-video>
                        </el-col>
                    </el-row>
                </el-card>
            </el-col>
        </el-row>
    </el-main>
</template>

<script>
import scVideo from '@/components/scVideo'

export default {
    name: 'scvideo',
    components: {
        scVideo,
    },
    data() {
        return {
            playUrl: 'https://fastly.jsdelivr.net/gh/scuiadmin/CDN/video/scui-player-demo-720.mp4',
            options: {
                poster: 'https://fastly.jsdelivr.net/gh/scuiadmin/CDN/video/scui-player-demo-1080_Moment.jpg',
                pip: true,
            },
            liveUrl: 'http://local.live.jxtvcn.com.cn/live-jxtv/tv_jxtv1.m3u8',
            liveUrl_2: 'http://local.live.jxtvcn.com.cn/live-jxtv/tv_jxtv6.m3u8',
            liveUrl_3: 'http://local.live.jxtvcn.com.cn/live-jxtv/tv_jxtv7.m3u8',
            liveUrl_4: 'http://local.live.jxtvcn.com.cn/live-jxtv/tv_jxtv8.m3u8',
        }
    },
    mounted() {},
    methods: {},
}
</script>

<style></style>