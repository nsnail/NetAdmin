FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
COPY ./dist/backend/NetAdmin.BizServer.Host/bin/Release/net8.0/publish .
COPY ./dist/frontend/admin /admin
ENTRYPOINT ["dotnet", "NetAdmin.BizServer.Host.dll"]
