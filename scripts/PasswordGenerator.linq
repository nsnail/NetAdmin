<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>NSExt</NuGetReference>
  <Namespace>NSExt.Extensions</Namespace>
</Query>

void Main()
{
    "admin".Pwd().Guid().Dump();
}