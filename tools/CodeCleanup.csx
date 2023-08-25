using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Json;


{
    var files = string.Join(
        ';',
        Args[0]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Replace('\\', '/').Trim())
    );

    Console.WriteLine(files);

    using var p = Process.Start(
        new ProcessStartInfo
        {
            CreateNoWindow = true,
            FileName = "dotnet",
            Arguments = $"jb cleanupcode --include=\"{files}\" --no-build ../NetAdmin.sln",
            UseShellExecute = false,
            RedirectStandardOutput = true
        }
    );
    p.WaitForExit();
    Console.WriteLine(p.StandardOutput.ReadToEnd());

    using var p2 = Process.Start(
        new ProcessStartInfo
        {
            CreateNoWindow = true,
            FileName = "git",
            Arguments = $"status",
            UseShellExecute = false,
            RedirectStandardOutput = true
        }
    );
    p2.WaitForExit();
    var content = p2.StandardOutput.ReadToEnd();
    Console.WriteLine(content);

    return content.Contains("working tree clean") ? 0 : 1;
}