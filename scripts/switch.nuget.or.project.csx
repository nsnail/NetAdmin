using System.Text.RegularExpressions;
string input = string.Empty;
while (!new[] { "1", "2" }.Contains(input))
{
    Console.WriteLine("1.nuget 2.project");
    input = Console.ReadLine();
}
var slnFile = Directory.GetFiles(@"../", "*.sln").First();
var csprojFiles = Directory.GetFiles(@"../src", "*.csproj", new EnumerationOptions { RecurseSubdirectories = true });
var slnContent = File.ReadAllText(slnFile);

if (input == "1")
{
    slnContent = Regex.Replace(slnContent, "\\nProject\\((.*)#refs", "\n##Project($1#refs");
    slnContent = Regex.Replace(slnContent, "\\nEndProject#refs", "\n##EndProject#refs");
    foreach (Match m in Regex.Matches(slnContent, "\"(\\{[A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12}\\})\"#refs"))
    {
        slnContent = slnContent.Replace($" {m.Groups[1].Value}.", $" ##{m.Groups[1].Value}.");
    }
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
    slnContent = Regex.Replace(slnContent, "##", "");
    foreach (var csprojFile in csprojFiles)
    {
        var csprojContent = File.ReadAllText(csprojFile);
        csprojContent = Regex.Replace(csprojContent," <!--<ProjectReference(.*)Label=\"refs\"(.*)>-->", " <ProjectReference$1Label=\"refs\"$2>");
        csprojContent = Regex.Replace(csprojContent," <PackageReference(.*)Label=\"refs\"(.*)>", " <!--<PackageReference$1Label=\"refs\"$2>-->");
        File.WriteAllText(csprojFile, csprojContent);
    }
}


Console.WriteLine(slnContent);
File.WriteAllText(slnFile, slnContent);