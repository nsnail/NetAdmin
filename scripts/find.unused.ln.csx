using System.Text.RegularExpressions;

Console.WriteLine(string.Join(Environment.NewLine
                                        , Regex
                                          .Matches(File.ReadAllText(@"../assets/res/Ln.resx")
                                                 , "data name=\"(.*?)\"")
                                          .Select(x => x.Groups[1].Value)
                                          .Where(x => !Directory
                                                       .GetFiles(@"../src/backend/", "*.cs"
                                                               , new EnumerationOptions {
                                                                     RecurseSubdirectories = true
                                                                 })
                                                       .Select(File.ReadAllText)
                                                       .Any(y => y.Contains(x)))));
Console.ReadKey();