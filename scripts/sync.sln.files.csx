using System.Text.RegularExpressions;

var slnFile = Directory.GetFiles(@"../", "*.sln").First();
var content = File.ReadAllText(slnFile);

content = Regex.Replace(
    content,
    "Project\\(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\"\\) = \"meta\", \"meta\", \"{5198A03D-0CAC-4828-A807-34A693F73859}\"(?:.|\n)*?EndProject",
    $$"""
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "meta", "meta", "{5198A03D-0CAC-4828-A807-34A693F73859}"
    ProjectSection(SolutionItems) = preProject
{{string.Join('\n',
             Directory.GetFiles(@"../", "*").Where(x => !x.EndsWith(".sln") && !x.EndsWith(".user"))
                      .Select(x=>$"        {Path.GetFileName(x)} = {Path.GetFileName(x)}")
                      )}}
    EndProject
"""
);

content = Regex.Replace(
    content,
    "Project\\(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\"\\) = \"docker\", \"docker\", \"{E80A1018-C354-4A26-9029-8847BB9DA864}\"(?:.|\n)*?EndProject",
    $$"""
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "docker", "docker", "{E80A1018-C354-4A26-9029-8847BB9DA864}"
    ProjectSection(SolutionItems) = preProject
{{string.Join('\n',
             Directory.GetFiles(@"../docker", "*")
                      .Select(x=>$"        {Path.GetFileName(x)} = docker/{Path.GetFileName(x)}")
                      )}}
    EndProject
"""
);

content = Regex.Replace(
    content,
    "Project\\(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\"\\) = \"workflows\", \"workflows\", \"{3C6F049E-3EE8-4D66-9AFF-E8A369032487}\"(?:.|\n)*?EndProject",
    $$"""
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "workflows", "workflows", "{3C6F049E-3EE8-4D66-9AFF-E8A369032487}"
    ProjectSection(SolutionItems) = preProject
{{string.Join('\n',
             Directory.GetFiles(@"../.github/workflows", "*")
                      .Select(x=>$"        {Path.GetFileName(x)} = .github/workflows/{Path.GetFileName(x)}")
                      )}}
    EndProject
"""
);

content = Regex.Replace(
    content,
    "Project\\(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\"\\) = \"workflows\", \"workflows\", \"{BB5BB244-E7D2-4368-8C18-C1C0ED1E12D1}\"(?:.|\n)*?EndProject",
    $$"""
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "workflows", "workflows", "{BB5BB244-E7D2-4368-8C18-C1C0ED1E12D1}"
    ProjectSection(SolutionItems) = preProject
{{string.Join('\n',
             Directory.GetFiles(@"../.drone/workflows", "*")
                      .Select(x=>$"        {Path.GetFileName(x)} = .drone/workflows/{Path.GetFileName(x)}")
                      )}}
    EndProject
"""
);

content = Regex.Replace(
    content,
    "Project\\(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\"\\) = \"scripts\", \"scripts\", \"{BB0B25C9-0901-4923-913F-00F9A6B352A5}\"(?:.|\n)*?EndProject",
    $$"""
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "scripts", "scripts", "{BB0B25C9-0901-4923-913F-00F9A6B352A5}"
    ProjectSection(SolutionItems) = preProject
{{string.Join('\n',
             Directory.GetFiles(@"../scripts", "*")
                      .Select(x=>$"        {Path.GetFileName(x)} = scripts/{Path.GetFileName(x)}")
                      )}}
    EndProject
"""
);

content = Regex.Replace(
    content,
    "Project\\(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\"\\) = \"build\", \"build\", \"{8E4C93BA-9493-4892-80C4-5E174C504829}\"(?:.|\n)*?EndProject",
    $$"""
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "build", "build", "{8E4C93BA-9493-4892-80C4-5E174C504829}"
    ProjectSection(SolutionItems) = preProject
{{string.Join('\n',
             Directory.GetFiles(@"../build", "*")
                      .Select(x=>$"        {Path.GetFileName(x)} = build/{Path.GetFileName(x)}")
                      )}}
    EndProject
"""
);

Console.WriteLine(content);
File.WriteAllText(slnFile, content);