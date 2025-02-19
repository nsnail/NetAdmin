using System.Text.RegularExpressions;

var slnxFile = Directory.GetFiles(@"../", "*.slnx").First();
var content = File.ReadAllText(slnxFile);

content = Regex.Replace(
    content,
    "<Folder Name=\"/meta/\">(?:.|\n)*?</Folder>",
    $$"""
<Folder Name="/meta/">
{{string.Join('\n',
             Directory.GetFiles(@"../", "*").Where(x => !x.EndsWith(".slnx") && !x.EndsWith(".user"))
                      .Select(x=>$"        <File Path=\"{Path.GetFileName(x)}\"/>")
                      )}}
    </Folder>
"""
);

content = Regex.Replace(
    content,
    "<Folder Name=\"/docker/\">(?:.|\n)*?</Folder>",
    $$"""
<Folder Name="/docker/">
{{string.Join('\n',
             Directory.GetFiles(@"../docker", "*")
                      .Select(x=>$"        <File Path=\"docker/{Path.GetFileName(x)}\"/>")
                      )}}
    </Folder>
"""
);

content = Regex.Replace(
    content,
    "<Folder Name=\"/.github/workflows/\">(?:.|\n)*?</Folder>",
    $$"""
<Folder Name="/.github/workflows/">
{{string.Join('\n',
             Directory.GetFiles(@"../.github/workflows", "*")
                      .Select(x=>$"        <File Path=\".github/workflows/{Path.GetFileName(x)}\"/>")
                      )}}
    </Folder>
"""
);

content = Regex.Replace(
    content,
    "<Folder Name=\"/.drone/workflows/\">(?:.|\n)*?</Folder>",
    $$"""
<Folder Name="/.drone/workflows/">
{{string.Join('\n',
             Directory.GetFiles(@"../.drone/workflows", "*")
                      .Select(x=>$"        <File Path=\".drone/workflows/{Path.GetFileName(x)}\"/>")
                      )}}
    </Folder>
"""
);

content = Regex.Replace(
    content,
    "<Folder Name=\"/scripts/\">(?:.|\n)*?</Folder>",
    $$"""
<Folder Name="/scripts/">
{{string.Join('\n',
             Directory.GetFiles(@"../scripts", "*")
                      .Select(x=>$"        <File Path=\"scripts/{Path.GetFileName(x)}\"/>")
                      )}}
    </Folder>
"""
);



content = Regex.Replace(
    content,
    "<Folder Name=\"/build/\">(?:.|\n)*?</Folder>",
    $$"""
<Folder Name="/build/">
{{string.Join('\n',
             Directory.GetFiles(@"../build", "*")
                      .Select(x=>$"        <File Path=\"build/{Path.GetFileName(x)}\"/>")
                      )}}
    </Folder>
"""
);

Console.WriteLine(content);
File.WriteAllText(slnxFile, content);