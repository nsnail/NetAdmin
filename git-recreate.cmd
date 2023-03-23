@echo off
chcp 65001
set /p b=input branch name:
git checkout dev
git pull
git branch -D %b%
git branch %b%
git checkout %b%