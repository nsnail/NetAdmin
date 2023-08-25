module.exports = {
    root: true,
    extends: ['plugin:vue/vue3-essential', 'eslint:recommended', '@vue/eslint-config-prettier'],
    rules: {
        'vue/multi-word-component-names': [
            'off',
            {
                ignores: [],
            },
        ],
        'vue/attributes-order': [
            'error',
            {
                order: [
                    'DEFINITION',
                    'LIST_RENDERING',
                    'CONDITIONALS',
                    'RENDER_MODIFIERS',
                    'GLOBAL',
                    ['UNIQUE', 'SLOT'],
                    'TWO_WAY_BINDING',
                    'OTHER_DIRECTIVES',
                    'OTHER_ATTR',
                    'EVENTS',
                    'CONTENT',
                ],
                alphabetical: false,
            },
        ],
        'vue/no-unused-vars': ['warn'],
        'no-return-await': ['warn'],
        'no-multiple-empty-lines': ['warn'],
        'no-inner-declarations': ['off'],
        eqeqeq: 'error',
    },
    env: {
        browser: true,
        es2021: true,
        node: true,
        'vue/setup-compiler-macros': true,
    },
    globals: {
        defineOptions: 'writable',
        defineProps: 'readonly',
        NodeJS: true,
    },
}