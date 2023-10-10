import { defineConfig } from 'vite'
import path from 'path'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
    base: './',
    plugins: [vue()],
    resolve: {
        alias: {
            // 设置路径
            '~': path.resolve(__dirname, './'),
            // 设置别名
            '@': path.resolve(__dirname, './src'),
        },
        extensions: ['.mjs', '.js', '.ts', '.jsx', '.tsx', '.json', '.vue'],
    },
    // vite 相关配置
    server: {
        port: 65020,
        host: true,
        open: false,
        proxy: {
            '/api': {
                target: 'http://localhost:65010/api',
                changeOrigin: true,
                rewrite: (p) => p.replace(/^\/api/, ''),
            },
        },
    },
    css: {
        postcss: {
            plugins: [
                {
                    postcssPlugin: 'internal:charset-removal',
                    AtRule: {
                        charset: (atRule) => {
                            if (atRule.name === 'charset') {
                                atRule.remove()
                            }
                        },
                    },
                },
            ],
        },
    },
    build: {
        emptyOutDir: true,
        outDir: '../../../dist/frontend/admin',
        assetsDir: 'assets',
        minify: 'terser',
        terserOptions: {
            compress: {
                drop_console: true,
                drop_debugger: true,
            },
        },
        rollupOptions: {
            // 拆包
            output: {
                chunkFileNames: 'js/[name]-[hash].js',
                entryFileNames: 'js/[name]-[hash].js',
                assetFileNames: '[ext]/[name]-[hash].[ext]',
                manualChunks(id) {
                    if (id.includes('node_modules')) {
                        return id.split('/node_modules/').pop()?.split('/')[0]
                    }
                },
                // 第三方库拆包
                // manualChunks: {
                //   xgplayer: ['xgplayer'],
                //   xlsx: ['xlsx'],
                //   tinymce: ['tinymce'],
                //   elicons: ['@element-plus/icons-vue']
                // }
            },
        },
    },
})