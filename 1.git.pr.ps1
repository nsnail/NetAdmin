$branch = $( git branch --show-current )
./dot.clean.cmd
git add .
git cz
git pull
git push --set-upstream origin $branch
Start-Process -FilePath "https://github.com/nsnail/NetAdmin/compare/main...$branch"
Write-Host "按 「 Enter 」 重建分支，「 Ctrl+C 」 退出"
Pause
git checkout main
git pull
git branch -D $branch
git branch $branch
git checkout $branch