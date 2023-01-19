#r "nuget: Newtonsoft.Json, 13.0.0"

using System.Xml;
using System.IO;
using Newtonsoft.Json.Linq;

var path = Directory.GetFiles(@".idea", "workspace.xml", SearchOption.AllDirectories).First();
XmlDocument xdoc = new XmlDocument();
using(var fs = File.Open(path, FileMode.Open)){
    xdoc.Load(fs);
    fs.Seek(0, SeekOrigin.Begin);
    var propertiesComponent = xdoc.SelectSingleNode("""//component[@name="PropertiesComponent"]""");
    var jsonStr = propertiesComponent.InnerText;
    var jsonObj = JObject.Parse(jsonStr);
    var keyToStringObj = jsonObj["keyToString"] as JObject;
    if (keyToStringObj.ContainsKey("rider.code.cleanup.on.save")) return;

    keyToStringObj.Add(new JProperty("rider.code.cleanup.on.save", "true"));
    var newNode = xdoc.CreateCDataSection(jsonObj.ToString());
    propertiesComponent.InnerText=string.Empty;
    propertiesComponent.AppendChild(newNode);
    var settings = new XmlWriterSettings { Indent = true };
    using(var writer = XmlWriter.Create(fs, settings)){
        xdoc.WriteTo(writer);
    }

}