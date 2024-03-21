// .commitlintrc.js
/** @type {import('cz-git').UserConfig} */
module.exports = {
  rules: {
    // @see: https://commitlint.js.org/#/reference-rules
  },
  prompt: {
    alias: { fd: 'docs: fix typos' },
    messages: {
      type: '选择你要提交的类型 :',
      scope: '选择一个提交范围（可选）:',
      customScope: '请输入自定义的提交范围 :',
      subject: '填写简短精炼的变更描述 :\n',
      body: '填写更加详细的变更描述（可选）。使用 "|" 换行 :\n',
      breaking: '列举非兼容性重大的变更（可选）。使用 "|" 换行 :\n',
      footerPrefixesSelect: '选择关联issue前缀（可选）:',
      customFooterPrefix: '输入自定义issue前缀 :',
      footer: '列举关联issue (可选) 例如: #31, #I3244 :\n',
      confirmCommit: '是否提交或修改commit ?'
    },
    types: [
      { value: 'wip', name: 'wip:        开发之中 | In development ', emoji:'🧠' },
      { value: 'feat', name: 'feat:       新增功能 | A new feature', emoji:'✨' },
      { value: 'fix', name: 'fix:        修复缺陷 | A bug fix', emoji:'🐛' },
      { value: 'docs', name: 'docs:       文档更新 | Documentation only changes', emoji:'📝' },
      { value: 'style', name: 'style:      代码格式 | Changes that do not affect the meaning of the code', emoji:'💄' },
      { value: 'refactor', name: 'refactor:   代码重构 | A code change that neither fixes a bug nor adds a feature', emoji:'♻️' },
      { value: 'perf', name: 'perf:       性能提升 | A code change that improves performance', emoji:'⚡' },
      { value: 'test', name: 'test:       测试相关 | Adding missing tests or correcting existing tests', emoji:'✅' },
      { value: 'build', name: 'build:      构建相关 | Changes that affect the build system or external dependencies', emoji:'📦' },
      { value: 'ci', name: 'ci:         持续集成 | Changes to our CI configuration files and scripts', emoji:'🎡' },
      { value: 'revert', name: 'revert:     回退代码 | Revert to a commit', emoji:'⏪' },
      { value: 'chore', name: 'chore:      其他修改 | Other changes that do not modify src or test files', emoji:'🔨' },
    ],
    useEmoji: true,
    emojiAlign: 'center',
    useAI: false,
    aiNumber: 1,
    themeColorCode: '',
    scopes: [],
    allowCustomScopes: true,
    allowEmptyScopes: true,
    customScopesAlign: 'bottom',
    customScopesAlias: 'custom',
    emptyScopesAlias: 'empty',
    upperCaseSubject: false,
    markBreakingChangeMode: false,
    allowBreakingChanges: ['feat', 'fix'],
    breaklineNumber: 100,
    breaklineChar: '|',
    skipQuestions: [],
    issuePrefixes: [
      // 如果使用 gitee 作为开发管理
      { value: 'link', name: 'link:     链接 ISSUES 进行中' },
      { value: 'closed', name: 'closed:   标记 ISSUES 已完成' }
    ],
    customIssuePrefixAlign: 'top',
    emptyIssuePrefixAlias: 'skip',
    customIssuePrefixAlias: 'custom',
    allowCustomIssuePrefix: true,
    allowEmptyIssuePrefix: true,
    confirmColorize: true,
    maxHeaderLength: Infinity,
    maxSubjectLength: Infinity,
    minSubjectLength: 0,
    scopeOverrides: undefined,
    defaultBody: '[skip ci]',
    defaultIssues: '',
    defaultScope: '',
    defaultSubject: ''
  }
}