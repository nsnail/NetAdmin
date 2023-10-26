#r "nuget: NSExt, 1.1.0"
using NSExt.Extensions;

Console.WriteLine("请输入原始名称（NetAdmin）：");
var oldName = Console.ReadLine().NullOrEmpty("NetAdmin");
Console.WriteLine("请输入替换名称：");
var newName = Console.ReadLine();
foreach (var path in Directory.EnumerateDirectories("./", $"*{oldName}*",
             SearchOption.AllDirectories))
{
    Console.Write($"{path} --> ");
    var newPath = path.Replace(oldName, newName);
    Directory.Move(path, newPath);
    Console.WriteLine(newPath);
}


Console.WriteLine();
foreach (var path in Directory.EnumerateFiles("./", $"*.*", SearchOption.AllDirectories))
{
    File.WriteAllText(path, File.ReadAllText(path).Replace(oldName, newName));
    var newPath = path.Replace(oldName, newName);
    if (newPath == path) continue;
    Console.Write($"{path} --> ");
    Directory.Move(path, newPath);
    Console.WriteLine(newPath);
}