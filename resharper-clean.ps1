$files=$(foreach ($line in $(git diff head origin/dev --stat-width 200) | findstr '\|'){$line.split('\|')[0].trim()}) -join ';'
echo $files
dotnet jb cleanupcode --no-build --include="$files" ./NetAdmin.sln
dotnet script ./PushSign.csx