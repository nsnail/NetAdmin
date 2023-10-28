FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
COPY ./dist/backend/NetAdmin.BizServer.Host/bin/Release/net8.0/publish .
ENTRYPOINT ["dotnet", "NetAdmin.BizServer.Host.dll"]
