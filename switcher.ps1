# https://github.com/RicoSuter/DNT#switch-to-projects
$targets = @{
    '1' = 'switch-to-projects'
    '2' = 'switch-to-packages'
}
$key = ''
while ($null -eq $targets[$key])
{
    $key = Read-Host '请选择：1（切换到项目引用） 2（切换到Nuget包引用）'
}
$files = Get-ChildItem Switcher.*.json
$file = 9999
while ($null -eq $files[[int]$file - 1])
{
    $i = 0
    Write-Host '请选择要切换的配置文件文件'
    foreach ($file in $files)
    {
        $i++
        Write-Host $i $file.Name
    }
    $file = Read-Host
}
$file = [int]$file - 1
Copy-Item $files[$file] 'switcher.json' -Force
dotnet dnt $targets[$key]
Remove-Item switcher.json