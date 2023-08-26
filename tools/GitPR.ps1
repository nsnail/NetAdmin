$types = @{
    '1' = @('FEA', '新增特性')
    '2' = @('REF', '项目重构')
    '3' = @('FIX', '缺陷修复')
    '4' = @('PER', '性能优化')
    '5' = @('RVT', '还原变更')
    '6' = @('FMT', '格式整理')
    '7' = @('DOC', '文档变更')
    '8' = @('TST', '单元测试')
    '9' = @('BLD', '工程构建')
}
git add ../
$prefix = ''
while ($null -eq $types[$prefix])
{
    $prefix = Read-Host "请选择提交类型`n" $( & { param($i) $i | ForEach-Object { "$_ : $( $types[$_][0] )（$( $types[$_][1] )）`n" } } $types.Keys | Sort-Object )
}
git commit -m "[$($types[$prefix][0])] $(($(Read-Host '是否跳过自动构建？（y/N）') -eq 'y') ? '[SKIP CI] ': '')$(Read-Host '请输入提交消息')"
$branch = $(git branch --show-current)
& './DotClean.cmd'
git add ../
git commit --amend --no-edit
git pull
git push --set-upstream origin $branch
Start-Process -FilePath "https://github.com/nsnail/NetAdmin/compare/main...$branch"
Pause