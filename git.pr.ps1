$branch = $( git branch --show-current )
./dot.clean.cmd
git add .
git cz
git pull
git push --set-upstream origin $branch
Start-Process -FilePath "https://github.com/nsnail/NetAdmin/compare/main...$branch"
Pause
./git.rc.ps1