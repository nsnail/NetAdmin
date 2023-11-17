$branch = $( git branch --show-current )
./dot.clean.cmd
git add .
./node_modules/.bin/git-cz.ps1
git pull
git push --set-upstream origin $branch
Start-Process -FilePath "https://github.com/nsnail/NetAdmin/compare/main...$branch"
Write-Host "按『Enter』重建分支，『Ctrl+C』退出"
Pause
./git.rc.ps1