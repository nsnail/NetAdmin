using System.Text.RegularExpressions;

var csFiles = Directory.EnumerateFiles(@"../src/backend", $"*.cs", new EnumerationOptions { RecurseSubdirectories = true });
foreach (var lnFile in Directory.EnumerateFiles("../assets/res", "*.ln"))
{
    var newLines = new List<string>();
    foreach (var line in File.ReadAllLines(lnFile))
    {
        var found = false;
        foreach (var csFile in csFiles)
        {
            if (File.ReadAllText(csFile).Contains($"Ln.{(Regex.IsMatch(line, @"^\d") ? "_" : "") + line}"))
            {
                found = true;
                newLines.Add(line);
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine(line);
        }
    }
    File.WriteAllLines(lnFile, newLines);
}