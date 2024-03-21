$branch = $( git branch --show-current )
git add ../
$skipFormat = Read-Host "输入 n 跳过代码整理"
if ($skipFormat -ne "n")
{
    ./code.clean.ps1
}
git add ../
../node_modules/.bin/git-cz.ps1
git pull
git push --set-upstream origin $branch
Start-Process -FilePath "https://github.com/nsnail/NetAdmin/compare/main...$branch"
Write-Host "按『Enter』重建分支，『Ctrl+C』退出"
Pause
./3.git.recreate.branch.ps1