$refs = ('https://github.com/nsnail/ns-ext.git', 'https://github.com/nsnail/Furion.git', 'https://github.com/nsnail/FreeSql.git')

foreach ($item in $refs)
{
    git clone --depth 1 --config "http.proxy=http://127.0.0.1:1081" $item "../refs/$( [regex]::Match($item, '/([^/]+)\.git$').Groups[1] )"
}