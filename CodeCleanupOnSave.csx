var path = Directory.GetFiles(@".idea", "workspace.xml", SearchOption.AllDirectories).First();
        const string findStr = """
&quot;keyToString&quot;: {
""";
        const string replaceStr = """
&quot;keyToString&quot;: {
    &quot;rider.code.cleanup.on.save&quot;: &quot;true&quot;,
""";
        var content = File.ReadAllText(path);
        if(content.Contains("rider.code.cleanup.on.save")){
                Console.WriteLine("alreay added");
                return;
        }
        content = content.Replace(findStr, replaceStr);
        Console.WriteLine(content);
        File.WriteAllText(path, content);