using System.Text.RegularExpressions;

var slnFile = Directory.GetFiles(@"../", "*.sln").First();
var content = File.ReadAllText(slnFile);

content = Regex.Replace(
    content,
    "Project\\(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\"\\) = \"meta\", \"meta\", \"{5198A03D-0CAC-4828-A807-34A693F73859}\"(?:.|\n)*?EndProject",
    $$"""
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "meta", "meta", "{5198A03D-0CAC-4828-A807-34A693F73859}"
{{'\t'}}ProjectSection(SolutionItems) = preProject
{{string.Join('\n',
             Directory.GetFiles(@"../", "*").Where(x => !x.EndsWith(".sln") && !x.EndsWith(".user"))
                      .Select(x=>$"\t\t{Path.GetFileName(x)} = {Path.GetFileName(x)}")
                      )}}
{{'\t'}}EndProject
"""
);
content = Regex.Replace(
    content,
    "Project\\(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\"\\) = \"tools\", \"tools\", \"{2D81D62C-1B2E-4758-84C6-728343CB734F}\"(?:.|\n)*?EndProject",
    $$"""
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "tools", "tools", "{2D81D62C-1B2E-4758-84C6-728343CB734F}"
{{'\t'}}ProjectSection(SolutionItems) = preProject
{{string.Join('\n',
             Directory.GetFiles(@"../tools", "*").Where(x => !x.EndsWith(".sln") && !x.EndsWith(".user"))
                      .Select(x=>$"\t\t{Path.GetFileName(x)} = tools/{Path.GetFileName(x)}")
                      )}}
{{'\t'}}EndProject
"""
);
Console.WriteLine(content);
File.WriteAllText(slnFile, content);