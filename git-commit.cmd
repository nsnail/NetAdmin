@echo off
chcp 65001
set /p m=input commit message:
git add .
git commit -m %m%