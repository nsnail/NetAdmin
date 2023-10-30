FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
RUN apt update
RUN apt install -y redis
COPY ./dist/backend/NetAdmin.BizServer.Host/bin/Release/net8.0/publish .
COPY ./dist/frontend/admin ./admin
ENTRYPOINT redis-server --daemonize yes && dotnet NetAdmin.BizServer.Host.dll