<Query Kind="Program">
  <Connection>
    <ID>235d9a2f-8b68-43b8-99ff-f1b3108ebe69</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>47.99.59.246,6060</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sluser</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAASu05WBZY1UOTUQkpnu6GXAAAAAACAAAAAAAQZgAAAAEAACAAAAC3T6O6wQDKoA1AC+pNMlL/umPkrtfXi6CPREhBNIXJmgAAAAAOgAAAAAIAACAAAABy3KhCuMP70JwPq7nsaAbijtZckX+L9Ac6x0FEGLLPEyAAAACfnnMAcn9jNOdHITTFL9fZDUnd83v+Npllk46odqqOxUAAAABT/N/rj13TVNqNDSod6fxfC5YEyST3S5GtHpoZdxKd3We4cEB8NLxgjVsH+KCoMuesMP7CWtisruQOno4AOP6M</Password>
    <Database>NetAdmin_Res</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>NSExt</NuGetReference>
  <Namespace>NSExt.Extensions</Namespace>
</Query>

void Main()
{ 
    var text = File.ReadAllText(@"c:\users\nsnail\desktop\fuck.txt").Replace("\\", string.Empty);
    foreach (Match m in Regex.Matches(text, "\"success\": true,  \"message\": \"\",  \"data\": {    \"serial_no\": \"\\d+\",    \"phone\": \"(\\d+)\",.*?\"cache_info\": \"(.*?)\""))
    {

        var sdf = from a in Res_Robots
                  join b in Res_RobotCaches on a.Id equals b.Id
                  where a.Phone == m.Groups[1].Value
                  select b
        ;
        sdf.FirstOrDefault().CacheInfo = m.Groups[2].Value; 
        sdf.FirstOrDefault().ModifiedTime = DateTime.Now;

    }
    SubmitChanges();

}