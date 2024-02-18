cd ..
$types = @{
    '1' = @('major', '主版本')
    '2' = @('minor', '次版本')
    '3' = @('patch', '修订版本')
}
$prefix = ''
while ($null -eq $types[$prefix])
{
    $prefix = Read-Host "请选择版本类型`n" $( & { param($i) $i | ForEach-Object { "$_ : $( $types[$_][0] )（$( $types[$_][1] )）`n" } } $types.Keys | Sort-Object )
}
git checkout main
git branch -D release
git checkout -b release
./node_modules/.bin/standard-version -r $types[$prefix][0]
cd ./scripts
./code.clean.ps1
git commit --amend --no-edit -a
$tag = $(git describe --tags $(git rev-list --tags --max-count=1))
git tag -d $tag
git tag $tag
git push --tags origin release
Start-Process -FilePath "https://github.com/nsnail/NetAdmin/compare/main...release"
Write-Host "按『Enter』回到主分支，『Ctrl+C』退出"
Pause
git checkout main
git pull
git branch -D release