<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
    foreach (var file in Directory.GetFiles(Path.Combine(Util.CurrentQueryPath,
    "../../assets/seed-data"), "*.json"))
    {

        try
        {
            var json = File.ReadAllText(file);
            var jo = JObject.Parse(json);
            var jarr = new JArray();
            foreach (JToken jt in jo["RECORDS"])
            {
                jarr.Add(Test(jt));
            }
            NormalizeToken(jarr);
            File.WriteAllText(file, jarr.ToString());
        }
        catch { }
    }

}

private static JToken NormalizeToken(JToken token)
{
    JObject o;
    JArray array;
    if ((o = token as JObject) != null)
    {
        List<JProperty> orderedProperties = new List<JProperty>(o.Properties());
        orderedProperties.Sort(delegate (JProperty x, JProperty y) { return x.Name.CompareTo(y.Name); });
        JObject normalized = new JObject();
        foreach (JProperty property in orderedProperties)
        {
            normalized.Add(property.Name, NormalizeToken(property.Value));
        }
        return normalized;
    }
    else if ((array = token as JArray) != null)
    {
        for (int i = 0; i < array.Count; i++)
        {
            array[i] = NormalizeToken(array[i]);
        }
        return array;
    }
    else
    {
        return token;
    }
}


JToken Test(JToken jt)
{
    foreach (JProperty jp in jt)
    {
        if (jp.Value.Value<string>() == null || jp.Value.Value<string>() == "0"
        || new[]{"CreatedTime",
"ModifiedUserId",
"ModifiedUserName",
"ModifiedTime","Version"
}.Contains(jp.Name))
        {
            jp.Remove();
            return Test(jt);
        }
    }
    return jt;
}