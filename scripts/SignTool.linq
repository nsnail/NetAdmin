<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>NSExt</NuGetReference>
  <Namespace>NSExt.Extensions</Namespace>
</Query>

//Line2302071000 172021011012011 ios RobotHeart,RobotHeart 02424b91bdd33fdc44aafda59adeb098 "{\x0D\x0A  \x22serial_no\x22: \x222023020410001003\x22,\x0D\x0A  \x22success\x22: true,\x0D\x0A  \x22message\x22: \x22\x22,\x0D\x0A  \x22data\x22: {\x0D\x0A    \x22serial_no\x22: \x222023020410001003\x22,\x0D\x0A    \x22country_code\x22: \x2260\x22,\x0D\x0A    \x22phone\x22: \x22601169730387\x22,\x0D\x0A    \x22app_version\x22: \x22Line2302071000\x22,\x0D\x0A    \x22dc_version\x22: \x22130001.0.3\x22,\x0D\x0A    \x22line_version\x22: \x228.9.2.8685\x22\x0D\x0A  }\x0D\x0A}"

void Main()
{
    Console.WriteLine("nginx 原始日志："); 
    var str = @"{\x0D\x0A  \x22serial_no\x22: \x222023020410001003\x22,\x0D\x0A  \x22success\x22: true,\x0D\x0A  \x22message\x22: \x22\x22,\x0D\x0A  \x22data\x22: {\x0D\x0A    \x22serial_no\x22: \x222023020410001003\x22,\x0D\x0A    \x22country_code\x22: \x2260\x22,\x0D\x0A    \x22phone\x22: \x22601169730387\x22,\x0D\x0A    \x22app_version\x22: \x22Line2302071000\x22,\x0D\x0A    \x22dc_version\x22: \x22130001.0.3\x22,\x0D\x0A    \x22line_version\x22: \x228.9.2.8685\x22\x0D\x0A  }\x0D\x0A}";
    Console.WriteLine(str);
    Console.WriteLine("转义后：");
    str = str.Replace(@"\x0D","\r").Replace(@"\x0A","\n").Replace(@"\x22","\"");
    Console.WriteLine(str);
    var appVersion = "Line2302071000";
    var deviceNo = "172021011012011";
    var protocol = "ios";
    var method = "RobotHeart,RobotHeart";
    var apiKey = "58d4jkO00FKLGnn4i89356OD388jn302E1";
    str = $"{appVersion}{deviceNo}{method}{protocol}{str}{apiKey}";
    Console.WriteLine("拼接后：");
    Console.WriteLine(str);

        Console.WriteLine("计算签名：");
    Console.WriteLine(str.Md5(Encoding.UTF8));
    
 Console.WriteLine("你的签名: 02424b91bdd33fdc44aafda59adeb098" );   
}