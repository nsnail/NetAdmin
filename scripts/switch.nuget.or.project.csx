using System.Text.RegularExpressions;
string input = string.Empty;
while (!new[] { "1", "2" }.Contains(input))
{
    Console.WriteLine("1.nuget 2.project");
    input = Console.ReadLine();
}
var slnxFile = Directory.GetFiles(@"../", "*.slnx").First();
var csprojFiles = Directory.GetFiles(@"../src", "*.csproj", new EnumerationOptions { RecurseSubdirectories = true });
var slnContent = File.ReadAllText(slnxFile);

if (input == "1")
{
    slnContent = Regex.Replace(slnContent, "<Project Type=\"Refs\"(.*)>", "<!--<Project Type=\"Refs\"$1>-->");
    foreach (var csprojFile in csprojFiles)
    {
        var csprojContent = File.ReadAllText(csprojFile);
        csprojContent = Regex.Replace(csprojContent," <ProjectReference(.*)Label=\"refs\"(.*)>", " <!--<ProjectReference$1Label=\"refs\"$2>-->");
        csprojContent = Regex.Replace(csprojContent," <!--<PackageReference(.*)Label=\"refs\"(.*)>-->", " <PackageReference$1Label=\"refs\"$2>");
        File.WriteAllText(csprojFile, csprojContent);
    }
}
else
{
    slnContent = Regex.Replace(slnContent, "<!--(.*)-->", "$1");
    foreach (var csprojFile in csprojFiles)
    {
        var csprojContent = File.ReadAllText(csprojFile);
        csprojContent = Regex.Replace(csprojContent," <!--<ProjectReference(.*)Label=\"refs\"(.*)>-->", " <ProjectReference$1Label=\"refs\"$2>");
        csprojContent = Regex.Replace(csprojContent," <PackageReference(.*)Label=\"refs\"(.*)>", " <!--<PackageReference$1Label=\"refs\"$2>-->");
        File.WriteAllText(csprojFile, csprojContent);
    }
}


Console.WriteLine(slnContent);
File.WriteAllText(slnxFile, slnContent);