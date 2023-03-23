@echo off
chcp 65001
set /p b=input branch name:
set /p m=input commit message:
git add .
git commit -m %m%
powershell /c ./resharper-clean.ps1
powershell /c ./dot-clean.cmd
git add .
git commit -m style
git push --set-upstream origin %b%
start http://git.shequnpay.com/lingyun/NetAdmin/compare/dev...%b%