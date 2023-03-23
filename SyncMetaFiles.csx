using System.Text.RegularExpressions;

var slnFile = Directory.GetFiles(@".", "*.sln").First();
var content = File.ReadAllText(slnFile);
content = Regex.Replace(
    content,
    @"ProjectSection\(SolutionItems\) = preProject(?:.|\n)*?EndProjectSection",
    $"""
ProjectSection(SolutionItems) = preProject
{string.Join('\n',
             Directory.GetFiles(@".", "*").Where(x => !x.EndsWith(".sln") && !x.EndsWith(".user"))
                      .Select(x=>$"\t\t{Path.GetFileName(x)} = {Path.GetFileName(x)}"))}
{'\t'}EndProjectSection
"""
);
Console.WriteLine(content);
File.WriteAllText(slnFile, content);